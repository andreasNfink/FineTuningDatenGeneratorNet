using System.Text.RegularExpressions;
using HtmlAgilityPack;

namespace FineTuningDataGenerator.Core.Services;

/// <summary>
/// Service zum Bereinigen von GitBook Markdown-Dateien
/// </summary>
public class GitBookMarkdownCleaner
{    private readonly Regex _gitBookHintRegex = new(@"{% hint.*?%}.*?{% endhint %}", RegexOptions.Singleline | RegexOptions.IgnoreCase);
    private readonly Regex _gitBookIncludeRegex = new(@"{% include.*?%}", RegexOptions.IgnoreCase);
    private readonly Regex _gitBookMentionRegex = new(@"\[([^\]]+)\]\([^)]+\s+""mention""\)", RegexOptions.IgnoreCase);
    private readonly Regex _internalLinkRegex = new(@"\[([^[\]]+\.md)\]", RegexOptions.IgnoreCase);
    private readonly Regex _markTagRegex = new(@"<mark[^>]*>(.*?)</mark>", RegexOptions.Singleline | RegexOptions.IgnoreCase);
    private readonly Regex _htmlTagRegex = new(@"<[^>]+>", RegexOptions.IgnoreCase);
    private readonly Regex _htmlEntityRegex = new(@"&#x[0-9a-fA-F]+;|&[a-zA-Z]+;", RegexOptions.IgnoreCase);
    private readonly Regex _gitBookCommentRegex = new(@"<!-- filepath:.*?-->", RegexOptions.IgnoreCase);
    private readonly Regex _mermaidDiagramRegex = new(@"```mermaid.*?```", RegexOptions.Singleline | RegexOptions.IgnoreCase);
    private readonly Regex _multipleEmptyLinesRegex = new(@"\n\s*\n\s*\n", RegexOptions.Multiline);

    /// <summary>
    /// Bereinigt eine GitBook Markdown-Datei von allen GitBook-spezifischen Elementen
    /// </summary>
    /// <param name="content">Der ursprüngliche Markdown-Inhalt</param>
    /// <returns>Der bereinigte Markdown-Inhalt</returns>
    public string CleanGitBookMarkdown(string content)
    {
        if (string.IsNullOrWhiteSpace(content))
            return string.Empty;

        var cleanedContent = content;

        // 1. GitBook Hints entfernen ({% hint %} ... {% endhint %})
        cleanedContent = _gitBookHintRegex.Replace(cleanedContent, string.Empty);

        // 2. GitBook Includes entfernen ({% include "..." %})
        cleanedContent = _gitBookIncludeRegex.Replace(cleanedContent, string.Empty);        // 3. GitBook Mentions bereinigen [text](link "mention") -> [text](link)
        cleanedContent = _gitBookMentionRegex.Replace(cleanedContent, "[$1]");        // 4. Interne Links bereinigen [filename.md] -> filename
        cleanedContent = _internalLinkRegex.Replace(cleanedContent, match =>
        {
            var filename = match.Groups[1].Value;
            // Entferne .md Endung und gib nur den Dateinamen zurück, mache den aber mit großem Anfangsbuchstaben
            // und ohne Dateiendung
            var cleanFilename = filename.EndsWith(".md", StringComparison.OrdinalIgnoreCase)
                ? filename.Substring(0, filename.Length - 3)
                : filename;
            
            // Ersten Buchstaben großschreiben
            return string.IsNullOrEmpty(cleanFilename) 
                ? cleanFilename 
                : char.ToUpper(cleanFilename[0]) + cleanFilename.Substring(1);
                
        });// 5. Mark-Tags bereinigen und Inhalt extrahieren
        cleanedContent = _markTagRegex.Replace(cleanedContent, "$1");

        // 6. HTML-Tags entfernen
        cleanedContent = RemoveHtmlTags(cleanedContent);

        // 7. HTML-Entities dekodieren
        cleanedContent = DecodeHtmlEntities(cleanedContent);        // 8. GitBook Kommentare entfernen
        cleanedContent = _gitBookCommentRegex.Replace(cleanedContent, string.Empty);        // 9. Mermaid-Diagramme entfernen
        cleanedContent = _mermaidDiagramRegex.Replace(cleanedContent, string.Empty);

        // 10. Mehrfache Leerzeilen reduzieren
        cleanedContent = _multipleEmptyLinesRegex.Replace(cleanedContent, "\n\n");

        // 11. Trim und finale Bereinigung
        cleanedContent = cleanedContent.Trim();

        return cleanedContent;
    }

