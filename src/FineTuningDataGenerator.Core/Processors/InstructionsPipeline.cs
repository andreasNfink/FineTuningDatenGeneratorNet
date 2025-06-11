using System.Collections.Concurrent;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using FineTuningDataGenerator.Core.Models;
using FineTuningDataGenerator.Core.Services;

namespace FineTuningDataGenerator.Core.Processors;

/// <summary>
/// Enhanced pipeline für die Generierung von kontextuellen Finetuning-Daten aus Anleitungstexten mit Multithreading-Unterstützung
/// </summary>
public class InstructionsPipeline : IDisposable
{
    private readonly PipelineConfig _config;
    private readonly LLMService _llmService;
    private readonly DocumentChunker _chunker;
    private readonly SemaphoreSlim _llmSemaphore;
    private readonly object _fileLock = new object();
    private readonly object _consoleWriteLock = new object();

    public InstructionsPipeline(PipelineConfig config)
    {
        _config = config;
        _llmService = new LLMService(config.LLMConfig);
        _chunker = new DocumentChunker();
        
        // Initialize semaphore based on configuration
        var maxConcurrentRequests = config.TrainingConfig is EnhancedTrainingDataConfig enhanced 
            ? enhanced.MaxConcurrentLLMRequests 
            : 4; // Default fallback
        
        _llmSemaphore = new SemaphoreSlim(maxConcurrentRequests, maxConcurrentRequests);
    }/// <summary>
    /// Verarbeitet eine Markdown-Datei und generiert kontextuelle Finetuning-Daten
    /// </summary>
    public async Task<TrainingDataGenerationResult> ProcessDocumentAsync(string filePath, string? outputDirectory = null)
    {
        var result = new TrainingDataGenerationResult
        {
            SourceFiles = new List<string> { filePath }
        };

        try
        {
            var content = await File.ReadAllTextAsync(filePath);
            var fileName = Path.GetFileNameWithoutExtension(filePath);
            
            WriteToConsole($"📄 Verarbeite Dokument: {fileName}");
            
            // Setup output file for this document
            outputDirectory ??= Path.Combine(Directory.GetCurrentDirectory(), "TrainingData");
            Directory.CreateDirectory(outputDirectory);
            
            var documentOutputFile = Path.Combine(outputDirectory, $"{fileName}_{DateTime.Now:yyyyMMdd_HHmmss}.jsonl");
            
            // Step 1: Generate document context
            var documentContext = await GenerateDocumentContextAsync(content, fileName);
            WriteToConsole($"📋 Dokument-Kontext: {documentContext.MainTopic}");
            WriteToConsole($"📝 Zusammenfassung: {documentContext.Summary.Substring(0, Math.Min(200, documentContext.Summary.Length))}...");
            
            // Step 2: Create contextual chunks
            var chunks = CreateContextualChunks(content, documentContext, fileName);
            WriteToConsole($"📦 Erstellt {chunks.Count} kontextuelle Chunks");            // Step 3: Generate training samples with parallel processing
            var allSamples = new ConcurrentBag<ConversationSample>();
            var processedSamples = 0L; // Use long for Interlocked operations
            var maxConcurrentChunks = _config.TrainingConfig is EnhancedTrainingDataConfig enhanced 
                ? enhanced.MaxConcurrentChunks 
                : Environment.ProcessorCount;

            WriteToConsole($"🚀 Starte parallele Verarbeitung mit max. {maxConcurrentChunks} gleichzeitigen Chunks");
            
            var chunkTasks = chunks.Select(async (chunk, index) =>
            {
                if (Interlocked.Read(ref processedSamples) >= _config.TrainingConfig.MaxTotalSamples)
                    return new List<ConversationSample>();

                try
                {
                    var chunkSamples = await GenerateQuestionsForChunkAsync(chunk);
                    var samplesToAdd = chunkSamples.Take(_config.TrainingConfig.MaxSamplesPerChunk).ToList();
                    
                    // Thread-safe file writing
                    await WriteChunkSamplesToFileAsync(documentOutputFile, samplesToAdd);
                    
                    // Update counters thread-safely
                    Interlocked.Add(ref processedSamples, samplesToAdd.Count);
                    
                    WriteToConsole($"✅ Chunk {chunk.ChunkIndex}: {samplesToAdd.Count} Fragen generiert und gespeichert");
                    
                    return samplesToAdd;
                }
                catch (Exception ex)
                {
                    WriteToConsole($"❌ Fehler bei Chunk {chunk.ChunkIndex}: {ex.Message}");
                    return new List<ConversationSample>();
                }
            }).ToArray();

            // Wait for all chunk processing to complete
            var chunkResults = await Task.WhenAll(chunkTasks);
            var samples = chunkResults.SelectMany(s => s).ToList();
            
            WriteToConsole($"🎯 Gesamt: {samples.Count} Training-Samples für {fileName}");
            WriteToConsole($"📁 Dokument-Datei: {documentOutputFile}");
            
            // Convert to TrainingData objects
            result.TrainingData = samples.Select(s => new TrainingData
            {
                System = s.Messages.FirstOrDefault(m => m.Role.ToLower() == "system")?.Content ?? "",
                User = s.Messages.FirstOrDefault(m => m.Role.ToLower() == "user")?.Content ?? "",
                Assistant = s.Messages.FirstOrDefault(m => m.Role.ToLower() == "assistant")?.Content ?? ""
            }).ToList();
            
            result.Success = true;
        }
        catch (Exception ex)
        {
            result.ErrorMessage = ex.Message;
            WriteToConsole($"❌ Fehler bei der Verarbeitung: {ex.Message}");
        }

        return result;
    }

