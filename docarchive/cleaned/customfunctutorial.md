# Tutorial Level 1

## Einleitung

Dieses Tutorial zeigt praxisnahe Beispiele für den Einsatz von Paledo Custom Functions innerhalb der **Workflow Engine**. Speziell fokussieren wir uns auf **State Actions** – Aktionen, die automatisch ausgeführt werden, wenn ein Business Objekt einen bestimmten Zustand in einem Workflow erreicht.

Diese Beispiele sollen erfahrenen Paledo-Anwendern ("Power Usern") und Administratoren helfen zu verstehen, wie sie mit Hilfe der **XAF Criteria Language** und den Paledo Custom Functions typische Automatisierungsaufgaben umsetzen können. Grundlegende Kenntnisse der XAF Criteria Language werden vorausgesetzt.

## Kernkonzept: State Actions

In der Paledo Workflow Engine können Sie für jeden Workflow-Status Aktionen definieren. Jede Aktion wird als ein **einziger Ausdruck** in der XAF Criteria Language formuliert. Wenn ein Objekt in den entsprechenden Status wechselt, wird dieser Ausdruck ausgewertet.

* **Ausdrucksauswertung:** Das Ergebnis des Ausdrucks selbst wird oft nicht direkt verwendet. Der Hauptzweck liegt in den **Seiteneffekten** der aufgerufenen Funktionen.
* **Aktionsfunktionen:** Funktionen wie `SetMemberValue`, `ChangeWorkflowState`, `CreateObject`, `AddUserCommentLogEntry` etc. führen Aktionen aus (Daten ändern, Objekte erstellen, Status wechseln).
* **Bedingte Logik:** Um Aktionen nur unter bestimmten Bedingungen auszuführen, wird die `Iif(Bedingung, Ausdruck_wenn_wahr, Ausdruck_wenn_falsch)` Funktion verwendet. Wenn eine Aktion nur bei `true` ausgeführt werden soll, kann der `Ausdruck_wenn_falsch` oft `null` oder `''` (leerer String) sein.
* **Kontext:** Innerhalb des Ausdrucks bezieht sich `this` (oder `CurrentObject()`) auf das Business Objekt, dessen Workflow-Status gerade geändert wurde. Eigenschaften des Objekts können direkt über `[PropertyName]` angesprochen werden.

***

## Beispiel-Sammlung

Wir haben ca. 20 Beispiele aus realen Paledo-Konfigurationen ausgewählt, um verschiedene Anwendungsfälle zu illustrieren.

### 1. Einfache Feldzuweisungen

Diese Beispiele zeigen, wie Felder des aktuellen Objekts (`this`) beim Erreichen eines Workflow-Status automatisch gesetzt werden können.

***

**Beispiel 1: Genehmigenden Benutzer setzen**

*   **Code Snippet:**

    ```criteria
    SetMemberValue(this,'ApprovedBy',CurrentUser())
    ```
* **Beschreibung:** Setzt das Feld `ApprovedBy` des aktuellen Objekts auf den aktuell angemeldeten Paledo-Benutzer.
* **Kontext/Anwendung:** Nützlich in einem Genehmigungsstatus, um automatisch zu protokollieren, wer die Genehmigung im System ausgelöst hat.
* **Verwendete Funktionen:** `SetMemberValue`, `CurrentUser`

***

**Beispiel 2: Statusfeld (Enum) setzen**

*   **Code Snippet:**

    ```criteria
    SetMemberValue(this,'PaledoStatus',##Enum#SynX.Xaf.Paledo.SAP.BusinessObjects.NotificationStatus,ToBeReleased#)
    ```
* **Beschreibung:** Setzt das Feld `PaledoStatus` auf einen spezifischen Wert (`ToBeReleased`) eines Enumeration-Typs (hier ein SAP-spezifischer Status).
* **Kontext/Anwendung:** Setzen eines festen Statuswertes, wenn ein bestimmter Workflow-Schritt erreicht wird (z.B. "Zur Freigabe vorbereitet").
* **Verwendete Funktionen:** `SetMemberValue`
* **Hinweis:** Die `##Enum#...#` Syntax ist spezifisch für die Referenzierung von Enum-Werten in der Criteria Language.

