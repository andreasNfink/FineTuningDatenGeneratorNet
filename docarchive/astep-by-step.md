---
icon: shoe-prints
---

# Step-by-Step

### Schritt-für-Schritt-Anleitung: Erstellung eines Wartungsprotokolls

In dieser Anleitung erstellen wir gemeinsam eine Protokollvorlage für eine **"Monatliche Sicherheitsprüfung für das Förderband FB-01"**. Diese Vorlage wird alle wichtigen Elemente enthalten: automatisch befüllte Kopfdaten, eine Checkliste mit Prüfpunkten, ein Feld zur Erfassung eines Messwerts mit Toleranzprüfung sowie ein obligatorisches Unterschriftenfeld.

**Voraussetzungen:** Sie haben Zugriff auf das Paledo-Modul "Protokollvorlagen" und verfügen über die notwendigen Berechtigungen zum Erstellen und Bearbeiten von Vorlagen.

***

#### Schritt 1: Die Protokollvorlage anlegen

Zuerst legen wir die leere Hülle für unsere Vorlage an.

1. Öffnen Sie das Modul **Protokollvorlagen** über die Navigation (`Berichtsverwaltung` > `Protokollvorlagen`).
2. Klicken Sie in der Ribbon-Leiste auf die Aktion **Neu**. Der Berichtsassistent öffnet sich.
3. Wählen Sie die Option, mit einer **leeren Vorlage** zu starten.
4. Geben Sie im Feld **Name** den Titel `Monatliche Sicherheitsprüfung Förderband FB-01` ein.
5. Klicken Sie auf **Fertig**. Der Protokoll-Designer wird nun geöffnet.

**Ergebnis:** Sie befinden sich im leeren Designer und können mit der Gestaltung beginnen.

#### Schritt 2: Die Grundstruktur mit Überschriften schaffen

Eine gute Struktur verbessert die Lesbarkeit. Wir fügen daher zuerst statische Überschriften für die einzelnen Bereiche hinzu.

1. Ziehen Sie ein **Textbox**-Element aus der Werkzeugleiste (links) auf die Zeichenfläche.
2. Doppelklicken Sie in die Textbox und schreiben Sie `Allgemeine Daten`.
3. Markieren Sie den Text und formatieren Sie ihn über die Menüleiste **Fett**.
4. Wiederholen Sie diesen Vorgang für die weiteren Überschriften:
   * `Prüfpunkte`
   * `Messwerte`
   * `Abschluss`

**Ergebnis:** Ihre Vorlage hat nun eine klare Gliederung, die Sie im nächsten Schritt mit Feldern füllen.

#### Schritt 3: Kopfdaten mit Datenbindung befüllen

In diesem Schritt fügen wir Felder hinzu, die Paledo später automatisch aus dem Auftrag oder den Benutzerdaten befüllen wird.

1. Fügen Sie unter die Überschrift `Allgemeine Daten` drei statische Textboxen als Beschriftung ein: `Anlage:`, `Geprüft am:` und `Geprüft von:`.
2. Ziehen Sie nun aus der **Liste der Felder** (rechts) die folgenden Datenfelder neben die entsprechenden Beschriftungen:
   * `Anlagenbezeichnung` (aus dem Bereich _Record_)
   * `Erstellt am` (aus dem Bereich _Record_)
   * `Verantwortlicher Mitarbeiter > Name` (aus dem Bereich _Record_)
3. Paledo legt automatisch Textboxen an, die bereits mit der korrekten Datenquelle verknüpft sind.

**Ergebnis:** Der Kopfbereich der Vorlage wird bei der späteren Dokumentation automatisch mit den Stammdaten der Anlage, dem aktuellen Datum und dem Namen des Prüfers befüllt.

#### Schritt 4: Die Checkliste mit Prüfpunkten erstellen

Jetzt erstellen wir das Kernstück: eine interaktive Checkliste mit "OK"- und "NOK"-Optionen.

1. Fügen Sie unter der Überschrift `Prüfpunkte` eine statische Textbox mit dem Text `1. Lager auf übermäßiges Spiel geprüft` ein.
2. Ziehen Sie zwei **Kontrollkästchen** (Checkboxen) daneben.
3. Doppelklicken Sie neben das erste Kontrollkästchen und schreiben Sie `OK`. Machen Sie dasselbe für das zweite mit `NOK`.
4. **Konfiguration der Kontrollkästchen:**
   * Markieren Sie das **OK-Kontrollkästchen**. Stellen Sie im Eigenschaftenfenster (unten rechts) den **Bearbeitungsmodus** auf `Ist-Wert` und tragen Sie bei der Eigenschaft **Radiogruppe** den Wert `pruefung_lager` ein.
   * Markieren Sie das **NOK-Kontrollkästchen**. Stellen Sie ebenfalls den **Bearbeitungsmodus** auf `Ist-Wert` und tragen Sie bei **Radiogruppe** denselben Wert (`pruefung_lager`) ein. Setzen Sie zusätzlich die Eigenschaft **Markiert Befund** auf `True`.