    /// <summary>
    /// Generiert Dokument-Kontext aus den ersten 5000 Zeichen
    /// </summary>
    private async Task<DocumentContext> GenerateDocumentContextAsync(string content, string fileName)
    {
        // Take first 5000 characters for context analysis
        var contextContent = content.Length > 5000 ? content.Substring(0, 5000) : content;
          var contextPrompt = $@"
            Analysiere dieses Dokument und erstelle eine Zusammenfassung:

            DOKUMENT: {fileName}
            INHALT: {contextContent}

            Erstelle eine strukturierte Analyse im JSON-Format:
            {{
                ""title"": ""Haupttitel des Dokuments"",
                ""mainTopic"": ""Hauptthema in 2-3 Worten (z.B. 'Paledo Aufträge', 'Strategiemanager', 'Instandhaltung')"",
                ""summary"": ""Zusammenfassung in 2-3 Sätzen was dieses Dokument beschreibt"",
                ""keyConcepts"": [""Konzept1"", ""Konzept2"", ""Konzept3""]
            }}

            Antwort nur mit dem JSON, keine Erklärungen.";

        var messages = new List<Message>
        {
            new() { Role = "system", Content = "Du bist ein Experte für technische Dokumentationsanalyse. Antworte nur mit validem JSON." },
            new() { Role = "user", Content = contextPrompt }
        };

        try
        {
            var response = await _llmService.ChatCompletionAsync(messages);
            
            // Clean response (remove potential markdown formatting)
            var cleanResponse = response.Trim().TrimStart('`').TrimEnd('`');
            if (cleanResponse.StartsWith("json")) cleanResponse = cleanResponse.Substring(4);
            
            var contextData = JsonSerializer.Deserialize<DocumentContext>(cleanResponse);
            if (contextData != null)
            {
                contextData.SourceFile = fileName;
                return contextData;
            }
        }
        catch (Exception ex)
        {
            WriteToConsole($"⚠️ Kontext-Generierung fehlgeschlagen: {ex.Message}");
        }

        // Fallback: Create basic context from filename and content
        return new DocumentContext
        {
            Title = fileName,
            MainTopic = ExtractTopicFromFilename(fileName),
            Summary = $"Technische Dokumentation über {fileName}",
            KeyConcepts = ExtractKeyConceptsFromContent(contextContent),
            SourceFile = fileName
        };
    }

