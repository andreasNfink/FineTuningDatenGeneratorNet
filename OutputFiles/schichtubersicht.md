# Schichtübersicht

Die Schichtübersicht in Paledo ist ein zentrales Werkzeug zur Planung und Verwaltung von Mitarbeitereinsätzen, Abwesenheiten und zusätzlichen Informationen (Infoeinträgen). Sie bietet eine visuelle Darstellung in Form eines Zeitstrahls und ermöglicht sowohl die Einzelbearbeitung als auch effiziente Massenoperationen. Ziel ist eine übersichtliche und leistungsstarke Einsatzplanung.

Diese Dokumentation beschreibt die Bedienung der Schichtübersicht für Endanwender. Die Konfiguration (z. B. das Anlegen von Teams, Schichten oder Vorlagen) ist nicht Gegenstand dieser Anleitung und wird separat behandelt.

***

## 1. Zugang zur Schichtübersicht

Im Paledo Webzugang erreichen Sie die Schichtübersicht über den folgenden Navigationspfad:

* `Aufgaben` → `Schichtübersicht`

***

## 2. Aufbau und Struktur der Ansicht

Die Schichtübersicht stellt Daten in einer kombinierten Zeitstrahl- und Hierarchieansicht dar.

### Zeitstrahl (Horizontal)

* **Obere Zeile:** Zeigt die Monate (z. B. Mai, Juni).
* **Mittlere Zeile:** Zeigt die Kalenderwochen (z. B. KW 18, KW 19).
* **Untere Zeile/Spalten:** Stellt die einzelnen Tage dar. Die Darstellung ist kompakt, jeder Tag wird als Kästchen pro Mitarbeiter angezeigt.
* **Feiertage/Sondertage:** Werden farblich hervorgehoben (z. B. rosa Spalte). Beim Überfahren mit der Maus wird der Name des Feiertags in einem Tooltip angezeigt.

### Mitarbeiterstruktur (Vertikal)

Die Ansicht ist hierarchisch gegliedert:

1. **Schichtplan:** Die oberste Ebene (z. B. "Werkstatt"). Schichtpläne können ein- und ausgeklappt werden, um die Übersichtlichkeit bei mehreren Plänen zu wahren.
2. **Teams:** Innerhalb eines Schichtplans sind die Teams aufgeführt (z. B. Team 1, Team 2).
3. **Mitarbeiter:** Innerhalb jedes Teams sind die einzelnen Mitarbeiter aufgelistet.

### Visuelle Hervorhebung

* **Selektierter Mitarbeiter:** Wenn Sie einen Mitarbeiter in der Liste anklicken, wird dessen Zeile farblich hervorgehoben und die Schriftart fett dargestellt, um die Orientierung im Zeitstrahl zu erleichtern.

***

## 3. Eintragsarten und Regeln

In der Schichtübersicht können drei Hauptkategorien von Einträgen pro Tag und Mitarbeiter verwaltet werden:

### Schichten

* Beschreiben die geplante Arbeitszeit (z. B. Frühschicht, Spätschicht, Nachtschicht).
* Werden durch Kürzel und Farben dargestellt (Beispiele siehe Legende).

### Abwesenheiten

* Dokumentieren Zeiten, in denen ein Mitarbeiter nicht arbeitet (z. B. Urlaub, Krankheit, Dienstreise, Schulung).
* Werden ebenfalls durch Kürzel und Farben dargestellt.

### Infoeinträge

* Stellen zusätzliche Informationen dar, die parallel zu einer Schicht oder Abwesenheit bestehen können (z. B. Rufbereitschaft, Besprechung, Mehrarbeit).
* Sind additiv, d.h., mehrere Infoeinträge pro Tag sind möglich.
* Werden oft als kleine Markierungen oder separate Symbole/Farben im Kästchen angezeigt (z. B. brauner Balken für Rufbereitschaft).

### Regeln für Einträge

* Pro Tag und Mitarbeiter kann **nur eine Schicht ODER eine Abwesenheit** eingetragen sein. Diese schließen sich gegenseitig aus.
* Zusätzlich zu einer Schicht oder Abwesenheit können **ein oder mehrere Infoeinträge** am selben Tag vorhanden sein.

