# Tutorial Protokollvorlagen

## Einleitung

Willkommen zu diesem erweiterten Tutorial über die Anwendung von Low-Code Skripting in Paledo, speziell im Kontext von **Berichten (BORecords)** und deren **Protokollvorlagen**. Paledo ermöglicht es Ihnen, nicht nur Daten in Berichten zu erfassen, sondern auch dynamische Berechnungen durchzuführen und Prozesse nach Abschluss eines Berichts zu automatisieren – alles mit Hilfe der **XAF Criteria Language** und Paledo **Custom Functions**.

Dieses Tutorial richtet sich an **erfahrene Paledo-Anwender, Administratoren und Vorlagendesigner**, die lernen möchten, wie sie:

1. **Berechnete Felder** in Protokollvorlagen definieren, um Werte dynamisch zu kalkulieren (ähnlich wie Formeln in Excel).
2. **Befundauswertungsformeln** (Issue Evaluation Formulas) nutzen, um auf Basis von erfassten Befunden (z.B. "nicht in Ordnung") automatisch Aktionen wie die Erstellung von Folgemeldungen auszulösen.
3. **Skripte bei Berichtsabschluss** (On Approval Evaluation Formulas) definieren, um nach der Genehmigung oder dem Abschluss eines Berichts Daten zu verarbeiten, Objekte zu aktualisieren oder externe Prozesse anzustoßen.

Wir werden uns nun **genau 60 Beispiele** aus der Praxis ansehen, die von einfachen Berechnungen bis hin zu komplexen Automatisierungsabläufen reichen und eine schrittweise Lernerfahrung ermöglichen sollen.

## Teil 1: Berechnete Felder (`Expressions`)

Berechnete Felder sind ideal für die dynamische Anzeige von Informationen basierend auf anderen Daten im Bericht oder verknüpften Objekten.

### 1.1 Einfache Textformatierung und Verkettung

Diese Beispiele zeigen grundlegende Textmanipulationen.

**Beispiel 1: Statischer Text mit Datum (Allgemein)**

```criteria
'Berlin, den ' + ToDateString([DateStart])
```

* **Erklärung:** Kombiniert einen festen Ort mit dem formatierten Startdatum des Berichts. `ToDateString` wandelt das Datum um.

**Beispiel 2: Ort und Datum aus Firma (Allgemein)**

```criteria
[Event].[WorkCenter].[Company].[City]+', den '+ ToDateString([DateStart])
```

* **Erklärung:** Holt den Ort (`City`) dynamisch aus den Firmendaten des zugeordneten Arbeitsplatzes (`WorkCenter`) des Ereignisses (`Event`) und kombiniert ihn mit dem formatierten Startdatum.

**Beispiel 3: Verkettung von Berichtsfeldern (Allgemein)**

```criteria
GetValue('paledoXRLabel1') + GetValue('paledoXRLabel2')
```

* **Erklärung:** Verbindet die Werte der Berichtsfelder `paledoXRLabel1` und `paledoXRLabel2`. `GetValue` ist hier entscheidend.

**Beispiel 4: Verkettung mit fester Zeichenkette (Transportwesen)**

```criteria
'D '+[Equipment].[Kennzeichen]
```

* **Erklärung:** Stellt dem Wert des Feldes `Kennzeichen` des verknüpften Equipments (Fahrzeug) den Buchstaben 'D ' voran.

**Beispiel 5: Kombination von Auftrags- und Vorgangsnummer (SAP-Kontext)**

```criteria
[Order].[SAPOrderNo] + '/' + [Operation].[Number]
```

* **Erklärung:** Zeigt die SAP-Auftragsnummer (`SAPOrderNo`) des Auftrags (`Order`) und die Nummer des Vorgangs (`Operation`), getrennt durch einen Schrägstrich.

**Beispiel 6: Titel aus Katalogeintrag holen (Fertigung/Wartung)**

```criteria
GetMemberValue(GetCatalogEntry('Mangelarten',GetValue('Bezeichnung1')),'Description')
```

* **Erklärung:** Holt einen Katalogeintrag (`GetCatalogEntry`) aus dem Katalog 'Mangelarten', wobei der Code des Eintrags aus dem Berichtsfeld `Bezeichnung1` stammt (`GetValue('Bezeichnung1')`). Vom gefundenen Katalogeintrag wird dann die Beschreibung (`Description`) mittels `GetMemberValue` angezeigt.

**Beispiel 7: Benutzer und Organisationseinheit anzeigen (Allgemein)**

```criteria
Coalesce([DocumentedBy],CurrentUser())+' ('+Coalesce([DocumentedBy].[OrganisationalUnit].[Code],GetMemberValue(GetMemberValue(CurrentUser(),'OrganisationalUnit'),'Code'))+')'
```

* **Erklärung:** Zeigt den Namen des dokumentierenden Benutzers an. Wenn keiner explizit gesetzt ist (`[DocumentedBy]`), wird der aktuelle Benutzer (`CurrentUser()`) verwendet (`Coalesce`). Dahinter wird in Klammern der Code der Organisationseinheit angezeigt, ebenfalls mit `Coalesce` als Fallback auf die Einheit des aktuellen Benutzers, falls der dokumentierende Benutzer keine hat. `GetMemberValue` wird hier genutzt, um verschachtelt auf die Eigenschaften zuzugreifen.

### 1.2 Zugriff auf Objekt-Eigenschaften (Direkt & Verknüpft)

Diese Beispiele zeigen, wie man auf Daten des Berichts selbst oder verbundener Objekte zugreift.

**Beispiel 8: Name der Komponente (Allgemein)**

```criteria
[Component.Name]
```

* **Erklärung:** Greift auf die Eigenschaft `Name` des direkt mit dem Bericht verknüpften `Component`-Objekts zu.

**Beispiel 9: Technischer Platz der Komponente (Anlagenmanagement)**

```criteria
[Component.Technischer Platz]
```

* **Erklärung:** Zeigt den Wert des Feldes `Technischer Platz` der verknüpften Komponente an.

**Beispiel 10: Kritikalität der Komponente (Anlagenmanagement/PSM)**

```criteria
[Component].[PSMCritical]
```

* **Erklärung:** Zeigt den Wert des Feldes `PSMCritical` (vermutlich ein Boolean für Prozesssicherheits-Management) der Komponente an.

**Beispiel 11: Materialnummer und Seriennummer (Fertigung/Wartung)**

```criteria
[Equipment].[MaterialNo]+'#'+[Equipment].[SerialNoObjekt]
```

* **Erklärung:** Kombiniert Materialnummer (`MaterialNo`) und Seriennummer (`SerialNoObjekt`) des verknüpften Equipments, getrennt durch '#'.

**Beispiel 12: Auftrags-Betreff anzeigen (Allgemein)**

```criteria
[Order].[Subject]
```

