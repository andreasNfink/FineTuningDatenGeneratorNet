# LINQ Tutorial

## Tutorial: Fortgeschrittene Datenmanipulation mit Linq-Style Custom Functions

### Einleitung

Willkommen zu diesem Tutorial über die **Linq-Style Custom Functions** in Paledo. Diese Funktionen sind eine leistungsstarke Erweiterung der **XAF Criteria Language** und ermöglichen es Ihnen, komplexe Operationen auf Daten-Sammlungen (Listen von Objekten) direkt innerhalb von Criteria-Ausdrücken durchzuführen – ähnlich wie Sie es vielleicht von LINQ (Language Integrated Query) in C# kennen.

Dieses Tutorial richtet sich an erfahrene Paledo-Anwender, Administratoren und Entwickler, die bereits mit den Grundlagen der Criteria Language vertraut sind und lernen möchten, wie sie Listen filtern, transformieren, sortieren, gruppieren und aggregieren können, um anspruchsvolle Automatisierungen und Berechnungen in Paledo umzusetzen.

Wir werden die wichtigsten Linq-Style Funktionen vorstellen und anhand von komplexen Beispielen aus der Praxis (insbesondere aus **Workflow State Actions**) deren kombinierten Einsatz demonstrieren.

**Kontext:** Diese Funktionen können in verschiedenen Bereichen von Paledo eingesetzt werden, darunter:

* Filterkriterien für Listenansichten
* Berechnete Felder (Calculated Properties)
* Validierungsregeln
* Regeln für bedingte Formatierung oder Sichtbarkeit
* **Workflow State Actions** (hier liegt der Fokus der komplexen Beispiele)

### Kernkonzepte

1. **Operationen auf Sammlungen:** Im Gegensatz zu Funktionen, die auf einzelnen Werten operieren, arbeiten Linq-Style Funktionen auf ganzen Listen oder Sammlungen von Objekten (technisch: `IEnumerable`). Dies können z.B. die Positionen eines Auftrags (`[Order.Operations]`), die Messwerte einer Anlage (`[Equipment.MeasurementPoints]`) oder das Ergebnis einer anderen Linq-Style Funktion sein.
2. **Verkettung (Chaining):** Die Stärke dieser Funktionen liegt in ihrer Kombinierbarkeit. Das Ergebnis einer Funktion (z.B. `Where` zum Filtern) kann direkt als Eingabe für die nächste Funktion (z.B. `Select` zum Transformieren) verwendet werden, was komplexe Datenpipelines ermöglicht.
3. **Ausdrucks-Parameter:** Viele dieser Funktionen erwarten als Parameter einen _String_, der selbst wieder ein Criteria Language Ausdruck ist (z.B. ein Filterkriterium für `Where` oder ein Transformationsausdruck für `Select`).
4. **Kontext `This`:** Innerhalb der Ausdrucks-Parameter (`selector`, `predicate`) bezieht sich das Schlüsselwort `This` (oder `CurrentObject()`, je nach Kontext) auf das **aktuelle Element der Sammlung**, das gerade verarbeitet wird – ähnlich einer Lambda-Variable in LINQ (`item => ...`).
5. **Auswertung:** Der gesamte Ausdruck, inklusive aller verketteten Linq-Style Funktionen, wird als eine Einheit ausgewertet und liefert ein Ergebnis (z.B. eine gefilterte/transformierte Liste, einen aggregierten Wert, einen String).

***

### Übersicht & Referenz der Linq-Style Funktionen

#### 1. Filtern: `Where`

* **Zweck:** Wählt Elemente aus einer Sammlung aus, die eine bestimmte Bedingung erfüllen.
* **Analogie:** LINQ `Where()`
* **Signatur:** `IEnumerable<XPBaseObject> Where(IEnumerable collection, string predicate)`
* **Parameter:**
  * `collection`: Die Eingabesammlung.
  * `predicate`: Ein boolescher Ausdruck (als String). `This` bezieht sich auf das aktuelle Element.
*   **Beispiel (Filter):**

    ```criteria
    // Wähle alle Aufträge mit Priorität 'Hoch'
    Where([AuftragsListe], "[Priority.Code] = 'HOCH'")
    ```
*   **Beispiel (Innerhalb einer komplexeren Kette):**

    ```criteria
    // Wähle nur aktive Benutzer aus, bevor ihre Namen selektiert werden
    Select(Where([BenutzerListe], "[IsActive] = true"), "FullName")
    ```

#### 2. Transformieren/Projizieren: `Select`

* **Zweck:** Projiziert jedes Element einer Sammlung auf einen neuen Wert oder eine neue Struktur.
* **Analogie:** LINQ `Select()`
* **Signatur:** `IEnumerable<object> Select(IEnumerable collection, string selector)`
* **Parameter:**
  * `collection`: Die Eingabesammlung.
  * `selector`: Ein Ausdruck (als String), der für jedes Element den neuen Wert berechnet. `This` bezieht sich auf das aktuelle Element.
*   **Beispiel (Berechnetes Feld):**

    ```criteria
    // Extrahiere nur die E-Mail-Adressen aus einer Benutzerliste
    Select([BenutzerListe], "Email")
    ```
*   **Beispiel (Formatierung für `Join`):**

    ```criteria
    // Erzeuge formatierte Strings für jeden Logeintrag
    Select(GetMemberValue(this,'LogEntries'),'CreationDate + " " + PaledoUser + ": " + Text')
    ```

#### 3. Sortieren: `OrderBy`, `OrderByDescending`, `ThenBy`, `ThenByDescending`, `Order`, `OrderDescending`

* **Zweck:** Ordnet die Elemente einer Sammlung nach einem oder mehreren Kriterien.
* **Analogie:** LINQ `OrderBy()`, `OrderByDescending()`, `ThenBy()`, `ThenByDescending()`
*   **Signaturen:**

    ```
    IEnumerable<XPBaseObject> OrderBy(IEnumerable collection, string predicate)
    IEnumerable<XPBaseObject> OrderByDescending(IEnumerable collection, string predicate)
    IEnumerable<XPBaseObject> ThenBy(IEnumerable collection, string predicate) // Muss auf sortierte Liste angewendet werden!
    IEnumerable<XPBaseObject> ThenByDescending(IEnumerable collection, string predicate) // Muss auf sortierte Liste angewendet werden!
    IEnumerable<object> Order(IEnumerable collection) // Natürliche Sortierung einfacher Typen
    IEnumerable<object> OrderDescending(IEnumerable collection) // Absteigende natürliche Sortierung
    ```
* **Parameter:**
  * `collection`: Die Eingabesammlung (für `ThenBy*` muss diese bereits sortiert sein).
  * `predicate`: (Nur für `OrderBy*`, `ThenBy*`) Der Ausdruck (als String), nach dem sortiert wird. `This` bezieht sich auf das aktuelle Element.
*   **Beispiel (Sortierte Liste für Anzeige):**

    ```criteria
    // Aufträge nach Fälligkeitsdatum aufsteigend sortieren
    OrderBy([AuftragsListe], "DueDate")
    ```
*   **Beispiel (Mehrstufige Sortierung):**

    ```criteria
    // Aufgaben nach Abteilung (A-Z) und dann nach Priorität (Hoch zuerst) sortieren
    ThenByDescending(OrderBy([AufgabenListe], "Department.Name"), "PriorityValue")
    ```

#### 4. Gruppieren: `GroupBy`