***

**Beispiel 3: Datum/Zeit auf aktuellen Zeitpunkt setzen**

*   **Code Snippet:**

    ```criteria
    SetMemberValue(this,'Arbeitsbeginn',LocalDateTimeNow())
    ```
* **Beschreibung:** Setzt das Feld `Arbeitsbeginn` auf den aktuellen Datum- und Zeitstempel des Servers.
* **Kontext/Anwendung:** Protokollieren des Zeitpunkts, wann eine Bearbeitung beginnt (z.B. beim Übergang in den Status "In Arbeit").
* **Verwendete Funktionen:** `SetMemberValue`, `LocalDateTimeNow`

***

**Beispiel 4: Boolesches Feld setzen (Flag)**

*   **Code Snippet:**

    ```criteria
    SetMemberValue(this,'Approved',True)
    ```
* **Beschreibung:** Setzt das boolesche Feld `Approved` auf den Wert `True`.
* **Kontext/Anwendung:** Markieren eines Objekts als genehmigt beim Eintritt in den "Genehmigt"-Status.
* **Verwendete Funktionen:** `SetMemberValue`

***

**Beispiel 5: Aktuelle Schicht zuweisen**

*   **Code Snippet:**

    ```criteria
    SetMemberValue(this,'Shift',GetCurrentShift())
    ```
* **Beschreibung:** Weist dem Feld `Shift` die aktuell aktive Schicht zu (basierend auf Systemzeit und Schichtplan).
* **Kontext/Anwendung:** Automatische Zuordnung eines Vorgangs zur Schicht, in der er z.B. gestartet oder abgeschlossen wurde.
* **Verwendete Funktionen:** `SetMemberValue`, `GetCurrentShift`

***

### 2. Bedingte Logik mit `Iif`

Die `Iif`-Funktion ist zentral für bedingte Aktionen in State Actions. `Iif(Bedingung, Aktion_Wenn_Wahr, Aktion_Wenn_Falsch)`.

***

**Beispiel 6: Enddatum nur setzen, wenn leer**

*   **Code Snippet:**

    ```criteria
    Iif(IsNullOrEmpty([Arbeitsende]), SetMemberValue(this, 'Arbeitsende', Now()), '')
    ```
* **Beschreibung:** Prüft, ob das Feld `Arbeitsende` leer ist (`IsNullOrEmpty`). Wenn ja, wird es auf das aktuelle Datum/Uhrzeit (`Now()`) gesetzt. Wenn nicht, wird keine Aktion ausgeführt (leerer String `''` als Ergebnis für den False-Zweig).
* **Kontext/Anwendung:** Setzen eines Abschlussdatums beim ersten Erreichen des "Abgeschlossen"-Status, aber nicht bei erneutem Eintritt (falls möglich).
* **Verwendete Funktionen:** `Iif`, `IsNullOrEmpty`, `SetMemberValue`, `Now`

***

**Beispiel 7: Standard-Verantwortlichen setzen, falls leer**

*   **Code Snippet:**

    ```criteria
    Iif(IsNullOrEmpty([ResponsiblePerson]), SetMemberValue(this, 'ResponsiblePerson', [][[Oid] = CurrentUserId()].Min(this)), '')
    ```
* **Beschreibung:** Prüft, ob das Feld `ResponsiblePerson` leer ist. Wenn ja, wird der aktuell angemeldete Benutzer als Verantwortlicher gesetzt. Der Benutzer wird über seine ID (`CurrentUserId()`) gesucht (`[][[Oid] = ...].Min(this)` ist eine Lookup-Syntax).
* **Kontext/Anwendung:** Sicherstellen, dass ein Objekt immer einen Verantwortlichen hat, wenn es einen bestimmten Status erreicht, falls nicht schon manuell zugewiesen.
* **Verwendete Funktionen:** `Iif`, `IsNullOrEmpty`, `SetMemberValue`, `CurrentUserId`

***

**Beispiel 8: Bedingter Workflow-Statuswechsel**