* **Erklärung:** Zeigt den Betreff (`Subject`) des mit dem Bericht verknüpften Auftrags (`Order`) an.

**Beispiel 13: Fallback zwischen Feldern (Allgemein)**

```criteria
Coalesce([Description],[Subject] )
```

* **Erklärung:** Zeigt den Wert des Feldes `Description` an. Wenn `Description` leer ist, wird stattdessen der Wert des Feldes `Subject` angezeigt. `Coalesce` gibt das erste nicht-leere Argument zurück.

**Beispiel 14: Zählerstand aus Auftrag oder Anlage (Transportwesen/Fuhrpark)**

```criteria
Coalesce([Order].[Kilometrage],[FuncLoc].[CurrentKilometrage] )
```

* **Erklärung:** Zeigt primär den Kilometerstand (`Kilometrage`) aus dem Auftrag (`Order`). Ist dieser nicht vorhanden oder 0, wird als Fallback der aktuelle Kilometerstand (`CurrentKilometrage`) vom Technischen Platz (`FuncLoc`, oft das Fahrzeug) verwendet.

**Beispiel 15: Dynamischer Zugriff via GetMemberValue (Allgemein)**

```criteria
GetMemberValue([Component].[Raum],'')
```

* **Erklärung:** Greift auf die Eigenschaft `Raum` des verknüpften `Component`-Objekts zu. Der zweite Parameter `''` ist hier wahrscheinlich ein Standardwert oder Kontext, der in diesem Fall nicht benötigt wird. `GetMemberValue` ist nützlich, wenn der Eigenschaftsname selbst dynamisch ist.

### 1.3 Bedingte Logik mit `Iif()`

`Iif(Condition, TrueValue, FalseValue)` ist essentiell für bedingte Anzeigen.

**Beispiel 16: Bedingte Textanzeige (Transportwesen/HU)**

```criteria
IIF(IsNull([Event]),'Hauptuntersuchung', iif([Event.EventTask.Name]='HU','Hauptuntersuchung','Nachkontrolle'))
```

* **Erklärung:** Prüft, ob ein Ereignis (`Event`) verknüpft ist.
  * Wenn nein (`IsNull([Event])`), wird "Hauptuntersuchung" angezeigt.
  * Wenn ja, wird geprüft, ob der Name der Aufgabe (`[Event.EventTask.Name]`) "HU" ist.
    * Wenn ja, wird "Hauptuntersuchung" angezeigt.
    * Wenn nein, wird "Nachkontrolle" angezeigt.

**Beispiel 17: Bedingter Zugriff auf Firmendaten (Allgemein)**

```criteria
Iif(IsNullOrEmpty([Order].[ExternalCompany].[Name]),[Order].[WorkCenter].[Company].[Name] ,false )
```

* **Erklärung:** Prüft, ob der Name einer externen Firma (`ExternalCompany`) im Auftrag leer ist. Wenn ja, wird der Name der Firma des Arbeitsplatzes (`WorkCenter`) angezeigt. Andernfalls wird `false` zurückgegeben (was hier wahrscheinlich bedeutet, dass der Name der externen Firma _direkt_ im Berichtselement angezeigt wird, und dieses berechnete Feld nur den Fallback liefert). Ähnliche Logik wird für City, Street etc. verwendet.

**Beispiel 18: Toleranzanzeige (wie Beispiel 5, leicht variiert)**

```criteria
Iif([TargetToleranceMode]=2 Or [TargetToleranceMode]=1,
    'Toleranz'+ ' -'+ToString(GetMemberValue([This], 'TargetToleranceMin'))+ ' / +' 
    +ToString(GetMemberValue([This], 'TargetToleranceMax'))+Iif([TargetToleranceMode]=2,[QuantityUnit.Name],'%')
,'Min. / Max. Wert')
```

* **Erklärung:** Siehe Beispiel 5. Demonstriert die Wiederverwendung von `Iif` für komplexe bedingte Formatierungen.

**Beispiel 19: Einfache Farbcodierung (Allgemein)**

```criteria
Iif([IsNotDone] = True, 'Red', ?)
```

* **Erklärung:** Wenn das Feld `IsNotDone` wahr ist, gibt die Formel den String 'Red' zurück (könnte zur bedingten Formatierung der Schriftfarbe verwendet werden). Der `?`-Teil ist unvollständig, sollte aber den Wert für den False-Fall enthalten (z.B. 'Black' oder `null`).

**Beispiel 20: Komponententyp kürzen (Fertigung/Wartung)**

```criteria
IIF(Contains([Component.Name],'üfter'),'Lüfter',
IIF(Contains([Component.Name],'hitzer'),'Erhitzer',
IIF(Contains([Component.Name],'Filter'),'Filter',
[Component.Name] )))
```

* **Erklärung:** Verschachtelte `Iif`-Abfragen, um den Komponentennamen zu kürzen oder zu standardisieren. Wenn der Name "üfter" enthält, wird "Lüfter" zurückgegeben, sonst wenn "hitzer" enthalten ist, "Erhitzer", sonst wenn "Filter" enthalten ist, "Filter", andernfalls der ursprüngliche Name.

### 1.4 Datum, Zeit und Zahlen

Beispiele für Berechnungen und Formatierungen.

**Beispiel 21: Aktuelle Kalenderwoche (Allgemein)**

```criteria
GetCalendarWeek([DateStart])
```

* **Erklärung:** Gibt die Kalenderwoche des Startdatums des Berichts zurück.

**Beispiel 22: Zeit formatieren (Allgemein)**

```criteria
ToString([DocumentedDate], 'H:mm')
```

* **Erklärung:** Formatiert das Dokumentationsdatum (`DocumentedDate`), um nur die Uhrzeit im Format "Stunde:Minute" (z.B. "14:35") anzuzeigen.

**Beispiel 23: Einfache Addition (Allgemein)**

```criteria
ToInt([Number]) + 2
```

* **Erklärung:** Wandelt den Wert des Feldes `Number` in eine Ganzzahl (`ToInt`) um und addiert 2 hinzu.

**Beispiel 24: Nächstes Prüfdatum berechnen (Transportwesen/HU)**

```criteria
Substring(AddDays(ToDateTime(Substring(GetValue('Deckblatt Werkstattbericht','DFDATUM'),0,10)),28),0,10)
```

* **Erklärung:** Berechnet ein Datum 28 Tage nach einem Datum aus dem Berichtsfeld `DFDATUM`.
  * `GetValue('Deckblatt Werkstattbericht','DFDATUM')`: Holt den Wert des Feldes.
  * `Substring(...,0,10)`: Nimmt die ersten 10 Zeichen (vermutlich um nur den Datumsteil TT.MM.JJJJ zu erhalten).
  * `ToDateTime(...)`: Wandelt den Datumsstring in ein echtes Datum um.
  * `AddDays(..., 28)`: Addiert 28 Tage hinzu.
  * `Substring(...,0,10)`: Formatiert das Ergebnis wieder als String TT.MM.JJJJ.

