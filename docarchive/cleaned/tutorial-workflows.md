# Arbeitsabläufe Tutorial

Dieses Tutorial führt Sie Schritt für Schritt durch die Erstellung eines praxisnahen Workflows für Paledo Berichte (Business-Objekt `BORecord`). Am Ende dieses Tutorials haben Sie einen voll funktionsfähigen "Record Workflow" konfiguriert, der typische Phasen eines Berichtsprozesses abbildet, von der Erstellung bis zur Abnahme.

**Lernziele:**

* Verständnis für die grundlegenden Komponenten eines Paledo Workflows.
* Erstellen und Konfigurieren einer State Machine.
* Definieren von Workflow-Zuständen (States).
* Einrichten von Übergängen (Transitionen) zwischen Zuständen.
* Implementieren von automatisierten Aktionen (State Actions) bei Zustandseintritt.
* Anwenden von bedingter Logik in Transitionen.

**Voraussetzungen:**

* Zugriff auf die Paledo Administrationsoberfläche.
* Grundlegendes Verständnis der Paledo-Navigation.
* Kenntnisse der XAF Criteria Language sind hilfreich für das Verständnis der State Actions, werden aber im Tutorial erklärt.

***

## 1. Einleitung: Der Record Workflow

Paledo Berichte (`BORecord`) durchlaufen oft einen mehrstufigen Prozess von der ersten Erfassung über die Bearbeitung bis hin zur finalen Genehmigung oder Abnahme. Dieser Workflow dient dazu, diesen Prozess zu strukturieren, zu automatisieren und nachvollziehbar zu machen.

Der "Record Workflow", den wir in diesem Tutorial erstellen, wird folgende Hauptphasen umfassen:

1. **Eröffnet**: Ein neuer Bericht wird erstellt.
2. **Eingeplant**: Der Bericht wird einem Verantwortlichen zugewiesen und für die Bearbeitung eingeplant.
3. **In Arbeit**: Der Bericht wird aktiv bearbeitet.
4. **Abgeschlossen**: Die Bearbeitung des Berichts ist abgeschlossen.
5. **Abgenommen**: Der Bericht wurde geprüft und final freigegeben.
6. **Storniert**: Der Bericht wird nicht weiter bearbeitet.

Wir werden lernen, wie man diese Zustände anlegt, die Übergänge zwischen ihnen definiert und automatische Aktionen einrichtet, die z.B. Felder setzen oder verknüpfte Objekte beeinflussen.

***

## 2. Vorbereitung: Die Workflow-Struktur planen

Bevor wir mit der Konfiguration beginnen, visualisieren wir den geplanten Ablauf. Dies hilft, den Überblick zu behalten.

***

## 3. Schritt 1: Neuen Arbeitsablauf (State Machine) anlegen

Zuerst erstellen wir die übergeordnete State Machine für unsere Berichte.

1. Navigieren Sie im Paledo Desktop Client zu **Administration** -> **Arbeitsabläufe** -> **Arbeitsabläufe**.
2. Klicken Sie im Ribbon-Menü auf **Neu**. Es öffnet sich die Detailansicht für einen neuen Arbeitsablauf.
3. Füllen Sie die Felder im Kopfbereich "Arbeitsablauf" wie folgt aus:
   * **Name**: `Record Workflow`
   * **Aktiv**: Setzen Sie den Haken (aktivieren Sie die Checkbox).
   * **Objekttyp**: Wählen Sie `SynX.Xaf.Paledo.Core.BusinessObjects.Reporting.BORecord` aus der Liste.

*   **Status-Eigenschaft**: Tragen Sie `WorkflowState` ein. Dies ist das Standardfeld in Paledo-Objekten, das den aktuellen Workflow-Zustand speichert.
*   **Anfangsstatus**: Lassen Sie dieses Feld vorerst leer. Wir definieren den Anfangszustand, nachdem wir die Zustände erstellt haben.

4\. Klicken Sie auf **Speichern**.

***

## 4. Schritt 2: Workflow-Zustände definieren