* **Zweck:** Gruppiert Elemente einer Sammlung basierend auf einem gemeinsamen Schlüssel.
* **Analogie:** LINQ `GroupBy()`
* **Signatur:** `IEnumerable<IGrouping<object, object>> GroupBy(IEnumerable collection, string keySelector)`
* **Parameter:**
  * `collection`: Die Eingabesammlung.
  * `keySelector`: Ein Ausdruck (als String), der den Gruppierungsschlüssel für jedes Element bestimmt. `This` bezieht sich auf das aktuelle Element.
* **Rückgabewert:** Eine Sammlung von Gruppen. Jede Gruppe hat eine `Key`-Eigenschaft und eine `Items`-Eigenschaft (oder man verwendet Aggregatfunktionen wie `Count()`, `Sum()` direkt auf der Gruppe).
*   **Beispiel (Aggregation nach Gruppe):**

    ```criteria
    // Zähle Aufträge pro Status
    Select(GroupBy([AuftragsListe], "Status"), "Key + ': ' + Count()") // Ergibt z.B. ["Offen: 5", "In Arbeit: 12", ...]
    ```
*   **Beispiel (Aggregation von Werten pro Gruppe):**

    ```criteria
    // Summiere geplante Stunden pro Arbeitsplatz
    Select(GroupBy([OperationenListe], "WorkCenter.Code"), "Key + ': ' + Sum(PlannedDuration) + 'h'")
    ```

#### 5. Eindeutige Elemente: `Distinct`

* **Zweck:** Entfernt Duplikate aus einer Sammlung.
* **Analogie:** LINQ `Distinct()`
* **Signatur:** `IEnumerable<object> Distinct(IEnumerable collection)`
* **Parameter:**
  * `collection`: Die Eingabesammlung (oft das Ergebnis von `Select`).
*   **Beispiel (Berechnetes Feld):**

    ```criteria
    // Erhalte eine Liste aller eindeutigen Länder aus einer Kundenliste
    Distinct(Select([KundenListe], "Country"))
    ```

#### 6. Teilmengen auswählen: `Take`

* **Zweck:** Wählt eine definierte Anzahl von Elementen vom Anfang einer Sammlung aus.
* **Analogie:** LINQ `Take()`
* **Signatur:** `IEnumerable Take(IEnumerable collection, int count)`
* **Parameter:**
  * `collection`: Die Eingabesammlung (oft zuvor sortiert).
  * `count`: Die Anzahl der zu nehmenden Elemente.
*   **Beispiel (Berechnetes Feld):**

    ```criteria
    // Wähle die Top 3 Aufträge mit der höchsten Priorität
    Take(OrderByDescending([AuftragsListe], "PriorityValue"), 3)
    ```

#### 7. Aktion für jedes Element: `ForEach`

* **Zweck:** Führt eine Aktion (einen Ausdruck mit Seiteneffekten) für jedes Element aus, das optional einem Filter entspricht. **Achtung:** Hauptsächlich in Workflow State Actions oder ähnlichen Aktionskontexten sinnvoll!
* **Analogie:** `foreach`-Schleife mit Aktion
* **Signatur:** `bool ForEach(IEnumerable<XPBaseObject> collection, string filterPredicate, string evaluationExpression)`
* **Parameter:**
  * `collection`: Die Eingabesammlung.
  * `filterPredicate`: Filterbedingung (als String), `'True'` für alle Elemente. `This` bezieht sich auf das aktuelle Element.
  * `evaluationExpression`: Der auszuführende Ausdruck (als String), oft `SetMemberValue` oder `ChangeWorkflowState`. `This` bezieht sich auf das aktuelle Element.
*   **Beispiel (Workflow State Action):**

    ```criteria
    // Setze alle offenen Positionen eines stornierten Auftrags auf 'Storniert'
    Iif([Status] == 'Storniert',
        ForEach([Positionen], '[Status] == "Offen"', 'SetMemberValue(This, "Status", "Storniert")'),
        null
    )
    ```

    **Beispiel aus `Examplescustomfuncs.md`:**

    ```criteria
    // Setze alle älteren Dokumentrevisionen auf 'Obsolet'
    Iif(true, ForEach([OldDVSDocumentRevisions], '[RevisionNo] < ' + [RevisionNo], 'SetMemberValue(This, ''Obsolete'', True)'), null)
    ```

{% hint style="warning" %}
`ForEach` führt Aktionen mit Seiteneffekten aus. Verwenden Sie es mit Vorsicht und hauptsächlich in dafür vorgesehenen Kontexten wie Workflow Actions. Der Rückgabewert (`bool`) ist meist irrelevant.
{% endhint %}

#### 8. Sammlung zu String verbinden: `Join`

* **Zweck:** Konvertiert eine Sammlung in einen einzelnen String, wobei die Elemente durch ein Trennzeichen verbunden werden.
* **Analogie:** `string.Join()`
* **Signatur:** `string Join(IEnumerable collection, string separator)`
* **Parameter:**
  * `collection`: Die Eingabesammlung (oft das Ergebnis von `Select`).
  * `separator`: Das Trennzeichen.
*   **Beispiel (Berechnetes Feld):**

    ```criteria
    // Erzeuge eine kommaseparierte Liste der zugewiesenen Benutzer
    Join(Select(Distinct([AufgabenListe].Where('AssignedUser != null')), 'AssignedUser.UserName'), ', ')
    ```

    **Beispiel aus `Examplescustomfuncs.md` (Logbuch):**

    ```criteria
    // Verbinde formatierte Log-Strings (angenommen CHAR(13)+CHAR(10) statt ' ')
    Join(Select(GetMemberValue(this,'LogEntries'),'CreationDate + " " + PaledoUser + ": " + Text'), CHAR(13)+CHAR(10))
    ```

***

### Komplexe Beispiele aus der Praxis (Workflow State Actions)

Hier sehen wir, wie diese Funktionen in realen Workflow State Actions kombiniert werden:

***

**Beispiel 1: Fortschrittsberechnung basierend auf Record-Status**

* **Ziel:** Berechne den Fortschritt eines Auftrags basierend auf den Status der zugehörigen Records (Dokumentationseinträge). Abgenommene zählen 100%, andere (außer 'Vorbereitet') zählen 25%.
*   **Code Snippet:**

    ```criteria
    // Kontext: State Action für ein Objekt, das einen Auftrag referenziert (z.B. ein Record)
    IIF(not ISNULLOREMPTY([Order]), // Nur wenn ein Auftrag verknüpft ist
        SETMEMBERVALUE(Order, 'Progress', // Setze das Feld 'Progress' am Auftrag
            ToInt(Round( // Runde das Ergebnis auf eine Ganzzahl
                (ToDouble(Order.Records[WorkflowState.Name<>'Abgenommen' and WorkflowState.Name<>'Vorbereitet' ].Count()) // Anzahl Records "in Arbeit"
                * (ToDouble(100) / ToDouble(Iif(Order.Records.Count() == 0, 1, Order.Records.Count())) ) // Anteil an Gesamt (verhindere Div/0)
                * ToDouble(0.25)) // Gewichtung 25%
                + // Addiere...
                (ToDouble(Order.Records[WorkflowState.Name='Abgenommen'].Count()) // Anzahl Records "abgenommen"
                * (ToDouble(100) / ToDouble(Iif(Order.Records.Count() == 0, 1, Order.Records.Count()))) // Anteil an Gesamt (verhindere Div/0)
                // * 100% (implizit)
                )
            ))
        )
    , '') // Tu nichts, wenn kein Auftrag verknüpft ist
    ```