**Beispiel 25: Differenz berechnen (Fertigung/Wartung)**

```criteria
GV('ÖlstandNachWechsel') - GV('ÖlstandVorWechsel')
```

* **Erklärung:** Berechnet die Differenz zwischen den Werten der Berichtsfelder `ÖlstandNachWechsel` und `ÖlstandVorWechsel`. `GV` ist eine Abkürzung für `GetValue`.

### 1.5 String-Manipulation

Extrahieren oder Bearbeiten von Teilen von Zeichenketten.

**Beispiel 26: Einfacher Substring (Allgemein)**

```criteria
Substring('dies ist ein langer Text',1,5)
```

* **Erklärung:** Extrahiert 5 Zeichen aus dem String, beginnend beim zweiten Zeichen (Index 1). Ergebnis: "ies i".

**Beispiel 27: Fahrzeugnummer extrahieren (Transportwesen)**

```criteria
Substring([FuncLoc].[Technischer Platz],0,3 )
```

* **Erklärung:** Nimmt die ersten 3 Zeichen aus dem Feld `Technischer Platz` des Technischen Platzes (Fahrzeug).

**Beispiel 28: KBA-Schlüssel Teil extrahieren (Transportwesen)**

```criteria
Substring(Trim(Substring([FuncLoc.KBA-Schlüssel],10)),0,4)
```

* **Erklärung:** Extrahiert einen Teil eines KBA-Schlüssels.
  * `Substring([FuncLoc.KBA-Schlüssel],10)`: Nimmt den Teilstring ab dem 11. Zeichen.
  * `Trim(...)`: Entfernt führende/nachfolgende Leerzeichen.
  * `Substring(...,0,4)`: Nimmt die ersten 4 Zeichen des getrimmten Ergebnisses.

**Beispiel 29: Teil eines Namens extrahieren mit Trennzeichen (Allgemein)**

```criteria
GetStringPart([EventTasks],1,'+')
```

* **Erklärung:** Teilt den String im Feld `EventTasks` am Trennzeichen `+` und gibt den zweiten Teil (Index 1) zurück.

**Beispiel 30: Ersten Teil eines Strategienamens (Anlagenmanagement)**

```criteria
Split([Component].[StrategyOverview].[Strategy].[Name], '-', 0)
```

* **Erklärung:** Teilt den Namen der Strategie am Zeichen `-` und gibt den ersten Teil (Index 0) zurück. `Split` ist eine Alternative zu `GetStringPart`.

**Beispiel 31: Text ersetzen (Allgemein)**

```criteria
Replace([Description],'Freitext','')
```

* **Erklärung:** Entfernt das Wort "Freitext" aus dem Feld `Description`.

### 1.6 Arbeiten mit Listen und Aggregationen (Linq-Style)

Diese Beispiele nutzen Linq-ähnliche Funktionen, um Daten aus Listen innerhalb des Berichts oder verknüpfter Objekte zu verarbeiten.

**Beispiel 32: Kommentare aus Unteroperationen verketten (Allgemein)**

```criteria
Join(Select([SubOperations],'Comment'), '\n')
```

* **Erklärung:** Holt alle Kommentare (`Comment`) aus der Liste der Unteroperationen (`SubOperations`), wählt diese aus (`Select`) und verbindet sie mit einem Zeilenumbruch () zu einem einzigen String (`Join`).

**Beispiel 33: Anzahl bestimmter Listenelemente zählen (Allgemein)**

```criteria
[CurrentRecordReport.RecordReportListElements][[NOK]=true And ([Resolved]=false Or IsNullOrEmpty([Resolved]))].Count()
```

* **Erklärung:** Zählt die Anzahl der Listenelemente (`RecordReportListElements`) im aktuellen Bericht (`CurrentRecordReport`), bei denen `NOK` wahr ist UND `Resolved` entweder falsch oder leer ist. Dies nutzt die eingebaute Filterung und Aggregation von XAF-Collections.

**Beispiel 34: Bestimmte Beschreibungen aus Liste extrahieren und verbinden (Transportwesen/HU)**

```criteria
Join(Select(
Where(GetMemberValue([CurrentRecordReport], 'RecordReportListElements'), 'not IsNullOrEmpty(Description) and IsNumber(Substring(Description,0,3))'), 
'Description'), ',')
```

* **Erklärung:** Ein komplexeres Beispiel:
  1. `GetMemberValue([CurrentRecordReport], 'RecordReportListElements')`: Holt die Liste der Elemente.
  2. `Where(..., 'not IsNullOrEmpty(Description) and IsNumber(Substring(Description,0,3))')`: Filtert die Liste. Behält nur Elemente, deren `Description` nicht leer ist UND deren erste drei Zeichen eine Zahl sind (`IsNumber(Substring(...))`).
  3. `Select(..., 'Description')`: Wählt aus den gefilterten Elementen nur die `Description` aus.
  4. `Join(..., ',')`: Verbindet die ausgewählten Beschreibungen mit einem Komma zu einem einzigen String.

**Beispiel 35: Prüfen, ob bestimmte gefilterte Elemente existieren (Transportwesen/HU)**

```criteria
not IsNullOrEmpty(Join(Select(
Where(GetMemberValue([CurrentRecordReport], 'RecordReportListElements'), 'not IsNullOrEmpty(Description) and IsNumber(Substring(Description,0,3))'), 
'Description'), ','))
```

* **Erklärung:** Baut auf Beispiel 34 auf. Es prüft, ob der resultierende String aus der `Join`-Operation _nicht_ leer ist. Das bedeutet, es wird geprüft, ob mindestens ein Listenelement existiert, das die `Where`-Bedingung erfüllt.

## Teil 2: Befundauswertungsformeln (`IssueEvaluationFormulas`)

Diese Formeln reagieren auf negative Befunde (NOK) in einzelnen Berichtszeilen.

### 2.1 Einfache Folgemeldungen erstellen

**Beispiel 36: Standard-Folgemeldung (Allgemein)**

```criteria
CreateFollowup('Achtung: '+GetValue('BEMFM'),[Component.Equipment],'P2')
```

* **Erklärung:** Erstellt eine einfache Folgemeldung mit Titel "Achtung: \[Inhalt Feld BEMFM]", verknüpft sie mit dem Equipment und setzt Priorität P2. (Siehe auch Beispiel 6).

**Beispiel 37: Folgemeldung mit fester Beschreibung (Fertigung/Wartung)**

```criteria
CreateFollowup('Tageswartung nicht durchgeführt. Grund: '+ GetValue('begrND'),[Component.Technischer Platz],'P2')
```

