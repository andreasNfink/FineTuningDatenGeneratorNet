# FineTuning Data Generator

Eine .NET 8 Bibliothek und Konsolenanwendung zum Erstellen von Finetuning-Daten. Das System bereinigt GitBook Markdown-Dateien und generiert automatisch Frage-Antwort-Paare für das Training von LLMs.

## 🎯 Funktionen

### GitBook Markdown Bereinigung
- **GitBook Markdown Bereinigung**: Entfernt GitBook-spezifische Elemente aus Markdown-Dateien
- **Batch-Verarbeitung**: Verarbeitung einzelner Dateien oder ganzer Verzeichnisse
- **Statistiken**: Detaillierte Berichte über entfernte Elemente

### Finetuning-Daten Generierung
- **KI-gestützte Frage-Antwort-Generierung**: Automatische Erstellung von Training-Samples mit LLM
- **Multiple Pipelines**: Anleitungen, Troubleshooting, Anlagenhistorien
- **OpenAI-kompatible Ausgabe**: JSONL-Format für Finetuning
- **Deutsche Sprache**: Optimiert für deutschsprachige technische Dokumentation
- **Intelligente Chunking**: Semantische Aufteilung von Dokumenten
- **Verschiedene Fragetypen**: "Was ist", "Wie funktioniert", "Welche Schritte", etc.

### Interaktive Bedienung
- **Interaktiver Modus**: Benutzerfreundliche Kommandozeileninteraktion
- **LLM-Verbindungstest**: Überprüfung der API-Konnektivität
- **Konfigurierbare Parameter**: Flexible Anpassung an verschiedene LLMs

## 🧹 Bereinigte Elemente

Die Anwendung entfernt folgende GitBook-spezifische Elemente:

- `{% hint %}...{% endhint %}` Blöcke
- `{% include "..." %}` Anweisungen
- `[text](link "mention")` Mentions → `[text]`
- `<mark>` HTML-Tags (behält Inhalt)
- Alle anderen HTML-Tags
- HTML-Entities (`&#x20;`, `&nbsp;`, etc.)
- GitBook Kommentare (`<!-- filepath: ... -->`)
- Mermaid-Diagramme (`\`\`\`mermaid ... \`\`\``)
- Mehrfache Leerzeilen

## 🚀 Installation & Verwendung

### Voraussetzungen

- .NET 8.0 SDK oder höher

### Build

```bash
# Solution bauen
dotnet build

# Nur die Konsolenanwendung bauen
dotnet build src/FineTuningDataGenerator.Console
```

### Verwendung

#### Interaktiver Modus
```bash
dotnet run --project src/FineTuningDataGenerator.Console
```

Optionen:
1. Einzelne Datei bereinigen
2. Verzeichnis bereinigen  
3. Beispieldateien aus InputFiles bereinigen
4. **Finetuning-Daten aus Anleitungen generieren**
5. **LLM-Verbindung testen**

#### Command-Line Modus

Einzelne Datei bereinigen:
```bash
dotnet run --project src/FineTuningDataGenerator.Console -- "InputFiles/auftrage.md"
```

Ganzes Verzeichnis bereinigen:
```bash
dotnet run --project src/FineTuningDataGenerator.Console -- "InputFiles" "OutputFiles"
```

## 🤖 Finetuning-Pipeline Verwendung

### LLM-Konfiguration
Die Anwendung unterstützt OpenAI-kompatible APIs. Beispielkonfiguration:
- **API-Host**: `http://your-llm-server:port/v1`
- **Model**: Name des zu verwendenden Modells
- **API-Key**: Optional, je nach Server-Konfiguration

### Pipeline-Typen
1. **Instructions Pipeline**: Generiert Frage-Antwort-Paare aus technischen Anleitungen
2. **Troubleshooting Pipeline**: Für industrielle Troubleshooting-Wissensdatenbanken (geplant)
3. **Maintenance History Pipeline**: Für Anlagenhistorien und Reparaturaufträge (geplant)

### Fragetypen für Anleitungen
- `was_ist`: "Was ist...?" Fragen
- `wie_funktioniert`: "Wie funktioniert...?" Fragen  
- `welche_schritte`: "Welche Schritte...?" Fragen
- `warum`: "Warum...?" Fragen
- `wann_verwendet`: "Wann wird...verwendet?" Fragen
- `welche_vorteile`: "Welche Vorteile...?" Fragen
- `wie_konfiguriert`: "Wie konfiguriert man...?" Fragen
- `was_beachten`: "Was sollte man beachten...?" Fragen

## 📁 Projektstruktur