*   **Code Snippet:**

    ```criteria
    Iif([Event.EventTask.Name] = 'Wartungsrundgang' And [Order] <> null, ChangeWorkflowState([Order], 'Abgeschlossen'), null)
    ```
* **Beschreibung:** Prüft, ob der Name der verknüpften Aufgabe (`Event.EventTask.Name`) 'Wartungsrundgang' ist UND ob ein verknüpfter Auftrag (`Order`) existiert. Wenn beides zutrifft, wird der Workflow-Status des Auftrags (`[Order]`) auf 'Abgeschlossen' gesetzt. Andernfalls passiert nichts (`null`).
* **Kontext/Anwendung:** Automatisches Abschließen eines Auftrags, wenn ein spezifischer, zugehöriger Dokumentationseintrag (Record) abgeschlossen wird.
* **Verwendete Funktionen:** `Iif`, `ChangeWorkflowState`

***

### 3. Workflow- und Objektstatus ändern

***

**Beispiel 9: Einfacher Workflow-Statuswechsel**

*   **Code Snippet:**

    ```criteria
    ChangeWorkflowState([Order], 'In SAP abschließen')
    ```
* **Beschreibung:** Ändert den Workflow-Status des Objekts, das im Feld `Order` referenziert ist, in den Zustand 'In SAP abschließen'.
* **Kontext/Anwendung:** Auslösen des nächsten Schritts in einem abhängigen Prozess, wenn das aktuelle Objekt einen bestimmten Status erreicht.
* **Verwendete Funktionen:** `ChangeWorkflowState`

***

**Beispiel 10: Mehrere Statuswechsel (mit Concat - ungewöhnlich, aber möglich)**

*   **Code Snippet:**

    ```criteria
    Concat(ChangeWorkflowState(GetMemberValue(this, 'FaultNotifications'), 'Erledigt'), ChangeWorkflowState(GetMemberValue(this, 'RequirementNotifications'), 'Erledigt'))
    ```
* **Beschreibung:** Ändert den Workflow-Status für _zwei verschiedene_ Sammlungen von Benachrichtigungen (`FaultNotifications` und `RequirementNotifications`, auf die über `GetMemberValue` zugegriffen wird) auf 'Erledigt'. `Concat` wird hier technisch verwendet, um beide Aktionen im selben Ausdruck auszuführen, auch wenn der zurückgegebene String (Ergebnis von `Concat`) wahrscheinlich irrelevant ist.
* **Kontext/Anwendung:** Abschließen oder Stornieren mehrerer verknüpfter Objekte gleichzeitig beim Abschluss des Hauptobjekts.
* **Verwendete Funktionen:** `Concat`, `ChangeWorkflowState`, `GetMemberValue`
* **Hinweis:** Für solche Fälle ist oft `BulkExecute` oder `ForEach` (siehe unten) eine sauberere Alternative, falls mehrere Objekte betroffen sind.

***

**Beispiel 11: Bedingter Statuswechsel basierend auf Enum-Wert**

*   **Code Snippet:**

    ```criteria
    IIF([DVSDocumentRevision].[CheckResult] = ##Enum#SynX.Xaf.Paledo.Core.BusinessObjects.DVS.DocumentRevisionCheckResult,Verified#,
    ChangeWorkflowState([DVSDocumentRevision], 'Freigegeben'), '')
    ```
* **Beschreibung:** Prüft, ob das Feld `CheckResult` im Objekt `DVSDocumentRevision` den Enum-Wert `Verified` hat. Wenn ja, wird der Workflow-Status desselben Objekts auf 'Freigegeben' geändert.
* **Kontext/Anwendung:** Automatischer Übergang in den Freigabe-Status nach erfolgreicher Prüfung.
* **Verwendete Funktionen:** `Iif`, `ChangeWorkflowState`

***

### 4. Integration von KI-Funktionen

***

**Beispiel 12: Automatische Titelgenerierung**

*   **Code Snippet:**

    ```criteria
    SetMemberValue(this,'Subject',PROCESSTEXTINPUT_AI('Titelberechnung',[Description]))
    ```
