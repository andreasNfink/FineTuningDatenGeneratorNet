using System.Diagnostics;
using FineTuningDataGenerator.Core.Models;
using FineTuningDataGenerator.Core.Services;

namespace FineTuningDataGenerator.Core.Processors;

/// <summary>
/// Prozessor für die Batch-Bereinigung von GitBook Markdown-Dateien
/// </summary>
public class GitBookFileProcessor
{
    private readonly GitBookMarkdownCleaner _cleaner;

    public GitBookFileProcessor()
    {
        _cleaner = new GitBookMarkdownCleaner();
    }

    /// <summary>
    /// Verarbeitet eine einzelne Datei
    /// </summary>
    /// <param name="inputFilePath">Pfad zur Eingabedatei</param>
    /// <param name="outputFilePath">Pfad zur Ausgabedatei (optional)</param>
    /// <returns>Ergebnis der Verarbeitung</returns>
    public async Task<FileProcessingResult> ProcessFileAsync(string inputFilePath, string? outputFilePath = null)
    {
        var stopwatch = Stopwatch.StartNew();
        var result = new FileProcessingResult
        {
            OriginalFilePath = inputFilePath,
            OutputFilePath = outputFilePath ?? GetDefaultOutputPath(inputFilePath)
        };

        try
        {
            // Prüfen ob Eingabedatei existiert
            if (!File.Exists(inputFilePath))
            {
                result.ErrorMessage = $"Eingabedatei nicht gefunden: {inputFilePath}";
                return result;
            }

            // Datei lesen
            var originalContent = await File.ReadAllTextAsync(inputFilePath);
            result.OriginalSize = originalContent.Length;

            // Statistiken vor der Bereinigung
            var stats = _cleaner.GetCleaningStats(originalContent);
            result.ElementsRemoved = stats.TotalElementsFound;

            // Bereinigen
            var cleanedContent = _cleaner.CleanGitBookMarkdown(originalContent);
            result.CleanedSize = cleanedContent.Length;

            // Ausgabeordner erstellen falls nötig
            var outputDir = Path.GetDirectoryName(result.OutputFilePath);
            if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
            {
                Directory.CreateDirectory(outputDir);
            }

            // Bereinigte Datei schreiben
            await File.WriteAllTextAsync(result.OutputFilePath, cleanedContent);

            result.Success = true;
        }
        catch (Exception ex)
        {
            result.ErrorMessage = ex.Message;
        }
        finally
        {
            stopwatch.Stop();
            result.ProcessingTime = stopwatch.Elapsed;
        }

        return result;
    }

    /// <summary>
    /// Verarbeitet alle Markdown-Dateien in einem Verzeichnis
    /// </summary>
    /// <param name="inputDirectory">Eingabeverzeichnis</param>
    /// <param name="outputDirectory">Ausgabeverzeichnis (optional)</param>
    /// <param name="recursive">Rekursiv in Unterverzeichnisse</param>
    /// <returns>Batch-Verarbeitungsergebnis</returns>
    public async Task<BatchProcessingResult> ProcessDirectoryAsync(
        string inputDirectory, 
        string? outputDirectory = null, 
        bool recursive = true)
    {
        var stopwatch = Stopwatch.StartNew();
        var batchResult = new BatchProcessingResult();

        try
        {
            if (!Directory.Exists(inputDirectory))
            {
                throw new DirectoryNotFoundException($"Eingabeverzeichnis nicht gefunden: {inputDirectory}");
            }

            outputDirectory ??= Path.Combine(inputDirectory, "cleaned");

            var searchOption = recursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;
            var markdownFiles = Directory.GetFiles(inputDirectory, "*.md", searchOption);

            foreach (var inputFile in markdownFiles)
            {
                var relativePath = Path.GetRelativePath(inputDirectory, inputFile);
                var outputFile = Path.Combine(outputDirectory, relativePath);

                var result = await ProcessFileAsync(inputFile, outputFile);
                batchResult.Results.Add(result);
            }
        }
        catch (Exception ex)
        {
            // Bei einem Verzeichnisfehler fügen wir ein fehlgeschlagenes Ergebnis hinzu
            batchResult.Results.Add(new FileProcessingResult
            {
                OriginalFilePath = inputDirectory,
                Success = false,
                ErrorMessage = ex.Message
            });
        }
        finally
        {
            stopwatch.Stop();
            batchResult.TotalProcessingTime = stopwatch.Elapsed;
        }

        return batchResult;
    }

    /// <summary>
    /// Erstellt einen Standard-Ausgabepfad für eine Datei
    /// </summary>
    private static string GetDefaultOutputPath(string inputFilePath)
    {
        var directory = Path.GetDirectoryName(inputFilePath) ?? string.Empty;
        var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(inputFilePath);
        var extension = Path.GetExtension(inputFilePath);
        
        return Path.Combine(directory, $"{fileNameWithoutExtension}_cleaned{extension}");
    }

    /// <summary>
    /// Validiert eine Datei und gibt an, ob sie bereinigt werden sollte
    /// </summary>
    /// <param name="filePath">Pfad zur zu prüfenden Datei</param>
    /// <returns>True wenn die Datei GitBook-Elemente enthält</returns>
    public async Task<bool> ShouldProcessFileAsync(string filePath)
    {
        try
        {
            if (!File.Exists(filePath) || !filePath.EndsWith(".md", StringComparison.OrdinalIgnoreCase))
                return false;

            var content = await File.ReadAllTextAsync(filePath);
            return _cleaner.ContainsGitBookElements(content);
        }
        catch
        {
            return false;
        }
    }
}