Jetzt legen wir die einzelnen Zustände (Workflow States) an, die ein Bericht durchlaufen kann. Wir tun dies direkt in der Detailansicht unseres "Record Workflow" im Tab "Status".

Für jeden Zustand klicken Sie auf **Neu** in der Werkzeugleiste des "Status"-Tabs.

### Zustand 1: Eröffnet

1. **Name**: `Eröffnet`
2. **Marker**: `Eröffnet` (Es ist üblich, Name und Marker identisch zu halten)
3. **TaskStatusDefinition**: `10` (entspricht oft "Offen" oder "In Bearbeitung" in Planungsansichten)
4. Klicken Sie auf **Speichern und Schließen**.

### Zustand 2: Eingeplant

1. **Name**: `Eingeplant`
2. **Marker**: `Eingeplant`
3. **TaskStatusDefinition**: `10`
4. Klicken Sie auf **Speichern und Schließen**.

### Zustand 3: In Arbeit

1. **Name**: `In Arbeit`
2. **Marker**: `In Arbeit`
3. **TaskStatusDefinition**: `10`
4. Klicken Sie auf **Speichern und Schließen**.

### Zustand 4: Abgeschlossen

1. **Name**: `Abgeschlossen`
2. **Marker**: `Abgeschlossen`
3. **TaskStatusDefinition**: `20` (entspricht oft "Erledigt")
4. Klicken Sie auf **Speichern und Schließen**.

### Zustand 5: Abgenommen

1. **Name**: `Abgenommen`
2. **Marker**: `Abgenommen`
3. **TaskStatusDefinition**: `30` (entspricht oft "Genehmigt" oder "Bestätigt")
4. Klicken Sie auf **Speichern und Schließen**.

### Zustand 6: Storniert

1. **Name**: `Storniert`
2. **Marker**: `Storniert`
3. **TaskStatusDefinition**: `40` (entspricht oft "Abgebrochen")
4. Klicken Sie auf **Speichern und Schließen**.

Nachdem alle Zustände erstellt wurden, gehen Sie zurück zur Hauptkonfiguration des Arbeitsablaufs "Record Workflow" (Schritt 1, Punkt 3).\
Wählen Sie nun im Feld **Anfangsstatus** den Zustand **"Eröffnet"** aus.\
Klicken Sie auf **Speichern**.

***

## 5. Schritt 3: Transitionen (Zustandsübergänge) konfigurieren

Transitionen definieren, von welchem Zustand in welchen anderen gewechselt werden darf. Wir konfigurieren diese für jeden unserer Ausgangszustände. Öffnen Sie dazu nacheinander jeden Zustand per Doppelklick aus der "Status"-Liste Ihres "Record Workflows" und wechseln Sie in den Tab "Transitionen".

### Transitionen von "Eröffnet"

Öffnen Sie den Zustand **"Eröffnet"**. Im Tab "Transitionen" klicken Sie zweimal auf **Neu**, um zwei Transitionen zu erstellen:

1. **Transition 1: Einplanen**
   * **Name**: `Einplanen`
   * **Zielstatus**: Wählen Sie `Eingeplant`
   * **Bestätigung**: Setzen Sie den Haken (Checkbox aktivieren). Der Benutzer muss den Übergang bestätigen.!
   * Klicken Sie auf **Speichern und Schließen**.
2. **Transition 2: Stornieren**
   * **Name**: `Storniert`
   * **Zielstatus**: Wählen Sie `Storniert`
   * **Bestätigung**: Haken setzen.
   * Klicken Sie auf **Speichern und Schließen**.

Schließen Sie die Detailansicht des Zustands "Eröffnet".

### Transitionen von "Eingeplant"

Öffnen Sie den Zustand **"Eingeplant"**. Im Tab "Transitionen" klicken Sie auf **Neu**:

1. **Transition 1: In Arbeit nehmen**
   * **Name**: `In Arbeit` (oder "In Arbeit nehmen" für mehr Klarheit im UI)
   * **Zielstatus**: Wählen Sie `In Arbeit`
   * **Bestätigung**: Haken setzen.
   * Klicken Sie auf **Speichern und Schließen**.

