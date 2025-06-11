# Protokollvorlagen

### 1. Einführung in Protokollvorlagen

#### 1.1 Was ist eine Protokollvorlage?

Protokollvorlagen sind die digitale Grundlage für jede standardisierte Dokumentation in Paledo. Sie definieren, welche Informationen, Felder, Prüfungen und Messwerte bei einer Tätigkeit erfasst werden müssen. Inhaltlich und gestalterisch entsprechen sie einer qualitätsgesicherten Papiervorlage, die im System hinterlegt wird, um eine einheitliche und revisionssichere Datenerfassung zu gewährleisten.

#### 1.2 Abgrenzung: Protokollvorlage vs. Bericht

Es ist wichtig, zwischen diesen beiden Begriffen zu unterscheiden:

* **Protokollvorlage:** Dies ist die leere, freigegebene Master-Vorlage. Sie wird im Modul "Protokollvorlagen" zentral erstellt und verwaltet. Sie definiert das Layout und die Struktur der zu erfassenden Daten.
* **Bericht:** Dies ist die ausgefüllte "Kopie" einer Protokollvorlage. Wenn ein Anwender eine Dokumentation (z.B. im Rahmen eines Auftrags) durchführt, wird eine Instanz der Protokollvorlage als Bericht erzeugt und mit den Ist-Werten befüllt.

#### 1.3 Typische Einsatzbereiche

Protokollvorlagen sind vielseitig einsetzbar und bilden das Rückgrat der Dokumentation in Bereichen wie:

* **Instandhaltung:** Wartungs- und Inspektionsprotokolle, DGUV V3 Prüfungen, Reparaturberichte.
* **Produktion:** Maschinen-Checklisten im Rahmen von TPM, Schichtübergabeprotokolle, Qualitätskontrollen.
* **Arbeitssicherheit:** Dokumentation von Sicherheitsrundgängen, Erstellung von Gefährdungsbeurteilungen (GBU).

***

### 2. Grundlagen und Navigation

#### 2.1 Modulzugriff und Benutzeroberfläche

Sie finden das Modul "Protokollvorlagen" im Navigationsbereich unter der Gruppe **Berichtsverwaltung**.

Die Benutzeroberfläche ist als Master-Detail-Ansicht aufgebaut:

* **Linke Seite (Master-Liste):** Zeigt eine Liste aller verfügbaren Protokollvorlagen. Sie können hier suchen, filtern und eine Vorlage zur Bearbeitung auswählen.
* **Rechte Seite (Detail-Ansicht):** Zeigt die Details und Inhalte der in der Liste ausgewählten Vorlage an.

#### 2.2 Die Hauptansicht: Übersicht und Kopfdaten

Wenn Sie eine Vorlage in der Liste auswählen, werden auf der rechten Seite deren Kopfdaten angezeigt. Diese umfassen:

* **Protokoll-ID:** Eine eindeutige, vom System automatisch vergebene Nummer.
* **Name:** Eine aussagekräftige Bezeichnung der Vorlage.
* **Organisationseinheit:** Die Abteilung, die für diese Vorlage verantwortlich ist oder sie verwendet.
* **Beschreibung:** Ein Langtextfeld für detailliertere Erläuterungen.
* **Protokollüberschrift:** Ein alternativer Titel, der im finalen Bericht angezeigt werden kann.
* **Verantwortliche Person:** Der für die Vorlage zuständige Mitarbeiter.
* **Anlagenbereich / Gruppe:** Optionale Felder zur weiteren Strukturierung und Filterung.

#### 2.3 Reiter und ihre Funktionen

Unterhalb der Kopfdaten finden Sie eine Reihe von Reitern zur Verwaltung der Vorlage:

* **Revisionen:** Zeigt die gesamte Versionshistorie der Vorlage. Jede Änderung erzeugt eine neue Revision.
* **Anhänge:** Hier können Sie relevante Dokumente wie Bilder, technische Zeichnungen oder PDF-Dateien hinzufügen, die in der Vorlage verlinkt oder angezeigt werden sollen.
* **Log:** Protokolliert alle wichtigen Ereignisse wie Statusänderungen (z.B. Freigabe) und Kommentare.
* **Berechtigungen:** Ermöglicht die Einschränkung des Zugriffs auf spezifische Benutzer oder Rollen, zusätzlich zum Standard-Berechtigungssystem.
* **Eigenschaften:** Enthält weitere Konfigurationsoptionen, z.B. ob Anhänge in der Berichtsvorschau standardmäßig angezeigt werden sollen.

#### 2.4 Aktionen in der Ribbon-Leiste

Die Ribbon-Leiste am oberen Rand des Fensters bietet die zentralen Aktionen zur Verwaltung von Protokollvorlagen:

* **Neu:** Startet den Assistenten zur Erstellung einer neuen Vorlage.
* **Kopieren:** Dupliziert die aktuell ausgewählte Vorlage als Basis für eine neue.
* **Speichern / Speichern & Schließen:** Sichert Ihre Änderungen.
* **Löschen:** Markiert eine Vorlage als storniert (siehe Kapitel 3.4).
* **Freigabe / Korrektur:** Startet den Workflow zur Freigabe oder Überarbeitung einer Vorlage.
* **Extras:** Bietet Funktionen zum Export und Import von Vorlagen, z.B. für den Austausch zwischen Test- und Produktivsystemen.

***

### 3. Erstellung und Verwaltung von Protokollvorlagen

#### 3.1 Eine neue Protokollvorlage anlegen

1. Klicken Sie in der Ribbon-Leiste auf **Neu**.
2. Es öffnet sich der **Berichtsassistent**. Hier haben Sie zwei Möglichkeiten:
   * **Mit PDF-Hintergrund starten:** Sie können eine bestehende PDF-Datei (z.B. ein altes Papierformular) hochladen. Paledo konvertiert die Seiten in Hintergrundbilder, die Sie als Layout-Grundlage verwenden können.
   * **Mit leerer Vorlage starten:** Sie beginnen mit einer leeren Seite und gestalten die Vorlage von Grund auf neu.
3. Vergeben Sie einen aussagekräftigen **Namen** für Ihre Vorlage und klicken Sie auf **Fertig**.
4. Der **Protokoll-Designer** wird automatisch geöffnet, in dem Sie die Vorlage gestalten können.

#### 3.2 Revisionsverwaltung und Freigabeprozess

Paledo stellt durch ein strenges Revisionssystem sicher, dass nur qualitätsgesicherte Vorlagen verwendet werden. Jede Vorlage durchläuft dabei verschiedene Status:

* **In Bearbeitung:** Dies ist der initiale Status einer neuen Revision. In diesem Zustand kann die Vorlage frei bearbeitet werden.
* **Freigegeben:** Nach Abschluss der Bearbeitung muss eine Revision explizit freigegeben werden. Eine freigegebene Revision ist schreibgeschützt und kann produktiv zur Dokumentation verwendet werden.
* **Veraltet:** Sobald eine neue Revision einer bereits freigegebenen Vorlage erstellt und ebenfalls freigegeben wird, erhält die vorherige Revision automatisch den Status "Veraltet". Sie kann nicht mehr für neue Berichte verwendet werden, bleibt aber aus Nachverfolgbarkeitsgründen im System erhalten.

**Der Freigabeprozess:**

1. Nachdem Sie eine Vorlage fertig gestaltet haben, speichern Sie diese und schließen den Designer.
2. Wählen Sie die Vorlage in der Übersicht aus und klicken Sie auf die Aktion **Freigabe**.
3. Bestätigen Sie den Dialog. Die aktuelle Revision wechselt den Status auf "Freigegeben". Die Felder `Freigabedatum` und `Freigegeben von` werden automatisch mit den entsprechenden Informationen befüllt.

#### 3.3 Protokollvorlagen kopieren

Wenn Sie eine neue Vorlage erstellen möchten, die einer bestehenden sehr ähnlich ist, nutzen Sie die Funktion **Kopieren**. Dies dupliziert die ausgewählte Vorlage inklusive ihres Layouts und ihrer Felder. Die Kopie wird als neue Vorlage mit einer neuen ID und einer neuen ersten Revision im Status "In Bearbeitung" angelegt.

#### 3.4 Protokollvorlagen stornieren (löschen)

Aus Gründen der Nachvollziehbarkeit werden Protokollvorlagen in Paledo nicht physisch aus der Datenbank gelöscht. Stattdessen werden sie "storniert".

1. Wählen Sie die zu löschende Vorlage aus und klicken Sie auf **Löschen**.
2. Die Vorlage wird als "storniert" markiert und verschwindet aus der Standardansicht.
3. Um stornierte Vorlagen anzuzeigen oder wiederherzustellen, nutzen Sie den Filter in der Suchleiste und aktivieren Sie die Option **Stornierte Anzeigen**.

#### 3.5 Berechtigungen verwalten

Standardmäßig richtet sich der Zugriff auf Protokollvorlagen nach dem allgemeinen Rollenkonzept von Paledo. Im Reiter **Berechtigungen** können Sie jedoch für eine spezifische Vorlage den Zugriff weiter einschränken. Die hier definierten Berechtigungen wirken **additiv**, d.h. nur die hier eingetragenen Benutzer oder Rollen haben Zugriff, zusätzlich zu ihren allgemeinen Rechten.