    /// <summary>
    /// Erstellt kontextuelle Chunks mit Dokumentinformationen
    /// </summary>
    private List<ContextualChunk> CreateContextualChunks(string content, DocumentContext docContext, string fileName)
    {
        var chunks = new List<ContextualChunk>();
        var lines = content.Split('\n', StringSplitOptions.RemoveEmptyEntries);
        
        var currentChunk = new StringBuilder();
        var currentSection = "";
        int chunkIndex = 0;
        
        foreach (var line in lines)
        {
            // Detect section headers
            if (line.StartsWith("#"))
            {
                // If we have accumulated content, create a chunk
                if (currentChunk.Length >= _config.TrainingConfig.MinChunkSize)
                {
                    chunks.Add(CreateContextualChunk(currentChunk.ToString(), docContext, fileName, chunkIndex++, currentSection));
                    currentChunk.Clear();
                }
                currentSection = line.Trim('#', ' ');
            }
            
            currentChunk.AppendLine(line);
            
            // Create chunk if it gets too large
            if (currentChunk.Length >= _config.TrainingConfig.MaxChunkSize)
            {
                chunks.Add(CreateContextualChunk(currentChunk.ToString(), docContext, fileName, chunkIndex++, currentSection));
                currentChunk.Clear();
            }
        }
        
        // Add final chunk
        if (currentChunk.Length >= _config.TrainingConfig.MinChunkSize)
        {
            chunks.Add(CreateContextualChunk(currentChunk.ToString(), docContext, fileName, chunkIndex, currentSection));
        }
        
        return chunks;
    }

    /// <summary>
    /// Erstellt einen kontextuellen Chunk
    /// </summary>
    private ContextualChunk CreateContextualChunk(string content, DocumentContext docContext, string fileName, int index, string section)
    {
        return new ContextualChunk
        {
            Content = content.Trim(),
            Source = fileName,
            ChunkIndex = index,
            Title = docContext.Title,
            Section = section,
            DocumentContext = docContext,
            ChunkSummary = GenerateChunkSummary(content, section)
        };
    }