***

## 4. Die Legende

Die Legende ist ein zentrales Element zur Anzeige und Steuerung der Schichtübersicht.

### Zugriff und Anzeige

* Die Legende befindet sich im **Burger-Menü** auf der linken Seite des Bildschirms.
* Sie kann über das Burger-Menü ein- und ausgeklappt werden.
* Innerhalb des Menüs kann auch der Legendenbereich selbst separat ein- und ausgeklappt werden.

### Inhalt der Legende

Die Legende zeigt alle konfigurierten Eintragsarten übersichtlich an, gruppiert nach den drei Kategorien:

* **Schichtarten:** Zeigt Kürzel, volle Bezeichnung und Farbe jeder Schicht (z. B. FS: Frühschicht, grün; SS: Spätschicht, gelb; NS: Nachtschicht, orange).
* **Abwesenheitsgründe:** Zeigt Kürzel, Bezeichnung und Farbe jeder Abwesenheit (z. B. VA: Urlaub genehmigt, hellrot; DR: Dienstreise, hellblau; (␣) für Krankheit, schwarz).
* **Infoeinträge:** Zeigt Kürzel, Bezeichnung und Farbe jedes Infoeintrags (z. B. RB: Bereitschaft, braun; ME: Besprechung, mintgrün; EW: Mehrarbeit, türkis).

### Funktionen der Legende (Massenbearbeitung)

Die Legende dient nicht nur zur Information, sondern ist das Hauptwerkzeug für Massenbearbeitungen:

* **Auswahl/Bearbeitung (✏️ Stift-Symbol):** Durch Klicken auf den Stift oder den Eintrag selbst wird dieser für die Massenzuweisung ausgewählt. Der Mauszeiger verwandelt sich im Planungsbereich in ein Fadenkreuz.
* **Gezieltes Löschen (❌ X-Symbol):** Aktiviert den Löschmodus _nur_ für die ausgewählte Kategorie.
* **Kommentar:** Ermöglicht das massenhafte Erstellen oder Bearbeiten von Kommentaren.
* **Einträge leeren:** Ermöglicht das Entfernen _aller_ Einträge in einem markierten Bereich.

***

## 5. Arbeiten mit Einträgen (Einzelbearbeitung)

Sie können Einträge für einzelne Tage und Mitarbeiter direkt in der Übersicht bearbeiten.

### Eintrag erstellen oder ändern (Rechtsklick)

1. Klicken Sie mit der **rechten Maustaste** auf das Kästchen des gewünschten Tages und Mitarbeiters.
2. Ein Kontextmenü öffnet sich mit den verfügbaren Optionen, gegliedert nach:
   * Schichten
   * Abwesenheitsgründe
   * Info Einträge
   * Kommentar erstellen
3. Wählen Sie den gewünschten Eintrag aus (z. B. "Spätschicht" unter Schichten oder "Urlaub" unter Abwesenheitsgründe).
4. Der Eintrag im Kästchen wird sofort aktualisiert (Kürzel und Farbe ändern sich). Ein bereits vorhandener Eintrag wird durch den neuen ersetzt (außer bei additiven Infoeinträgen).

### Details anzeigen (Linksklick / Flyout-Menü)

1. Klicken Sie mit der **linken Maustaste** auf ein Kästchen mit einem Eintrag.
2. Ein **Flyout-Menü** erscheint und zeigt Detailinformationen an:
   * Mitarbeitername
   * Datum
   * Teamzugehörigkeit
   * Art des Eintrags (Schicht oder Abwesenheit) mit Details
   * Ggf. die ursprünglich geplante Schicht (siehe nächster Punkt)
   * Ein eventuell vorhandener Kommentar

### Ursprüngliche Schicht bei Abwesenheit

* Wenn Sie eine Abwesenheit eintragen (z. B. Urlaub), merkt sich das System die Schicht, die ursprünglich für diesen Tag geplant war.
* Diese Information wird im Flyout-Menü der Abwesenheit angezeigt ("Ursprüngliche Schicht").
* Dies erleichtert das Wiederherstellen der ursprünglichen Planung, falls die Abwesenheit entfernt wird.