***

### 4. Der Protokoll-Designer: Gestaltung im Detail

#### 4.1 Übersicht der Benutzeroberfläche

Der Designer ist das Herzstück der Vorlagenerstellung. Er ist in mehrere Bereiche aufgeteilt:

* **Menüleiste (oben):** Bietet Aktionen wie Speichern, Formatierungsoptionen für Schriftarten und die Protokoll-Vorschau.
* **Werkzeugleiste (links):** Enthält alle verfügbaren Designelemente (Textbox, Checkbox, Bild etc.), die Sie per Drag & Drop auf der Zeichenfläche platzieren können.
* **Zeichenfläche (Mitte):** Der Hauptbereich, in dem Sie das Layout Ihrer Vorlage gestalten.
* **Berichtsexplorer (rechts):** Zeigt die hierarchische Struktur aller Elemente in Ihrer Vorlage.
* **Liste der Felder (rechts):** Bietet Zugriff auf alle Datenfelder aus dem Paledo-System (z.B. aus Aufträgen, Anlagen), die Sie per Drag & Drop in die Vorlage ziehen können.
* **Eigenschaftenfenster (unten rechts):** Zeigt alle konfigurierbaren Eigenschaften des aktuell ausgewählten Elements an.

#### 4.2 Grundlegende Designelemente

* **Textbox:** Das vielseitigste Element. Es kann als statische Beschriftung oder als Eingabefeld für Text, Zahlen oder Daten dienen.
* **Kontrollkästchen (Checkbox):** Für Ja/Nein-Abfragen oder als Teil einer Auswahlliste.
* **Bild:** Zum Einfügen von statischen Bildern (Logos, Grafiken) oder als Platzhalter für ein bei der Dokumentation aufzunehmendes Foto.
* **Freihandfeld:** Wird für digitale Unterschriften verwendet.
* **Tabelle:** Zur strukturierten Anordnung von Daten in Zeilen und Spalten.

#### 4.3 Wichtige Eigenschaften der Designelemente

Für jedes Element können Sie im Eigenschaftenfenster spezifische Verhaltensweisen definieren. Die wichtigsten sind:

* **Bearbeitungsmodus:** Definiert, wie ein Element im späteren Bericht funktioniert.
  * **Statisch:** Das Element ist eine reine Beschriftung und kann nicht bearbeitet werden (z.B. eine Überschrift).
  * **Ist-Wert:** Dies ist der Standard für ein Eingabefeld. Der Anwender kann hier bei der Dokumentation einen Wert eintragen.
  * **Soll-Wert:** Ermöglicht die Definition eines vordefinierten Wertes, der je nach Verwendungskontext (z.B. in verschiedenen Berichtsvorlagen) variieren kann.
  * **Abnahme:** Das Feld kann nur im nachgelagerten Abnahmeprozess eines Berichts befüllt werden.
* **Pflichtfeld:** Wenn aktiviert, muss der Anwender dieses Feld ausfüllen, bevor der Bericht abgeschlossen werden kann.
* **Markiert Befund:** Eine Eingabe in einem so markierten Feld wird automatisch als Abweichung oder Befund gewertet, was zu Folgeprozessen führen kann.
* **Datentyp:** Legt fest, welche Art von Daten erwartet wird (z.B. Text, Ganzzahl, Fließkommazahl, Datum/Uhrzeit). Dies ermöglicht Validierungen.
* **Minimum / Maximum:** Für Zahlenwerte können hier Toleranzgrenzen definiert werden. Werte außerhalb dieses Bereichs werden als Abweichung gewertet.
* **Thesaurus:** Ein Textfeld kann an einen vordefinierten Katalog (Thesaurus) angebunden werden, um eine Auswahlliste (Dropdown) zu erzeugen.

#### 4.4 Datenbindung: Felder mit Systemdaten verknüpfen

Datenbindung ermöglicht es, Felder in Ihrer Vorlage automatisch mit Daten aus Paledo vorzubelegen.

* **Zugriff:** Markieren Sie ein Element und klicken Sie auf das kleine **Zahnrad-Symbol** daneben, oder ziehen Sie ein Feld direkt aus der **Liste der Felder** (rechts) auf die Zeichenfläche.
* **Anwendungsbeispiel:** Sie können ein Textfeld an das Feld `Auftragsnummer` binden. Im späteren Bericht wird dann automatisch die Nummer des zugehörigen Auftrags in diesem Feld angezeigt.

#### 4.5 Spezialfunktionen im Designer