    /// <summary>
    /// Generiert eine einfache Chunk-Zusammenfassung
    /// </summary>
    private string GenerateChunkSummary(string content, string section)
    {
        // Simple summary: first meaningful sentence + section info
        var sentences = content.Split('.', StringSplitOptions.RemoveEmptyEntries);
        var firstSentence = sentences.FirstOrDefault(s => s.Trim().Length > 20)?.Trim() ?? "";
        
        return string.IsNullOrEmpty(section) 
            ? firstSentence 
            : $"{section}: {firstSentence}";
    }    /// <summary>
    /// Generiert kontextuelle Fragen für einen Chunk mit LLM-Ratenbegrenzung und Qualitätsfilterung
    /// </summary>
    private async Task<List<ConversationSample>> GenerateQuestionsForChunkAsync(ContextualChunk chunk)
    {
        var samples = new List<ConversationSample>();

        // First, evaluate chunk suitability
        var suitabilityPrompt = $@"
            AUFGABE: Bewerte ob dieser Textabschnitt für die Generierung von Finetuning-Daten geeignet ist.

            DOKUMENT-KONTEXT:
            - Thema: {chunk.DocumentContext.MainTopic}
            - Abschnitt: {chunk.Section}

            CHUNK-INHALT:
            {chunk.Content}

            BEWERTUNGSKRITERIEN:
            Ein Chunk ist NICHT geeignet wenn er:
            - Nur ein Inhaltsverzeichnis oder Navigation enthält
            - Unvollständigen Code oder Code-Fragmente ohne Erklärung enthält
            - Hauptsächlich aus Listen ohne Kontext besteht
            - Zu fragmentiert ist um sinnvolle Fragen zu generieren
            - Keine praktischen Informationen für Anwender enthält
            - Nur Metadaten, Formatierung oder strukturelle Elemente enthält

            Ein Chunk ist geeignet wenn er:
            - Konkrete Anweisungen oder Prozesse beschreibt
            - Erklärungen von Funktionen oder Features enthält
            - Praktische Beispiele oder Anwendungsfälle zeigt
            - Zusammenhängende, verständliche Informationen bietet

            Antwort nur mit einem JSON-Objekt:
            {{
                ""suitable"": true/false,
                ""reason"": ""Kurze Begründung warum geeignet oder nicht geeignet""
            }}";

        var suitabilityMessages = new List<Message>
        {
            new() { Role = "system", Content = "Du bist ein Experte für die Bewertung von Textqualität für Finetuning-Daten. Antworte nur mit validem JSON." },
            new() { Role = "user", Content = suitabilityPrompt }
        };

        // Use semaphore to control concurrent LLM requests
        await _llmSemaphore.WaitAsync();
        try
        {
            var suitabilityResponse = await _llmService.ChatCompletionAsync(suitabilityMessages);
            
            // Clean and parse suitability response
            var cleanSuitabilityResponse = suitabilityResponse.Trim().TrimStart('`').TrimEnd('`');
            if (cleanSuitabilityResponse.StartsWith("json")) cleanSuitabilityResponse = cleanSuitabilityResponse.Substring(4);
            
            var suitabilityResult = JsonSerializer.Deserialize<JsonElement>(cleanSuitabilityResponse);
            
            bool isSuitable = true;
            string reason = "Keine Bewertung erhalten";
            
            if (suitabilityResult.TryGetProperty("suitable", out var suitableProperty))
            {
                isSuitable = suitableProperty.GetBoolean();
            }
            
            if (suitabilityResult.TryGetProperty("reason", out var reasonProperty))
            {
                reason = reasonProperty.GetString() ?? reason;
            }

            // Log the suitability assessment
            if (!isSuitable)
            {
                WriteToConsole($"⚠️ Chunk {chunk.ChunkIndex} übersprungen: {reason}");
                
                // Save unsuitable chunk for review
                await SaveUnsuitableChunkAsync(chunk, reason);
                return samples; // Return empty list
            }
            else
            {
                WriteToConsole($"✅ Chunk {chunk.ChunkIndex} als geeignet bewertet: {reason}");
            }
        }
        catch (Exception ex)
        {
            WriteToConsole($"⚠️ Chunk-Bewertung für Chunk {chunk.ChunkIndex} fehlgeschlagen: {ex.Message}. Verarbeitung wird fortgesetzt.");
            // Continue with processing if evaluation fails
        }
        finally
        {
            _llmSemaphore.Release();
        }

        // If chunk is suitable, generate questions
        var questionPrompt = $@"
            DOKUMENT-KONTEXT:
            - Thema: {chunk.DocumentContext.MainTopic}
            - Zusammenfassung: {chunk.DocumentContext.Summary}
            - Schlüsselkonzepte: {string.Join(", ", chunk.DocumentContext.KeyConcepts)}
            - Aktueller Abschnitt: {chunk.Section}

            CHUNK-INHALT:
            {chunk.Content}

            Generiere {_config.TrainingConfig.MaxSamplesPerChunk} verschiedene Frage-Antwort-Paare für diesen Textabschnitt.
            
            WICHTIG:
            - Jede Frage muss den Dokumentkontext berücksichtigen (z.B. ""Im Paledo-System..."" oder ""Bei der Auftragserstellung..."")
            - Antworten sollen präzise und basierend auf dem gegebenen Text sein
            - Verwende verschiedene Fragetypen: Was ist, Wie funktioniert, Welche Schritte, etc.
            - Fragen sollen praktisch und für Anwender relevant sein

            Format als JSON-Array:
            [
              {{
                ""question"": ""Frage die den Kontext {chunk.DocumentContext.MainTopic} verrknüpft mit der spezifischen Frage basierend auf dem Chunk enthält"",
                ""answer"": ""Detaillierte Antwort basierend auf dem Textinhalt""
              }}
            ]

            Antwort nur mit dem JSON-Array, keine Erklärungen.";

        var messages = new List<Message>
        {
            new() { Role = "system", Content = _config.TrainingConfig.SystemMessage },
            new() { Role = "user", Content = questionPrompt }
        };

        // Use semaphore to control concurrent LLM requests
        await _llmSemaphore.WaitAsync();
        try
        {
            var response = await _llmService.ChatCompletionAsync(messages);
            
            // Clean and parse response
            var cleanResponse = response.Trim().TrimStart('`').TrimEnd('`');
            if (cleanResponse.StartsWith("json")) cleanResponse = cleanResponse.Substring(4);
              var questionAnswerPairs = JsonSerializer.Deserialize<JsonElement[]>(cleanResponse);
            
            if (questionAnswerPairs != null)
            {
                foreach (var pair in questionAnswerPairs)
                {
                    if (pair.TryGetProperty("question", out var q) && 
                        pair.TryGetProperty("answer", out var a))
                    {
                        var sample = new ConversationSample();
                        sample.Messages.Add(new Message { Role = "system", Content = _config.TrainingConfig.SystemMessage });
                        sample.Messages.Add(new Message { Role = "user", Content = q.GetString() ?? "" });
                        sample.Messages.Add(new Message { Role = "assistant", Content = a.GetString() ?? "" });
                        samples.Add(sample);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            WriteToConsole($"⚠️ Frage-Generierung für Chunk {chunk.ChunkIndex} fehlgeschlagen: {ex.Message}");
        }
        finally
        {
            _llmSemaphore.Release();
        }

        return samples;
    }

    /// <summary>
    /// Speichert ungeeignete Chunks in einer separaten Datei zur Überprüfung
    /// </summary>
    private async Task SaveUnsuitableChunkAsync(ContextualChunk chunk, string reason)
    {
        try
        {
            var outputDirectory = Path.Combine(Directory.GetCurrentDirectory(), "TrainingData");
            Directory.CreateDirectory(outputDirectory);
            
            var unsuitableFile = Path.Combine(outputDirectory, $"unsuitable_chunks_{DateTime.Now:yyyyMMdd}.jsonl");
            
            var unsuitableChunkInfo = new
            {
                timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                sourceFile = chunk.Source,
                chunkIndex = chunk.ChunkIndex,
                section = chunk.Section,
                reason = reason,
                content = chunk.Content,
                documentContext = new
                {
                    mainTopic = chunk.DocumentContext.MainTopic,
                    summary = chunk.DocumentContext.Summary,
                    keyConcepts = chunk.DocumentContext.KeyConcepts
                }
            };
            
            var jsonLine = JsonSerializer.Serialize(unsuitableChunkInfo, new JsonSerializerOptions 
            { 
                WriteIndented = false,
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            });
            
            // Thread-safe file writing
            await Task.Run(() =>
            {
                lock (_fileLock)
                {
                    File.AppendAllText(unsuitableFile, jsonLine + Environment.NewLine, System.Text.Encoding.UTF8);
                }
            });
        }
        catch (Exception ex)
        {
            WriteToConsole($"⚠️ Fehler beim Speichern des ungeeigneten Chunks: {ex.Message}");
        }
    }

    /// <summary>
    /// Extrahiert das Hauptthema aus dem Dateinamen
    /// </summary>
    private string ExtractTopicFromFilename(string fileName)
    {
        return fileName switch
        {
            "auftrage" => "Paledo Aufträge",
            "strategiemanager" => "Strategiemanager",
            "anlagenstruktur" => "Anlagenstruktur", 
            "instandhaltung" => "Instandhaltung",
            _ => fileName.Replace("-", " ").Replace("_", " ")
        };
    }

    /// <summary>
    /// Extrahiert Schlüsselkonzepte aus dem Inhalt (Fallback)
    /// </summary>
    private List<string> ExtractKeyConceptsFromContent(string content)
    {
        var keywords = new List<string>();
        var words = content.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        
        // Look for capitalized words that appear multiple times
        var wordCounts = words
            .Where(w => w.Length > 4 && char.IsUpper(w[0]))
            .GroupBy(w => w.ToLower())
            .Where(g => g.Count() > 1)
            .OrderByDescending(g => g.Count())
            .Take(5)
            .Select(g => g.Key)
            .ToList();
                
        return wordCounts;
    }

    /// <summary>
    /// Exportiert die generierten Daten als JSONL-Datei
    /// </summary>
    public async Task<string> ExportToJsonLAsync(List<ConversationSample> samples, string outputPath)
    {
        var lines = samples.Select(s => s.ToJsonL());
        await File.WriteAllLinesAsync(outputPath, lines, System.Text.Encoding.UTF8);
        return outputPath;
    }

    /// <summary>
    /// Verarbeitet mehrere Dokumente und erstellt sowohl Einzeldateien als auch eine kombinierte Ausgabedatei
    /// </summary>
    public async Task<BatchTrainingDataResult> ProcessMultipleDocumentsAsync(string[] filePaths, string? outputDirectory = null)
    {
        var result = new BatchTrainingDataResult
        {
            SourceFiles = filePaths.ToList()
        };

        try
        {
            outputDirectory ??= Path.Combine(Directory.GetCurrentDirectory(), "TrainingData");
            Directory.CreateDirectory(outputDirectory);            var allSamples = new ConcurrentBag<ConversationSample>();
            var combinedOutputFile = Path.Combine(outputDirectory, $"combined_training_data_{DateTime.Now:yyyyMMdd_HHmmss}.jsonl");

            var maxConcurrentDocs = _config.TrainingConfig is EnhancedTrainingDataConfig enhanced 
                ? enhanced.MaxConcurrentDocuments 
                : Environment.ProcessorCount;

            WriteToConsole($"🚀 Starte parallele Verarbeitung von {filePaths.Length} Dokumenten");
            WriteToConsole($"📁 Ausgabeverzeichnis: {outputDirectory}");
            WriteToConsole($"📄 Kombinierte Datei: {Path.GetFileName(combinedOutputFile)}");
            WriteToConsole($"🔧 Max. parallele Dokumente: {maxConcurrentDocs}");

            // Process documents in parallel with controlled concurrency
            var semaphore = new SemaphoreSlim(maxConcurrentDocs, maxConcurrentDocs);
            var documentTasks = filePaths.Select(async filePath =>
            {
                await semaphore.WaitAsync();
                try
                {
                    var fileName = Path.GetFileNameWithoutExtension(filePath);
                    WriteToConsole($"\n📄 Starte Verarbeitung: {fileName}");

                    var documentResult = await ProcessDocumentAsync(filePath, outputDirectory);
                    
                    if (documentResult.Success)
                    {
                        // Convert TrainingData back to ConversationSample for consistency
                        var documentSamples = documentResult.TrainingData.Select(td => new ConversationSample
                        {
                            Messages = new List<Message>
                            {
                                new() { Role = "user", Content = td.User },
                                new() { Role = "assistant", Content = td.Assistant }
                            }
                        });

                        // Add to concurrent collection
                        foreach (var sample in documentSamples)
                        {
                            allSamples.Add(sample);
                        }

                        WriteToConsole($"✅ {documentResult.TotalSamples} Samples generiert für {fileName}");
                    }
                    else
                    {
                        WriteToConsole($"❌ Fehler bei {fileName}: {documentResult.ErrorMessage}");
                    }

                    return documentResult;
                }
                finally
                {
                    semaphore.Release();
                }
            }).ToArray();

            // Wait for all document processing to complete
            var documentResults = await Task.WhenAll(documentTasks);
            
            // Update result
            result.DocumentResults.AddRange(documentResults);
            result.TotalSamples = documentResults.Where(r => r.Success).Sum(r => r.TotalSamples);

            // Create combined output file
            if (allSamples.Count > 0)
            {
                var combinedSamples = allSamples.ToList();
                await ExportToJsonLAsync(combinedSamples, combinedOutputFile);
                result.CombinedOutputFile = combinedOutputFile;
                result.Success = true;

                WriteToConsole($"\n🎯 Parallele Verarbeitung abgeschlossen:");
                WriteToConsole($"📊 Gesamt: {allSamples.Count} Training-Samples aus {filePaths.Length} Dokumenten");
                WriteToConsole($"📁 Einzeldateien: {result.SuccessfulDocuments} erfolgreich");
                WriteToConsole($"📄 Kombinierte Datei: {combinedOutputFile}");
            }
            else
            {
                result.ErrorMessage = "Keine Training-Samples generiert";
                WriteToConsole("⚠️ Keine Training-Samples generiert");
            }
        }
        catch (Exception ex)
        {
            result.ErrorMessage = ex.Message;
            result.Success = false;
            WriteToConsole($"❌ Fehler bei der Batch-Verarbeitung: {ex.Message}");
        }

        return result;
    }

    /// <summary>
    /// Thread-safe method to write chunk samples to file
    /// </summary>
    private async Task WriteChunkSamplesToFileAsync(string filePath, List<ConversationSample> samples)
    {
        if (samples.Count == 0) return;

        var jsonLines = samples.Select(s => s.ToJsonL() + Environment.NewLine);
        var content = string.Join("", jsonLines);

        // Use lock to ensure thread-safe file writing
        await Task.Run(() =>
        {
            lock (_fileLock)
            {
                File.AppendAllText(filePath, content, System.Text.Encoding.UTF8);
            }
        });
    }

    /// <summary>
    /// Thread-safe console writing method
    /// </summary>
    private void WriteToConsole(string message)
    {
        lock (_consoleWriteLock)
        {
            Console.WriteLine(message);
        }
    }    public void Dispose()
    {
        _llmService?.Dispose();
        _llmSemaphore?.Dispose();
    }
}