* **Analyse:**
  * Greift auf die Liste `Records` des verknüpften `Order`-Objekts zu.
  * Verwendet Filter (`[...]`) und `Count()`, um die Anzahl der Records in verschiedenen Status zu ermitteln.
  * Berechnet prozentuale Anteile und wendet Gewichtungen an.
  * Verwendet `Iif`, um Division durch Null zu vermeiden, falls keine Records vorhanden sind.
  * Konvertiert Typen (`ToDouble`, `ToInt`) und rundet das Ergebnis (`Round`).
  * Setzt das Ergebnis im `Order`-Objekt mit `SetMemberValue`.

***

**Beispiel 2: Zusammenfassung von Rückmeldungskommentaren**

* **Ziel:** Sammle alle Kommentare aus den Rückmeldungen (`Confirmations`) eines Auftrags, sortiere sie und füge sie (mit Zeitstempel etc.) zum `Result`-Feld des Auftrags hinzu, wobei vorhandene Inhalte erhalten bleiben.
*   **Code Snippet:**

    ```criteria
    // Kontext: State Action für einen Auftrag (this)
    Iif(IsNullOrEmpty([TechnicalCompleteDate]), // Nur wenn noch nicht technisch abgeschlossen
        SetMemberValue(this, 'Result', // Setze das Feld 'Result'
            Trim( // Entferne Leerzeichen am Ende
                Iif( // Prüfe, ob neue Kommentare hinzugefügt werden
                    IsNullOrEmpty(Join(Select(OrderBy(Where(GetMemberValue(this, 'Confirmations'), '[Comment] != null'), 'DateEnd'), 'IIF(IsNullOrEmpty([Comment]),'''',Substring(ToString(DateEnd),0, 16)+'' - ''+Substring(OperationContext, 0, 3)+'' - ''+WorkCenter.Code+CHAR(13)+CHAR(10)+''''+Comment+CHAR(13)+CHAR(10)+CHAR(13)+CHAR(10))'), '')),
                    [Result], // Wenn keine neuen Kommentare, behalte alten Wert
                    // Wenn neue Kommentare:
                    Coalesce([Result] + Char(13) + Char(10) + '------------------------------------' + Char(13) + Char(10), '') // Füge Trenner hinzu (nur wenn Result schon was enthält)
                    + Join(Select(OrderBy(Where(GetMemberValue(this, 'Confirmations'), '[Comment] != null'), 'DateEnd'), 'IIF(IsNullOrEmpty([Comment]),'''',Substring(ToString(DateEnd),0, 16)+'' - ''+Substring(OperationContext, 0, 3)+'' - ''+WorkCenter.Code+CHAR(13)+CHAR(10)+''''+Comment+CHAR(13)+CHAR(10)+CHAR(13)+CHAR(10))'), '') // Füge neue Kommentare hinzu
                )
            )
        )
    , '') // Tu nichts, wenn technisch abgeschlossen
    ```
* **Analyse:**
  * Filtert (`Where`) die `Confirmations`-Liste nach Einträgen, die einen Kommentar haben.
  * Sortiert (`OrderBy`) die gefilterten Rückmeldungen nach Enddatum.
  * Projiziert (`Select`) jede Rückmeldung auf einen formatierten String, der Datum, Kontext, Arbeitsplatz und den Kommentar enthält, inklusive Zeilenumbrüchen (`CHAR(13)+CHAR(10)`). Leere Kommentare werden übersprungen (`IIF`).
  * Verbindet (`Join`) diese formatierten Strings zu einem einzigen Textblock.
  * Prüft (`Iif(IsNullOrEmpty(Join(...)))`), ob überhaupt neue Kommentare formatiert wurden.
  * Hängt die neuen Kommentare an den bestehenden `Result`-Text an, getrennt durch eine Linie (`Coalesce` stellt sicher, dass der Trenner nur hinzugefügt wird, wenn `Result` schon Text enthält).
  * Entfernt abschließend mögliche Leerzeichen (`Trim`).
  * Das Ganze geschieht nur, wenn `TechnicalCompleteDate` leer ist.

***

**Beispiel 3: KI-Zusammenfassung für Logbuch und Ersatzteile**

* **Ziel:** Erzeuge eine KI-generierte Zusammenfassung eines Auftrags für das Feld `AISummary`, die Titel, Beschreibung, formatierte Logbucheinträge und verwendete Ersatzteile enthält.
*   **Code Snippet:**

    ```criteria
    // Kontext: State Action für einen Auftrag (this)
    SetMemberValue(this,'AISummary',PROCESSTEXTINPUT_AI('CreateOrderSummaryHH', // Prompt für KI
        Subject, // Parameter 1 für Prompt (optional, je nach Prompt-Design)
        Replace(Description,'FM zu ',''), // Parameter 2: Bereinigte Beschreibung
        // Parameter 3: Zusammengesetzter Text mit Details
        'Anlage: ' + FirstComponent.Caption+CHAR(13)+CHAR(10)+
        'Problem: ' + Subject+CHAR(13)+CHAR(10)+
        'Störbeginn: ' + FaultStart +CHAR(13)+CHAR(10)+
        'Störende: ' + FaultEnd +CHAR(13)+CHAR(10)+
        'Maschinenstillstand: '+IIF(HasDowntime=True, 'JA', 'NEIN')+CHAR(13)+CHAR(10)+CHAR(13)+CHAR(10)+
        'Logbuch:'+CHAR(13)+CHAR(10)+
        Join(Select(Where(GetMemberValue([this], 'Confirmations'),'ActualTime>0 and not isnullorempty(Comment)'), 'CreationDate+'' / Arbeitszeit: ''+ActualTime+''h / Bearbeiter: ''+PaledoUser+'' / Bemerkung: ''+Comment+CHAR(13)+CHAR(10)'),'') + // Formatierte Logeinträge
        CHAR(13)+CHAR(10)+CHAR(13)+CHAR(10)+
        'Ersatzteile:'+CHAR(13)+CHAR(10)+
        Join(Select(Where(GetMemberValue([this], 'MatReservations'),'Isnullorempty(StorniertAm) '), 'Material.MaterialNo+'' - ''+Material.Description+CHAR(13)+CHAR(10)'),'') // Verwendete Ersatzteile
    ))
    ```
* **Analyse:**
  * Ruft `PROCESSTEXTINPUT_AI` mit einem spezifischen Prompt 'CreateOrderSummaryHH' auf.
  * Übergibt mehrere Parameter an den Prompt:
    * `Subject` (Auftragstitel)
    * Bereinigte `Description` (entfernt 'FM zu ')
    * Einen langen String, der dynamisch aufgebaut wird:
      * Anlagen-Caption
      * Problem (nochmal Subject)
      * Start-/Endzeiten der Störung
      * Stillstandsinfo (`Iif`)
      * Formatierte Logbucheinträge (gefiltert nach Zeit > 0 und Kommentar nicht leer, projiziert und gejoint)
      * Verwendete Ersatzteile (gefiltert nach nicht storniert, projiziert auf Nummer+Beschreibung, gejoint)
  * Das Ergebnis der KI wird im Feld `AISummary` gespeichert.