* **Radiogruppe (für Kontrollkästchen):** Wenn Sie mehrere Checkboxen zu einer Gruppe zusammenfassen möchten, aus der nur eine Option ausgewählt werden kann (z.B. für eine "Ja/Nein/NV"-Abfrage), tragen Sie für alle betreffenden Checkboxen denselben Text in der Eigenschaft **Radiogruppe** ein.
* **Formatierungsregeln:** Mit dieser mächtigen Funktion können Sie das Aussehen von Elementen dynamisch an Bedingungen knüpfen.
  * **Definition:** Sie definieren eine Regel (z.B. "Wenn der Wert in `Textfeld1` gleich 'NOK' ist") und eine daraus resultierende Formatierung (z.B. "setze die Hintergrundfarbe auf Rot").
  * **Anwendung:** Weisen Sie diese Regel einem oder mehreren Elementen zu. Dies ist ideal für visuelles Feedback, z.B. bei der Hervorhebung von Abweichungen.
* **Zauberstab-Funktion:** Wenn Sie ein PDF als Hintergrund verwenden, kann Paledo versuchen, die darin enthaltenen Formularfelder automatisch zu erkennen.
  * **Aktivierung:** Wählen Sie in der Ribbon-Leiste den gewünschten Feldtyp (Text, Kontrollkästchen, Freihand) und halten Sie die `Alt`-Taste gedrückt.
  * **Anwendung:** Der Mauszeiger verwandelt sich in ein Kreuz. Klicken Sie auf ein Feld im PDF-Hintergrund. Paledo platziert an dieser Stelle ein entsprechendes, editierbares Designelement.
* **Protokoll-Vorschau:** Mit dieser Funktion können Sie jederzeit testen, wie Ihre Vorlage bei der späteren Dokumentation aussehen und sich verhalten wird. Sie können zwischen einer **Ist-Wert-** und einer **Soll-Wert-Vorschau** wählen.

***

### 5. Praktische Anwendung und Best Practices

#### 5.1 Anwendungsbeispiel: Erstellung eines Wartungsprotokolls

1. **Anlegen:** Erstellen Sie eine neue, leere Protokollvorlage mit dem Namen "Monatliche Prüfung Förderband FB-01".
2. **Struktur:** Fügen Sie statische Textfelder für Überschriften hinzu: "Allgemeine Daten", "Mechanische Prüfung", "Elektrische Prüfung" und "Abschluss".
3. **Allgemeine Daten:** Fügen Sie Textfelder hinzu und binden Sie diese an die Datenquellen `Anlagenbezeichnung`, `Auftragsnummer` und `Aktueller Benutzer`.
4. **Mechanische Prüfung:** Fügen Sie mehrere Kontrollkästchen hinzu und beschriften Sie diese (z.B. "Lager auf Spiel geprüft", "Kette gespannt"). Definieren Sie eine **Radiogruppe** für jede Prüfung mit den Optionen "OK" und "NOK".
5. **Elektrische Prüfung:** Fügen Sie ein Textfeld "Motorstromaufnahme \[A]" hinzu. Setzen Sie den **Datentyp** auf "Fließkommazahl" und definieren Sie **Minimum** und **Maximum** als Toleranzgrenzen.
6. **Abschluss:** Fügen Sie ein großes Textfeld für "Bemerkungen" und ein **Freihandfeld** für die "Unterschrift des Prüfers" hinzu. Markieren Sie das Unterschriftenfeld als **Pflichtfeld**.
7. **Freigabe:** Speichern und schließen Sie den Designer und geben Sie die erste Revision der Vorlage frei.

#### 5.2 Tipps für die Praxis

* **Systematische Benennung:** Geben Sie allen Eingabefeldern im Eigenschaftsfenster einen aussagekräftigen Namen (z.B. `pruefung_lager_spiel` statt `checkbox1`). Dies erleichtert die Erstellung von Formatierungsregeln und die spätere Datenauswertung.
* **Wiederverwendbarkeit:** Erstellen Sie generische Vorlagen, die für mehrere ähnliche Anlagen oder Tätigkeiten verwendet werden können. Anlagenspezifische Daten sollten über Datenbindung gefüllt werden.
* **Klarheit vor Komplexität:** Eine gut strukturierte, einfache Vorlage führt zu einer besseren Datenqualität als ein überladenes Formular. Nutzen Sie Überschriften und Abstände, um die Lesbarkeit zu verbessern.
* **Kataloge nutzen:** Vermeiden Sie Freitexteingaben, wo immer es möglich ist. Binden Sie Felder an Kataloge (Thesaurus), um standardisierte und auswertbare Eingaben zu erhalten.
* **Performance:** Vermeiden Sie die Verwendung von übermäßig großen Bildern oder hochauflösenden, mehrseitigen PDFs als Hintergrund, da dies die Ladezeiten im Designer und in der mobilen Anwendung beeinträchtigen kann.