```
FineTuningDataGenerator/
├── src/
│   ├── FineTuningDataGenerator.Core/          # Kernbibliothek
│   │   ├── Models/                            # Datenmodelle (TrainingData, LLMConfig, etc.)
│   │   ├── Services/                          # Services (GitBookMarkdownCleaner, LLMService, etc.)
│   │   └── Processors/                        # Pipelines (InstructionsPipeline, etc.)
│   └── FineTuningDataGenerator.Console/       # Konsolenanwendung
├── InputFiles/                                # Beispiel-Eingabedateien (GitBook Markdown)
├── OutputFiles/                               # Bereinigte Markdown-Dateien
├── TrainingData/                              # Generierte JSONL Finetuning-Dateien
└── FineTuningDataGenerator.sln               # Solution-Datei
```

## 🔧 Nutzung als Bibliothek

### GitBook Bereinigung
```csharp
using FineTuningDataGenerator.Core.Services;
using FineTuningDataGenerator.Core.Processors;

// Einzelne Bereinigung
var cleaner = new GitBookMarkdownCleaner();
var cleanedContent = cleaner.CleanGitBookMarkdown(originalContent);

// Datei-Verarbeitung
var processor = new GitBookFileProcessor();
var result = await processor.ProcessFileAsync("input.md", "output.md");
```

### Finetuning-Daten Generierung
```csharp
using FineTuningDataGenerator.Core.Models;
using FineTuningDataGenerator.Core.Processors;

// Pipeline konfigurieren
var pipelineConfig = new PipelineConfig
{
    Type = PipelineType.Instructions,
    Language = "de",
    LLMConfig = new LLMConfig
    {
        ApiHost = "http://your-llm:9005/v1",
        Model = "your-model",
        ApiKey = "your-api-key"
    },
    TrainingConfig = new TrainingDataConfig
    {
        MaxSamplesPerChunk = 3,
        QuestionTypes = new[] { "was_ist", "wie_funktioniert" }
    }
};

// Pipeline ausführen
var pipeline = new InstructionsPipeline(pipelineConfig);
var result = await pipeline.ProcessDocumentAsync("cleaned-document.md");

// Als JSONL exportieren
var samples = result.TrainingData.Select(td => new ConversationSample
{
    Messages = new List<Message>
    {
        new() { Role = "system", Content = td.System },
        new() { Role = "user", Content = td.User },
        new() { Role = "assistant", Content = td.Assistant }
    }
}).ToList();

await pipeline.ExportToJsonLAsync(samples, "training_data.jsonl");
```

## 📊 Beispiel-Output

### GitBook Bereinigung
```
=== FineTuning Data Generator ===
GitBook Markdown Bereinigungstool & Finetuning Pipeline

Verarbeite Verzeichnis: InputFiles
Ausgabe: OutputFiles

=== Verarbeitungsergebnis ===
📊 Verarbeitet: 2 Dateien, Erfolgreich: 2, Fehlgeschlagen: 0, Elemente entfernt: 214, Zeit: 0,04s

✅ Erfolgreich verarbeitete Dateien:
   auftrage.md -> auftrage.md (205 Elemente entfernt)
   strategiemanager.md -> strategiemanager.md (9 Elemente entfernt)

🎉 Verarbeitung abgeschlossen!
```

### Finetuning-Daten Generierung
```
=== Finetuning-Daten Generierung ===
Verarbeite: auftrage.md
✅ 9 Samples generiert
Verarbeite: strategiemanager.md  
✅ 12 Samples generiert

🎉 Finetuning-Daten erfolgreich generiert!
📊 Gesamt: 21 Training-Samples
📁 Datei: TrainingData/instructions_training_data_20250611_164523.jsonl

=== Beispiel Training-Sample ===
SYSTEM: Sie sind ein hilfreicher Assistent für Instandhaltungsmanagement und technische Anleitungen.
USER: Was ist ein Paledo-Auftrag?
ASSISTANT: Ein Paledo-Auftrag ist ein digitaler Workflow für Instandhaltungsmaßnahmen, der die gesamte Kette von der Erstellung und Einplanung...
```

## 🛠 Entwicklung

### Dependencies

- **HtmlAgilityPack**: HTML-Parsing und -bereinigung
- **Markdig**: Markdown-Verarbeitung  
- **Microsoft.Extensions.Hosting**: Dependency Injection und Hosting
- **Microsoft.Extensions.Logging**: Logging-Framework
- **System.Text.Json**: JSON-Serialisierung für JSONL-Output

### Tests ausführen

```bash
# Unit-Tests (falls vorhanden)
dotnet test
```

## 📝 Lizenz

Dieses Projekt steht unter der MIT-Lizenz.

## 🤝 Beitragen

1. Fork des Repositories
2. Feature-Branch erstellen (`git checkout -b feature/amazing-feature`)
3. Änderungen commiten (`git commit -m 'Add amazing feature'`)
4. Branch pushen (`git push origin feature/amazing-feature`)
5. Pull Request erstellen

## 🐛 Issues

Gefundene Bugs oder Feature-Requests können als [GitHub Issues](../../issues) gemeldet werden.