* **Key Concepts Demonstrated:** Komplexe String-Konstruktion, Kombination von Objektfeldern und formatierten Listen (`Join`, `Select`, `Where`), bedingte Ausgabe (`Iif`), Übergabe strukturierter Daten an eine KI-Funktion.

***

### Performance-Überlegungen

Linq-Style Funktionen sind mächtig, aber ihre Ausführung kann, insbesondere bei **großen Sammlungen** und **komplexen Verkettungen**, Performance-Auswirkungen haben.

* **Datenbankoptimierung:** Funktionen wie `Where`, `Select` (einfache Property-Selektion), `OrderBy`, `Count` können oft effizient in Datenbankabfragen übersetzt werden (besonders wenn die Funktion `ICustomFunctionOperatorFormattable` implementiert).
* **Client vs. Server:** Komplexe Transformationen (`Select` mit Berechnungen), `Join`, `GroupBy` oder `ForEach` erfordern oft, dass die Daten zuerst vom Server geladen und dann im Speicher verarbeitet werden. Führen Sie solche Operationen **bevorzugt serverseitig** aus (z.B. in Workflow Actions, serverseitigen Berechneten Feldern oder durch dedizierte Dienste), um die Client-Performance (insbesondere in Blazor oder mobilen Apps) nicht zu beeinträchtigen.
* **`ForEach` Vorsicht:** Das Ausführen von Aktionen für jedes Element einer großen Liste kann zeitaufwändig sein und sollte in performanzkritischen Szenarien überdacht werden (z.B. durch Batch-Verarbeitung oder asynchrone Ausführung).

### Fazit

Die Linq-Style Custom Functions bieten eine deklarative und oft sehr kompakte Möglichkeit, komplexe Datenoperationen direkt in der XAF Criteria Language durchzuführen. Sie ermöglichen das Filtern, Transformieren, Sortieren und Aggregieren von Datenlisten ohne die Notwendigkeit von benutzerdefiniertem Code im Backend. Durch geschickte Kombination dieser Funktionen lassen sich anspruchsvolle Automatisierungs- und Analyseaufgaben direkt in Paledo konfigurieren. Beachten Sie jedoch stets die Lesbarkeit und potenzielle Performance-Implikationen bei sehr komplexen Ausdrücken.

## Tutorial Level 2: Fortgeschrittene Datenmanipulation mit Linq-Style Custom Functions

### Einleitung

Willkommen zum Level 2 Tutorial für Paledo Custom Functions. Dieses Tutorial baut auf den Grundlagen der XAF Criteria Language und den Basis-Funktionen auf und taucht tief in die **Linq-Style Custom Functions** ein. Diese Funktionen ermöglichen **mächtige Operationen auf Datensammlungen (Listen)** direkt in Criteria-Ausdrücken, ähnlich den Möglichkeiten von LINQ in C#.

Dieses Tutorial richtet sich an **erfahrene Paledo-Anwender, Administratoren und Entwickler**, die komplexe Datenmanipulationen, Aggregationen und Transformationen innerhalb von Paledo Workflows (insbesondere State Actions), Berechneten Feldern oder Filtern umsetzen möchten.

Wir werden die wichtigsten Linq-Style Funktionen kurz wiederholen und uns dann auf **komplexe, verschachtelte Beispiele** konzentrieren, die zeigen, wie diese Funktionen kombiniert werden können, um anspruchsvolle Probleme zu lösen und neue Perspektiven für die Automatisierung zu eröffnen.

### Kurze Wiederholung: Die wichtigsten Linq-Style Funktionen

* **`Where(collection, predicate)`**: Filtert Elemente basierend auf einer Bedingung.
* **`Select(collection, selector)`**: Transformiert jedes Element in einen neuen Wert/Struktur.
* **`OrderBy(collection, predicate)` / `OrderByDescending(collection, predicate)`**: Sortiert Elemente aufsteigend/absteigend.
* **`ThenBy(collection, predicate)` / `ThenByDescending(collection, predicate)`**: Fügt eine sekundäre Sortierung hinzu (nur auf bereits sortierte Listen).
* **`GroupBy(collection, keySelector)`**: Gruppiert Elemente nach einem Schlüssel.
* **`Join(collection, separator)`**: Verbindet Elemente (oft Strings) zu einem einzigen String.
* **`Distinct(collection)`**: Entfernt Duplikate.
* **`Count(collection)`**: Zählt Elemente (kann auch auf gefilterte Listen angewendet werden: `collection[predicate].Count()`).
* **`Sum(collection, selector)` / `Max(collection, selector)` / `Min(collection, selector)` / `Avg(collection, selector)`**: Aggregationsfunktionen (oft auf das Ergebnis von `Select` oder direkt auf Collection-Properties angewendet).
* **`Take(collection, count)`**: Nimmt die ersten `count` Elemente.
* **`ForEach(collection, filterPredicate, evaluationExpression)`**: Führt eine Aktion für gefilterte Elemente aus (hauptsächlich für **Workflow Actions**).

***

### Fortgeschrittene Beispiele und Anwendungsfälle

Die wahre Stärke dieser Funktionen zeigt sich in ihrer Kombination.

#### Beispiel 1 (Praxis): KI-Wissensextraktion aus Auftragsdetails

* **Use Case/Szenario:** Automatische Erstellung eines Wissensdatenbankeintrags nach Auftragsabschluss, der Titel, Beschreibung und eine formatierte Liste aller relevanten Logbucheinträge und verwendeten Ersatzteile enthält, aufbereitet für eine KI-Zusammenfassung.
*   **Code Snippet (State Action für Auftrag `this`):**

    ```criteria
    // Erstelle Wissenseintrag, dessen Inhalt durch KI aus Auftragsdetails generiert wird
    CreateComponentKnowledge( // Funktion zum Erstellen des Wissenseintrags
        '#KI# '+PROCESSTEXTINPUT_AI('ExtractKnowledgeFromOrderTitle', Subject), // Titel: KI-generiert aus Betreff
        PROCESSTEXTINPUT_AI('ExtractKnowledgeFromOrderBody', // Beschreibung: KI-generiert aus...
            // ...einem zusammengesetzten Text:
            'Anlage: ' + FirstComponent.Caption + CHAR(13)+CHAR(10)+ // Anlage
            'Problem: ' + Subject + CHAR(13)+CHAR(10)+ // Betreff
            'Störbeginn: ' + FaultStart + CHAR(13)+CHAR(10)+ // Zeiten
            'Störende: ' + FaultEnd + CHAR(13)+CHAR(10)+
            'Maschinenstillstand: '+ Iif(HasDowntime=True, 'JA', 'NEIN') + CHAR(13)+CHAR(10)+CHAR(13)+CHAR(10)+
            'Logbuch:'+CHAR(13)+CHAR(10)+
            Join( // Verbinde formatierte Logeinträge mit Zeilenumbruch
                Select( // Wähle und formatiere jeden relevanten Logeintrag
                    Where(GetMemberValue(this, 'Confirmations'), // Filtere Rückmeldungen...
                          'ActualTime > 0 and not isnullorempty(Comment)'), // ...nach Zeit > 0 und vorhandenem Kommentar
                    // Formatiere den Eintrag:
                    'CreationDate + " / Arbeitszeit: " + ActualTime + "h / Bearbeiter: " + PaledoUser + " / Bemerkung: " + Comment'
                ),
                CHAR(13)+CHAR(10) // Trennzeichen für Join (Zeilenumbruch)
            ) + CHAR(13)+CHAR(10)+CHAR(13)+CHAR(10)+
            'Ersatzteile:'+CHAR(13)+CHAR(10)+
            Join( // Verbinde Ersatzteile mit Zeilenumbruch
                Select( // Wähle und formatiere jede nicht stornierte Reservierung
                    Where(GetMemberValue(this, 'MatReservations'), 'isnullorempty(StorniertAm)'),
                    // Formatiere die Teileinfo:
                    'Material.MaterialNo + " - " + Material.Description'
                ),
                CHAR(13)+CHAR(10) // Trennzeichen für Join
            )
        ),
        GetCatalogEntry('Wissenskategorie', 'AUTO_GENERATED'), // Kategorie zuweisen
        this // Kontextobjekt
    )
    ```
