using System.Text.Json;

namespace FineTuningDataGenerator.Core.Models;

/// <summary>
/// Repräsentiert ein Training-Sample für Finetuning
/// </summary>
public class TrainingData
{
    public string System { get; set; } = string.Empty;
    public string User { get; set; } = string.Empty;
    public string Assistant { get; set; } = string.Empty;
    
    /// <summary>
    /// Konvertiert zu JSONL-Format für OpenAI Fine-tuning
    /// </summary>
    public string ToJsonL()
    {
        var messages = new[]
        {
            new { role = "system", content = System },
            new { role = "user", content = User },
            new { role = "assistant", content = Assistant }
        };
        
        return JsonSerializer.Serialize(new { messages });
    }
}

/// <summary>
/// Repräsentiert ein Trainingsbeispiel für Finetuning
/// </summary>
public class TrainingExample
{
    public string Prompt { get; set; } = string.Empty;
    public string Completion { get; set; } = string.Empty;
    public string? Source { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public Dictionary<string, object> Metadata { get; set; } = new();
}

/// <summary>
/// Sammlung von Trainingsbeispielen
/// </summary>
public class TrainingDataset
{
    public List<TrainingExample> Examples { get; set; } = new();
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public Dictionary<string, object> Metadata { get; set; } = new();

    public int Count => Examples.Count;
    public long TotalTokens => Examples.Sum(e => EstimateTokens(e.Prompt + e.Completion));

    private static long EstimateTokens(string text)
    {
        // Grobe Schätzung: ~4 Zeichen pro Token
        return text.Length / 4;
    }
}

/// <summary>
/// Ergebnis der Training-Daten-Generierung
/// </summary>
public class TrainingDataGenerationResult
{
    public List<TrainingData> TrainingData { get; set; } = new();
    public int TotalSamples => TrainingData.Count;
    public TimeSpan GenerationTime { get; set; }
    public List<string> SourceFiles { get; set; } = new();
    public string? ErrorMessage { get; set; }
    public bool Success { get; set; }

    public override string ToString()
    {
        return $"Generiert: {TotalSamples} Training-Samples aus {SourceFiles.Count} Dateien in {GenerationTime.TotalSeconds:F2}s";
    }
}
