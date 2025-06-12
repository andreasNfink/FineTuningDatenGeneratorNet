using System.Text.Json;

namespace FineTuningDataGenerator.Core.Models;

/// <summary>
/// Konfiguration für die Verbindung zu einem OpenAI-kompatiblen LLM
/// </summary>
public class LLMConfig
{
    public string Name { get; set; } = string.Empty;
    public string ApiHost { get; set; } = string.Empty;
    public string ApiKey { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public bool IsOpenAICompatible { get; set; } = true;    public int MaxTokens { get; set; } = 2000;
    public double Temperature { get; set; } = 0.7;
    public int RequestTimeout { get; set; } = 180;
}

/// <summary>
/// Pipeline-Typen für verschiedene Anwendungsfälle
/// </summary>
public enum PipelineType
{
    Instructions,           // Anleitungen
    Troubleshooting,       // Troubleshooting Wissensdatenbanken
    MaintenanceHistory     // Anlagenhistorien/Reparaturaufträge
}

/// <summary>
/// Konfiguration für eine Finetuning-Pipeline
/// </summary>
public class PipelineConfig
{
    public PipelineType Type { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Language { get; set; } = "de";
    public LLMConfig LLMConfig { get; set; } = new();
    public TrainingDataConfig TrainingConfig { get; set; } = new();
    public List<string> PromptTemplates { get; set; } = new();
}

/// <summary>
/// Konfiguration für die Generierung von Training-Daten
/// </summary>
public class TrainingDataConfig
{
    public string SystemMessage { get; set; } = "Sie sind ein hilfreicher Assistent für Instandhaltungsmanagement und technische Anleitungen.";
    public int MinChunkSize { get; set; } = 200;
    public int MaxChunkSize { get; set; } = 1500;
    public int MaxSamplesPerChunk { get; set; } = 3;
    public int MaxTotalSamples { get; set; } = 1000;
    public bool GenerateQuestions { get; set; } = true;
    public bool GenerateSummaries { get; set; } = true;
    public bool GenerateExplanations { get; set; } = true;
    public List<string> QuestionTypes { get; set; } = new()
    {
        "was_ist", "wie_funktioniert", "welche_schritte", "warum", "wann_verwendet",
        "welche_vorteile", "welche_nachteile", "wie_konfiguriert", "was_beachten"
    };
}

/// <summary>
/// Trainings-Sample im JSONL-Format für OpenAI-kompatible Modelle
/// </summary>
public class ConversationSample
{
    public List<Message> Messages { get; set; } = new();
    
    /// <summary>
    /// Converts to JSONL format without system message and with UTF-8 encoding
    /// </summary>
    public string ToJsonL()
    {
        // Filter out system messages and use lowercase role names
        var filteredMessages = Messages
            .Where(m => m.Role.ToLower() != "system")
            .Select(m => new { role = m.Role.ToLower(), content = m.Content })
            .ToArray();

        var jsonObject = new { messages = filteredMessages };
        return System.Text.Json.JsonSerializer.Serialize(jsonObject, new System.Text.Json.JsonSerializerOptions 
        { 
            Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping 
        });
    }
}

/// <summary>
/// Nachricht in einer Konversation
/// </summary>
public class Message
{
    public string Role { get; set; } = string.Empty; // "system", "user", "assistant"
    public string Content { get; set; } = string.Empty;
}

/// <summary>
/// Chunk eines Dokuments für die Verarbeitung
/// </summary>
public class DocumentChunk
{
    public string Content { get; set; } = string.Empty;
    public string Source { get; set; } = string.Empty;
    public int ChunkIndex { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Section { get; set; } = string.Empty;
}

/// <summary>
/// Document context information for better question generation
/// </summary>
public class DocumentContext
{
    public string Title { get; set; } = string.Empty;
    public string Summary { get; set; } = string.Empty;
    public string MainTopic { get; set; } = string.Empty;
    public List<string> KeyConcepts { get; set; } = new();
    public string SourceFile { get; set; } = string.Empty;
}

/// <summary>
/// Enhanced chunk with document context
/// </summary>
public class ContextualChunk : DocumentChunk
{
    public DocumentContext DocumentContext { get; set; } = new();
    public string ChunkSummary { get; set; } = string.Empty;
}

/// <summary>
/// Enhanced training data configuration with context support
/// </summary>
public class EnhancedTrainingDataConfig : TrainingDataConfig
{
    public bool UseDocumentContext { get; set; } = true;
    public int ContextAnalysisLength { get; set; } = 5000;
    public double ContextTemperature { get; set; } = 0.3;
    
    // Concurrency settings
    public int MaxConcurrentDocuments { get; set; } = Environment.ProcessorCount;
    public int MaxConcurrentChunks { get; set; } = Environment.ProcessorCount * 2;
    public int MaxConcurrentLLMRequests { get; set; } = 4; // Conservative default for LLM load
}

/// <summary>
/// Result of batch training data generation for multiple documents
/// </summary>
public class BatchTrainingDataResult
{
    public List<TrainingDataGenerationResult> DocumentResults { get; set; } = new();
    public List<string> SourceFiles { get; set; } = new();
    public int TotalSamples { get; set; }
    public string? CombinedOutputFile { get; set; }
    public bool Success { get; set; }
    public string? ErrorMessage { get; set; }
    public TimeSpan ProcessingTime { get; set; }

    public int SuccessfulDocuments => DocumentResults.Count(r => r.Success);
    public int FailedDocuments => DocumentResults.Count(r => !r.Success);

    public override string ToString()
    {
        return $"Batch: {TotalSamples} Samples aus {SourceFiles.Count} Dateien ({SuccessfulDocuments} erfolgreich, {FailedDocuments} fehlgeschlagen)";
    }
}