* **Detaillierte Breakdown:**
  1. `CreateComponentKnowledge`: Erstellt den finalen Eintrag.
  2. Titel wird mit `PROCESSTEXTINPUT_AI` aus `Subject` generiert.
  3. Beschreibung wird mit `PROCESSTEXTINPUT_AI` aus einem langen, dynamisch erzeugten String generiert.
  4. **String-Konstruktion:** Kombiniert statischen Text mit Werten aus dem Auftrag (`Subject`, `FaultStart`, `FaultEnd`, `HasDowntime` via `Iif`).
  5. **Logbuch-Extraktion:**
     * `GetMemberValue(this, 'Confirmations')`: Holt die Liste der Rückmeldungen.
     * `Where(...)`: Filtert nach Einträgen mit `ActualTime > 0` UND nicht-leerem `Comment`.
     * `Select(...)`: Formatiert jeden gefilterten Eintrag zu `Datum / Arbeitszeit: Xh / Bearbeiter: Y / Bemerkung: Z`.
     * `Join(...)`: Verbindet die formatierten Strings mit Zeilenumbrüchen.
  6. **Ersatzteil-Extraktion:**
     * `GetMemberValue(this, 'MatReservations')`: Holt die Materialreservierungen.
     * `Where(...)`: Filtert nach nicht stornierten Reservierungen.
     * `Select(...)`: Formatiert jede Reservierung zu `MaterialNr - Beschreibung`.
     * `Join(...)`: Verbindet die formatierten Strings mit Zeilenumbrüchen.
  7. `GetCatalogEntry`: Weist eine feste Kategorie zu.
* **Key Concepts Demonstrated:** Tiefe Verschachtelung, Datensammlung aus verknüpften Listen (`GetMemberValue`), Filtern (`Where`), Transformieren (`Select`), String-Konkatenation (`+`, `Join`), bedingte Ausgabe (`Iif`), Übergabe komplexer Daten an KI (`PROCESSTEXTINPUT_AI`), Objekterstellung (`CreateComponentKnowledge`).

***

#### Beispiel 2 (Praxis): Fortschrittsberechnung mit Gewichtung

* **Use Case/Szenario:** Berechne den Fortschritt eines Auftrags in Prozent, wobei abgenommene Unteraufgaben (Records) voll zählen und solche in Bearbeitung nur zu 25%.
*   **Code Snippet (State Action, setzt `Progress` am `Order`-Objekt):**

    ```criteria
    // Kontext: Ausgeführt, wenn sich ein Record ändert, wirkt auf den verknüpften Order
    IIF(not ISNULLOREMPTY([Order]), // Nur wenn ein Auftrag verknüpft ist
        SETMEMBERVALUE(Order, 'Progress', // Setze das Feld 'Progress' am Auftrag
            ToInt(Round( // Runde auf Ganzzahl
                ( // Teil 1: Gewichtete In-Arbeit-Records
                  ToDouble(Order.Records[WorkflowState.Name<>'Abgenommen' and WorkflowState.Name<>'Vorbereitet' ].Count()) // Zähle Records "in Arbeit" etc.
                  * (100.0 / Iif(Order.Records.Count() == 0, 1.0, ToDouble(Order.Records.Count())) ) // Berechne %-Anteil (vermeide Div/0)
                  * 0.25 // Wende 25% Gewichtung an
                )
                + // Addiere...
                ( // Teil 2: Abgenommene Records (100% Gewichtung)
                  ToDouble(Order.Records[WorkflowState.Name='Abgenommen'].Count()) // Zähle abgenommene Records
                  * (100.0 / Iif(Order.Records.Count() == 0, 1.0, ToDouble(Order.Records.Count()))) // Berechne %-Anteil (vermeide Div/0)
                )
            )) // Ende Round
        ) // Ende ToInt + SetMemberValue
    , '') // Tu nichts, wenn kein Auftrag verknüpft ist
    ```
* **Detaillierte Breakdown:**
  1. Greift auf die Collection `Records` des verknüpften `Order` zu.
  2. Verwendet Collection Filtering (`[...]`) und `Count()` zweimal:
     * Einmal für Records, die _nicht_ 'Abgenommen' und _nicht_ 'Vorbereitet' sind.
     * Einmal für Records, die 'Abgenommen' sind.
  3. Berechnet den prozentualen Anteil jeder Gruppe an der Gesamtzahl der Records (`Iif` zur Vermeidung von Division durch Null).
  4. Multipliziert den Anteil der "in Arbeit"-Gruppe mit `0.25`.
  5. Addiert die gewichteten Prozentwerte.
  6. Rundet (`Round`) das Ergebnis und konvertiert es in einen Integer (`ToInt`).
  7. Setzt das Ergebnis im Feld `Progress` des Auftrags mittels `SetMemberValue`.
* **Key Concepts Demonstrated:** Zugriff auf gefilterte Collections (`Collection[Filter].Count()`), Arithmetik, Typkonvertierung (`ToDouble`, `ToInt`), Runden (`Round`), bedingte Logik zur Fehlervermeidung (`Iif` gegen Division durch Null), Wertsetzung auf verknüpftem Objekt.

***

#### Beispiel 3 (Praxis): Aggregation von Kommentaren aus Rückmeldungen

* **Use Case/Szenario:** Sammle alle relevanten Kommentare aus den Arbeitsrückmeldungen (`Confirmations`) eines Auftrags, formatiere sie mit Zeitstempel und hänge sie an das bestehende `Result`-Feld des Auftrags an.
*   **Code Snippet (State Action für Auftrag `this`):**

    ```criteria
    // Kontext: State Action für Auftrag (this), ausgeführt z.B. beim Wechsel in einen Review-Status
    Iif(IsNullOrEmpty([TechnicalCompleteDate]), // Nur wenn noch nicht technisch abgeschlossen
        SetMemberValue(this, 'Result', // Setze das Feld 'Result'
            Trim( // Entferne Leerzeichen am Ende
                Iif( // Prüfe, ob neue (formatierte) Kommentare hinzugefügt werden
                    IsNullOrEmpty(
                        Join( // Verbinde die formatierten Strings
                            Select( // Projiziere jede gefilterte Rückmeldung...
                                OrderBy( // ...sortiert nach Datum...
                                    Where(GetMemberValue(this, 'Confirmations'), // ...aus der Liste der Rückmeldungen...
                                          '[Comment] != null And Trim([Comment]) != \'\''), // ...die einen nicht-leeren Kommentar haben...
                                'DateEnd' // ...sortiert nach Enddatum
                            ),
                            // ...auf einen formatierten String:
                            'Substring(ToString(DateEnd),0, 16) + " - " + Substring(Coalesce(OperationContext,"Unbekannt"), 0, 3) + " - " + Coalesce(WorkCenter.Code,"?") + CHAR(13)+CHAR(10) + Trim(Comment) + CHAR(13)+CHAR(10)+CHAR(13)+CHAR(10)'
                            ),
                        '') // Kein Trennzeichen für Join, da im Select enthalten
                    ),
                    [Result], // Wenn keine neuen Kommentare, behalte alten Wert
                    // Wenn neue Kommentare vorhanden:
                    Coalesce([Result] + CHAR(13) + CHAR(10) + '------------------------------------' + CHAR(13) + CHAR(10), '') // Füge Trenner hinzu (nur wenn Result schon was enthält)
                    + Join(Select(OrderBy(Where(GetMemberValue(this, 'Confirmations'), '[Comment] != null And Trim([Comment]) != \'\''), 'DateEnd'), 'Substring(ToString(DateEnd),0, 16) + " - " + Substring(Coalesce(OperationContext,"Unbekannt"), 0, 3) + " - " + Coalesce(WorkCenter.Code,"?") + CHAR(13)+CHAR(10) + Trim(Comment) + CHAR(13)+CHAR(10)+CHAR(13)+CHAR(10)'), '') // Füge neue Kommentare hinzu
                ) // Ende inneres Iif
            ) // Ende Trim
        ) // Ende SetMemberValue
    , '') // Tu nichts, wenn technisch abgeschlossen
    ```