* **Beschreibung:** Verwendet die KI-Funktion `PROCESSTEXTINPUT_AI` mit dem Prompt 'Titelberechnung', um aus dem Inhalt des Feldes `Description` einen passenden Titel zu generieren und diesen im Feld `Subject` zu speichern.
* **Kontext/Anwendung:** Automatisches Erstellen von prägnanten Titeln für Meldungen oder Aufträge basierend auf längeren Beschreibungen.
* **Verwendete Funktionen:** `SetMemberValue`, `PROCESSTEXTINPUT_AI`

***

**Beispiel 13: Automatische Kategorisierung per KI**

*   **Code Snippet:**

    ```criteria
    SetMemberValue(this,'FaultCategory',GetObject(DERIVECATALOGENTRYFROMLONGTEXT_AI('Störmeldungsprompt', 'SYS_FAULT_CATEGORY',Subject+' '+Description),this))
    ```
* **Beschreibung:** Kombiniert `Subject` und `Description`, übergibt dies an `DERIVECATALOGENTRYFROMLONGTEXT_AI` mit einem Prompt und dem Katalognamen `SYS_FAULT_CATEGORY`. Die Funktion versucht, den passenden Katalogeintrag zu finden. `GetObject` stellt sicher, dass das Ergebnis als Objekt behandelt wird (obwohl `DERIVECATALOGENTRYFROMLONGTEXT_AI` bereits ein Objekt zurückgibt, dient es hier ggf. zur Typsicherheit oder Kontextübergabe). Das Ergebnis wird im Feld `FaultCategory` gespeichert.
* **Kontext/Anwendung:** Intelligente, automatische Zuordnung einer Störung zu einer Fehlerkategorie basierend auf dem eingegebenen Text.
* **Verwendete Funktionen:** `SetMemberValue`, `GetObject`, `DERIVECATALOGENTRYFROMLONGTEXT_AI`

***

**Beispiel 14: Wissenseintrag aus Auftragsdaten erstellen**

*   **Code Snippet:**

    ```criteria
    CreateComponentKnowledge('#KI# '+PROCESSTEXTINPUT_AI('ExtractKnowledgeFromOrder',
    'Auftragstitel: '+Subject+'
    Beschreibung: '+Description+'
    Logbuch:
    '+
    Join(Select(GetMemberValue(this,'LogEntries'),'CreationDate+'' ''+PaledoUser+'': ''+Text') ,'
    ')
    ),' ',' ',this)
    ```
* **Beschreibung:** Erstellt einen neuen Wissensdatenbankeintrag (`CreateComponentKnowledge`). Der Inhalt wird durch `PROCESSTEXTINPUT_AI` generiert. Als Input für die KI dient ein zusammengesetzter Text aus Titel (`Subject`), Beschreibung (`Description`) und allen Logbucheinträgen (`LogEntries`) des aktuellen Auftrags. Die Logeinträge werden formatiert (`Select(...)`) und mit Zeilenumbrüchen verbunden (`Join(...)`). Der Titel des Wissenseintrags beginnt mit '#KI# '.
* **Kontext/Anwendung:** Automatisches Extrahieren und Speichern relevanter Informationen aus abgeschlossenen Aufträgen in einer Wissensdatenbank zur späteren Wiederverwendung.
* **Verwendete Funktionen:** `CreateComponentKnowledge`, `PROCESSTEXTINPUT_AI`, `Join`, `Select`, `GetMemberValue`

***

### 5. Arbeiten mit Listen und Schleifen (`ForEach`)

`ForEach` erlaubt die Ausführung einer Aktion für jedes Element einer Liste (Collection), das einer Bedingung entspricht.

***

**Beispiel 15: Veraltete Dokumentenrevisionen markieren**

*   **Code Snippet:**

    ```criteria
    ForEach([OldDVSDocumentRevisions], '[RevisionNo] < ' + [RevisionNo], 'SetMemberValue(This, ''Obsolete'', True)')
    ```