* **Erklärung:** Erstellt eine Folgemeldung, wenn eine Tageswartung nicht durchgeführt wurde. Der Grund wird aus dem Feld `begrND` geholt. Verknüpft mit dem Technischen Platz.

**Beispiel 38: Folgemeldung aus Bemerkung erstellen (Allgemein)**

```criteria
CreateFollowUp('NOK: ' + GetMemberValue(GetCurrentRow(), 'Remark') + ' (' + GetMemberValue(GetCurrentRow(), 'Description') + ')', [Equipment], 'P1')
```

* **Erklärung:** Erstellt eine Folgemeldung (Priorität P1), deren Titel aus der Bemerkung (`Remark`) und der Beschreibung (`Description`) der aktuellen NOK-Zeile (`GetCurrentRow()`) zusammengesetzt wird.

### 2.2 Bedingte Folgemeldungen

**Beispiel 39: Folgemeldung nur wenn nicht behoben (Fertigung/Wartung)**

```criteria
iif(GetValue('behoben_1')=False,CreateFollowup('X-Achse / '+ GetValue('tbBemerkung_1'),[Component.Technischer Platz],'P2'),' ')
```

* **Erklärung:** Nur wenn das Feld `behoben_1` falsch ist, wird eine Folgemeldung erstellt. Andernfalls passiert nichts (leerer String `' '` oder `null` wäre auch möglich).

**Beispiel 40: Folgemeldung mit/ohne direkten Erledigt-Status (Fertigung/Wartung)**

```criteria
iif(GetValue('behoben_1')=False,
    CreateFollowup('X-Achse / '+ GetValue('tbBemerkung_1'),[Component.Technischer Platz],'P2'),
    CreateFollowup('X-Achse / '+ GetValue('tbBemerkung_1'),[Component.Technischer Platz],'P2','','','','','','','Erledigt')
)
```

* **Erklärung:** Wie Beispiel 7. Zeigt, wie man über zusätzliche (optionale) Parameter von `CreateFollowUp` den initialen Status der erstellten Meldung beeinflussen kann.

### 2.3 Komplexere Aktionen bei Befund

**Beispiel 41: Zeitrückmeldung bei Mangel (SAP-Kontext/Transportwesen)**

```criteria
Concat(
    ConfirmTimeEx('PALEDOP1', [ExecutionDate], '0:01', [ExecutionDate], '0:01', ToString(ToFloat(Trim(Substring(GETMEMBERVALUE(GETCURRENTROW(), 'Number')+'  ',0,3))) / 60), [WorkCenter.Code], 
        ModifyObject(ADDOPERATION([][Subject like 'Daueraufträge ab 2020 BF-BS% Reparatur' and IsOpenTask=true and Equipment= ^.Equipment].Min(this), GETMEMBERVALUE(GETCURRENTROW(), 'Comment')+ ' ( ' +ToString(ExecutionDate,'dd.MM.yyyy')+' '+GETMEMBERVALUE(GETCURRENTROW(), 'Description')+': '+GETMEMBERVALUE(GETCURRENTROW(), 'Remark')+')'),'PreventFromSAPUpload',false),
    [Name]+ ' PR1 Pos.'+GETMEMBERVALUE(GETCURRENTROW(), 'Position')),

    IIF(IsNullOrEmpty(GV('PRUEFER2')),'',
        ConfirmTimeEx(...) /* Ähnlich für Prüfer 2 */
    ),

    IIF(IsNullOrEmpty(GV('PRUEFER3')),'',
        ConfirmTimeEx(...) /* Ähnlich für Prüfer 3 */
    )
)
```

* **Erklärung:** Extrem komplexes Beispiel, das bei einem Befund ausgelöst wird.
  1. `ConfirmTimeEx(...)`: Meldet eine Zeit zurück (vermutlich für den ersten Prüfer).
     * Die Zeitdauer wird aus der Nummer der aktuellen Zeile berechnet (`ToString(ToFloat(Trim(Substring(...))) / 60)`).
     * Als Referenzobjekt für die Zeitrückmeldung dient eine _neu hinzugefügte Operation_ zu einem _bestehenden Dauerauftrag_.
     * `ADDOPERATION(...)`: Fügt eine Operation hinzu.
       * Der Zielauftrag wird dynamisch gesucht: `[][Subject like 'Daueraufträge...' and IsOpenTask=true and Equipment= ^.Equipment].Min(this)`. Sucht den ältesten offenen Dauerauftrag für das Equipment.
       * Der Operationstext enthält den Kommentar, Datum, Beschreibung und Bemerkung aus der aktuellen NOK-Zeile.
     * `ModifyObject(..., 'PreventFromSAPUpload',false)`: Stellt sicher, dass die neue Operation zu SAP hochgeladen werden kann.
     * Der letzte Parameter von `ConfirmTimeEx` ist ein Kommentar für die Zeitrückmeldung.
  2. `IIF(IsNullOrEmpty(GV('PRUEFER2')),'', ConfirmTimeEx(...))`: Wiederholt die Zeitrückmeldung (mit leicht anderer Kennung, z.B. 'PALEDOP2') für einen zweiten Prüfer, aber nur, wenn das Berichtsfeld `PRUEFER2` nicht leer ist.
  3. Ähnlich für Prüfer 3.
  4. `Concat(...)`: Verbindet die (potenziell leeren) Ergebnisse der Aufrufe. Der Rückgabewert ist hier meist irrelevant, die Aktionen sind entscheidend.
  5. **Zweck:** Bei einem Mangel wird nicht nur eine Meldung erzeugt, sondern direkt Zeit auf einen Dauerreparaturauftrag gebucht und diesem eine detaillierte Operation hinzugefügt, potenziell für mehrere beteiligte Prüfer.

## Teil 3: Skripte bei Berichtsabschluss (`OnApprovalEvaluationFormulas`)

Diese Skripte laufen, wenn der _gesamte_ Bericht abgeschlossen wird.

### 3.1 Stammdatenaktualisierung (Anlagen, Equipments)

**Beispiel 42: Letzte/Nächste Prüfung & Status setzen (Allgemein)**

```criteria
IIF(1=1, ''+
    SETCLASSIFICATIONVALUE([CurrentRecordReport.Component], 'EQ_NAECHSTE_PRUEFUNG2', 
        ToString(AddMonths([DateStart], GetClassificationValue([CurrentRecordReport.Component], 'EQ_PRUEFINTERVALL')))
    )
    + SETCLASSIFICATIONVALUE([CurrentRecordReport.Component], 'EQ_LETZTE_PRUEFUNG',ToString([DateStart]))
    + SETCLASSIFICATIONVALUE([CurrentRecordReport.Component], 'EQ_PRUEFAUFTRAG',[Order.Number])
    + SETCLASSIFICATIONVALUE([CurrentRecordReport.Component], 'EQ_GEPRUEFT_DURCH',[DocumentedBy.Caption])
,'')
```