* **Detaillierte Breakdown:**
  * Ähnlich wie Beispiel 4 im Level 1 Tutorial, aber mit zusätzlicher `Where`-Filterung und leicht angepasster Formatierung im `Select`.
  * `Where(...)`: Filtert `Confirmations` auf Einträge mit nicht-leeren Kommentaren (`[Comment] != null And Trim([Comment]) != ''`).
  * `OrderBy(..., 'DateEnd')`: Sortiert die gefilterten Einträge nach Datum.
  * `Select(...)`: Formatiert jeden Eintrag zu `Datum - KontextKürzel - WC-Code\nKommentar\n`. Verwendet `Coalesce` für optionale Werte wie `OperationContext` und `WorkCenter.Code`.
  * Der Rest der Logik (Prüfung auf neue Kommentare, Anhängen an bestehenden `Result`-Wert mit Trennlinie) ist identisch zum früheren Beispiel.
* **Key Concepts Demonstrated:** Kombination von `Where`, `OrderBy`, `Select`, `Join`. String-Manipulation mit `Substring`, `ToString`, `Trim`, `Coalesce`. Verwendung von `CHAR(13)+CHAR(10)` für Zeilenumbrüche. Komplexes bedingtes Anhängen an bestehenden Text.

***

#### Beispiel 4 (Neu erfunden): Finde Aufträge, deren _alle_ Positionen einen bestimmten Status haben

* **Use Case/Szenario:** Filtere Aufträge heraus, bei denen _jede einzelne_ Auftragsposition (Operations) den Status 'Abgeschlossen' oder 'Storniert' hat. Dies simuliert eine "Alle Elemente erfüllen Bedingung"-Logik.
*   **Code Snippet (Filterkriterium für Auftragsliste):**

    ```criteria
    // Finde Aufträge, bei denen KEINE Position einen anderen Status als 'Abgeschlossen' oder 'Storniert' hat
    [Operations][[Status] <> 'Abgeschlossen' And [Status] <> 'Storniert'].Count() = 0
    And
    [Operations].Count() > 0 // Stelle sicher, dass der Auftrag überhaupt Positionen hat
    ```
* **Detaillierte Breakdown:**
  1. `[Operations]`: Greift auf die Liste der Auftragspositionen zu.
  2. `[[Status] <> 'Abgeschlossen' And [Status] <> 'Storniert']`: Filtert die Liste und behält nur die Positionen, die _nicht_ abgeschlossen und _nicht_ storniert sind (also z.B. 'Offen', 'In Arbeit').
  3. `.Count()`: Zählt die Anzahl dieser "nicht fertigen" Positionen.
  4. `= 0`: Die Bedingung ist wahr, wenn die Anzahl der "nicht fertigen" Positionen Null ist. Das bedeutet im Umkehrschluss, dass alle Positionen entweder 'Abgeschlossen' oder 'Storniert' sind.
  5. `And [Operations].Count() > 0`: Eine zusätzliche Sicherung, um Aufträge ohne Positionen auszuschließen (optional, je nach Anforderung).
* **Key Concepts Demonstrated:** Simulation einer "Alle erfüllen"-Bedingung durch Umkehrung ("Keine erfüllt nicht"). Filtern von Collections (`[...]`), Zählen (`Count()`), Kombinieren von Bedingungen (`And`).

***

#### Beispiel 5 (Neu erfunden): Gruppiere nach Monat und finde Top-Material pro Monat

* **Use Case/Szenario:** Erstelle eine monatliche Zusammenfassung des Materialverbrauchs, die für jeden Monat das Material mit dem höchsten Verbrauchswert (Menge \* Preis) anzeigt.
*   **Code Snippet (Berechnetes Feld oder Report-Datenquelle):**

    ```criteria
    // Gruppiere Materialbewegungen (Abgänge) nach Jahr und Monat, finde Top-Material pro Gruppe
    Select( // Projiziere jede Monatsgruppe auf einen Ergebnisstring
      GroupBy( // Gruppiere...
        Where([MaterialMovementHistory], "[MovementType] = 'Verbrauch' And [Quantity] < 0"), // ...alle Verbräuche...
        "GetYear(MovementDate) + '-' + FormatNumber(GetMonth(MovementDate), 'D2')" // ...nach Jahr-Monat (z.B. '2023-04')
      ),
      // Für jede Gruppe (Key = 'Jahr-Monat', Items = Bewegungen des Monats):
      "Key + ': Top Material: ' + " +
      "Iif(Items.Count() > 0, " + // Nur wenn Bewegungen im Monat vorhanden sind
          // Finde das Material mit dem höchsten Verbrauchswert in diesem Monat:
          "OrderByDescending(Items, 'Abs(Quantity) * Material.UnitPrice').First().Material.MaterialNo + " + // Hole MaterialNr vom ersten Element nach Sortierung
          "' (' + ToString(OrderByDescending(Items, 'Abs(Quantity) * Material.UnitPrice').First().Abs(Quantity) * Material.UnitPrice, '#,##0.00 €') + ')', " + // Hole den berechneten Wert
          "'N/A'" // Text, wenn keine Bewegungen im Monat
      + ")"
    ) // Ende Select
    ```
