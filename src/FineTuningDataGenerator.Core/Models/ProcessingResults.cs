namespace FineTuningDataGenerator.Core.Models;

/// <summary>
/// Ergebnis einer Datei-Bereinigungsoperation
/// </summary>
public class FileProcessingResult
{
    public string OriginalFilePath { get; set; } = string.Empty;
    public string OutputFilePath { get; set; } = string.Empty;
    public bool Success { get; set; }
    public string? ErrorMessage { get; set; }
    public long OriginalSize { get; set; }
    public long CleanedSize { get; set; }
    public int ElementsRemoved { get; set; }
    public TimeSpan ProcessingTime { get; set; }

    public double CompressionRatio => OriginalSize > 0 ? (double)CleanedSize / OriginalSize : 0;
}

/// <summary>
/// Ergebnis einer Batch-Bereinigungsoperation
/// </summary>
public class BatchProcessingResult
{
    public List<FileProcessingResult> Results { get; set; } = new();
    public TimeSpan TotalProcessingTime { get; set; }
    public int TotalFilesProcessed => Results.Count;
    public int SuccessfulFiles => Results.Count(r => r.Success);
    public int FailedFiles => Results.Count(r => !r.Success);
    public long TotalOriginalSize => Results.Sum(r => r.OriginalSize);
    public long TotalCleanedSize => Results.Sum(r => r.CleanedSize);
    public int TotalElementsRemoved => Results.Sum(r => r.ElementsRemoved);

    public override string ToString()
    {
        return $"Verarbeitet: {TotalFilesProcessed} Dateien, Erfolgreich: {SuccessfulFiles}, " +
               $"Fehlgeschlagen: {FailedFiles}, Elemente entfernt: {TotalElementsRemoved}, " +
               $"Zeit: {TotalProcessingTime.TotalSeconds:F2}s";
    }
}