* **Erklärung:** Identisch zu Beispiel 8. Aktualisiert mehrere Klassifizierungsmerkmale an der Komponente nach Abschluss der Prüfung.

**Beispiel 43: Nächsten SP-Termin setzen (Transportwesen/Fuhrpark)**

```criteria
ModifyObject([Equipment],'NaechsteSP',GetValue('modulo5XRLabel29'))
```

* **Erklärung:** Setzt das Feld `NaechsteSP` am Equipment (`ModifyObject`) auf den Wert aus dem Berichtsfeld `modulo5XRLabel29`.

**Beispiel 44: Kilometerstand für nächsten Wechsel berechnen (Transportwesen/Fuhrpark)**

```criteria
IIF(([Kilometerstand]<>0) AND not IsNullOrEmpty(GetValue('tbWechselArt')),
    SetMemberValue([Equipment],'Naechster'+GetValue('tbWechselArt'),[Kilometerstand]+ToInt(GetValue('tbWechselIntervall'))),
'')
```

* **Erklärung:** Wenn im Bericht ein Kilometerstand (`Kilometerstand`) erfasst wurde und ein Wechsel-Typ (`tbWechselArt`) angegeben ist:
  * Wird am Equipment (`[Equipment]`) ein Feld aktualisiert (`SetMemberValue`). Der Feldname wird dynamisch zusammengesetzt ('Naechster' + Wechselart, z.B. 'NaechsterOelwechsel').
  * Der neue Wert ist der aktuelle Kilometerstand plus das Wechselintervall (aus Feld `tbWechselIntervall`).

**Beispiel 45: Inspektionsstatus basierend auf Ergebnis-Dropdown (Fertigung/Wartung)**

```criteria
BULKEXECUTE(
IIF(GV('Pruefergbnis')='0 - Keine Mängel' Or GV('Pruefergbnis')='1 - Mängel' Or GV('Pruefergbnis')='2 - Erhebliche Mängel' Or GV('Pruefergbnis')='3 - Gesperrt / Gefährliche Mängel' ,
    SETCOMPONENTINSPECTIONSTATUS([Component],IIF(GV('Pruefergbnis')='0 - Keine Mängel',0 ,IIF(GV('Pruefergbnis')='1 - Mängel' Or GV('Pruefergbnis')='2 - Erhebliche Mängel',1,IIF(GV('Pruefergbnis')='3 - Gesperrt / Gefährliche Mängel',2,3))),
        [ApprovedDate],Join(Select(Where(GetMemberValue([CurrentRecordReport],'RecordReportListElements'),'NOK=true'),'Description+'' = ''+Remark'),',\n ')+ ',\tWeitere Bemerkung: ' +GV('tbBemSonstiges')),
    SETCOMPONENTINSPECTIONSTATUS([Component],3,[Component.InspectionLast],GV('begrND'))
)
,SETMEMBERVALUE(this,'Component.InspectionNext',ADDMONTHS([Component.InspectionLast],[Component.InspectionIntervalMo])) )
```

* **Erklärung:** Aktualisiert den Inspektionsstatus und den nächsten Termin.
  1. `BULKEXECUTE(...)`: Führt mehrere Befehle effizient aus.
  2. `IIF(GV('Pruefergbnis')= ...)`: Prüft den Wert des Dropdown-Feldes `Pruefergbnis`.
  3. `SETCOMPONENTINSPECTIONSTATUS(...)`: Setzt den Status der Komponente.
     * Der Statuscode (0, 1, 2 oder 3) wird basierend auf dem Text im Dropdown ermittelt.
     * Das Datum ist das Genehmigungsdatum (`[ApprovedDate]`).
     * Ein Kommentar wird generiert, der alle NOK-Punkte (`Join(Select(Where(...)))`) und eine allgemeine Bemerkung (`GV('tbBemSonstiges')`) enthält.
  4. Falls kein gültiges Ergebnis im Dropdown steht (z.B. Prüfung nicht durchgeführt), wird der Status auf 3 (Fehler?) gesetzt, das Datum der _vorherigen_ Prüfung (`[Component.InspectionLast]`) verwendet und der Grund aus `GV('begrND')` als Kommentar gesetzt.
  5. `SETMEMBERVALUE(this,'Component.InspectionNext',...)`: Unabhängig vom Ergebnis wird der nächste Inspektionstermin (`InspectionNext`) an der Komponente berechnet, indem das Inspektionsintervall (`InspectionIntervalMo`) zum Datum der _letzten_ Prüfung (`InspectionLast` - dieses wurde möglicherweise gerade durch `SETCOMPONENTINSPECTIONSTATUS` aktualisiert!) addiert wird.

### 3.2 Messwerterfassung

**Beispiel 46: Einfachen Messwert erstellen (Transportwesen/Fuhrpark)**

```criteria
CREATEMEASUREMENT(Equipment, 'Kilometerstand', [DocumentedDate], GV('istKMStand'), [DocumentedBy])
```

* **Erklärung:** Identisch zu Beispiel 10. Erstellt einen Messwert für den Kilometerstand.

**Beispiel 47: Messwert für Betriebsstunden (Fertigung/Anlagenmanagement)**

```criteria
Iif(Not IsNullOrEmpty(GetValue('mwBetriebsstunden')), CreateMeasurement([Equipment.Equipment], 'BTZ', CurrentDateTime(), GetValue('mwBetriebsstunden'), [ResponsiblePerson]), '')
```

* **Erklärung:** Nur wenn das Berichtsfeld `mwBetriebsstunden` einen Wert enthält, wird ein Messwert vom Typ 'BTZ' (Betriebsstunden) für das Equipment erstellt.

### 3.3 SAP-Integration

**Beispiel 48: Zeitrückmeldung für mehrere Personen (SAP-Kontext)**

```criteria
IIF(ISNULLOREMPTY(GV('Zeit1')), '', 
CONFIRMTIMEEX(GV('MA1'), ToDateString(LocalDateTimeNow()), ToDateString(LocalDateTimeNow()), ToDateString(LocalDateTimeNow()), ToDateString(LocalDateTimeNow()), TOSTRING(TOFLOAT(REPLACE(GV('Zeit1'), 'h', ''))), [Order].[WorkCenter].[Code], this, 'Zeitrückmeldung 1', false))
+
IIF(ISNULLOREMPTY(GV('Zeit2')), '', 
CONFIRMTIMEEX(GV('MA2'), ToDateString(LocalDateTimeNow()), ToDateString(LocalDateTimeNow()), ToDateString(LocalDateTimeNow()), ToDateString(LocalDateTimeNow()), TOFLOAT(REPLACE(GV('Zeit2'), 'h', '')), [Order].[WorkCenter].[Code], this, 'Zeitrückmeldung 2', false))
```