* **Detaillierte Breakdown:**
  1. `Where(...)`: Filtert die `MaterialMovementHistory` auf Verbräuche (`Quantity < 0`).
  2. `GroupBy(..., "GetYear(...) + '-' + FormatNumber(GetMonth(...), 'D2')")`: Gruppiert die gefilterten Bewegungen nach Jahr und Monat (formatiert als 'JJJJ-MM'). `FormatNumber` ist hier hypothetisch für führende Nullen, alternativ `ToString`.
  3. `Select(..., "Key + ': Top Material: ' + Iif(...)")`: Iteriert über jede Monatsgruppe. `Key` ist der 'JJJJ-MM'-String.
  4. **Innerhalb des `Select` für jede Gruppe:**
     * `Iif(Items.Count() > 0, ..., 'N/A')`: Prüft, ob die Gruppe (der Monat) Bewegungen enthält.
     * **Wenn Ja:**
       * `OrderByDescending(Items, 'Abs(Quantity) * Material.UnitPrice')`: Sortiert die Bewegungen _innerhalb des Monats_ absteigend nach dem Verbrauchswert (Menge \* Preis). `Abs` für negative Mengen.
       * `.First()`: Nimmt die erste Bewegung nach der Sortierung (also die mit dem höchsten Wert).
       * `.Material.MaterialNo`: Greift auf die Materialnummer des Top-Materials zu.
       * Der zweite Teil holt den berechneten Wert desselben Top-Materials und formatiert ihn als Währung.
     * **Wenn Nein:** Gibt 'N/A' zurück.
  5. Der formatierte String pro Monat wird zurückgegeben (die äußere `Select`-Funktion sammelt diese Strings).
* **Key Concepts Demonstrated:** Gruppierung nach berechnetem Schlüssel (`GroupBy`), verschachtelte Operationen auf den Gruppen (`Items`), Sortierung innerhalb von Gruppen (`OrderByDescending`), Zugriff auf das erste Element (`.First()` - _Achtung: `.First()` ist möglicherweise keine Standard-Criteria-Language-Funktion, evtl. `Take(..., 1)` verwenden oder Annahme, dass es unterstützt wird_), komplexe String-Formatierung, bedingte Logik innerhalb der Projektion.

***

#### Beispiel 6 (Praxis): `ForEach` zum Setzen von Werten und Auslösen von Statuswechseln

* **Use Case/Szenario:** Wenn ein Hauptauftrag abgeschlossen wird, sollen alle zugehörigen Fehlermeldungen ebenfalls abgeschlossen und zusätzlich mit dem Ergebnis des Hauptauftrags kommentiert werden.
*   **Code Snippet (State Action für Auftrag `this`):**

    ```criteria
    // Kontext: State Action, wenn Auftrag (this) in Status 'Abgeschlossen' wechselt
    Iif([Status] == 'Abgeschlossen',
        ForEach(GetMemberValue([This], 'FaultNotifications'), // Iteriere über Fehlermeldungen
                '1=1', // Bedingung: Alle Meldungen
                // Aktion für jede Meldung (This bezieht sich hier auf die Meldung):
                'BulkExecute( // Führe mehrere Aktionen für die Meldung aus
                    SetMemberValue(This, ''Comment'', Coalesce(GetMemberValue(Order, ''Result''), "Kein Ergebnis verfügbar.")), // Setze Kommentar
                    ChangeWorkflowState(This, ''Erledigt'') // Ändere Status
                 )' // Ende BulkExecute String
        ), // Ende ForEach Aktion
    null) // Tu nichts, wenn Status nicht 'Abgeschlossen'
    ```
* **Detaillierte Breakdown:**
  1. `Iif`: Führt die Aktion nur aus, wenn der Hauptauftrag abgeschlossen wird.
  2. `ForEach`: Iteriert über die `FaultNotifications`-Collection des Hauptauftrags (`GetMemberValue([This], ...)`).
  3. `filterPredicate`: `'1=1'` - Wendet die Aktion auf alle Fehlermeldungen an.
  4. `evaluationExpression`: `'BulkExecute(...)'` - Führt für jede Meldung einen Block von Aktionen aus.
  5. **Innerhalb `BulkExecute`:** (`This` bezieht sich jetzt auf die Fehlermeldung!)
     * `SetMemberValue(This, ''Comment'', Coalesce(GetMemberValue(Order, ''Result''), ...))`: Setzt das `Comment`-Feld der Fehlermeldung. Es nimmt das `Result`-Feld des _Hauptauftrags_ (hier als `Order` referenziert, Annahme: die Meldung hat eine Referenz `Order` zum Hauptauftrag) oder einen Standardtext, falls das Ergebnis leer ist.
     * `ChangeWorkflowState(This, ''Erledigt'')`: Ändert den Status der Fehlermeldung auf 'Erledigt'.
* **Key Concepts Demonstrated:** `ForEach` zur Massenaktion, `GetMemberValue` zum Holen der Liste, `BulkExecute` zur Bündelung von Aktionen pro Listenelement, `SetMemberValue` und `ChangeWorkflowState` innerhalb der Iteration, Zugriff auf das übergeordnete Objekt (`Order`) aus dem Kontext des Listenelements (`This`), `Coalesce` für Standardwerte.

***

#### Beispiel 7 (Neu erfunden): Bedingte Aggregation basierend auf Benutzerrollen

* **Use Case/Szenario:** Berechne die Summe der Auftragswerte, aber schließe Aufträge mit Status 'Intern' aus, es sei denn, der aktuelle Benutzer hat die Rolle 'Controller'.
*   **Code Snippet (Berechnetes Feld):**

    ```criteria
    // Summiere Auftragswerte, filtere 'Intern' außer für Controller
    Sum( // Aggregiere...
      Select( // ...die Werte von...
        Where( // ...gefilterten Aufträgen...
          [AuftragsListe],
          // Filterbedingung:
          "[Status] <> 'Intern' Or HasRole('Controller')" // Behalte Auftrag, wenn Status nicht 'Intern' ODER Benutzer ist Controller
        ),
        'Value' // ...aus dem Feld 'Value'
      )
    )
    ```
* **Detaillierte Breakdown:**
  1. `Where(...)`: Filtert die `AuftragsListe`.
  2. **Filterbedingung:** `"[Status] <> 'Intern' Or HasRole('Controller')"`
     * Lässt alle Aufträge durch, deren Status _nicht_ 'Intern' ist.
     * Lässt _zusätzlich_ alle Aufträge durch (unabhängig vom Status), wenn der aktuelle Benutzer die Rolle 'Controller' hat (`HasRole('Controller')`).
  3. `Select(..., 'Value')`: Wählt von den gefilterten Aufträgen nur das Feld `Value` aus.
  4. `Sum(...)`: Summiert die ausgewählten Werte.
* **Key Concepts Demonstrated:** Bedingte Filterung (`Where` mit `Or`), Einbeziehung von Benutzerberechtigungen (`HasRole`) in die Filterlogik, Aggregation (`Sum`) auf dem gefilterten Ergebnis.

***

#### Beispiel 8 (Neu erfunden): Finde doppelte Einträge basierend auf mehreren Feldern

* **Use Case/Szenario:** Identifiziere Materialien in einer Importliste, die Duplikate basierend auf der Kombination von `MaterialNo` und `Lieferant` sind.
*   **Code Snippet (Berechnetes Feld oder Validierung, gibt Liste der doppelten Nummern zurück):**

    ```criteria
    // Finde Materialnummern, die mehr als einmal vorkommen (Duplikate)
    Select( // Wähle nur den Schlüssel (MaterialNo) der Gruppen aus...
      Where( // ...die Gruppen...
        GroupBy( // ...gruppiert nach Materialnummer...
          [ImportMaterialListe],
          "MaterialNo"
        ),
        "Count() > 1" // ...die mehr als ein Element enthalten (also Duplikate sind)
      ),
      "Key" // Wähle den Gruppenschlüssel (die MaterialNo) aus
    )
    ```

    _Anmerkung: Dies prüft nur auf doppelte `MaterialNo`. Eine Prüfung auf Kombination `MaterialNo` + `Lieferant` wäre komplexer:_

    ```criteria
    // Finde Kombinationen aus MaterialNo+Lieferant, die mehr als einmal vorkommen
    Select(
      Where(
        GroupBy(
          [ImportMaterialListe],
          "MaterialNo + '|' + Lieferant.Name" // Gruppiere nach kombinierterm Schlüssel
        ),
        "Count() > 1" // Finde Gruppen mit mehr als einem Element
      ),
      "Key" // Gib die doppelten Schlüssel zurück (z.B. "M123|Lieferant A")
    )
    ```
