using System.Text;
using System.Text.RegularExpressions;
using FineTuningDataGenerator.Core.Models;

namespace FineTuningDataGenerator.Core.Services;

/// <summary>
/// Service für die Aufteilung von Dokumenten in verarbeitbare Chunks
/// </summary>
public class DocumentChunker
{
    private readonly Regex _headerRegex = new(@"^#{1,6}\s+(.+)$", RegexOptions.Multiline);
    private readonly Regex _sentenceRegex = new(@"[.!?]+\s+", RegexOptions.Compiled);

    /// <summary>
    /// Teilt ein Dokument in semantisch sinnvolle Chunks auf
    /// </summary>
    public List<DocumentChunk> ChunkDocument(string content, string source, TrainingDataConfig config)
    {
        var chunks = new List<DocumentChunk>();
        
        // Dokument nach Überschriften aufteilen
        var sections = SplitByHeaders(content);
        
        foreach (var section in sections)
        {
            var sectionChunks = ChunkSection(section.Content, section.Title, source, config);
            chunks.AddRange(sectionChunks);
        }

        // Chunk-Indizes setzen
        for (int i = 0; i < chunks.Count; i++)
        {
            chunks[i].ChunkIndex = i;
        }

        return chunks;
    }

    /// <summary>
    /// Teilt Inhalt nach Markdown-Überschriften auf
    /// </summary>
    private List<(string Title, string Content)> SplitByHeaders(string content)
    {
        var sections = new List<(string Title, string Content)>();
        var lines = content.Split('\n');
        
        string currentTitle = "Einleitung";
        var currentContent = new List<string>();

        foreach (var line in lines)
        {
            var headerMatch = _headerRegex.Match(line);
            if (headerMatch.Success)
            {
                // Vorherige Sektion speichern
                if (currentContent.Count > 0)
                {
                    sections.Add((currentTitle, string.Join('\n', currentContent).Trim()));
                    currentContent.Clear();
                }
                
                currentTitle = headerMatch.Groups[1].Value.Trim();
            }
            else
            {
                currentContent.Add(line);
            }
        }

        // Letzte Sektion speichern
        if (currentContent.Count > 0)
        {
            sections.Add((currentTitle, string.Join('\n', currentContent).Trim()));
        }

        return sections.Where(s => !string.IsNullOrWhiteSpace(s.Content)).ToList();
    }

    /// <summary>
    /// Teilt eine Sektion in Chunks basierend auf Größe auf
    /// </summary>
    private List<DocumentChunk> ChunkSection(string content, string title, string source, TrainingDataConfig config)
    {
        var chunks = new List<DocumentChunk>();
        
        if (content.Length <= config.MaxChunkSize)
        {
            // Sektion passt in einen Chunk
            if (content.Length >= config.MinChunkSize)
            {
                chunks.Add(new DocumentChunk
                {
                    Content = content,
                    Source = source,
                    Title = title,
                    Section = title
                });
            }
        }
        else
        {
            // Sektion muss aufgeteilt werden
            var sentences = _sentenceRegex.Split(content);
            var currentChunk = new StringBuilder();
            var chunkIndex = 0;

            foreach (var sentence in sentences)
            {
                if (string.IsNullOrWhiteSpace(sentence)) continue;

                var potentialLength = currentChunk.Length + sentence.Length + 1;
                
                if (potentialLength > config.MaxChunkSize && currentChunk.Length >= config.MinChunkSize)
                {
                    // Aktuellen Chunk speichern
                    chunks.Add(new DocumentChunk
                    {
                        Content = currentChunk.ToString().Trim(),
                        Source = source,
                        Title = title,
                        Section = $"{title} (Teil {++chunkIndex})"
                    });
                    
                    currentChunk.Clear();
                }

                if (currentChunk.Length > 0)
                    currentChunk.Append(' ');
                currentChunk.Append(sentence.Trim());
            }

            // Letzten Chunk speichern
            if (currentChunk.Length >= config.MinChunkSize)
            {
                chunks.Add(new DocumentChunk
                {
                    Content = currentChunk.ToString().Trim(),
                    Source = source,
                    Title = title,
                    Section = chunkIndex > 0 ? $"{title} (Teil {++chunkIndex})" : title
                });
            }
        }

        return chunks;
    }

    /// <summary>
    /// Extrahiert wichtige Keywords aus einem Text
    /// </summary>
    public List<string> ExtractKeywords(string content)
    {
        // Einfache Keyword-Extraktion basierend auf häufigen Begriffen
        var words = Regex.Split(content.ToLower(), @"\W+")
            .Where(w => w.Length > 3)
            .Where(w => !IsStopWord(w))
            .GroupBy(w => w)
            .OrderByDescending(g => g.Count())
            .Take(10)
            .Select(g => g.Key)
            .ToList();

        return words;
    }

    /// <summary>
    /// Prüft ob ein Wort ein Stopwort ist
    /// </summary>
    private bool IsStopWord(string word)
    {
        var stopWords = new HashSet<string>
        {
            "und", "oder", "aber", "dass", "eine", "einer", "eines", "dem", "den", "der", "die", "das",
            "ist", "sind", "war", "waren", "wird", "werden", "kann", "könnte", "sollte", "muss",
            "haben", "hatte", "hat", "für", "mit", "von", "bei", "über", "unter", "nach", "vor",
            "auch", "wenn", "dann", "noch", "nur", "schon", "alle", "durch", "beim", "zur", "zum"
        };

        return stopWords.Contains(word);
    }
}
