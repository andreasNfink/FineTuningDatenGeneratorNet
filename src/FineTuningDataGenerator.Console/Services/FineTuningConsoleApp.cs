using FineTuningDataGenerator.Core.Models;
using FineTuningDataGenerator.Core.Processors;
using FineTuningDataGenerator.Core.Services;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace FineTuningDataGenerator.Console.Services;

/// <summary>
/// Hauptanwendung für die FineTuning Data Generator Konsole
/// </summary>
public class FineTuningConsoleApp
{
    private readonly ILogger<FineTuningConsoleApp> _logger;
    private readonly GitBookFileProcessor _processor;

    public FineTuningConsoleApp(ILogger<FineTuningConsoleApp> logger)
    {
        _logger = logger;
        _processor = new GitBookFileProcessor();
    }

    /// <summary>
    /// Haupteinstiegspunkt der Anwendung
    /// </summary>
    public async Task RunAsync(string[] args)
    {
        try
        {
            _logger.LogInformation("=== FineTuning Data Generator ===");
            _logger.LogInformation("GitBook Markdown Bereinigungstool & Finetuning Pipeline");
            _logger.LogInformation("");

            if (args.Length == 0)
            {
                await RunInteractiveMode();
            }
            else
            {
                await RunCommandLineMode(args);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ein unerwarteter Fehler ist aufgetreten");
            Environment.Exit(1);
        }
    }

    /// <summary>
    /// Interaktiver Modus für Benutzerinteraktion
    /// </summary>
    private async Task RunInteractiveMode()
    {
        _logger.LogInformation("Interaktiver Modus gestartet");
        _logger.LogInformation("");        System.Console.WriteLine("Optionen:");
        System.Console.WriteLine("1. Einzelne Datei bereinigen");
        System.Console.WriteLine("2. Verzeichnis bereinigen");
        System.Console.WriteLine("3. Beispieldateien aus InputFiles bereinigen");
        System.Console.WriteLine("4. Kontextbewusste Finetuning-Daten generieren (Enhanced)");
        System.Console.WriteLine("5. LLM-Verbindung testen");
        System.Console.WriteLine("");
        System.Console.Write("Wählen Sie eine Option (1-5): ");

        var choice = System.Console.ReadLine();

        switch (choice)
        {
            case "1":
                await ProcessSingleFileInteractive();
                break;
            case "2":
                await ProcessDirectoryInteractive();
                break;
            case "3":
                await ProcessInputFilesExample();
                break;
            case "4":
                await GenerateFineTuningDataInteractive();
                break;
            case "5":
                await TestLLMConnectionInteractive();
                break;
            default:
                _logger.LogWarning("Ungültige Auswahl. Anwendung wird beendet.");
                break;
        }
    }

    /// <summary>
    /// Command-Line Modus
    /// </summary>
    private async Task RunCommandLineMode(string[] args)
    {
        var inputPath = args[0];
        var outputPath = args.Length > 1 ? args[1] : null;

        if (File.Exists(inputPath))
        {
            await ProcessSingleFile(inputPath, outputPath);
        }
        else if (Directory.Exists(inputPath))
        {
            await ProcessDirectory(inputPath, outputPath);
        }
        else
        {
            _logger.LogError("Eingabepfad nicht gefunden: {InputPath}", inputPath);
            Environment.Exit(1);
        }
    }

    /// <summary>
    /// Interaktive Verarbeitung einer einzelnen Datei
    /// </summary>
    private async Task ProcessSingleFileInteractive()
    {
        System.Console.Write("Pfad zur Markdown-Datei: ");
        var inputPath = System.Console.ReadLine();

        if (string.IsNullOrWhiteSpace(inputPath))
        {
            _logger.LogWarning("Kein Pfad angegeben.");
            return;
        }

        System.Console.Write("Ausgabepfad (Enter für automatisch): ");
        var outputPath = System.Console.ReadLine();

        if (string.IsNullOrWhiteSpace(outputPath))
            outputPath = null;

        await ProcessSingleFile(inputPath, outputPath);
    }

    /// <summary>
    /// Interaktive Verarbeitung eines Verzeichnisses
    /// </summary>
    private async Task ProcessDirectoryInteractive()
    {
        System.Console.Write("Pfad zum Verzeichnis: ");
        var inputPath = System.Console.ReadLine();

        if (string.IsNullOrWhiteSpace(inputPath))
        {
            _logger.LogWarning("Kein Pfad angegeben.");
            return;
        }

        System.Console.Write("Ausgabeverzeichnis (Enter für 'cleaned' Unterordner): ");
        var outputPath = System.Console.ReadLine();

        if (string.IsNullOrWhiteSpace(outputPath))
            outputPath = null;

        await ProcessDirectory(inputPath, outputPath);
    }

    /// <summary>
    /// Verarbeitung der Beispieldateien aus dem InputFiles Verzeichnis
    /// </summary>
    private async Task ProcessInputFilesExample()
    {
        var currentDir = Directory.GetCurrentDirectory();
        var inputFilesPath = Path.Combine(currentDir, "InputFiles");

        if (!Directory.Exists(inputFilesPath))
        {
            _logger.LogWarning("InputFiles Verzeichnis nicht gefunden: {Path}", inputFilesPath);
            _logger.LogInformation("Versuche alternatives Verzeichnis...");
            
            // Versuche das Verzeichnis im Parent-Ordner
            var parentInputFiles = Path.Combine(Directory.GetParent(currentDir)?.FullName ?? currentDir, "InputFiles");
            if (Directory.Exists(parentInputFiles))
            {
                inputFilesPath = parentInputFiles;
            }
            else
            {
                _logger.LogError("InputFiles Verzeichnis konnte nicht gefunden werden.");
                return;
            }
        }

        var outputPath = Path.Combine(currentDir, "OutputFiles");
        _logger.LogInformation("Verarbeite Beispieldateien...");
        _logger.LogInformation("Eingabe: {InputPath}", inputFilesPath);
        _logger.LogInformation("Ausgabe: {OutputPath}", outputPath);

        await ProcessDirectory(inputFilesPath, outputPath);
    }

    /// <summary>
    /// Verarbeitung einer einzelnen Datei
    /// </summary>
    private async Task ProcessSingleFile(string inputPath, string? outputPath)
    {
        _logger.LogInformation("Verarbeite Datei: {InputPath}", inputPath);

        var result = await _processor.ProcessFileAsync(inputPath, outputPath);

        if (result.Success)
        {
            _logger.LogInformation("✅ Datei erfolgreich bereinigt:");
            _logger.LogInformation("   Ausgabe: {OutputPath}", result.OutputFilePath);
            _logger.LogInformation("   Größe: {OriginalSize} -> {CleanedSize} Zeichen ({Ratio:P1})", 
                result.OriginalSize, result.CleanedSize, result.CompressionRatio);
            _logger.LogInformation("   Entfernte Elemente: {ElementsRemoved}", result.ElementsRemoved);
            _logger.LogInformation("   Zeit: {ProcessingTime:F2}s", result.ProcessingTime.TotalSeconds);
        }
        else
        {
            _logger.LogError("❌ Fehler beim Verarbeiten der Datei: {ErrorMessage}", result.ErrorMessage);
        }
    }

    /// <summary>
    /// Verarbeitung eines Verzeichnisses
    /// </summary>
    private async Task ProcessDirectory(string inputPath, string? outputPath)
    {
        _logger.LogInformation("Verarbeite Verzeichnis: {InputPath}", inputPath);
        
        if (!string.IsNullOrEmpty(outputPath))
        {
            _logger.LogInformation("Ausgabe: {OutputPath}", outputPath);
        }

        var result = await _processor.ProcessDirectoryAsync(inputPath, outputPath);

        _logger.LogInformation("");
        _logger.LogInformation("=== Verarbeitungsergebnis ===");
        _logger.LogInformation("📊 {Result}", result.ToString());
        _logger.LogInformation("");

        if (result.FailedFiles > 0)
        {
            _logger.LogWarning("❌ Fehlgeschlagene Dateien:");
            foreach (var failedResult in result.Results.Where(r => !r.Success))
            {
                _logger.LogWarning("   {File}: {Error}", failedResult.OriginalFilePath, failedResult.ErrorMessage);
            }
            _logger.LogInformation("");
        }

        if (result.SuccessfulFiles > 0)
        {
            _logger.LogInformation("✅ Erfolgreich verarbeitete Dateien:");
            foreach (var successResult in result.Results.Where(r => r.Success))
            {
                _logger.LogInformation("   {File} -> {Output} ({ElementsRemoved} Elemente entfernt)", 
                    Path.GetFileName(successResult.OriginalFilePath),
                    Path.GetFileName(successResult.OutputFilePath),
                    successResult.ElementsRemoved);
            }
        }

        _logger.LogInformation("");
        _logger.LogInformation("🎉 Verarbeitung abgeschlossen!");
    }    /// <summary>
    /// Interaktive Generierung von Finetuning-Daten mit verbesserter kontextbewusster Pipeline
    /// </summary>
    private async Task GenerateFineTuningDataInteractive()
    {
        _logger.LogInformation("=== Kontextbewusste Finetuning-Daten Generierung ===");
        
        // LLM-Konfiguration abfragen
        var llmConfig = GetLLMConfigInteractive();
        if (llmConfig == null) return;

        // Eingabeverzeichnis abfragen
        System.Console.Write("Pfad zu den bereinigten Markdown-Dateien (Enter für OutputFiles): ");
        var inputPath = System.Console.ReadLine();
        if (string.IsNullOrWhiteSpace(inputPath))
            inputPath = Path.Combine(Directory.GetCurrentDirectory(), "OutputFiles");

        // Erweiterte Konfiguration abfragen
        System.Console.Write("Maximale Samples pro Chunk (Enter für 3): ");
        var maxSamplesInput = System.Console.ReadLine();
        var maxSamplesPerChunk = int.TryParse(maxSamplesInput, out var samples) ? samples : 3;

        System.Console.Write("Maximale Gesamtsamples (Enter für 1000): ");
        var maxTotalInput = System.Console.ReadLine();
        var maxTotalSamples = int.TryParse(maxTotalInput, out var total) ? total : 1000;

        // Ausgabeverzeichnis
        var outputDir = Path.Combine(Directory.GetCurrentDirectory(), "TrainingData");
        Directory.CreateDirectory(outputDir);

        _logger.LogInformation("Starte erweiterte Finetuning-Daten Generierung...");
        _logger.LogInformation("Eingabe: {InputPath}", inputPath);
        _logger.LogInformation("Ausgabe: {OutputPath}", outputDir);
        _logger.LogInformation("Konfiguration: {MaxSamples} max. Samples pro Chunk, {MaxTotal} max. Gesamtsamples", 
            maxSamplesPerChunk, maxTotalSamples);

        // Enhanced Pipeline konfigurieren mit kontextbewussten Features
        var pipelineConfig = new PipelineConfig
        {
            Type = PipelineType.Instructions,
            Name = "Enhanced Context-Aware Instructions Pipeline",
            Description = "Generiert kontextuelle Frage-Antwort-Paare aus technischen Anleitungen mit Dokumentkontext",
            Language = "de",
            LLMConfig = llmConfig,
            TrainingConfig = new EnhancedTrainingDataConfig
            {
                SystemMessage = "Sie sind ein hilfreicher Assistent für Instandhaltungsmanagement und technische Anleitungen in Paledo.",
                MinChunkSize = 300,
                MaxChunkSize = 1500,
                MaxSamplesPerChunk = maxSamplesPerChunk,
                MaxTotalSamples = maxTotalSamples,
                QuestionTypes = new List<string> 
                { 
                    "was_ist", "wie_funktioniert", "welche_schritte", "warum", 
                    "wann_verwendet", "welche_vorteile", "wie_konfiguriert", 
                    "was_beachten", "welche_optionen", "wie_unterscheidet" 
                },
                UseDocumentContext = true,
                ContextAnalysisLength = 5000,
                ContextTemperature = 0.3
            }
        };

        var pipeline = new InstructionsPipeline(pipelineConfig);

        try
        {
            // Alle Markdown-Dateien finden
            var markdownFiles = Directory.GetFiles(inputPath, "*.md", SearchOption.AllDirectories);
            
            if (markdownFiles.Length == 0)
            {
                _logger.LogWarning("Keine Markdown-Dateien in {InputPath} gefunden.", inputPath);
                return;
            }

            _logger.LogInformation("Gefunden: {Count} Markdown-Dateien", markdownFiles.Length);

            // Verwende die neue Batch-Verarbeitung für per-Document-Files + kombinierte Ausgabe
            var batchResult = await pipeline.ProcessMultipleDocumentsAsync(markdownFiles, outputDir);
            
            if (batchResult.Success)
            {
                _logger.LogInformation("");
                _logger.LogInformation("🎉 Kontextbewusste Finetuning-Daten erfolgreich generiert!");
                _logger.LogInformation("📊 Gesamt: {TotalSamples} Training-Samples aus {TotalFiles} Dateien", 
                    batchResult.TotalSamples, markdownFiles.Length);
                _logger.LogInformation("✅ Erfolgreich: {Success} Dokumente", batchResult.SuccessfulDocuments);
                _logger.LogInformation("❌ Fehlgeschlagen: {Failed} Dokumente", batchResult.FailedDocuments);
                _logger.LogInformation("⏱️ Verarbeitungszeit: {Time:F2} Sekunden", batchResult.ProcessingTime.TotalSeconds);
                
                if (!string.IsNullOrEmpty(batchResult.CombinedOutputFile))
                {
                    _logger.LogInformation("📄 Kombinierte Datei: {CombinedFile}", batchResult.CombinedOutputFile);
                }

                // Zeige Details der einzelnen Dokumente
                _logger.LogInformation("");
                _logger.LogInformation("=== Dokumentdetails ===");
                foreach (var docResult in batchResult.DocumentResults.Where(r => r.Success))
                {
                    var fileName = Path.GetFileNameWithoutExtension(docResult.SourceFiles.FirstOrDefault() ?? "");
                    _logger.LogInformation("📄 {FileName}: {Count} Samples", fileName, docResult.TotalSamples);
                }

                // Zeige Beispiel aus der kombinierten Datei
                if (batchResult.TotalSamples > 0 && !string.IsNullOrEmpty(batchResult.CombinedOutputFile) && File.Exists(batchResult.CombinedOutputFile))
                {
                    _logger.LogInformation("");
                    _logger.LogInformation("=== Beispiel Training-Sample (kontextbewusst) ===");
                    
                    var firstLine = (await File.ReadAllLinesAsync(batchResult.CombinedOutputFile)).FirstOrDefault();
                    if (!string.IsNullOrEmpty(firstLine))
                    {
                        try
                        {
                            var sample = JsonSerializer.Deserialize<ConversationSample>(firstLine);
                            if (sample?.Messages != null)
                            {
                                foreach (var message in sample.Messages)
                                {
                                    var content = message.Content.Length > 300 ? message.Content.Substring(0, 300) + "..." : message.Content;
                                    _logger.LogInformation("{Role}: {Content}", message.Role.ToUpper(), content);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            _logger.LogWarning("Konnte Beispiel nicht anzeigen: {Error}", ex.Message);
                        }
                    }
                }
            }
            else
            {
                _logger.LogError("❌ Fehler bei der Batch-Verarbeitung: {Error}", batchResult.ErrorMessage);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Fehler bei der erweiterten Finetuning-Daten Generierung");
        }
        finally
        {
            pipeline.Dispose();
        }
    }/// <summary>
    /// Interaktive LLM-Konfiguration
    /// </summary>
    private LLMConfig? GetLLMConfigInteractive()
    {
        System.Console.WriteLine("=== LLM-Konfiguration ===");
        
        // Standard-Werte basierend auf dem Screenshot
        System.Console.Write("API-Host (Enter für http://svki01:9005/v1/): ");
        var apiHost = System.Console.ReadLine();
        if (string.IsNullOrWhiteSpace(apiHost))
            apiHost = "http://svki01:9005/v1";

        System.Console.Write("Model Name (Enter für vllm9005): ");
        var modelName = System.Console.ReadLine();
        if (string.IsNullOrWhiteSpace(modelName))
            modelName = "vllm9005";

        System.Console.Write("API-Key (Enter für leer): ");
        var apiKey = System.Console.ReadLine();

        var config = new LLMConfig
        {
            Name = modelName,
            ApiHost = apiHost,
            ApiKey = apiKey ?? string.Empty,
            Model = "mydata", // Wie im Screenshot
            IsOpenAICompatible = true,
            MaxTokens = 1000,
            Temperature = 0.7
        };

        return config;
    }

    /// <summary>    /// Testet die LLM-Verbindung
    /// </summary>
    private async Task TestLLMConnectionInteractive()
    {
        _logger.LogInformation("=== LLM-Verbindungstest ===");
        
        var config = GetLLMConfigInteractive();
        if (config == null) return;

        using var llmService = new LLMService(config);
        
        _logger.LogInformation("Teste Verbindung zu {Host}...", config.ApiHost);
        
        try
        {
            var success = await llmService.TestConnectionAsync();
            
            if (success)
            {
                _logger.LogInformation("✅ Verbindung erfolgreich!");
                
                // Kurzen Test durchführen
                var messages = new List<Message>
                {
                    new() { Role = "system", Content = "Sie sind ein hilfreicher Assistent." },
                    new() { Role = "user", Content = "Erkläre kurz was Instandhaltung ist." }
                };
                
                var response = await llmService.ChatCompletionAsync(messages);
                _logger.LogInformation("📝 Test-Antwort: {Response}", 
                    response.Length > 200 ? response.Substring(0, 200) + "..." : response);
            }
            else
            {
                _logger.LogError("❌ Verbindung fehlgeschlagen!");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError("❌ Verbindungsfehler: {Error}", ex.Message);
        }
    }
}