* **Beschreibung:** Geht durch alle Elemente in der Collection `OldDVSDocumentRevisions`. Für jedes Element (`This` im inneren Ausdruck), bei dem die `RevisionNo` kleiner ist als die `RevisionNo` des _aktuellen_ Objekts (das den Workflow auslöst), wird das Feld `Obsolete` auf `True` gesetzt.
* **Kontext/Anwendung:** Beim Freigeben einer neuen Dokumentenrevision automatisch alle älteren Revisionen als veraltet kennzeichnen.
* **Verwendete Funktionen:** `ForEach`, `SetMemberValue`

***

**Beispiel 16: Workflow-Status für alle Listenelemente ändern**

*   **Code Snippet:**

    ```criteria
    ForEach(GetMemberValue([this], 'FaultNotifications'), '1=1', 'ChangeWorkflowState(this,''Erledigt'')')
    ```
* **Beschreibung:** Geht durch alle Elemente in der Collection `FaultNotifications` (Bedingung `1=1` ist immer wahr). Für jedes Element (`this` im inneren Ausdruck) wird der Workflow-Status auf 'Erledigt' gesetzt.
* **Kontext/Anwendung:** Beim Abschließen eines Hauptobjekts alle zugehörigen Fehlermeldungen ebenfalls abschließen.
* **Verwendete Funktionen:** `ForEach`, `GetMemberValue`, `ChangeWorkflowState`

***

### 6. Komplexe Beispiele und Kombinationen

***

**Beispiel 17: Standardwert mit Coalesce und Objekt-Lookup setzen**

*   **Code Snippet:**

    ```criteria
    SetMemberValue(this,'ResponsiblePerson',Coalesce([ResponsiblePerson], GetObject(CurrentUser(), [this])))
    ```
* **Beschreibung:** Setzt das Feld `ResponsiblePerson`. Es wird zuerst geprüft, ob `ResponsiblePerson` bereits einen Wert hat. Wenn ja, bleibt dieser erhalten (`Coalesce` nimmt den ersten nicht-leeren Wert). Wenn `ResponsiblePerson` leer ist, wird der aktuelle Benutzer (`CurrentUser()`) als Standardwert verwendet. `GetObject` wird hier vermutlich verwendet, um den Kontext (`[this]`) an eine eventuelle Logik innerhalb von `CurrentUser` oder der Zuweisung zu übergeben (die genaue Notwendigkeit hängt von der Implementierung ab, oft reicht `CurrentUser()` direkt).
* **Kontext/Anwendung:** Sicherstellen, dass ein Feld immer gefüllt ist, wobei ein vorhandener Wert Priorität hat.
* **Verwendete Funktionen:** `SetMemberValue`, `Coalesce`, `GetObject`, `CurrentUser`

***

**Beispiel 18: Komplexer `Iif` mit Feldprüfung und Lookup**

*   **Code Snippet:**

    ```criteria
    IIF(ISNULL([PlannerGroup]), SetMemberValue(this,'PlannerGroup',FirstComponent.PlannerGroup),'')
    ```
* **Beschreibung:** Prüft, ob das Feld `PlannerGroup` des aktuellen Objekts leer ist (`ISNULL`). Wenn ja, wird die `PlannerGroup` vom Objekt übernommen, das im Feld `FirstComponent` referenziert ist (`FirstComponent.PlannerGroup`). Wenn `PlannerGroup` bereits gesetzt ist, passiert nichts.
* **Kontext/Anwendung:** Übernehmen von Standardwerten aus übergeordneten oder verknüpften Objekten, falls das Feld nicht explizit gesetzt wurde.
* **Verwendete Funktionen:** `Iif`, `IsNull`, `SetMemberValue`

***

**Beispiel 19: `BulkExecute` für mehrere Aktionen**

*   **Code Snippet:**

    ```criteria
    BULKEXECUTE(SETMEMBERVALUE(this,'Description', Subject), SetMemberValue(this,'Subject',PROCESSTEXTINPUT_AI('Titelberechnung',Subject)), ADDUSERCOMMENTLOGENTRY(this, [Description]))
    ```
* **Beschreibung:** Führt drei Aktionen in einem Block aus:
  1. Setzt das Feld `Description` auf den Wert von `Subject`.
  2. Setzt das Feld `Subject` auf das Ergebnis einer KI-Titelberechnung basierend auf dem _alten_ Wert von `Subject`.
  3. Fügt einen Benutzerkommentar mit dem _alten_ Wert von `Description` hinzu.