* **Detaillierte Breakdown (Kombinierter Schlüssel):**
  1. `GroupBy(..., "MaterialNo + '|' + Lieferant.Name")`: Gruppiert die Importliste. Als Schlüssel für die Gruppe wird eine Kombination aus Materialnummer und Lieferantenname (getrennt durch '|' zur Eindeutigkeit) verwendet.
  2. `Where(..., "Count() > 1")`: Filtert die Gruppen und behält nur diejenigen, die mehr als ein Element enthalten. Das sind die Duplikate bezüglich der Schlüsselkombination.
  3. `Select(..., "Key")`: Wählt von diesen Duplikat-Gruppen nur den zusammengesetzten Schlüssel aus. Das Ergebnis ist eine Liste der doppelt vorkommenden Kombinationen.
* **Key Concepts Demonstrated:** Gruppierung (`GroupBy`) nach einem _berechneten_ oder _kombinierten_ Schlüssel, Zählen innerhalb von Gruppen (`Count()`), Filtern von Gruppen (`Where` nach `GroupBy`), Extrahieren des Gruppenschlüssels (`Key`).

***

#### Beispiel 9 (Neu erfunden): Setze Status basierend auf verknüpften Checklisten-Ergebnissen

* **Use Case/Szenario:** Ändere den Status eines Prüfauftrags auf 'Abgeschlossen mit Mängeln', wenn mindestens eine zugehörige Checkliste den Status 'Nicht OK' hat, ansonsten auf 'Abgeschlossen OK'.
*   **Code Snippet (State Action für Prüfauftrag `this`):**

    ```criteria
    // Kontext: State Action, wenn z.B. die letzte Checkliste abgeschlossen wurde
    Iif( // Entscheide den Zielstatus
        [Checklisten][[Status] = 'Nicht OK'].Count() > 0, // Prüfe, ob es Checklisten mit Status 'Nicht OK' gibt
        ChangeWorkflowState(this, 'Abgeschlossen mit Mängeln'), // Wenn ja, setze Status "mit Mängeln"
        ChangeWorkflowState(this, 'Abgeschlossen OK') // Wenn nein, setze Status "OK"
    )
    ```
* **Detaillierte Breakdown:**
  1. `Iif(...)`: Trifft die Entscheidung zwischen zwei verschiedenen Statuswechseln.
  2. **Bedingung:** `[Checklisten][[Status] = 'Nicht OK'].Count() > 0`
     * Greift auf die `Checklisten`-Collection des aktuellen Prüfauftrags zu.
     * Filtert (`[...]`) nach Checklisten mit dem Status 'Nicht OK'.
     * Zählt (`Count()`) diese gefilterten Checklisten.
     * Prüft, ob die Anzahl größer als 0 ist.
  3. **Aktion\_Wahr:** `ChangeWorkflowState(this, 'Abgeschlossen mit Mängeln')` - Wird ausgeführt, wenn mindestens eine Checkliste 'Nicht OK' ist.
  4. **Aktion\_Falsch:** `ChangeWorkflowState(this, 'Abgeschlossen OK')` - Wird ausgeführt, wenn keine Checkliste 'Nicht OK' ist.
* **Key Concepts Demonstrated:** Bedingte Statusänderung (`Iif` mit `ChangeWorkflowState`), Zählen von gefilterten Elementen in einer verknüpften Liste zur Entscheidungsfindung.

***

#### Beispiel 10 (Neu erfunden): Finde Benutzer ohne zugewiesene Aufgaben in einer bestimmten Kategorie

* **Use Case/Szenario:** Erstelle eine Liste von Benutzernamen der Abteilung 'Support', die aktuell _keine_ offenen Aufgaben in der Kategorie 'Tickets' haben.
*   **Code Snippet (Berechnetes Feld oder Report-Datenquelle):**

    ```criteria
    // Finde Support-Benutzer ohne offene Ticket-Aufgaben
    Select( // Wähle die Namen der gefilterten Benutzer
      Where( // Filtere die Benutzer...
        [<BOPaledoUser>][Department.Name = 'Support' And IsActive = true], // ...aus der Abteilung Support (Aktive)...
        // Filterbedingung für jeden Benutzer (This):
        "[Aufgaben][[Status] <> 'Abgeschlossen' And [Category.Name] = 'Tickets'].Count() = 0" // Zähle offene Ticket-Aufgaben und prüfe ob = 0
      ),
      "FullName" // Wähle den vollen Namen aus
    )
    ```
* **Detaillierte Breakdown:**
  1. `[<BOPaledoUser>][Department.Name = 'Support' And IsActive = true]`: Selektiert initial alle aktiven Benutzer der Abteilung 'Support'.
  2. `Where(..., "[Aufgaben][[...]].Count() = 0")`: Filtert diese Benutzerliste weiter. Die Bedingung wird für jeden Benutzer (`This`) ausgewertet:
     * `[Aufgaben]`: Greift auf die Liste der Aufgaben des _aktuellen Benutzers_ (`This`) zu.
     * `[[Status] <> 'Abgeschlossen' And [Category.Name] = 'Tickets']`: Filtert diese Aufgabenliste nach offenen ('nicht Abgeschlossen') Aufgaben der Kategorie 'Tickets'.
     * `.Count()`: Zählt die Anzahl dieser spezifischen Aufgaben für den Benutzer.
     * `= 0`: Die Bedingung ist wahr für Benutzer, die _keine_ solchen Aufgaben haben.
  3. `Select(..., "FullName")`: Wählt von den Benutzern, die die `Where`-Bedingung erfüllen (also keine offenen Ticket-Aufgaben haben), den `FullName` aus.
* **Key Concepts Demonstrated:** Filtern einer Hauptliste (`Where`), verschachteltes Filtern und Zählen auf einer Collection-Property des iterierten Objekts (`[Aufgaben][[...]].Count()`), Kombination von Bedingungen, Projektion auf das gewünschte Ergebnis (`Select`).

***

### Fazit Level 2

Die Linq-Style Custom Functions entfalten ihr volles Potenzial, wenn sie kreativ kombiniert und verschachtelt werden. Sie ermöglichen die Formulierung komplexer Datenabfragen, Transformationen und bedingter Logiken direkt in der XAF Criteria Language, was zu mächtigen Automatisierungen führt, insbesondere in Workflow State Actions.

* **Lesbarkeit beachten:** Bei sehr hoher Komplexität steigt die Notwendigkeit guter Kommentierung oder eventuell der Kapselung in eigene, wiederverwendbare Custom Functions.
* **Kontext verstehen:** Wissen, auf welches Objekt (`this`, `Order`, etc.) sich die Eigenschaften und Listen beziehen, ist entscheidend.
* **Performance im Auge behalten:** Komplexe Operationen auf großen Listen sollten idealerweise serverseitig und effizient ausgeführt werden.

Nutzen Sie diese fortgeschrittenen Techniken, um Ihre Paledo-Prozesse weiter zu optimieren und anzupassen!