5. **Wiederholen** Sie die Schritte 1-4 für einen zweiten Prüfpunkt, z.B. `2. Kettenspannung korrekt`. Verwenden Sie hier für die Radiogruppe einen neuen, eindeutigen Namen (z.B. `pruefung_kette`).

**Ergebnis:** Sie haben eine Checkliste erstellt. Durch die **Radiogruppe** kann pro Prüfpunkt immer nur "OK" oder "NOK" ausgewählt werden. Wenn "NOK" gewählt wird, erzeugt dies automatisch einen Befund im System.

#### Schritt 5: Messwerterfassung mit Toleranzprüfung

Nun fügen wir ein Feld hinzu, um einen numerischen Wert zu erfassen und automatisch zu validieren.

1. Fügen Sie unter der Überschrift `Messwerte` eine statische Textbox mit der Beschriftung `Motorstromaufnahme [A]:` ein.
2. Ziehen Sie eine weitere **Textbox** daneben. Dies wird unser Eingabefeld.
3. Markieren Sie die neue Textbox und konfigurieren Sie folgende Eigenschaften:
   * **Bearbeitungsmodus:** `Ist-Wert`
   * **Datentyp:** `Fließkommazahl`
   * **Minimum:** `4.5`
   * **Maximum:** `5.5`

**Ergebnis:** Der Anwender muss hier eine Zahl eingeben. Liegt der Wert außerhalb des Bereichs von 4,5 bis 5,5, wird die Eingabe automatisch als Abweichung (Befund) gewertet.

#### Schritt 6: Bemerkungen und Pflicht-Unterschrift hinzufügen

Zum Abschluss ermöglichen wir die Eingabe von Kommentaren und fordern eine digitale Unterschrift an.

1. Fügen Sie unter der Überschrift `Abschluss` eine statische Textbox mit der Beschriftung `Bemerkungen:` ein.
2. Ziehen Sie eine große **Textbox** darunter. Setzen Sie deren **Bearbeitungsmodus** auf `Ist-Wert` und aktivieren Sie im Eigenschaftenfenster die Option für mehrzeilige Eingaben, falls vorhanden.
3. Ziehen Sie ein **Freihandfeld**-Element auf die Zeichenfläche. Ziehen Sie es auf eine angemessene Größe.
4. Markieren Sie das Freihandfeld und konfigurieren Sie folgende Eigenschaften:
   * **Bearbeitungsmodus:** `Ist-Wert`
   * **Pflichtfeld:** `True`
5. Fügen Sie unter das Freihandfeld eine statische Textbox mit dem Text `Unterschrift des Prüfers` ein.

**Ergebnis:** Der Prüfer kann zusätzliche Kommentare hinterlassen und muss den Bericht mit seiner digitalen Unterschrift bestätigen, bevor er ihn abschließen kann.

#### Schritt 7: Vorschau, Speichern und Freigabe

Ihre Protokollvorlage ist nun fertig gestaltet. Führen Sie die letzten Schritte durch, um sie produktiv nutzbar zu machen.

1. Klicken Sie in der Menüleiste auf **Protokoll-Vorschau**. Testen Sie die Eingabefelder, die Checkboxen und die Toleranzprüfung, um sicherzustellen, dass alles wie erwartet funktioniert. Schließen Sie die Vorschau.
2. Klicken Sie auf die Aktion **Speichern** und schließen Sie anschließend den Designer über das "X" oben rechts.
3. Sie befinden sich nun wieder in der Hauptansicht des Moduls. Die von Ihnen erstellte Vorlage ist in der Liste markiert und ihre erste Revision hat den Status "In Bearbeitung".
4. Klicken Sie in der Ribbon-Leiste auf die Aktion **Freigabe** und bestätigen Sie den Dialog.

**Herzlichen Glückwunsch!** Sie haben erfolgreich eine komplexe, praxisnahe Protokollvorlage erstellt. Die Revision hat nun den Status "Freigegeben" und die Vorlage kann ab sofort in Berichtsvorlagen eingebunden und zur Dokumentation von Wartungsaufträgen verwendet werden.