* **Kontext/Anwendung:** Bündelung mehrerer zusammenhängender Aktionen, potenziell zur Performance-Optimierung oder besseren Lesbarkeit bei vielen Schritten.
* **Verwendete Funktionen:** `BulkExecute`, `SetMemberValue`, `PROCESSTEXTINPUT_AI`, `AddUserCommentLogEntry`
* **Hinweis:** Die Ausführungsreihenfolge innerhalb von `BulkExecute` ist garantiert. Beachten Sie, dass spätere Schritte auf den Ergebnissen vorheriger Schritte innerhalb des Blocks basieren können (oder auch nicht, wie hier beim `Subject`).

***

**Beispiel 20: Kilometerstand aus Messpunkten übernehmen**

*   **Code Snippet:**

    ```criteria
    SetMemberValue(this,'Kilometrage',[Equipment.MeasurementPoints][[Position] = 'KMSTAND'].Max([LastValueDouble]))
    ```
* **Beschreibung:** Setzt das Feld `Kilometrage` des aktuellen Objekts. Der Wert wird ermittelt, indem in der Collection `MeasurementPoints` des verknüpften `Equipment` nach dem Messpunkt mit der `Position` 'KMSTAND' gesucht wird. Von diesem Messpunkt (falls gefunden) wird der Maximalwert des Feldes `LastValueDouble` genommen (nützlich, falls es mehrere Einträge geben könnte, oder einfach, um den Wert zu extrahieren).
* **Kontext/Anwendung:** Automatisches Übertragen des letzten erfassten Kilometerstands von einem Messpunkt-Objekt in ein Feld des Hauptobjekts (z.B. eines Fahrzeugs oder Auftrags).
* **Verwendete Funktionen:** `SetMemberValue`, Aggregationsfunktion `Max` auf gefilterter Collection.

***

## Tipps & Best Practices

* **Testen:** Testen Sie Ihre State Actions gründlich in einer Testumgebung, bevor Sie sie produktiv einsetzen. Achten Sie auf unbeabsichtigte Seiteneffekte.
* **Klarheit:** Versuchen Sie, Ausdrücke möglichst lesbar zu halten. Bei sehr komplexer Logik kann es sinnvoll sein, diese auf mehrere Status oder Aktionen aufzuteilen oder Custom Functions zu erstellen, die die Komplexität kapseln.
* **`Iif` für Bedingungen:** Verwenden Sie `Iif` konsequent für bedingte Ausführungen von Aktionen.
* **Kontext (`this`):** Denken Sie daran, dass `this` sich immer auf das Objekt bezieht, das den Workflow-Statuswechsel ausgelöst hat. Zugriffe auf verknüpfte Objekte erfolgen über deren Property-Namen (z.B. `[Order]`, `[Equipment]`).
* **Fehlerbehandlung:** Die Criteria Language bietet begrenzte Möglichkeiten zur Fehlerbehandlung. Wenn eine Funktion fehlschlägt (z.B. `GetObject` findet nichts, `ToDateTime` bekommt ungültigen String), gibt sie oft `null` oder einen Standardwert zurück. Prüfen Sie auf `null`-Werte mit `IsNull` oder `IsNullOrEmpty`, bevor Sie auf Eigenschaften zugreifen (z.B. `Iif(GetObject(...) != null, GetObject(...).PropertyName, 'Standard')`). Laufzeitfehler können im Paledo-Systemlog protokolliert werden.
* **Performance:** Sehr komplexe Ausdrücke oder `ForEach`-Schleifen über große Listen können die Performance beeinflussen. Optimieren Sie, wo möglich, oder verlagern Sie aufwändige Operationen in asynchrone Prozesse (z.B. über `CreateTriggeredRestApiCall` oder dedizierte Server Tasks).

Wir hoffen, dieses Tutorial gibt Ihnen einen guten Einblick in die praktische Anwendung der Paledo Custom Functions in der Workflow Engine!