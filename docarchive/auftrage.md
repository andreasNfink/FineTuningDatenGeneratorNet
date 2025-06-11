# Aufträge

Ein Paledo-Auftrag durchläuft digital die gesamte Kette von der **Erstellung** und **Einplanung**, über die **mobile oder stationäre Ausführung**, bis hin zur **Rückmeldung und Auswertung** – vollständig dokumentiert, optional mit **SAP-Integration**, **Materialfluss**, **Zeitdokumentation** und **Berichtserstellung**.

***

## Erstellung oder Import von Aufträgen

### Ursprung eines Auftrags

Ein Auftrag kann auf verschiedenen Wegen entstehen:

* **Manuelle Anlage** im System&#x20;
* **Automatisch**, z.B. durch den [strategiemanager.md](module/auftragswesen/strategiemanager/README.md "mention") oder bei der Einplanung einer Störmeldung in der [aufgabenverteilung.md](../aufgaben/aufgabenverteilung.md "mention")
* Über **Import** aus einem Drittsystem (z. B. SAP PM)

### Manuelle Erstellung

{% hint style="info" %}
Aufträge können sowohl in der Paledo-App, als auch in Paledo-Web, im Desktop-Client und im Tablet-Client angelegt werden.
{% endhint %}

Aufträge können aus verschiedenen Ansicht heraus angelegt werden. Der Ablauf ist dabei je Auftragsart immer gleich.

* Auswahl eines **Objekts** aus der [anlagenstruktur.md](../anlagenverwaltung/anlagenstruktur.md "mention") – manuell oder via **Barcode-Scan**
* Eingabe eines aussagekräftigen **Titels** und einer **Problembeschreibung**
* Festlegen von - je nach Konfiguration - **Verantwortlichkeiten, Datum**, **Priorität** und weiteren Details
* Ggf. Festlegung von **Vorgängen** oder Unteraufträgen&#x20;

{% include "../../.gitbook/includes/hinweis-auftragsvorlagen.md" %}

### Automatische Erstellung

Mit dem Strategiemanager können regelmäßig wiederkehrende Aufträge automatisiert erstellt und direkt zugewiesen werden. Details sind im Abschnitt [strategiemanager.md](module/auftragswesen/strategiemanager/README.md "mention") beschrieben.&#x20;

### I<mark style="background-color:red;">mport von Aufträgen</mark>

***

## Zuweisung und Bearbeitung von Aufträgen

Manuell erstellte oder importierte Aufträge finden sich in verschiedenen Listenansichten wieder, z.B. in der [aufgabenverteilung.md](../aufgaben/aufgabenverteilung.md "mention"). Sofern an einem Auftrag noch keine Verantwortlichkeit und kein Datum festgelegt wurde, wird dieser in der Regel durch die Arbeitsvorbereitung zugewiesen und eingeplant.

### <mark style="background-color:red;">Zuweisung</mark>

### <mark style="background-color:red;">Bearbeitung</mark>

#### <mark style="background-color:red;">Mobil und stationär:</mark>

<mark style="background-color:red;">Bearbeitung ist</mark> <mark style="background-color:red;"></mark><mark style="background-color:red;">**sowohl mobil (auch offline)**</mark> <mark style="background-color:red;"></mark><mark style="background-color:red;">als auch am Desktop möglich.</mark>

#### <mark style="background-color:red;">Bearbeitung umfasst:</mark>

* <mark style="background-color:red;">Setzen des Status (z. B. „In Arbeit“)</mark>
* <mark style="background-color:red;">**Ausfüllen von Checklisten**</mark> <mark style="background-color:red;"></mark><mark style="background-color:red;">(z. B. Wartungspunkte, Prüfprotokolle)</mark>
* <mark style="background-color:red;">**Zeitrückmeldungen**</mark> <mark style="background-color:red;"></mark><mark style="background-color:red;">(für geleistete Arbeitszeit)</mark>
* <mark style="background-color:red;">**Materialbuchungen**</mark> <mark style="background-color:red;"></mark><mark style="background-color:red;">(Verbrauch dokumentieren)</mark>
* <mark style="background-color:red;">**Fotodokumentation**</mark> <mark style="background-color:red;"></mark><mark style="background-color:red;">(direkt vor Ort)</mark>
* <mark style="background-color:red;">**Logbucheinträge**</mark> <mark style="background-color:red;"></mark><mark style="background-color:red;">zur Nachverfolgung</mark>
* <mark style="background-color:red;">Übergabe an nächste Prozessstufe (z. B. Kontrolle, Freigabe)</mark>
* <mark style="background-color:red;">Integration von</mark> <mark style="background-color:red;"></mark><mark style="background-color:red;">**Materialreservierungen**</mark> <mark style="background-color:red;"></mark><mark style="background-color:red;">und</mark> <mark style="background-color:red;"></mark><mark style="background-color:red;">**Anlageninformationen, Gefährdungsbeurteilungen**</mark>

<mark style="background-color:red;">Der gesamte Bearbeitungsprozess ist</mark> <mark style="background-color:red;"></mark><mark style="background-color:red;">**workflowbasiert**</mark> <mark style="background-color:red;"></mark><mark style="background-color:red;">– Statusänderungen sind in Paledo definiert und protokolliert.</mark>

***

## <mark style="background-color:red;">Abschluss und Rückmeldung</mark>

#### <mark style="background-color:red;">Optionen:</mark>

* <mark style="background-color:red;">Abschluss durch Mitarbeitende</mark>
* <mark style="background-color:red;">**Prüfung & Abnahme**</mark> <mark style="background-color:red;"></mark><mark style="background-color:red;">im Vier-Augen-Prinzip (optional)</mark>
* <mark style="background-color:red;">**Synchronisation mit ERP-Systemen**</mark><mark style="background-color:red;">:</mark>
  * <mark style="background-color:red;">Statusrückmeldungen</mark>
  * <mark style="background-color:red;">Zeit- und Materialrückmeldungen</mark>
  * <mark style="background-color:red;">PDF-Dokumentation wird archiviert (z. B. in SAP)</mark>

#### <mark style="background-color:red;">Berichtswesen:</mark>

* <mark style="background-color:red;">Erzeugung eines</mark> <mark style="background-color:red;"></mark><mark style="background-color:red;">**PDF-Prüfberichts**</mark>
* <mark style="background-color:red;">Archivierung im</mark> <mark style="background-color:red;"></mark><mark style="background-color:red;">**Dokumentenverwaltungssystem (DVS)**</mark>
* <mark style="background-color:red;">**KPI-Auswertung und Reporting**</mark> <mark style="background-color:red;"></mark><mark style="background-color:red;">möglich</mark>

<mark style="background-color:red;">👉 Für spezielle Aufgaben (z. B. sicherheitsrelevante Tätigkeiten) können</mark> <mark style="background-color:red;"></mark><mark style="background-color:red;">**Genehmigungsprozesse**</mark> <mark style="background-color:red;"></mark><mark style="background-color:red;">eingebunden werden</mark>