***

## 6. Arbeiten mit Kommentaren

Zusätzlich zu Schichten, Abwesenheiten und Infoeinträgen können Sie Kommentare zu einzelnen Tagen hinterlegen.

### Kommentar erstellen

* **Über Rechtsklick:** Klicken Sie mit der rechten Maustaste auf das gewünschte Kästchen und wählen Sie im Kontextmenü die Option zum Erstellen eines Kommentars. Ein kleines Dialogfenster öffnet sich zur Texteingabe.
* **Über Legende:** Kommentare können auch massenhaft über die Legendenfunktion erstellt werden (siehe Abschnitt Massenbearbeitung).

### Kommentar erkennen und anzeigen

* Ein vorhandener Kommentar wird durch ein **rotes Dreieck** in der oberen rechten Ecke des Kästchens signalisiert.
* Der Inhalt des Kommentars wird im **Flyout-Menü** angezeigt, das sich bei einem Linksklick auf das Kästchen öffnet.

### Kommentar bearbeiten oder löschen

* **Über Flyout-Menü:** Öffnen Sie das Flyout-Menü (Linksklick). Dort finden Sie Optionen (z. B. Stiftsymbol), um den Kommentar direkt zu bearbeiten oder zu löschen.
* **Über Legende:** Kommentare können auch massenhaft über die Legendenfunktion gelöscht werden.

***

## 7. Massenbearbeitungsfunktionen

Die Legende ermöglicht die effiziente Bearbeitung von Einträgen für mehrere Tage oder Mitarbeiter gleichzeitig.

### Einträge zuweisen (über Legende)

1. Klicken Sie in der Legende auf den **Stift (✏️)** neben dem gewünschten Eintrag (z. B. "Urlaub genehmigt") oder direkt auf den Eintrag. Der Eintrag wird in der Legende markiert.
2. Der Mauszeiger verwandelt sich im Planungsbereich in ein **Fadenkreuz**.
3. Klicken und ziehen Sie mit der Maus über den gewünschten Bereich (mehrere Tage für einen oder mehrere Mitarbeiter). Sie können auch einzelne Tage durch Klicken auswählen.
4. Lassen Sie die Maustaste los. Der ausgewählte Eintrag wird für alle markierten Kästchen übernommen.
5. Beenden Sie den Massenbearbeitungsmodus, indem Sie auf das **rote X** in der Legende klicken (neben dem aktiven Eintrag) oder einen anderen Eintrag auswählen.

### Einträge löschen (über Legende)

1. Klicken Sie in der Legende auf das **rote X (❌)** neben dem Eintrag, den Sie löschen möchten (z. B. "Urlaub genehmigt"). Der Eintrag wird in der Legende rot umrandet markiert.
2. Der Mauszeiger verwandelt sich in ein Fadenkreuz (oder ein ähnliches Symbol für den Löschmodus).
3. Markieren Sie den Bereich (Tage/Mitarbeiter), in dem die Einträge gelöscht werden sollen, durch Klicken und Ziehen oder einzelne Klicks.
4. Nur Einträge der ausgewählten Kategorie werden gelöscht. Andere Einträge im markierten Bereich bleiben unberührt. (Beispiel: Markieren Sie eine Woche, nur die Urlaubstage werden entfernt, vorhandene Frühschichten bleiben bestehen).
5. Beenden Sie den Löschmodus über das rote X in der Legende.

### Kommentare massenhaft verwalten

* Analog zur Zuweisung und Löschung von Schichten/Abwesenheiten können über die entsprechenden Funktionen in der Legende ("Kommentar") Kommentare für mehrere Tage gleichzeitig erstellt oder gelöscht werden.

### Einträge leeren

* Die Legende bietet eine Funktion "Einträge leeren".
* Wenn diese Funktion aktiviert und ein Bereich markiert wird, werden **alle** Arten von Einträgen (Schichten, Abwesenheiten, Infoeinträge, Kommentare) im markierten Bereich entfernt. Seien Sie vorsichtig bei der Anwendung dieser Funktion.

### Letzte Änderung rückgängig machen