Schließen Sie die Detailansicht des Zustands "Eingeplant".

### Transitionen von "In Arbeit"

Öffnen Sie den Zustand **"In Arbeit"**. Im Tab "Transitionen" klicken Sie auf **Neu**:

1. **Transition 1: Abschließen**
   * **Name**: `Abschließen`
   * **Zielstatus**: Wählen Sie `Abgeschlossen`
   * **Bestätigung**: Haken setzen.
   * Klicken Sie auf **Speichern und Schließen**.

Schließen Sie die Detailansicht des Zustands "In Arbeit".

### Transitionen von "Abgeschlossen"

Öffnen Sie den Zustand **"Abgeschlossen"**. Im Tab "Transitionen" klicken Sie dreimal auf **Neu**:

1. **Transition 1: Abnehmen**
    * **Name**: `Abnehmen`
    * **Zielstatus**: Wählen Sie `Abgenommen`
    * **Berechtigungskriterium**: `Not Contains([Event.EventTask.Name], 'SP')`
      * Dieses Kriterium sorgt dafür, dass die Transition "Abnehmen" nur angezeigt wird, wenn der Name der verknüpften Aufgabe (`Event.EventTask.Name`) **nicht** "SP" enthält. Dies ermöglicht kontextabhängige Transitionen.
    * **Bestätigung**: Haken setzen.
    * Klicken Sie auf **Speichern und Schließen**.

2. **Transition 2: SP Abnehmen**
    * **Name**: `SP Abnehmen`
    * **Zielstatus**: Wählen Sie `Abgenommen`
    * **Berechtigungskriterium**: `Contains([Event.EventTask.Name], 'SP')`
        * Diese Transition wird nur angezeigt, wenn der Name der verknüpften Aufgabe "SP" enthält.
    * **Bestätigung**: Haken setzen.
    * Klicken Sie auf **Speichern und Schließen**.

3. **Transition 3: Korrektur** (Dies ist eine Rückwärts-Transition)\
    * **Name**: `Korrektur`
    * **Zielstatus**: Wählen Sie `In Arbeit`
    * **Bestätigung**: Haken setzen.
    * Klicken Sie auf **Speichern und Schließen**.

Schließen Sie die Detailansicht des Zustands "Abgeschlossen".

***

## 6. Schritt 4: State Actions (Automatisierte Aktionen) implementieren

State Actions werden ausgeführt, wenn ein Objekt in einen bestimmten Zustand eintritt. Wir konfigurieren diese nun für unsere Zustände. Öffnen Sie dazu nacheinander die relevanten Zustände per Doppelklick und wechseln Sie in den Tab "Aktionen".

### Aktionen für Zustand "Eingeplant"

Öffnen Sie den Zustand **"Eingeplant"**. Im Tab "Aktionen" klicken Sie auf **Neu**:

1. **Aktion 1: Verantwortlichen setzen, falls leer**
   * **Feldname**: `ResponsiblePerson`
   * **Ausdruck**: `Coalesce([ResponsiblePerson], GetObject(CurrentUser(), [this]))`
     * _Erklärung_: `Coalesce` prüft, ob `ResponsiblePerson` bereits einen Wert hat. Wenn ja, bleibt dieser. Wenn leer, wird der aktuelle Benutzer (`CurrentUser()`) als Verantwortlicher gesetzt. `GetObject(CurrentUser(), [this])` stellt sicher, dass der Benutzer als Objekt korrekt zugewiesen wird.
   * **Resultat** (wird automatisch generiert): `[ResponsiblePerson] = Coalesce([ResponsiblePerson], GetObject(CurrentUser(), [this]))`
   * Klicken Sie auf **Speichern und Schließen**.

Schließen Sie die Detailansicht des Zustands "Eingeplant".

### Aktionen für Zustand "In Arbeit"

Öffnen Sie den Zustand **"In Arbeit"**. Im Tab "Aktionen" klicken Sie dreimal auf **Neu**:

1. **Aktion 1: Ausführungsdatum setzen**
   * **Feldname**: `ExecutionDate`
   * **Ausdruck**: `LocalDateTimeNow()`
   * **Position**: `0` (Erste Aktion)
   * Klicken Sie auf **Speichern und Neu**.
2. **Aktion 2: Verknüpften Auftrag auf "In Arbeit" setzen**
   * **Feldname**: (leer lassen, da es eine Funktionsaktion ist)
   * **Ausdruck**: `Iif([Order] Is Null, '', ChangeWorkflowState([Order], 'In Arbeit'))`
     * _Erklärung_: Wenn das Feld `Order` (Verknüpfung zu einem Auftrag) nicht leer ist (`Is Null` ergibt `false`, also `[Order] Is Null` ist `false`), dann wird der Status des verknüpften Auftrags auf "In Arbeit" gesetzt. Sonst passiert nichts (`''`).
   * **Position**: `1`
   * Klicken Sie auf **Speichern und Neu**.
3. **Aktion 3: Feld "Documented" auf False setzen**
   * **Feldname**: `Documented`
   * **Ausdruck**: `False`
   * **Position**: `2`
   * Klicken Sie auf **Speichern und Schließen**.

Schließen Sie die Detailansicht des Zustands "In Arbeit".

### Aktionen für Zustand "Abgeschlossen"

Öffnen Sie den Zustand **"Abgeschlossen"**. Im Tab "Aktionen" klicken Sie dreimal auf **Neu**:

1. **Aktion 1: Ausführungsdatum setzen (falls noch leer)**
   * **Feldname**: `ExecutionDate`
   * **Ausdruck**: `Coalesce([ExecutionDate], LocalDateTimeNow())`
   * **Position**: `0`
   * Klicken Sie auf **Speichern und Neu**.
2. **Aktion 2: Verknüpften Auftrag abschließen, wenn alle Berichte abgeschlossen**
   * **Feldname**: (leer lassen)
   * **Ausdruck**: `IIF(not ISNULLOREMPTY([Order]) and [Order].[Records].Count()=[Order].[Records][WorkflowState.Name='Abgeschlossen'].Count() and [Order].[WorkflowState].[Name]<>'Abgeschlossen', CHANGEWORKFLOWSTATE([Order], 'Abgeschlossen'), '')`
     * _Erklärung_:
       * `not ISNULLOREMPTY([Order])`: Prüft, ob ein Auftrag verknüpft ist.
       * `[Order].[Records].Count()`: Zählt alle Berichte dieses Auftrags.
       * `[Order].[Records][WorkflowState.Name='Abgeschlossen'].Count()`: Zählt alle Berichte dieses Auftrags, die im Status "Abgeschlossen" sind.
       * `[Order].[WorkflowState].[Name]<>'Abgeschlossen'`: Prüft, ob der Auftrag selbst noch nicht "Abgeschlossen" ist.
       * Wenn alle diese Bedingungen wahr sind, wird der Status des Auftrags auf "Abgeschlossen" gesetzt.
   * **Position**: `1`
   * Klicken Sie auf **Speichern und Neu**.
3. **Aktion 3: Feld "Documented" auf True setzen**
   * **Feldname**: `Documented`
   * **Ausdruck**: `True`
   * **Position**: `2`
   * Klicken Sie auf **Speichern und Schließen**.

Schließen Sie die Detailansicht des Zustands "Abgeschlossen".

### Aktionen für Zustand "Abgenommen"

Öffnen Sie den Zustand **"Abgenommen"**. Im Tab "Aktionen" klicken Sie auf **Neu**:

1. **Aktion 1: Feld "Approved" auf True setzen**
   * **Feldname**: `Approved`
   * **Ausdruck**: `True`
   * Klicken Sie auf **Speichern und Schließen**.

Schließen Sie die Detailansicht des Zustands "Abgenommen".

### Aktionen für Zustand "Storniert"

Öffnen Sie den Zustand **"Storniert"**. Im Tab "Aktionen" klicken Sie zweimal auf **Neu**:

1. **Aktion 1: Verknüpften Auftrag auf "Storniert" setzen**
   * **Feldname**: (leer lassen)
   * **Ausdruck**: `Iif([Order] Is Null, '', ChangeWorkflowState([Order], 'Storniert'))`
   * **Position**: `0`
   * Klicken Sie auf **Speichern und Neu**.
2. **Aktion 2: Feld "Cancelled" auf True setzen**
   * **Feldname**: `Cancelled`
   * **Ausdruck**: `True`
   * **Position**: `1`
   * Klicken Sie auf **Speichern und Schließen**.

Schließen Sie die Detailansicht des Zustands "Storniert".

***

## 7. Schritt 5: Den Workflow testen

Nachdem der Workflow konfiguriert ist, ist es unerlässlich, ihn gründlich zu testen.

1. **Erstellen Sie einen neuen Bericht** (`BORecord`). Er sollte im Status "Eröffnet" starten.
2. **Durchlaufen Sie alle Transitionen**:
   * Klicken Sie auf "Einplanen". Prüfen Sie, ob der Status zu "Eingeplant" wechselt und ob der `ResponsiblePerson` gesetzt wurde (falls er leer war).
   * Klicken Sie auf "In Arbeit nehmen". Prüfen Sie den Statuswechsel und ob `ExecutionDate` gesetzt wurde. Wenn Sie den Bericht mit einem Test-Auftrag verknüpft haben, prüfen Sie, ob dessen Status auch auf "In Arbeit" gewechselt ist. Das Feld `Documented` sollte `False` sein.
   * Klicken Sie auf "Abschließen". Prüfen Sie Statuswechsel, ggf. `ExecutionDate` und ob `Documented` nun `True` ist. Testen Sie die Logik mit dem verknüpften Auftrag (erstellen Sie ggf. mehrere Berichte für einen Auftrag und schließen Sie sie nacheinander ab).
   * Testen Sie die bedingten Transitionen "Abnehmen" und "SP Abnehmen", indem Sie einem Testbericht eine Aufgabe zuweisen, deren Name einmal "SP" enthält und einmal nicht.
   * Testen Sie die "Korrektur"-Transition von "Abgeschlossen" zurück zu "In Arbeit".
   * Testen Sie die "Storniert"-Transition.
3. **Überprüfen Sie die State Actions**: Stellen Sie sicher,
   * dass Felder korrekt gesetzt werden.
   * dass Datumsstempel plausibel sind.
   * dass verknüpfte Objekte wie erwartet beeinflusst werden.
4. **Prüfen Sie Fehlermeldungen**: Wenn Transitionen nicht erscheinen oder Aktionen fehlschlagen, prüfen Sie die Paledo-Logs und Ihre Konfiguration (Syntax der Criteria, Feldnamen).

***

## 8. Zusammenfassung und nächste Schritte

Herzlichen Glückwunsch! Sie haben erfolgreich einen mehrstufigen Workflow für Paledo Berichte konfiguriert. Sie haben gelernt, wie man Zustände, Transitionen und automatisierte State Actions erstellt und bedingte Logik anwendet.

**Mögliche nächste Schritte zur Erweiterung dieses Workflows könnten sein:**

* **Rollenberechtigungen**: Definieren Sie, welche Benutzerrollen welche Transitionen ausführen dürfen.
* **Bedingungen für Zustandseintritt**: Fügen Sie z.B. eine Pflichtbegründung für die Transition "Storniert" hinzu.
* **Validierungsregeln**: Stellen Sie sicher, dass bestimmte Felder ausgefüllt sind, bevor ein Bericht z.B. abgeschlossen werden kann.
* **Appearance Rules**: Passen Sie die Benutzeroberfläche an, z.B. indem Sie Felder im Zustand "Abgenommen" schreibschützen.
* **Benachrichtigungen**: (Fortgeschritten) Konfigurieren Sie E-Mail-Benachrichtigungen bei bestimmten Zustandswechseln (oft über erweiterte Custom Functions oder Paledo-Systemeinstellungen).

Dieser "Record Workflow" bildet eine solide Grundlage. Passen Sie ihn weiter an die spezifischen Bedürfnisse und Prozesse Ihres Unternehmens an.