* **Erklärung:** Meldet Zeiten für bis zu zwei Mitarbeiter (MA1, MA2) zurück, wenn deren jeweilige Zeitfelder (Zeit1, Zeit2) ausgefüllt sind.
  * `IIF(ISNULLOREMPTY(GV('Zeit1')), '', ...)`: Prüft, ob Zeit für MA1 erfasst wurde.
  * `CONFIRMTIMEEX(...)`: Meldet die Zeit. Holt den Mitarbeiter aus `GV('MA1')`, die Zeit aus `GV('Zeit1')` (umgerechnet), verwendet aktuelles Datum/Zeit, Arbeitsplatzcode und einen Kommentar. Der letzte Parameter `false` bedeutet wahrscheinlich keine Endrückmeldung.
  * Das `+` verbindet die beiden `IIF`-Ausdrücke. Wenn nur eine Zeit erfasst ist, wird nur ein `CONFIRMTIMEEX` ausgeführt.

**Beispiel 49: Technische Rückmeldung (SAP-Kontext)**

```criteria
MODIFYOBJECT(ADDTECHNICALCONF(GETMEMBERVALUE(Order, 'HeaderNotification'), 
'Schadensbilder',GETCATALOGENTRY('Schadensbilder', GetValue('opDamage'), ''),'', 'Schadensursachen', GETCATALOGENTRY('Schadensursachen', GetValue('opCause'), '')
,'','','','','Massnahmen', GETCATALOGENTRY('Massnahmen', GetValue('opTask'), ''), 
Join(Select(GETMEMBERVALUE(GETCURRENTROW(), 'SubOperations'),'Comment'), ''),'true','true'
)
, 'CreationDate', GETMEMBERVALUE([Order.Records], 'CompletedDate'))
```

* **Erklärung:** Sendet eine technische Rückmeldung an die Kopfmeldung des Auftrags.
  1. `ADDTECHNICALCONF(...)`: Die Funktion zur technischen Rückmeldung. Parameter sind:
     * Die Kopfmeldung (`GETMEMBERVALUE(Order, 'HeaderNotification')`).
     * Katalogtypen und deren Werte (`Schadensbilder`, `Schadensursachen`, `Massnahmen`), die aus Berichtsfeldern (`GetValue('opDamage')` etc.) über `GETCATALOGENTRY` geholt werden.
     * Ein Text, der aus den Kommentaren der Unteroperationen zusammengesetzt wird (`Join(Select(...))`).
     * Flags `'true', 'true'` am Ende.
  2. `MODIFYOBJECT(..., 'CreationDate', ...)`: Modifiziert das Ergebnis des `ADDTECHNICALCONF`-Aufrufs (vermutlich ein Log-Objekt) und setzt dessen `CreationDate` auf das Abschlussdatum des Berichts (`GETMEMBERVALUE([Order.Records], 'CompletedDate')`).

**Beispiel 50: Eckende-Datum im Auftrag setzen (SAP-Kontext)**

```criteria
IIF(AddMinutes(AddHours(GV('Eckenddatum'),GetHour(GV('Eckendezeit'))),GetMinute(GV('Eckendezeit')))<[Order.BasicStart],'',
SETMEMBERVALUE([Order], 'BasicEnd',AddMinutes(AddHours(GV('Eckenddatum'),GetHour(GV('Eckendezeit'))),GetMinute(GV('Eckendezeit'))) ))
```

* **Erklärung:** Setzt das Eck-Enddatum (`BasicEnd`) im Auftrag (`[Order]`).
  * Prüft zuerst (`IIF`), ob das kombinierte Datum/Zeit aus den Berichtsfeldern `Eckenddatum` und `Eckendezeit` _vor_ dem Eck-Startdatum (`[Order.BasicStart]`) liegt. Das Kombinieren geschieht mit `AddMinutes(AddHours(...))`.
  * Wenn das Enddatum _nicht_ vor dem Startdatum liegt, wird `SETMEMBERVALUE` ausgeführt, um `[Order].BasicEnd` auf das kombinierte Datum/Zeit zu setzen.
  * Andernfalls passiert nichts ('').

### 3.4 Komplexe Workflow-Aktionen und Objekterstellung

**Beispiel 51: Folgeauftrag basierend auf NOK-Punkten (sehr komplex) (Fertigung/Anlagenbau)**

```markdown
IIF([CurrentRecordReport.RecordReportListElements][NOK = true And (Resolved = False or Resolved is null) ].EXISTS()  And GV('Folgemassnahme') = True,

CHANGEWORKFLOWSTATE(
    	MODIFYOBJECT(
       	CreateOrder(
            		'FFF', // Interner Typ?
            		[Component],
            		'Folgemaßnahme: ' + [Event.EventTask.Name] + '-' + [Order.SAPOrderNo]+' '+[Operation].[Number],
            		[DocumentedDate], 
            		AddDays([DocumentedDate],14),
            		'Folgemaßnahme: ' + [Event.EventTask.Name] + '-' + [Order.SAPOrderNo]+' '+[Operation].[Number]+'\n'+
                         Join(Select(Where(ReportListElementSummary,'NOK=true and Resolved<>true'),'Subject+'' -> NOK:'' + Remark + '' ''+  Char(13) + Char(10)'),''), // Fügt alle NOK-Punkte hinzu
            		'',
			[WorkCenter.Code] 
            		), 
        'ActivityType', 'XBW', 
	'SAPOrderType', [][DefaultActivityType='XBW' And SAPOrderType=IIF(^.[Component].[SAPComponentCategory].[ExternalKey]='P', 'PM02', IIF(^.[Component].[SAPComponentCategory].[ExternalKey]='M', 'PM01', IIF(^.[Component].[SAPComponentCategory].[ExternalKey]='G' and STARTSWITH(^.[Component].[PSPElement].[Name], 'X-001-1'), 'PM02', IIF(^.[Component].[SAPComponentCategory].[ExternalKey]='G' and STARTSWITH(^.[Component].[PSPElement].[Name], 'X-002-1'), 'PM01', 'PM01'))))].Min(This)), 
'Angenommen'), 

'')
```

