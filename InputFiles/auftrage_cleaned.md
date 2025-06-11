# Aufträge

Ein Paledo-Auftrag durchläuft digital die gesamte Kette von der **Erstellung** und **Einplanung**, über die **mobile oder stationäre Ausführung**, bis hin zur **Rückmeldung und Auswertung** – vollständig dokumentiert, optional mit **SAP-Integration**, **Materialfluss**, **Zeitdokumentation** und **Berichtserstellung**.

***

## Erstellung oder Import von Aufträgen

### Ursprung eines Auftrags

Ein Auftrag kann auf verschiedenen Wegen entstehen:

* **Manuelle Anlage** im System 
* **Automatisch**, z.B. durch den [strategiemanager.md] oder bei der Einplanung einer Störmeldung in der [aufgabenverteilung.md]
* Über **Import** aus einem Drittsystem (z. B. SAP PM)

### Manuelle Erstellung

Aufträge können aus verschiedenen Ansicht heraus angelegt werden. Der Ablauf ist dabei je Auftragsart immer gleich.

* Auswahl eines **Objekts** aus der [anlagenstruktur.md] – manuell oder via **Barcode-Scan**
* Eingabe eines aussagekräftigen **Titels** und einer **Problembeschreibung**
* Festlegen von - je nach Konfiguration - **Verantwortlichkeiten, Datum**, **Priorität** und weiteren Details
* Ggf. Festlegung von **Vorgängen** oder Unteraufträgen 

### Automatische Erstellung

Mit dem Strategiemanager können regelmäßig wiederkehrende Aufträge automatisiert erstellt und direkt zugewiesen werden. Details sind im Abschnitt [strategiemanager.md] beschrieben. 

### Import von Aufträgen

***

## Zuweisung und Bearbeitung von Aufträgen

Manuell erstellte oder importierte Aufträge finden sich in verschiedenen Listenansichten wieder, z.B. in der [aufgabenverteilung.md]. Sofern an einem Auftrag noch keine Verantwortlichkeit und kein Datum festgelegt wurde, wird dieser in der Regel durch die Arbeitsvorbereitung zugewiesen und eingeplant.

### Zuweisung

### Bearbeitung

#### Mobil und stationär:

Bearbeitung ist **sowohl mobil (auch offline)** als auch am Desktop möglich.

#### Bearbeitung umfasst:

* Setzen des Status (z. B. „In Arbeit“)
* **Ausfüllen von Checklisten** (z. B. Wartungspunkte, Prüfprotokolle)
* **Zeitrückmeldungen** (für geleistete Arbeitszeit)
* **Materialbuchungen** (Verbrauch dokumentieren)
* **Fotodokumentation** (direkt vor Ort)
* **Logbucheinträge** zur Nachverfolgung
* Übergabe an nächste Prozessstufe (z. B. Kontrolle, Freigabe)
* Integration von **Materialreservierungen** und **Anlageninformationen, Gefährdungsbeurteilungen**

Der gesamte Bearbeitungsprozess ist **workflowbasiert** – Statusänderungen sind in Paledo definiert und protokolliert.

***

## Abschluss und Rückmeldung

#### Optionen:

* Abschluss durch Mitarbeitende
* **Prüfung & Abnahme** im Vier-Augen-Prinzip (optional)
* **Synchronisation mit ERP-Systemen**:
  * Statusrückmeldungen
  * Zeit- und Materialrückmeldungen
  * PDF-Dokumentation wird archiviert (z. B. in SAP)

#### Berichtswesen:

* Erzeugung eines **PDF-Prüfberichts**
* Archivierung im **Dokumentenverwaltungssystem (DVS)**
* **KPI-Auswertung und Reporting** möglich

👉 Für spezielle Aufgaben (z. B. sicherheitsrelevante Tätigkeiten) können **Genehmigungsprozesse** eingebunden werden