* Oben in der Hauptansicht der Schichtübersicht befindet sich eine Schaltfläche **"Rückgängig"** (oft ein Pfeilsymbol).
* Diese Funktion macht die **zuletzt durchgeführte Aktion** (Einzel- oder Massenbearbeitung) ungeschehen.
* Es kann immer nur die _letzte_ Änderung rückgängig gemacht werden.

***

## 8. Schichtzuweisung per Assistent (Musterplanung)

Für die Vorplanung von Schichten über längere Zeiträume oder für ganze Teams gibt es einen Assistenten, der die Zuweisung von Schichtmustern automatisiert.

### Zweck und Zugriff

* Der Assistent dient dazu, wiederkehrende Schichtfolgen (Muster) effizient auf ausgewählte Mitarbeiter und Zeiträume anzuwenden.
* Sie erreichen den Assistenten über eine Schaltfläche in der oberen Leiste der Schichtübersicht (z. B. "Schichtzuweisung").

### Planungsparameter festlegen

Im oberen Bereich des Assistenten legen Sie die Rahmenbedingungen fest:

* **Schichtplan:** Wählen Sie den Schichtplan aus, für den die Zuweisung gelten soll (z. B. "Werkstatt").
* **Schichtvorlage:** Wählen Sie ein vordefiniertes Schichtmuster (z. B. ein Dreischichtmodell wie `FFSSNN--` oder eine reine Tagschicht `TTTTT--`). Diese Vorlagen werden in der Konfiguration definiert.
* **Erster Tag/Vorlage:** Bestimmen Sie, an welchem Tag des Musters die Zuweisung beginnen soll. Dies ermöglicht es, Teams versetzt zu planen (z. B. Team A startet mit Tag 1, Team B mit Tag 3 des Musters).
* **Start- und Enddatum:** Definieren Sie den Zeitraum, für den das Muster angewendet werden soll. Die Anzahl der Tage wird automatisch berechnet.

### Mitarbeiter auswählen

Im unteren Bereich des Assistenten wählen Sie die Mitarbeiter aus:

* **Team-Selektion (links):** Wählen Sie ein oder mehrere Teams aus. Die zugehörigen Mitarbeiter erscheinen im rechten Bereich.
* **Mitarbeiterauswahl (rechts):** Standardmäßig sind alle Mitarbeiter der ausgewählten Teams selektiert. Sie können hier einzelne Mitarbeiter abwählen oder manuell hinzufügen. Eine Filter-/Suchfunktion erleichtert das Finden bestimmter Personen.

### Zuweisung ausführen

* Klicken Sie auf die Schaltfläche **"Zuweisen"** (meist unten rechts im Assistenten).
* Das ausgewählte Schichtmuster wird auf alle selektierten Mitarbeiter im festgelegten Zeitraum angewendet, unter Berücksichtigung der gewählten Optionen.
* Die Änderungen sind sofort in der Schichtübersicht sichtbar.

### Wichtige Optionen

Der Assistent bietet zusätzliche Optionen zur Steuerung der Zuweisung:

* **Leeren:** Entfernt vor der Zuweisung alle bestehenden Einträge im gewählten Zeitraum.
* **Schichten & Abwesenheiten überschreiben:** Wenn aktiviert, werden bereits vorhandene Schicht- oder Abwesenheitseinträge durch das neue Muster überschrieben. Wenn deaktiviert, bleiben bestehende Einträge (z. B. bereits genehmigter Urlaub) erhalten.
* **Infoeinträge überschreiben:** Steuert, ob vorhandene Infoeinträge gelöscht werden sollen.
* **Feiertage beplanen:** Legt fest, ob das Schichtmuster auch an gesetzlichen Feiertagen angewendet werden soll.

### Änderung rückgängig machen

* Auch nach einer Zuweisung über den Assistenten steht die **"Rückgängig"**-Funktion zur Verfügung, um die gesamte Zuweisungsaktion rückgängig zu machen (nur die unmittelbar letzte Aktion).

***

## 9. Navigation im Zeitstrahl

Mehrere Bedienelemente erleichtern die Navigation entlang der Zeitachse:

### Navigationsschaltflächen

Diese befinden sich meist oberhalb des Zeitstrahls:

* **`Heute`:** Springt zur Ansicht, die mit dem aktuellen Datum beginnt.
* **`Nächster Monat`:** Wechselt die Ansicht zum folgenden Monat.
* **`Vorheriger Monat`:** Wechselt die Ansicht zum vorherigen Monat.
* **`Dieser Monat`:** Stellt die Ansicht so ein, dass sie mit dem 1. des aktuellen Monats beginnt.

### Dropdown-Optionen

* Die Schaltflächen `Nächster Monat` und `Vorheriger Monat` bieten oft ein kleines Pfeilsymbol für ein Dropdown-Menü.
* Dieses Menü ermöglicht das direkte Springen zu einem der nächsten bzw. vorherigen Monate (z. B. die nächsten/letzten 6 Monate), ohne mehrfach klicken zu müssen.

### Scrollen

* Sie können die horizontale Scrollleiste am unteren Rand des Zeitstrahls verwenden, um sich frei vorwärts und rückwärts in der Zeit zu bewegen.

***

## 10. Ansichtsoptionen anpassen

Über das Burger-Menü auf der linken Seite können Sie nicht nur die Legende, sondern auch weitere Ansichtsoptionen steuern.

### Zugriff auf Optionen

* Öffnen Sie das Burger-Menü links. Unterhalb oder neben der Legende finden Sie die Ansichtsoptionen.

### Schichtpläne auswählen/filtern

* **Anzeige steuern:** Sie können auswählen, welche Schichtpläne in der Übersicht angezeigt werden sollen (sofern Sie die Berechtigung dafür haben).
* **Alle auswählen:** Eine Checkbox, um schnell alle verfügbaren Pläne anzuzeigen.
* **Filterfeld:** Ein Eingabefeld ermöglicht das Filtern nach dem Namen eines bestimmten Schichtplans (z. B. "Werkstatt"), um nur diesen anzuzeigen.

### Zusätzliche Anzeigen (Gruppen, Summen)

* **Gruppen anzeigen:** Eine Checkbox, um die Anzeige der Teamstruktur (Team 1, Team 2 etc.) unterhalb der Schichtpläne ein- oder auszublenden.
* **Summen anzeigen:** Wenn aktiviert und konfiguriert, werden unterhalb der Schichtübersicht aggregierte Werte angezeigt (z. B. die Anzahl der Mitarbeiter in der Frühschicht pro Tag/Woche, Urlaubstage pro Woche etc.). Dies ist nützlich für die Kapazitätsplanung.

### Berechtigungskonzept

* Die Sichtbarkeit von Schichtplänen und Mitarbeiterdaten unterliegt dem Paledo-Berechtigungssystem.
* Anwender sehen nur die Daten, für die sie freigegeben sind. Dies kann auf bestimmte Schichtpläne oder Teams beschränkt sein.

***

## 11. Ausdruck-Funktion

Paledo bietet die Möglichkeit, die Schichtplanung als PDF-Dokument auszudrucken.

### Verfügbare Ausdrucke

* Eine Schaltfläche in der oberen Leiste (z. B. "Dienstplan drucken") ermöglicht den Zugriff auf die Druckfunktionen.
* Typische Optionen sind der Ausdruck eines **Monatsdienstplans** oder einer **Jahresübersicht**.

### Darstellung im Ausdruck

* Das generierte PDF stellt den Plan in einer kalendarischen Ansicht dar.
* Feiertage und andere Sondertage sind im Ausdruck farblich hervorgehoben, um die Lesbarkeit zu verbessern.

***

## 12. Zusammenfassung

Die Schichtübersicht in Paledo ist ein mächtiges Werkzeug für die Planung von Personalressourcen. Sie visualisiert Schichten, Abwesenheiten und Zusatzinformationen auf einem Zeitstrahl und bietet flexible Bearbeitungsmöglichkeiten – vom einzelnen Eintrag bis hin zu Massenoperationen über die Legende und den Schichtzuweisungs-Assistenten. Die intuitive Bedienung und die anpassbaren Ansichten unterstützen Planungsverantwortliche bei der effizienten und transparenten Organisation des Personaleinsatzes.

***

##