* **Erklärung:** Dieses Skript wird bei Berichtsabschluss ausgeführt und erstellt einen _einzigen_ Folgeauftrag, wenn _mindestens ein_ NOK-Punkt vorhanden ist und die Option `Folgemassnahme` aktiviert ist.
  1. **Bedingung:** `IIF([CurrentRecordReport.RecordReportListElements][NOK = true ... ].EXISTS() And GV('Folgemassnahme') = True, ... , '')`
     * Prüft, ob in der Liste der Berichtselemente (`RecordReportListElements`) mindestens ein Element (`EXISTS()`) existiert, das die Kriterien `NOK = true And (Resolved = False or Resolved is null)` erfüllt UND das Berichtsfeld `Folgemassnahme` wahr ist.
  2. **Aktion:** `CHANGEWORKFLOWSTATE( MODIFYOBJECT( CreateOrder(...) ) )`
     * `CreateOrder(...)`: Erstellt einen neuen Auftrag ('FFF').
       * Der Langtext wird erweitert: `...\n'+ Join(Select(Where(ReportListElementSummary,'NOK=true...'),'Subject+'' -> NOK:'' + Remark + ...'),'')`. Hier werden die Details _aller_ offenen NOK-Punkte aus dem Bericht gesammelt (`Join(Select(Where(...)))`) und dem Auftragstext hinzugefügt.
     * `MODIFYOBJECT(...)`: Modifiziert den neuen Auftrag, setzt `ActivityType` und bestimmt dynamisch den `SAPOrderType` (wie in Beispiel 11).
     * `CHANGEWORKFLOWSTATE(..., 'Angenommen')`: Setzt den neuen Auftrag direkt in den Status 'Angenommen'.
  3. **Zweck:** Fasst alle offenen Mängel eines Berichts in einem einzigen Folgeauftrag zusammen.

**Beispiel 52: Equipment-Logeintrag erstellen (Fertigung/Wartung)**

```criteria
Concat(IIF(ISNULLOREMPTY(Text1) And ISNULLOREMPTY(Text2),'',
CREATEEQUIPMENTLOG([Component], [Text1], [Text2], Order,Event.EventTask.Name ,[DocumentedBy],true	)),
SetMemberValue(Component.ChildComponentHistory[UploadToSap=false Or IsNull(UploadToSap)].Min(this),'UploadToSap',true))
```

* **Erklärung:** Erstellt einen Logeintrag am Equipment und markiert einen Historien-Eintrag für den SAP-Upload.
  1. `IIF(ISNULLOREMPTY(Text1) And ISNULLOREMPTY(Text2),'', ...)`: Nur wenn Text1 oder Text2 im Bericht ausgefüllt ist...
  2. `CREATEEQUIPMENTLOG(...)`: Wird ein Logeintrag für die Komponente erstellt. Die Texte kommen aus den Berichtsfeldern `Text1`, `Text2`. Referenzen auf den Auftrag und die Aufgabe werden mitgegeben, ebenso der Benutzer. `true` am Ende könnte "sichtbar" bedeuten.
  3. `SetMemberValue(Component.ChildComponentHistory[...].Min(this),'UploadToSap',true)`: Sucht den ältesten Eintrag in der Komponentenhistorie (`ChildComponentHistory`), der noch nicht zu SAP hochgeladen wurde (`UploadToSap=false Or IsNull(UploadToSap)`) und setzt dessen `UploadToSap`-Flag auf `true`.
  4. `Concat(...)`: Verbindet die Ergebnisse (die hier nicht direkt genutzt werden), um beide Aktionen in einem Ausdruck unterzubringen.

**Beispiel 53: Radmessdaten erstellen (Transportwesen/Fuhrpark)**

```criteria
CreateWheelMeasurement(GetValue('datum'), GetValue('kmstand'), 'DT1DS ' + GetValue('fzgnr'), 'DT1DSDG-Satz ' +  GetValue('dgnr'), '1', 'L', GetValue('LeitkreisRechts1'), GetValue('DurchmesserLinks1'), GetValue('SpurkranzdickeLinks1'), GetValue('SpurkranzhoeheLinks1'), GetValue('QuermassLinks1'), GetValue('UnrundLinks1'), GetValue('SchleifflaechenLinks1'), GetValue('zweiterSpurkLinks1'))
+ // Wiederholung für '1', 'R'
+ CreateWheelMeasurement(..., '2', 'L', ...) 
+ // ... und so weiter für alle Radpositionen
```

* **Erklärung:** Nach Abschluss eines Radvermessungsberichts werden für jede erfasste Radposition (`'1', 'L'`, `'1', 'R'`, `'2', 'L'`, etc.) separate `CreateWheelMeasurement`-Funktionsaufrufe ausgeführt.
  * Jeder Aufruf übergibt die relevanten Messwerte (Datum, KM-Stand, Fahrzeug-Nr, DG-Satz-Nr, Achsposition, Seite, und die spezifischen Messwerte wie Leitkreis, Durchmesser, Spurkranzdicke etc.) aus den entsprechenden Berichtsfeldern (`GetValue(...)`).
  * Das `+` dient wieder nur dazu, die Funktionsaufrufe innerhalb _eines_ Ausdrucks aneinanderzureihen.

**Beispiel 54: Dokumentations-Record erstellen (Allgemein)**

```criteria
IIF(GETMEMBERVALUE(GETCURRENTROW(), 'NOK')=True And (GV('Pruefergbnis')='1 - Mängel' Or GV('Pruefergbnis')='2 - Erhebliche Mängel' Or GV('Pruefergbnis')='3 - Gesperrt / Gefährliche Mängel') , 
MODIFYOBJECT(CREATELISTELEMENT(GETMEMBERVALUE(GETCURRENTROW(), 'Group'), GETMEMBERVALUE(GETCURRENTROW(), 'Description'), false, false, false, false, 
CREATEDOCUMENTATIONRECORD([Component],?,\n'Mängelbeseitigung',\n LOCALDATETIMENOW(),\n ADDDAYS(LOCALDATETIMENOW(),10),\n [Event.StrategyGroup.ResponsiblePerson.WorkCenter],\n [Event.StrategyGroup.ResponsiblePerson],\n false), 
'1167'),'Description',GETMEMBERVALUE(GETCURRENTROW(), 'Description'),'Remark',GETMEMBERVALUE(Getcurrentrow(), 'Remark') )\n, '')
```

* **Erklärung:** Wird wahrscheinlich pro NOK-Zeile bei Berichtsabschluss ausgeführt.
  1. **Bedingung:** Prüft, ob die Zeile NOK ist UND das Gesamtergebnis des Berichts (aus `GV('Pruefergbnis')`) auf einen Mangel hindeutet.
  2. `CREATEDOCUMENTATIONRECORD(...)`: Erstellt einen neuen, separaten Bericht (Record) vom Typ "Mängelbeseitigung" für die Komponente. Setzt Start-/Enddatum, Verantwortlichkeiten. Das `?` ist unklar, könnte der Titel oder ein fehlender Parameter sein.
  3. `CREATELISTELEMENT(...)`: Fügt ein Element zu einer Liste hinzu (Ziel-Liste unklar, '1167' könnte eine ID sein?). Das _gerade erstellte_ Dokumentationsobjekt wird als eines der Elemente übergeben.
  4. `MODIFYOBJECT(...)`: Modifiziert das _Ergebnis_ von `CREATELISTELEMENT` (vermutlich das hinzugefügte Listenelement selbst) und setzt dessen `Description` und `Remark` basierend auf der ursprünglichen NOK-Zeile (`GETCURRENTROW()`).