    /// <summary>
    /// Entfernt HTML-Tags unter Beibehaltung des Inhalts
    /// </summary>
    private string RemoveHtmlTags(string content)
    {
        var doc = new HtmlDocument();
        doc.LoadHtml(content);
        return doc.DocumentNode.InnerText;
    }

    /// <summary>
    /// Dekodiert HTML-Entities
    /// </summary>
    private string DecodeHtmlEntities(string content)
    {
        return System.Net.WebUtility.HtmlDecode(content);
    }

    /// <summary>
    /// Überprüft, ob eine Datei GitBook-spezifische Elemente enthält
    /// </summary>
    /// <param name="content">Der zu überprüfende Inhalt</param>
    /// <returns>True, wenn GitBook-Elemente gefunden wurden</returns>
    public bool ContainsGitBookElements(string content)
    {
        if (string.IsNullOrWhiteSpace(content))
            return false;        return _gitBookHintRegex.IsMatch(content) ||
               _gitBookIncludeRegex.IsMatch(content) ||
               _markTagRegex.IsMatch(content) ||
               _htmlTagRegex.IsMatch(content) ||
               _htmlEntityRegex.IsMatch(content) ||
               _mermaidDiagramRegex.IsMatch(content);
    }

    /// <summary>
    /// Gibt Statistiken über die gefundenen GitBook-Elemente zurück
    /// </summary>
    /// <param name="content">Der zu analysierende Inhalt</param>
    /// <returns>Statistiken über gefundene Elemente</returns>
    public GitBookCleaningStats GetCleaningStats(string content)
    {
        if (string.IsNullOrWhiteSpace(content))
            return new GitBookCleaningStats();        return new GitBookCleaningStats
        {
            HintBlocks = _gitBookHintRegex.Matches(content).Count,
            IncludeStatements = _gitBookIncludeRegex.Matches(content).Count,
            MentionLinks = _gitBookMentionRegex.Matches(content).Count,
            MarkTags = _markTagRegex.Matches(content).Count,
            HtmlTags = _htmlTagRegex.Matches(content).Count,
            HtmlEntities = _htmlEntityRegex.Matches(content).Count,
            MermaidDiagrams = _mermaidDiagramRegex.Matches(content).Count
        };
    }
}

/// <summary>
/// Statistiken über die Bereinigung einer GitBook Markdown-Datei
/// </summary>
public class GitBookCleaningStats
{
    public int HintBlocks { get; set; }
    public int IncludeStatements { get; set; }
    public int MentionLinks { get; set; }
    public int MarkTags { get; set; }
    public int HtmlTags { get; set; }
    public int HtmlEntities { get; set; }
    public int MermaidDiagrams { get; set; }

    public int TotalElementsFound => HintBlocks + IncludeStatements + MentionLinks + MarkTags + HtmlTags + HtmlEntities + MermaidDiagrams;

    public override string ToString()
    {
        return $"Gefundene Elemente: {TotalElementsFound} (Hints: {HintBlocks}, Includes: {IncludeStatements}, " +
               $"Mentions: {MentionLinks}, Mark-Tags: {MarkTags}, HTML-Tags: {HtmlTags}, HTML-Entities: {HtmlEntities}, " +
               $"Mermaid-Diagramme: {MermaidDiagrams})";
    }
}
