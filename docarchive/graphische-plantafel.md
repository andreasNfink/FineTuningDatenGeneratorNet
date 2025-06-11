---
description: 'Anwenderanleitung: Graphische Plantafel in Paledo'
---

# Graphische Plantafel

Die **Graphische Plantafel** in Paledo ist ein leistungsstarkes Modul zur **visuellen, kalenderbasierten Feinplanung** von Aufträgen, Vorgängen und Berichten aber auch anderen Ereignissen. In einer **Kalenderdarstellung** wird die intuitive Planung und Umplanung von Aufgaben per Drag & Drop ermöglicht. Dieses Modul dient der Optimierung von Arbeitsabläufen und der effizienten Ressourcenzuweisung.

<figure><img src="../../.gitbook/assets/Graphische Plantafel.png" alt=""><figcaption></figcaption></figure>

> **Hinweis:**\
> Die in der Plantafel angezeigten Kategorien, Farben, Zeitleisten und vertikalen Gruppierungen (z.B. nach Ressource, Auftragsart, Anlagenbereich, Störung, Reparatur, Wartung) werden **individuell in der Systemkonfiguration von Paledo definiert**. Diese Anleitung bezieht sich auf die Anwendung der Plantafel aus Endanwendersicht, nicht auf deren Konfiguration. Informationen zur Konfiguration finden Sie in einer separaten Anleitung.

## 1. Ziel und Nutzen der Graphischen Plantafel

Die Graphische Plantafel bietet zahlreiche Vorteile für die tägliche Planungsarbeit:

* **Visuelle Planung:** Ermöglicht eine übersichtliche Darstellung von Aufgaben gruppiert nach relevanten Kriterien wie Ressource, Auftragsart, Anlagenbereich etc.
* **Zeitlich präzise Zuordnung:** Aufgaben können exakt auf Tage und Zeiträume terminiert werden.
* **Schnelle Umplanung:** Änderungen an Terminen, Dauern oder Zuständigkeiten sind einfach per Drag & Drop oder über ein Detailfenster (Flyout) möglich.
* **Ressourcenschonende Einsatzplanung:** Bietet einen klaren Überblick über die Auslastung von Mitarbeitern, Teams und Betriebsmitteln.
* **Verbesserte Kommunikation:** Fördert eine transparente Planung und Abstimmung zwischen verschiedenen Abteilungen und Teams.
* **Ideal für verschiedene Bereiche:** Besonders geeignet für die Instandhaltung, technische Dienste und produktionsnahe Planungsprozesse.

## 2. Zugriff und Systemvoraussetzungen

* **Modulzugriff:** Die Graphische Plantafel wird über den Paledo Desktop-Client sowie den Web-Client aufgerufen. Sie ist als eigenständiges Modul verfügbar.
* **Benutzerrechte:** Für den Zugriff und die Nutzung der Plantafel sind entsprechende Benutzerrollen und Berechtigungen in Paledo erforderlich. Diese legen fest, ob ein Anwender die Plantafel nur einsehen oder auch aktiv planen und Änderungen vornehmen darf. Die Berechtigungen werden von den entsprechenden Objektberechtigungen abgeleitet. Darüber hinaus existiert ein grundsätzliches Recht die Plantafel aufzurufen.
* **Technische Empfehlungen:** Für eine optimale Darstellung und Bedienung wird ein breiter Bildschirm oder ein Zwei-Monitor-Setup empfohlen, um die Kalenderansicht bestmöglich nutzen zu können.

## 3. Oberflächenaufbau und Navigation

Die Oberfläche der Graphischen Plantafel ist klar strukturiert und auf eine intuitive Bedienung ausgelegt.

#### Hauptansicht

Die Hauptansicht besteht im Wesentlichen aus:

* **Zeitachse (horizontal):** Stellt die Kalendertage dar (z.B. Montag bis Sonntag einer Woche).
* **Gruppierte Zeilen (vertikal):** Zeigt die Aufgaben gruppiert nach den in der Systemkonfiguration festgelegten Kriterien (z.B. Ressourcen, Auftragsarten, Anlagenbereiche, Kategorien wie "Stillstände", "Störungen", "Reparatur", "Wartung").
* **Kalendereinträge als farbige Blöcke:** Repräsentieren die einzelnen Aufgaben, Aufträge oder Berichte. Die Farbe der Blöcke kann ebenfalls konfigurationsabhängig sein und z.B. den Status oder die Priorität visualisieren.

#### Obere Menüleiste

Am oberen Rand der Plantafel finden Sie eine Menüleiste mit verschiedenen Funktionen:

* **Speichern und Schließen:** Speichert alle vorgenommenen Änderungen und schließt die Plantafel.
* **Speichern:** Speichert alle vorgenommenen Änderungen, die Plantafel bleibt geöffnet.
* **AI:** (Funktionalität ggf. kontextabhängig Verweist auf mögliche KI-gestützte Planungsunterstützung sofern das Modul vorhanden ist.

#### Zeitnavigation

Über der Kalenderansicht befindet sich die Zeitnavigation:

* **Pfeiltasten (< >):** Blättern wochenweise vorwärts oder rückwärts.
* **Heute:** Springt zur aktuellen Woche.
* **Datumsanzeige:** Zeigt den aktuell dargestellten Zeitraum an (z.B. "5. Mai - 11. Mai 2025").

## 4. Kalenderansicht und Gruppierung

Die Stärke der Plantafel liegt in ihrer flexiblen und anpassbaren Kalenderdarstellung.

#### Zeitachse

Die horizontale Zeitachse zeigt standardmäßig die Tage einer Woche an (z.B. Montag, 5. bis Sonntag, 11.). Je nach Konfiguration und Filterung kann der dargestellte Zeitraum variieren.

#### Vertikale Gruppierung

Die vertikalen Zeilen der Plantafel gruppieren die Aufgaben nach vordefinierten Kriterien. Diese Gruppierung ist entscheidend für die Übersichtlichkeit und kann beispielsweise erfolgen nach:

* **Ressourcen:** Personen (z.B. Emil Blitz, Stephanie Hofmann), Teams (z.B. T-ME01 - Mechatronik Team 1), Fremdfirmen.
* **Strukturmerkmalen:** Anlagenbereiche (z.B. Kleinteilfertigung, Lamination).
* **Auftragsattributen:** Auftragsarten, Prioritäten.
* **Planungskategorien:** Wie im Screenshot ersichtlich z.B. "Stillstände", "Störungen", "Reparatur", "Wartung", "Wartung übergeordnet".

#### Kalendereinträge (Aufgabenblöcke)

Geplante Aufgaben, Aufträge oder Berichte werden als farbige Blöcke in der Plantafel dargestellt.

* **Farbe:** Die Farbe eines Blocks kann verschiedene Bedeutungen haben (z.B. Status, Priorität, Auftragsart) und wird in der Systemkonfiguration festgelegt.
* **Informationen auf dem Block:** Direkt auf dem Block können wichtige Kurzinformationen angezeigt werden, wie z.B. der Titel der Aufgabe ("Spannungsproblem beseitigen"), die Auftragsnummer ("10007041") und die zugewiesene Ressource ("Emil Blitz").
* **Icons:** Kleine Icons auf den Blöcken können zusätzliche Informationen visualisieren, z.B. ein Personen-Icon für zugewiesene Mitarbeiter oder ein Blatt-Icon für Berichte/Vorgänge.

## 5. Planung per Drag & Drop

Eine der Kernfunktionen der Graphischen Plantafel ist die intuitive Planung und Umplanung von Aufgaben mittels Drag & Drop.

#### Aufgaben in die Plantafel ziehen

Neue Aufgaben, die noch nicht terminiert sind, können aus anderen Paledo-Modulen (z.B. einer Auftragsliste oder dem Berichtswesen) direkt auf den gewünschten Tag und die gewünschte Gruppierungszeile (z.B. eine bestimmte Ressource oder einen Anlagenbereich) in der Plantafel gezogen werden.

#### Aufgaben innerhalb der Plantafel verschieben

Bereits geplante Aufgaben können einfach mit der Maus an einen anderen Tag oder in eine andere Gruppierungszeile verschoben werden.

* **Terminverschiebung:** Ziehen Sie den Aufgabenblock horizontal auf einen anderen Tag.
* **Ressourcen-/Gruppenänderung:** Ziehen Sie den Aufgabenblock vertikal in eine andere Zeile (z.B. von Mitarbeiter A zu Mitarbeiter B oder von "Reparatur" zu "Wartung", falls konfigurationsseitig erlaubt).

#### Dauer von Aufgaben anpassen

Die geplante Dauer einer Aufgabe kann direkt im Kalender angepasst werden, indem der rechte oder linke Rand des Aufgabenblocks mit der Maus gezogen und somit verlängert oder verkürzt wird.

## 6. Termininformationen und Bearbeitung (Flyout)

Für detaillierte Informationen zu einer Aufgabe und zur Bearbeitung spezifischer Attribute steht ein Flyout-Fenster zur Verfügung.

#### Flyout öffnen

Ein Klick auf einen Aufgabenblock in der Plantafel öffnet das Flyout-Fenster auf der rechten Seite. Oben im Flyout wird der Titel des Vorgangs angezeigt (z.B. "Intellicam CV Kamera / 10007041") sowie Icons zum Bearbeiten (Stift-Symbol, falls separate Bearbeitungsmaske existiert) und Schließen (X-Symbol) des Flyouts.

#### Angezeigte Informationen

Das Flyout zeigt umfassende Details zum ausgewählten Termin:

* **Titel des Vorgangs:** (z.B. "Spannungsproblem beseitigen")
* **Geplanter Zeitraum:** Start- und Enddatum sowie -uhrzeit (z.B. "Di, 6. Mai 00:00 - Mi, 7. Mai 00:00").
* **AuftragsNr.:** Die eindeutige Nummer des Auftrags.
* **Titel (Auftrag/Vorgang):** Der ausführliche Titel.
* **Beschreibung:** Detaillierte Beschreibung der Aufgabe (z.B. "Spannung zu hoch, mangelhafte Produktqualität").
* **Produktqualität:** Ggf. spezifische Angaben zur Produktqualität.
* **Ist Std:** Erfasste Ist-Stunden für den Vorgang.
* **Status:** Der aktuelle Bearbeitungsstatus des Vorgangs (z.B. "Freigegeben").

#### Bearbeitungsmöglichkeiten im Flyout

Direkt im Flyout können verschiedene Attribute des Termins schnell angepasst werden:

* **Datum ändern:** Über ein Datumsfeld (z.B. "06.05.2025") mit Kalender-Icon kann das Startdatum (und implizit bei Beibehaltung der Dauer auch das Enddatum) modifiziert werden.
* **Zugeordnete Person ändern:** Der verantwortliche Mitarbeiter (z.B. "Blitz, Emil") kann über ein Dropdown-Menü ausgewählt oder mittels X-Symbol entfernt/geändert werden.
* **Zugeordnetes Team ändern:** Das verantwortliche Team (z.B. "T-ME01 - Mechatronik Team 1") kann über ein Dropdown-Menü ausgewählt oder mittels X-Symbol entfernt/geändert werden.
* **Status ändern:** Der Status des Vorgangs (z.B. "Freigegeben") kann über ein Dropdown-Menü angepasst werden.

Alle Änderungen im Flyout werden in der Regel direkt in der Plantafel sichtbar.

## 7. Änderungsfeedback & Rückgängig-Funktion (Toast-Benachrichtigung)

Nachdem eine Änderung in der Plantafel vorgenommen wurde (z.B. durch Drag & Drop eines Termins oder eine Anpassung im Flyout), erscheint unten rechts auf dem Bildschirm eine temporäre Benachrichtigung, auch "Toast" genannt.

Diese Benachrichtigung liefert ein direktes Feedback über die erfolgte Aktion:

* **Art der Änderung:** Zeigt an, was geändert wurde (z.B. "Datumsänderung").
* **Details der Änderung:** Konkretisiert die Änderung (z.B. "06.05.2025 -> 07.05.2025").
* **Identifikation der Aufgabe:** Nennt die betroffene Aufgabe (z.B. "Mattentransport / 10007042").
* **Rückgängig-Option:** Bietet in der Regel eine Option (oft ein Icon, z.B. ein geschwungener Pfeil), um die letzte Aktion schnell und unkompliziert rückgängig zu machen.

Diese Funktion erhöht die Bediensicherheit und ermöglicht es, versehentliche Änderungen einfach zu korrigieren.

## 8. Fremdfirmenplanung in der Graphischen Plantafel

Die Graphische Plantafel eignet sich auch hervorragend zur Planung von Einsätzen externer Dienstleister (Fremdfirmen).

* **Darstellung als Ressource:** Fremdfirmen können in der Plantafel als eigene Zeilen (Ressourcen) konfiguriert und angezeigt werden.
* **Aufgabenzuweisung:** Aufträge und Vorgänge, die von Fremdfirmen durchgeführt werden sollen, können diesen direkt per Drag & Drop oder über das Flyout zugeordnet werden.
* **Integration mit FREGA:** Optional kann eine Integration mit dem Paledo-Modul FREGA (Fremdfirmen-Gateway) bestehen. Dies ermöglicht eine automatisierte Kommunikation und einen Datenaustausch mit den externen Partnern bezüglich der geplanten Einsätze.

## 9. Navigation, Filter & Ansichten (linke Seitenleiste)

Eine einklappbare Seitenleiste auf der linken Seite der Plantafel bietet umfangreiche Navigations-, Filter- und Ansichtsoptionen, um die Darstellung der Aufgaben an die individuellen Bedürfnisse anzupassen.

#### Seitenleiste öffnen/schließen

Die Seitenleiste kann über ein Burger-Menü-Icon (≡) oben links geöffnet und geschlossen werden.

#### Bereich "Filter"

Im oberen Teil der Seitenleiste befinden sich die Filteroptionen. Diese ermöglichen es, die Menge der angezeigten Aufgaben gezielt zu reduzieren.

* **Textsuche:** Ein freies Textfeld ermöglicht die Suche nach Begriffen in den Aufgabendetails.
* **Benutzerfilter:**
  * Checkbox "Alle Benutzer": Zeigt Aufgaben aller Benutzer an oder ermöglicht die Auswahl spezifischer Benutzer.
  * Auswahlfeld: Hier können einzelne Benutzer (z.B. "Hofmann, Stephanie", "Bieber, Fabian") ausgewählt werden, um nur deren Aufgaben anzuzeigen. Ausgewählte Benutzer erscheinen als "Pillen" mit einem X zum Entfernen.
* **Arbeitsplatzfilter:**
  * Checkbox "Alle Arbeitsplätze": Zeigt Aufgaben aller Arbeitsplätze an oder ermöglicht die Auswahl spezifischer Arbeitsplätze.
  * Auswahlfeld: Dient der Auswahl von Arbeitsplätzen, nach denen gefiltert werden soll.
* **Anlagenbereichfilter:**
  * Checkbox "Alle Anlagenbereiche": Zeigt Aufgaben aller Anlagenbereiche an oder ermöglicht die Auswahl spezifischer Bereiche.
  * Auswahlfeld: Hier können einzelne Anlagenbereiche (z.B. "Kleinteilfertigung", "Lamination", "Sonstige Anlagen", "Sortierung") ausgewählt werden. Ausgewählte Bereiche erscheinen als "Pillen".

Alle aktivierten Filter können kombiniert werden, um eine sehr fokussierte Sicht auf die relevanten Planungseinheiten zu erhalten.

#### Bereich "Ansicht / Gruppierung"

Im unteren Teil der Seitenleiste können Einstellungen zur Gruppierung und Darstellung der Kalendereinträge vorgenommen werden.

* **Plantafel Gruppe (Dropdown-Auswahl):** Dieses Dropdown-Menü steuert die primäre vertikale Gruppierung der Aufgaben in der Plantafel (z.B. Gruppierung nach Ressourcen, nach Auftragsart, nach Anlagenbereichen). Die verfügbaren Gruppen werden in der Systemkonfiguration definiert.
* **Alle Kategorien anzeigen/ausblenden (Checkbox):** Diese Option (z.B. "Alle Kategorien") steuert die Sichtbarkeit von Zeilen, die keiner spezifischen, aktiven Filterauswahl entsprechen, oder dient dazu, übergeordnete Kategorisierungen ein- oder auszublenden.

## 10. Typische Anwendungsfälle

Die Graphische Plantafel ist ein vielseitiges Werkzeug, das in verschiedenen Bereichen effektiv eingesetzt werden kann.

#### Instandhaltung

* Planung und Terminierung von technischen Berichten, Wartungsaufträgen und Inspektionen.
* Koordination von Reparaturmaßnahmen und Störungsbeseitigungen.
* Übersicht über parallel laufende Instandhaltungsmaßnahmen und Ressourcenauslastung (Mitarbeiter, Teams, Werkzeuge).
* Visualisierung von präventiven Wartungsplänen.

#### Produktion

* Planung von Rüstvorgängen und Maschinenumbauten.
* Koordination von Unterstützungsleistungen aus technischen Abteilungen für die Produktion.
* Planung und Überwachung von Fremdfirmeneinsätzen im Produktionsumfeld (z.B. für spezielle Wartungen oder Installationen).
* Visualisierung von Produktionsstillständen und deren Ursachenbehebung.

## 11. Häufige Fragen und Tipps

| Problem                                   | Lösung / Tipp                                                                                                                                                                                    |
| ----------------------------------------- | ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ |
| Ein bestimmter Eintrag ist nicht sichtbar | Überprüfen Sie die aktiven Filter (Benutzer, Arbeitsplatz, Anlagenbereich) in der linken Seitenleiste. Stellen Sie auch sicher, dass der korrekte Zeitraum in der Zeitnavigation ausgewählt ist. |
| Ein Eintrag lässt sich nicht verschieben  | Prüfen Sie Ihre Benutzerberechtigungen. Möglicherweise verhindert auch der Status des Vorgangs eine Umplanung.                                                                                   |
| Eine bestimmte Kategorie/Gruppe fehlt     | Die verfügbaren Gruppierungen und Kategorien werden in der Systemkonfiguration von Paledo festgelegt. Wenden Sie sich ggf. an Ihren Administrator.                                               |
| Die Farbgebung der Blöcke ist unklar      | Die Bedeutung der Farben wird ebenfalls in der Systemkonfiguration definiert. Fragen Sie Ihren Administrator nach einer Farblegende oder entsprechenden Informationen.                           |
| Änderungen werden nicht übernommen        | Stellen Sie sicher, dass Sie nach Ihren Änderungen auf "Speichern" oder "Speichern und Schließen" klicken.                                                                                       |
| Wie mache ich eine Änderung rückgängig?   | Nutzen Sie die Rückgängig-Option in der Toast-Benachrichtigung, die nach jeder Änderung kurzzeitig erscheint.                                                                                    |

## 12. Zusammenfassung

Die Graphische Plantafel in Paledo ist das zentrale Werkzeug für die visuelle Feinplanung von Aufgaben und Ressourcen. Ihre Stärke liegt in der klaren, kalenderbasierten Darstellung, der intuitiven Drag & Drop-Bedienung sowie den flexiblen Anpassungsmöglichkeiten über Filter und Gruppierungen. Durch Funktionen wie das Änderungsfeedback mit Rückgängig-Option und die detaillierten Bearbeitungsmöglichkeiten im Flyout wird eine effiziente und benutzerfreundliche Planungserfahrung ermöglicht. Sie unterstützt Planer dabei, den Überblick zu behalten, Ressourcen optimal einzusetzen und schnell auf Änderungen im Betriebsablauf reagieren zu können.

***