### 3.5 Statusänderungen und Abschlussaktionen

**Beispiel 55: Workflow-Status ändern (Allgemein)**

```criteria
ChangeWorkflowState(This, 'Archived')
```

* **Erklärung:** Ändert den Workflow-Status des aktuellen Berichts (`This`) auf 'Archived'. Simpel, aber effektiv für den Abschluss von Prozessen.

**Beispiel 56: Meldung aktualisieren und für SAP-Upload markieren (SAP-Kontext)**

```criteria
Iif(GetMemberValue(GetMemberValue([Order], 'SAPOrderType'), 'SAPOrderType') = 'WP', 
    SetMemberValue(GetMemberValue([Order], 'HeaderNotification'), 'CompletionDate', [WorkingEnd]), 
    MODIFYOBJECT(GetMemberValue([Order], 'HeaderNotification'), 'CompletionDate', [CompletedDate],'RequiresSAPUpdate',true))
```

* **Erklärung:** Aktualisiert das Abschlussdatum (`CompletionDate`) der Kopfmeldung des Auftrags.
  * Prüft, ob der Auftragstyp 'WP' ist.
  * Wenn ja, wird das Abschlussdatum auf das Feld `WorkingEnd` gesetzt (`SetMemberValue`). Ein SAP-Upload wird hier _nicht_ explizit angestoßen.
  * Wenn nein (anderer Auftragstyp), wird das Abschlussdatum auf `CompletedDate` gesetzt UND die Meldung für den SAP-Upload markiert (`MODIFYOBJECT(..., 'RequiresSAPUpdate',true)`).

**Beispiel 57: Alle Operationen für SAP-Upload markieren (SAP-Kontext)**

```criteria
foreach(GetMemberValue([Order],'Operations'),'SetMemberValue(this,''RequiresSAPUpdate'',true)','1=1')
```

* **Erklärung:** Nutzt die `ForEach`-Funktion, um eine Aktion auf alle Elemente einer Liste anzuwenden.
  * `GetMemberValue([Order],'Operations')`: Holt die Liste der Operationen des Auftrags.
  * `'SetMemberValue(this,''RequiresSAPUpdate'',true)'`: Die Aktion, die auf jede Operation angewendet wird. `this` bezieht sich hier auf die einzelne Operation in der Schleife. Setzt deren `RequiresSAPUpdate`-Flag auf `true`.
  * `'1=1'`: Der Filter - bedeutet "alle Elemente" (keine Einschränkung).

**Beispiel 58: Equipment erstellen und konfigurieren (Fertigung/Anlagenmanagement)**

```criteria
Iif(GetValue('cbPaledoEquipment'), 
    ModifyObject(
        CreateEquipment([Equipment.Equipment], GetValue('lblName'), [FuncLoc], [FuncLoc.ComponentType], [FuncLoc.ProductionArea], GetValue('lblLocation'),this),
            'IHArbeitsplatz', [FuncLoc.IHArbeitsplatz], 
            'Hersteller', GetDBObject('SynX.Xaf.Paledo.Core.BusinessObjects.Organisation.BOCompany', Concat('[Name] = ''', GetValue('lblManufacturer'), '''')), 
            'Seriennummer', GetValue('lblSerialNo'), 
            'Description', GetValue('lblDescription')
    ), 
?)
```

* **Erklärung:** Wenn die Checkbox `cbPaledoEquipment` im Bericht aktiviert ist:
  1. `CreateEquipment(...)`: Erstellt ein neues Equipment-Objekt. Parameter wie Nummer (`[Equipment.Equipment]`), Name (`GetValue('lblName')`), übergeordneter Ort (`[FuncLoc]`), Typ etc. werden aus Berichtsfeldern oder dem Kontext geholt. `this` am Ende könnte eine Referenz auf den erstellenden Bericht sein.
  2. `ModifyObject(...)`: Modifiziert das _gerade erstellte_ Equipment sofort weiter.
     * Setzt den Instandhaltungsarbeitsplatz (`IHArbeitsplatz`).
     * Sucht das Hersteller-Objekt (`BOCompany`) anhand des Namens aus dem Berichtsfeld `lblManufacturer` (`GetDBObject(...)`) und weist es zu.
     * Setzt Seriennummer und Beschreibung aus Berichtsfeldern.
  3. Der Else-Teil (`?`) ist unvollständig, sollte `null` oder `''` sein.

**Beispiel 59: Statuswechsel eines verknüpften Objekts (Allgemein)**

```criteria
Iif(GetMemberValue([Event], 'WorkflowState') Is Null, '', ChangeWorkflowState([Event], 'Abgeschlossen'))
```

* **Erklärung:** Wenn das mit dem Bericht verknüpfte Ereignis (`[Event]`) einen Workflow-Status hat (also nicht `Null` ist), wird dessen Status auf 'Abgeschlossen' geändert.

**Beispiel 60: Fehlerbehandlung beim Setzen des Eckdatums (SAP-Kontext)**

```criteria
IIF(AddMinutes(AddHours(GV('Eckenddatum'),GetHour(GV('Eckendezeit'))),GetMinute(GV('Eckendezeit')))<[Order.BasicStart],'Fehler: Datum/Zeit -> Eck-Ende vor Eck-Start ','')
```

* **Erklärung:** Dieses Beispiel ist wahrscheinlich _Teil einer Validierungsregel_ oder eines _berechneten Hilfsfeldes_, nicht einer Abschlussformel mit Aktion. Es prüft, ob das eingegebene Eck-Ende vor dem Eck-Start liegt. Wenn ja, wird eine Fehlermeldung zurückgegeben, andernfalls ein leerer String. Dies hilft, ungültige Eingaben im Bericht zu verhindern, _bevor_ eine Abschlussformel wie Beispiel 50 versucht, die Daten zu setzen.

## Analyse und Erstellung eigener Skripte

(Dieser Abschnitt bleibt identisch zum vorherigen Entwurf, da die Tipps allgemeingültig sind.)

Die mitgelieferten Beispiele zeigen die Mächtigkeit des Low-Code Skriptings, können aber auch komplex wirken. Hier sind einige Tipps, wie Sie bestehende Skripte verstehen und eigene erstellen können:

## Fazit

Low-Code Skripting mit der XAF Criteria Language und Paledo Custom Functions bietet mächtige Werkzeuge zur Anpassung und Automatisierung Ihrer Berichtsprozesse. Auch wenn manche Beispiele komplex erscheinen, lassen sich durch strukturiertes Vorgehen und Nutzung der Referenzdokumentation auch anspruchsvolle Anforderungen umsetzen. Beginnen Sie mit einfachen berechneten Feldern und steigern Sie die Komplexität schrittweise. Viel Erfolg beim Skripten!