---
icon: right-left
---

# REST API

**Zielgruppe:** Erfahrene Paledo-Anwender und Entwickler**Modul:** Generische REST API (Teil des Paledo Interface Toolkits)**Stand:** Basierend auf Informationen bis April 2025

## Inhaltsverzeichnis

1. [Einleitung und Einsatzkontext](anleitung.md#1-einleitung-und-einsatzkontext)
   * [Zweck des Moduls](anleitung.md#zweck-des-moduls)
   * [Typische Anwendungsfälle](anleitung.md#typische-anwendungsfälle)
   * [Grundprinzip und Technologie](anleitung.md#grundprinzip-und-technologie)
2. [Systemvoraussetzungen & Erforderliches Know-how](anleitung.md#2-systemvoraussetzungen--erforderliches-know-how)
   * [Technische Voraussetzungen](anleitung.md#technische-voraussetzungen)
   * [Erforderliches Wissen](anleitung.md#erforderliches-wissen)
3. [Grundlagen und Architektur](anleitung.md#3-grundlagen-und-architektur)
   * [Duale Funktionalität (Inbound/Outbound)](anleitung.md#duale-funktionalität-inboundoutbound)
   * [Konfigurationsbasierter Ansatz](anleitung.md#konfigurationsbasierter-ansatz)
   * [Verarbeitung über Migrationsdefinitionen](anleitung.md#verarbeitung-über-migrationsdefinitionen)
4. [Navigation im Paledo Client](anleitung.md#4-navigation-im-paledo-client)
   * [Zugriff auf die Module](anleitung.md#zugriff-auf-die-module)
5. [Konfiguration eingehender REST-Endpunkte](anleitung.md#5-konfiguration-eingehender-rest-endpunkte)
   * [5.1 Maskenübersicht: REST-Schnittstelle Konfiguration](anleitung.md#51-maskenübersicht-rest-schnittstelle-konfiguration)
   * [5.2 Detailmaske: REST-Endpunkt](anleitung.md#52-detailmaske-rest-endpunkt)
   * [5.3 YAML-Spezifikation (OpenAPI)](anleitung.md#53-yaml-spezifikation-openapi)
     * [Zweck und Aufbau](anleitung.md#zweck-und-aufbau)
     * [Beispiel einer YAML-Konfiguration](anleitung.md#beispiel-einer-yaml-konfiguration)
   * [5.4 Verarbeitungsmodi (Synchron/Asynchron)](anleitung.md#54-verarbeitungsmodi-synchronasynchron)
6. [Konfiguration ausgehender REST-Aufrufe (Trigger)](anleitung.md#6-konfiguration-ausgehender-rest-aufrufe-trigger)
   * [6.1 Maskenübersicht: Getriggerte REST-Aufrufe](anleitung.md#61-maskenübersicht-getriggerte-rest-aufrufe)
   * [6.2 Detailmaske: Getriggerter REST-Aufruf](anleitung.md#62-detailmaske-getriggerter-rest-aufruf)
     * [Allgemeine Einstellungen](anleitung.md#allgemeine-einstellungen)
     * [Trigger-Einstellungen](anleitung.md#trigger-einstellungen)
     * [Webrequest-Konfiguration](anleitung.md#webrequest-konfiguration)
     * [Logs](anleitung.md#logs)
   * [6.3 Body-Ausdruck im Expression Editor](anleitung.md#63-body-ausdruck-im-expression-editor)
   * [6.4 Zieltyp-Filter (Expression-Filter)](anleitung.md#64-zieltyp-filter-expression-filter)
   * [6.5 Authentifizierung für getriggerte REST-Aufrufe](anleitung.md#65-authentifizierung-für-getriggerte-rest-aufrufe)
     * [API Key (in Header-Konfiguration)](anleitung.md#api-key-in-header-konfiguration)
     * [OAuth2-Authentifizierung](anleitung.md#oauth2-authentifizierung)
       * [6.5.1 Maskenübersicht: Getriggerte REST-Aufrufe Authentifizierung](anleitung.md#651-maskenübersicht-getriggerte-rest-aufrufe-authentifizierung)
       * [6.5.2 Detailmaske: Authentifizierungsprofil (OAuth2)](anleitung.md#652-detailmaske-authentifizierungsprofil-oauth2)
       * [6.5.3 Verwendung in Triggern](anleitung.md#653-verwendung-in-triggern)
   * [6.6 Rückverarbeitung von Responses](anleitung.md#66-rückverarbeitung-von-responses)
7. [Webservice Nachrichten (Monitoring & Fehlerbehandlung)](anleitung.md#7-webservice-nachrichten-monitoring--fehlerbehandlung)
   * [7.1 Maskenübersicht: Webservice-Nachrichten](anleitung.md#71-maskenübersicht-webservice-nachrichten)
     * [Filteroptionen](anleitung.md#filteroptionen)
     * [Tabellenfelder](anleitung.md#tabellenfelder)
     * [Hinweise zur Ansicht (Grenzen, Fehlerhervorhebung)](anleitung.md#hinweise-zur-ansicht-grenzen-fehlerhervorhebung)
   * [7.2 Detailmaske: Webservice-Nachricht](anleitung.md#72-detailmaske-webservice-nachricht)
     * [Kopfzeilenfelder](anleitung.md#kopfzeilenfelder)
     * [Inhaltsbereich (JSON-Body)](anleitung.md#inhaltsbereich-json-body)
   * [7.3 Manuelle Wiederverarbeitung (Retry processing)](anleitung.md#73-manuelle-wiederverarbeitung-retry-processing)
   * [7.4 Automatische Bereinigung](anleitung.md#74-automatische-bereinigung)
8. [Hintergrundverarbeitung: Server Jobs](anleitung.md#8-hintergrundverarbeitung-server-jobs)
   * [8.1 Server Job: Webservice Message Service](anleitung.md#81-server-job-webservice-message-service)
   * [8.2 Server Job: Getriggerter REST API Service](anleitung.md#82-server-job-getriggerter-rest-api-service)
9. [Best Practices & Hinweise zur Datenkonsistenz](anleitung.md#9-best-practices--hinweise-zur-datenkonsistenz)
   * [Risiken bei Fehlkonfiguration](anleitung.md#risiken-bei-fehlkonfiguration)
   * [Empfehlungen](anleitung.md#empfehlungen)
10. [Anwendungsbeispiele aus der Praxis](anleitung.md#10-anwendungsbeispiele-aus-der-praxis)
    * [Beispiel 1: Anbindung eines Ticketsystems](anleitung.md#beispiel-1-anbindung-eines-ticketsystems)
    * [Beispiel 2: Schadenmeldung aus Fahrzeug-App](anleitung.md#beispiel-2-schadenmeldung-aus-fahrzeug-app)
    * [Beispiel 3: Paledo meldet Statusänderungen](anleitung.md#beispiel-3-paledo-meldet-statusänderungen)

***

## 1. Einleitung und Einsatzkontext

### Zweck des Moduls

Die **generische REST API** ist ein Sub-Modul des Paledo Interface Toolkits (PIT). Sie dient der flexiblen Anbindung von Drittsystemen an Paledo und umgekehrt. Das Modul ermöglicht es, sowohl **eingehende REST-Nachrichten** (typischerweise mit JSON-Body) zu empfangen und zu verarbeiten, als auch **ausgehende REST-Aufrufe** an externe Endpunkte zu senden, die durch Ereignisse innerhalb von Paledo ausgelöst werden (Trigger).

Ein wesentlicher Vorteil dieses Moduls ist, dass die Konfiguration von Schnittstellen vollständig innerhalb der Paledo Benutzeroberfläche erfolgen kann. Es ist **nicht notwendig**, für die Einführung einer neuen Schnittstelle zusätzliche Software zu programmieren oder Updates beim Kunden auszurollen. Konfigurationen können direkt vom Paledo Projektmanager, Entwickler oder spezialisierten Key-Usern des Kunden vorgenommen werden.

### Typische Anwendungsfälle

* **Integration von Ticketsystemen:** Empfang von Tickets (z.B. Instandhaltungstickets) aus einem unternehmensweiten System und Rückmeldung des Bearbeitungsstatus aus Paledo.
* **Anbindung von Betriebsleittechnik oder externen Erfassungssystemen:** Übermittlung von Zustandsdaten, Messwerten oder Schadensmeldungen an Paledo.
* **Synchronisation mit Drittsystemen:** Automatisierter Datenaustausch bei Statusänderungen oder relevanten Ereignissen in Paledo (z.B. Abschluss eines Auftrags).

### Grundprinzip und Technologie

Die generische REST API basiert auf dem **OpenAPI-Standard (Version 3.x)**. Eingehende Endpunkte werden über eine YAML-Spezifikation definiert. Paledo stellt für konfigurierte Endpunkte automatisch eine **Swagger UI** bereit, die es Entwicklern von Drittsystemen ermöglicht, die Schnittstellen zu erkunden, zu testen und die Definitionen einzusehen.

***

## 2. Systemvoraussetzungen & Erforderliches Know-how

### Technische Voraussetzungen

* Paledo-Serverkomponente mit lizenziertem und aktivem PIT-Modul.
* Netzwerkkonnektivität zwischen dem Paledo-Server und den anzubindenden Drittsystemen (ggf. über VPN, Firewall-Freischaltungen).
* Erreichbarkeit der Paledo-REST-Endpunkte von außen (für eingehende Nachrichten) bzw. der externen Endpunkte von Paledo aus (für ausgehende Nachrichten).

### Erforderliches Wissen

Die Konfiguration und Nutzung der generischen REST API erfordert tiefgreifendes Wissen in verschiedenen Bereichen:

* **Paledo Migrationsdefinitionen (Synonym: Importdefinitionen):** Diese sind fundamental für die Verarbeitung der Daten aus eingehenden JSON-Nachrichten sowie für die Rückverarbeitung von Antworten auf ausgehende Calls. Ein hohes Verständnis der Schemata und Mapping-Möglichkeiten ist unabdingbar.
* **Paledo Datenmodell:** Sehr gute Kenntnisse des Paledo-Datenmodells sind notwendig, um Daten korrekt zu mappen und Inkonsistenzen zu vermeiden. Fehlkonfigurationen können die Datenintegrität gefährden.
* **Paledo Skriptsprache (Expressions):** Wird benötigt für die dynamische Erstellung von JSON-Payloads bei ausgehenden Trigger-Aufrufen und für die Definition von Filterbedingungen.
* **Webtechnologien:** Gutes Verständnis von REST-Prinzipien, HTTP-Methoden (PUT, POST, GET etc.), JSON-Datenstruktur, HTTP-Headern.
* **Authentifizierungsmethoden:** Kenntnisse über API-Keys (typischerweise im Header) und OAuth2 (insbesondere Client Credentials Flow).
* **YAML-Syntax:** Notwendig für die Erstellung und das Verständnis der OpenAPI-Spezifikationen für eingehende Endpunkte.

**Wichtiger Hinweis:** Aufgrund der Komplexität und der potenziellen Auswirkungen auf die Datenkonsistenz sollte die Konfiguration nur von sehr erfahrenen Paledo-Anwendern, Entwicklern oder Consultants durchgeführt werden.

***

## 3. Grundlagen und Architektur

### Duale Funktionalität (Inbound/Outbound)

Die generische REST API deckt zwei Hauptrichtungen der Kommunikation ab:

1. **Inbound (Eingehend):** Paledo agiert als REST-Server. Es können Endpunkte konfiguriert werden, die JSON-Nachrichten von externen Systemen empfangen.
2. **Outbound (Ausgehend):** Paledo agiert als REST-Client. Basierend auf Triggern (Datenänderungen in Paledo) können proaktiv REST-Nachrichten an externe Endpunkte gesendet werden.

### Konfigurationsbasierter Ansatz

Alle Aspekte der Schnittstelle – Endpunkte, Trigger, Authentifizierung, Datenmapping – werden **direkt in der Paledo Benutzeroberfläche konfiguriert**. Es ist keine zusätzliche Programmierung oder ein Deployment erforderlich.

### Verarbeitung über Migrationsdefinitionen

Das Kernstück der Datenverarbeitung (sowohl für eingehende Nachrichten als auch für die Verarbeitung von Antworten auf ausgehende Calls) sind die **Paledo Migrationsdefinitionen**. Diese definieren, wie die Felder aus dem JSON-Body auf die Objekte und Attribute im Paledo-Datenmodell gemappt werden.

***

## 4. Navigation im Paledo Client

### Zugriff auf die Module

Alle relevanten Konfigurations- und Monitoring-Module für die generische REST API finden sich im Paledo Client unter dem Navigationspfad:

`Administration → Webschnittstelle`

Dort stehen folgende Unterpunkte zur Verfügung:

* **REST-Schnittstelle Konfiguration:** Verwaltung eingehender REST-Endpunkte.
* **Getriggerte REST-Aufrufe:** Verwaltung ausgehender, ereignisgesteuerter REST-Calls.
* **Getriggerte REST-Aufrufe Authentifizierung:** Verwaltung von OAuth2-Profilen für ausgehende Calls.
* **Webservice-Nachrichten:** Monitoring und Logbuch aller ein- und ausgehenden REST-Nachrichten.

***

## 5. Konfiguration eingehender REST-Endpunkte

Diese Funktionalität erlaubt es Paledo, als Server zu agieren und Daten von externen Systemen via REST entgegenzunehmen.

### 5.1 Maskenübersicht: REST-Schnittstelle Konfiguration

Diese Maske (erreichbar über `Administration → Webschnittstelle → REST-Schnittstelle Konfiguration`) bietet eine Übersicht aller konfigurierten eingehenden REST-Endpunkte.

* **Ribbon-Menü:** Bietet Standardfunktionen wie `Neu`, `Speichern`, `Löschen`, `Aktualisieren`, `Suche`.
* **Übersichtstabelle:** Zeigt die konfigurierten Endpunkte mit den wichtigsten Parametern:
  * **Endpunkt-Name:** Der Name, der Teil der URL wird (`https://host:port/data/{Endpunkt-Name}`).
  * **Ist aktiv:** Checkbox zum Aktivieren/Deaktivieren des Endpunkts.
  * **Direkte Verarbeitung:** Checkbox zur Steuerung des Verarbeitungsmodus (synchron/asynchron).
  * **API Key:** Der für diesen Endpunkt gültige API-Schlüssel zur Authentifizierung.
  * **Migrationsdefinition:** Die zugeordnete Definition zur Verarbeitung des JSON-Bodys.
  * **(Ergebnis-Export):** (Spalte vorhanden, Funktion nicht detailliert beschrieben).
* **Interaktion:**
  * Doppelklick öffnet die Detailmaske.
  * Checkboxen können direkt in der Liste geändert werden.
  * Spalten sind sortierbar.

### 5.2 Detailmaske: REST-Endpunkt

Diese Maske wird durch Doppelklick auf einen Eintrag in der Übersicht oder über `Neu` geöffnet und dient der detaillierten Konfiguration eines Endpunkts.

* **Basisdaten (Formularfelder):**
  * **Endpunkt-Name:** Eindeutiger technischer Name (wird Teil der URL).
  * **API Key:** Zeichenkette, die der Aufrufer im HTTP-Header `X-API-Key` zur Authentifizierung mitsenden muss.
  * **Migrationsdefinition:** Auswahl der Paledo-Migrationsdefinition, die den empfangenen JSON-Body verarbeitet.
  * **Ist aktiv:** Checkbox zur Aktivierung/Deaktivierung.
  * **Direkte Verarbeitung:** Checkbox zur Wahl des Verarbeitungsmodus (siehe Abschnitt 5.4).
* **Tab: Spezifikation:**
  * Enthält das Textfeld zur Eingabe der **YAML-basierten OpenAPI-Spezifikation** (siehe Abschnitt 5.3). Diese beschreibt den erwarteten Aufbau des JSON-Bodys und die verfügbaren Operationen (z.B. PUT, POST).
* **Tab: Rückgabe:**
  * (Funktion nicht detailliert beschrieben, vermutlich zur Konfiguration der HTTP-Antwort von Paledo).
* **Ribbon-Menü:** Bietet Navigation (`Letztes/Nächstes Objekt`), Speicherfunktionen und `Schließen`.

### 5.3 YAML-Spezifikation (OpenAPI)

#### Zweck und Aufbau

Die YAML-Spezifikation ist das Herzstück der Definition eines eingehenden Endpunkts. Sie folgt dem OpenAPI 3.x Standard.

* **Beschreibung:** Definiert die Struktur des erwarteten JSON-Requests (Schema), die erlaubten HTTP-Methoden (z.B. `put`, `post`), erwartete Responses und optional Sicherheitsmechanismen (wie den `ApiKeyAuth`).
* **Validierung:** Paledo nutzt diese Spezifikation zur Validierung eingehender Anfragen.
* **Swagger UI:** Die Spezifikation wird verwendet, um automatisch eine interaktive Swagger UI Dokumentation für den Endpunkt bereitzustellen.

#### Beispiel einer YAML-Konfiguration

```yaml
openapi: 3.1.0
info:
  title: Paledo-ÖVPad API - ÖVPad->Paledo
  version: '1.0'
  description: API zur Übermittlung von Schadensmeldungen von ÖVPad an Paledo
servers:
  - url: 'http://localhost:3000' # Beispiel-URL, wird durch Paledo-Instanz ersetzt
components:
  securitySchemes:
    ApiKeyAuth:
      type: apiKey
      in: header
      name: X-API-Key
paths:
  /ovpadschaden: # Wird relativ zum Basis-Pfad /data/{Endpunkt-Name} gesehen
    put:
      summary: Schaden erzeugen oder aktualisieren
      operationId: put-schaden
      security: # Verweis auf das definierte Security Scheme
        - ApiKeyAuth: []
      responses:
        '200':
          description: Schadensmeldung wurde neu angelegt
          content:
            application/json:
              schema:
                type: object
                required:
                  - schadenid
                properties:
                  schadenid:
                    type: string
                    format: uuid
                    description: Eindeutiger Schlüssel des Schadensvorgangs seitens Paledo
        '201':
           description: Schadensmeldung wurde aktualisiert
           # ... (ähnlich wie 200)
        '401':
          description: Fahrzeug nicht vorhanden # Beispiel für Fehler-Response
      requestBody:
        description: Schadensmeldung anlegen/aktualisieren.
        required: true
        content:
          application/json:
            schema:
              type: object
              required:
                - PAD_MELDUNGSID
                - GESCHAEFTSBEREICH
                - FAHRZEUGNR
                - ERFASSUNGSZEIT
              properties:
                PAD_MELDUNGSID:
                  type: integer
                  format: int32
                  description: Eindeutiger Schlüssel des Schadensvorgangs seitens Cockpit
                  example: 123
                GESCHAEFTSBEREICH:
                  type: string
                  example: VKU
                FAHRZEUGNR:
                  type: string
                  example: '50187'
                ERFASSUNGSZEIT:
                  type: string
                  format: date-time
                FAHRERNR:
                  type: string
                  example: AFINK
                KOMMENTARE:
                  type: array
                  items:
                    type: object
                    # ... (weitere Properties für Kommentare)
                SCHADENSLISTE:
                  type: array
                  items:
                    type: object
                    # ... (weitere Properties für Schadensliste)
                SCHADENSFOTOS:
                  type: array
                  items:
                    type: object
                    required:
                      - SCHADENSFOTOID
                      - SCHADENSFOTO
                    properties:
                      SCHADENSFOTOID:
                        type: string
                      SCHADENSFOTO:
                        type: string
                        format: byte # Base64 encodiertes Bild
      description: Erzeugt einen neuen Schaden oder aktualisiert einen bestehenden. Als Schlüssel wird hierbei die PAD_MELDUNGSID verwendet.
```

### 5.4 Verarbeitungsmodi (Synchron/Asynchron)

Über die Checkbox **"Direkte Verarbeitung"** in der Endpunkt-Detailmaske wird gesteuert, wie eingehende Nachrichten verarbeitet werden:

* **Direkte Verarbeitung = Aktiv (Synchron):**
  * Die Nachricht wird sofort nach dem Empfang an die zugeordnete Migrationsdefinition übergeben und verarbeitet.
  * Der Aufrufer erhält erst eine Antwort (z.B. HTTP 200 OK), nachdem die Verarbeitung abgeschlossen ist.
  * Geeignet für schnelle Operationen oder wenn der Aufrufer direkt ein Ergebnis der Verarbeitung benötigt.
* **Direkte Verarbeitung = Inaktiv (Asynchron):**
  * Die Nachricht wird nach dem Empfang zunächst in eine interne Queue eingereiht. Paledo antwortet dem Aufrufer sofort (z.B. mit HTTP 202 Accepted), ohne auf die eigentliche Verarbeitung zu warten.
  * Die eigentliche Verarbeitung erfolgt später durch den Server Job "Webservice Message Service" (siehe Abschnitt 8.1), der die Queue zyklisch abarbeitet.
  * Geeignet für zeitaufwändige Operationen oder wenn eine Entkopplung von Empfang und Verarbeitung gewünscht ist.

***

## 6. Konfiguration ausgehender REST-Aufrufe (Trigger)

Diese Funktionalität ermöglicht es Paledo, als Client zu agieren und bei bestimmten Ereignissen (Datenänderungen) proaktiv REST-Nachrichten an externe Systeme zu senden.

### 6.1 Maskenübersicht: Getriggerte REST-Aufrufe

Die Maske (erreichbar über `Administration → Webschnittstelle → Getriggerte REST-Aufrufe`) zeigt eine Liste aller konfigurierten ausgehenden Trigger.

* **Ribbon-Menü:** Standardfunktionen (`Neu`, `Speichern`, `Löschen`, etc.).
* **Übersichtstabelle:**
  * **Name:** Frei definierbarer Name des Triggers.
  * **Description:** Optionale Beschreibung des Zwecks.
  * **Aktiv?:** Checkbox zur Aktivierung/Deaktivierung des Triggers.
  * **REST API Call Action:** Die verwendete HTTP-Methode (z.B. `PUT`, `POST`).
  * **Exportdefinition:** (Optional) Zugeordnete Exportdefinition (Funktion nicht detailliert beschrieben).
  * **Authentifizierung:** Name des ggf. verwendeten OAuth2-Authentifizierungsprofils.
* **Interaktion:** Doppelklick öffnet Detailmaske, Checkboxen direkt änderbar, Spalten sortierbar.

### 6.2 Detailmaske: Getriggerter REST-Aufruf

Hier wird ein einzelner Trigger detailliert konfiguriert.

#### Allgemeine Einstellungen

* **Name:** Eindeutiger Name des Triggers.
* **Aktiv?:** Aktiviert/Deaktiviert den Trigger.
* **Description:** Freitextbeschreibung.
* **Zieltyp:** Die Paledo-Tabelle (Business Object Typ), die überwacht werden soll (z.B. "Störmeldung", "BOOrder").
* **Zieltyp-Filter:** Eine Paledo-Expression, die definiert, unter welchen Bedingungen ein Objekt dieses Typs den Trigger auslösen soll (siehe Abschnitt 6.4). Lässt sich über `[...]` im Expression Editor bearbeiten.

#### Trigger-Einstellungen

* **Mehrfaches Triggern... erlauben?:** Wenn aktiv, kann dasselbe Objekt bei wiederholtem Erfüllen der Filterbedingung erneut einen Aufruf auslösen.
* **Trigger zurücksetzen nach:** Entprellzeit. Definiert eine Mindestzeit, die verstreichen muss, bevor dasselbe Objekt erneut triggern kann (Format HH:MM:SS). `00:00:00` bedeutet potenziell sofortige Neutriggerung bei Änderung.
* **Bei Wertänderung – Wertberechnung:** Eine Paledo-Expression, die eine Eigenschaft des Objekts zurückgibt (z.B. `[WorkflowState]`). Der Trigger wird nur ausgelöst, wenn sich der _Wert dieser Eigenschaft_ ändert (und der Filter zutrifft). Dies ist wichtig für das mehrfache Triggern, um sicherzustellen, dass nur bei relevanten Änderungen (z.B. Statuswechsel) ausgelöst wird.

#### Webrequest-Konfiguration (Tab: Webrequest)

* **REST API URL:** Die Ziel-URL des externen REST-Endpunkts (kann relativ sein, wenn eine Basis-URL konfiguriert ist, oder absolut).
* **REST API Call Action:** Die zu verwendende HTTP-Methode (`PUT`, `POST`, `PATCH`, etc.).
* **Authentifizierung:** Auswahl eines vorkonfigurierten OAuth2-Profils (siehe Abschnitt 6.5.2). Bleibt leer, wenn API-Key-Authentifizierung über Header erfolgt.
* **Zusätzliche Header:** Ein Feld zur Definition von benutzerdefinierten HTTP-Headern im JSON-Format (Key-Value-Paare). Hier wird typischerweise ein API-Key für die Authentifizierung übergeben, falls kein OAuth2 verwendet wird (z.B. `"X-API-Key" : "meinSecretKey"`).
* **Exportdefinition:** (Optional) Auswahl einer Exportdefinition.
* **Body Ausdruck:** Eine Paledo-Expression, die zur Laufzeit den JSON-Body der ausgehenden Nachricht dynamisch generiert (siehe Abschnitt 6.3). Lässt sich über `[...]` im Expression Editor bearbeiten.

#### Logs

* Ein Bereich zur Anzeige von Informationen über die letzten Ausführungen dieses spezifischen Triggers für einzelne Objekte. Nützlich für Debugging und Nachverfolgung.

### 6.3 Body-Ausdruck im Expression Editor

Der JSON-Body für ausgehende Trigger-Nachrichten wird dynamisch über eine Paledo-Expression im Feld **"Body Ausdruck"** erstellt. Der `[...]`-Button öffnet den Expression Editor.

* **Funktion:** Ermöglicht die Erstellung komplexer JSON-Strukturen basierend auf den Daten des auslösenden Paledo-Objekts.
* **Typische Funktion:** `CREATEJSONSTRING(key1, value1, key2, value2, ...)` wird verwendet, um Key-Value-Paare in ein JSON-Objekt umzuwandeln.
* **Dynamische Werte:** Feldwerte des auslösenden Objekts können direkt verwendet werden (z.B. `[OVPadID]`, `[WorkflowState].[Name]`).
* **Logik:** Funktionen wie `IIF(condition, true_value, false_value)`, `TOINT()`, `TOSTRING()` etc. können verwendet werden, um Werte zu transformieren oder bedingt zu setzen.
* **Editor-UI:** Bietet Zugriff auf Objektfelder, Operatoren und Paledo-Funktionen zur komfortablen Erstellung des Ausdrucks.

**Beispiel-Ausdruck:**

```plaintext
CREATEJSONSTRING(
  'PAD_MELDUNGSID', TOINT([OVPadID]),
  'STATUS',
    IIF([WorkflowState].[Name] = 'In Arbeit', 'in Bearbeitung',
    IIF([WorkflowState].[Name] = 'Aufgenommen', 'offen',
    IIF([WorkflowState].[Name] = 'Erledigt', 'erledigt', 'unbekannt')))
)
```

Erzeugt JSON wie: `{"PAD_MELDUNGSID": 123, "STATUS": "in Bearbeitung"}`

### 6.4 Zieltyp-Filter (Expression-Filter)

Der **"Zieltyp-Filter"** definiert die genauen Bedingungen, wann ein Objekt des gewählten Zieltyps den Trigger auslösen soll. Auch dieser Filter wird über den `[...]`-Button in einem grafischen Expression Editor bearbeitet.

* **Funktion:** Verhindert, dass der Trigger für jedes Objekt oder bei jeder kleinen Änderung ausgelöst wird.
* **Editor:** Ermöglicht die grafische Erstellung von UND/ODER-verknüpften Bedingungen basierend auf den Feldern des Zieltyps.
* **Operatoren:** Standardvergleichsoperatoren (`=`, `<>`, `Is Null`, `Is Not Null`, `Contains`, etc.) stehen zur Verfügung.

**Beispiel-Filter (logisch):**`( [Storniert am] ist Null UND [OVPad ID] ist nicht leer ) UND ( [Prozessstatus.Name] = 'Aufgenommen' ODER [Prozessstatus.Name] = 'In Arbeit' ODER [Prozessstatus.Name] = 'Erledigt' )`

Dieser Filter stellt sicher, dass nur nicht-stornierte Objekte mit einer externen ID und in einem relevanten Status den Trigger auslösen.

### 6.5 Authentifizierung für getriggerte REST-Aufrufe

Für ausgehende REST-Aufrufe unterstützt Paledo primär zwei Authentifizierungsmethoden:

#### API Key (in Header-Konfiguration)

* Der API-Schlüssel wird als statischer Wert in den **"Zusätzliche Header"** der Trigger-Konfiguration eingetragen, typischerweise als `X-API-Key`-Header.
* Einfachste Methode, wenn das Zielsystem dies unterstützt.

#### OAuth2-Authentifizierung

* Für Systeme, die eine dynamische Token-basierte Authentifizierung (OAuth2) erfordern.
* Die Konfiguration erfolgt in einem separaten Modul und wird im Trigger referenziert.

**6.5.1 Maskenübersicht: Getriggerte REST-Aufrufe Authentifizierung**

* Erreichbar über `Administration → Webschnittstelle → Getriggerte REST-Aufrufe Authentifizierung`.
* Zeigt eine Liste aller vorkonfigurierten OAuth2-Profile.
* Ribbon-Menü für `Neu`, `Speichern`, `Löschen`.
* Spalten: **Name** (des OAuth2-Profils).

**6.5.2 Detailmaske: Authentifizierungsprofil (OAuth2)**

* Öffnet sich bei Doppelklick auf ein Profil oder über `Neu`.
* Felder zur Konfiguration des OAuth2 Client Credentials Flows:
  * **Name:** Interner Name zur Referenzierung (z.B. "OVPad Auth").
  * **Client ID:** Vom Zielsystem bereitgestellte Client ID.
  * **Client Secret:** Das zugehörige Secret.
  * **Scope:** (Optional) Benötigte Berechtigungsbereiche.
  * **Audience:** (Optional) Zielgruppe des Tokens (spezifisch für manche Auth-Server).
  * **Endpunkt:** Die URL des Token-Endpunkts des Authentifizierungsservers.
  * **CSRF-Token Endpunkt:** (Optional) Für Systeme mit zusätzlichem CSRF-Schutz.

**6.5.3 Verwendung in Triggern**

* In der Detailmaske des getriggerten REST-Aufrufs wird im Feld **"Authentifizierung"** das gewünschte, vorkonfigurierte OAuth2-Profil ausgewählt.
* Paledo holt dann zur Laufzeit automatisch ein Access Token vom konfigurierten Endpunkt unter Verwendung der Client ID/Secret und fügt es dem ausgehenden Request hinzu (üblicherweise als `Authorization: Bearer ` Header).

### 6.6 Rückverarbeitung von Responses

Paledo kann die Antwort (Response), die es von einem externen Endpunkt nach einem getriggerten Aufruf erhält, weiterverarbeiten.

* **Zweck:** Um Informationen aus der Antwort (z.B. eine externe ID, einen Erfolgsstatus) zurück in das Paledo-Objekt zu schreiben, das den Trigger ausgelöst hat.
* **Konfiguration:** Erfordert die Zuweisung einer **Migrationsdefinition** (ähnlich wie bei eingehenden Endpunkten), die den JSON-Body der Response parsen und die Daten entsprechend im Paledo-System aktualisieren kann. (Die genaue Konfiguration hierfür war im Chat nicht detailliert beschrieben, aber das Prinzip wurde erwähnt).

***

## 7. Webservice Nachrichten (Monitoring & Fehlerbehandlung)

Dieses Modul dient als zentrales Logbuch für alle über die generische REST API laufenden Kommunikationen.

### 7.1 Maskenübersicht: Webservice-Nachrichten

* Erreichbar über `Administration → Webschnittstelle → Webservice-Nachrichten`.
* Zeigt eine chronologische Liste aller ein- und ausgehenden REST-Nachrichten.

#### Filteroptionen

* **Zeitraum:** Vordefinierte Filter (Heute, Gestern, Dieses Jahr etc.) oder benutzerdefinierte Datumsbereiche (`Von`/`Bis`).
* **Textsuche:** Volltextsuche in den Nachrichteninhalten.
* **Filter zurücksetzen:** Setzt alle Filter zurück.

#### Tabellenfelder

* **Erstellungsdatum:** Zeitstempel des Nachrichteneingangs/-ausgangs in Paledo.
* **Kennung:** Name des Endpunkts (eingehend) oder des Triggers (ausgehend).
* **Webservice Action:** HTTP-Methode (`PUT`, `POST`, etc.).
* **Letzte Verarbeitung:** Zeitstempel der letzten (ggf. wiederholten) Verarbeitung durch den Server Job (nur relevant bei asynchroner Verarbeitung).
* **Zeitpunkt erfolgreiche Verarbeitung:** Zeitstempel der erfolgreichen Verarbeitung (nur relevant bei asynchroner Verarbeitung).
* **Wiederholungen:** Anzahl der Verarbeitungsversuche (relevant bei Fehlern und Retries).
* **Status:** Verarbeitungsstatus (`Successfully processed`, `Failed`, `Skipped`).

#### Hinweise zur Ansicht (Grenzen, Fehlerhervorhebung)

* **Fehlerhafte Nachrichten:** Zeilen von Nachrichten mit dem Status `Failed` werden **rot hervorgehoben**.
* **Anzeigelimit:** Die Ansicht ist standardmäßig auf **2000 Datensätze begrenzt**. Bei Überschreitung erscheint eine Warnung. Filterung ist dann notwendig, um ältere Nachrichten zu sehen.

### 7.2 Detailmaske: Webservice-Nachricht

* Öffnet sich bei Doppelklick auf einen Eintrag in der Übersicht.
* Zeigt alle Details zu einer einzelnen Nachricht.

#### Kopfzeilenfelder

* Zeigt die Werte der Spalten aus der Übersichtstabelle für die ausgewählte Nachricht (Kennung, Action, Status, Verarbeitungszeiten, Wiederholungen).
* **Fehlermeldung:** Enthält die detaillierte Fehlermeldung, falls die Verarbeitung fehlgeschlagen ist.

#### Inhaltsbereich (JSON-Body)

* Zeigt den vollständigen **JSON-Body** der Nachricht (entweder der empfangene Request-Body bei eingehenden Nachrichten oder der gesendete Request-Body bei ausgehenden Nachrichten).
* Essentiell für die Fehleranalyse, um zu sehen, welche Daten gesendet/empfangen wurden.

### 7.3 Manuelle Wiederverarbeitung (Retry processing)

* Über den Button **"Retry processing"** in der Ribbon-Leiste (sowohl in der Übersicht als auch in der Detailmaske) können Nachrichten mit dem Status `Failed` manuell zur erneuten Verarbeitung angestoßen werden.
* Dies ist nützlich, nachdem das zugrundeliegende Problem (z.B. Fehlkonfiguration der Migrationsdefinition, temporäres Netzwerkproblem) behoben wurde.
* Die erneute Verarbeitung erfolgt durch den "Webservice Message Service" Job.

### 7.4 Automatische Bereinigung

* Gespeicherte Webservice-Nachrichten werden nicht unbegrenzt aufbewahrt.
* Ein automatisierter Job löscht alte Einträge nach einer konfigurierbaren Zeit (z.B. 30 Tage) aus der Datenbank, um die Datenbankgröße zu begrenzen.

***

## 8. Hintergrundverarbeitung: Server Jobs

Zwei zentrale Server Jobs sind für den Betrieb der asynchronen Verarbeitung und der Trigger-Funktionalität notwendig. Sie müssen im Bereich `Administration → Server Jobs & Monitoring → Server Jobs` konfiguriert und aktiviert sein.

### 8.1 Server Job: Webservice Message Service

* **Funktion:** Verarbeitet Nachrichten aus der internen Queue. Dies umfasst:
  * Erstverarbeitung von Nachrichten, die über einen **asynchron konfigurierten** Endpunkt empfangen wurden.
  * Wiederholte Verarbeitung von Nachrichten, die manuell über **"Retry processing"** angestoßen wurden.
* **Konfiguration:**
  * **Typ:** `Webservice Message Service`
  * **Intervall (s):** Bestimmt, wie oft der Job die Queue prüft (z.B. `30`).
  * **Ist Aktiv:** Muss aktiviert sein, damit die Verarbeitung stattfindet.
  * Weitere Felder wie `Priorität`, `Fehler s.l. Erfolg` steuern das Laufzeitverhalten.
* **Wichtigkeit:** Ohne diesen aktiven Job findet **keine asynchrone Verarbeitung** statt.

### 8.2 Server Job: Getriggerter REST API Service

* **Funktion:** Überwacht kontinuierlich die konfigurierten und aktiven REST-Trigger.
  * Prüft zyklisch, ob die **Filterbedingungen** für Objekte der jeweiligen Zieltypen erfüllt sind.
  * Prüft bei Triggern mit "Bei Wertänderung", ob sich der relevante Wert seit der letzten Prüfung geändert hat.
  * Löst bei erfüllten Bedingungen die Erstellung und den Versand der konfigurierten REST-Nachricht aus.
* **Konfiguration:**
  * **Typ:** `Getriggerter Rest Api Service`
  * **Intervall (s):** Bestimmt, wie oft auf Trigger-Ereignisse geprüft wird (z.B. `30`).
  * **Ist Aktiv:** Muss aktiviert sein, damit Trigger ausgelöst werden.
  * Weitere Felder analog zum anderen Job.
* **Wichtigkeit:** Ohne diesen aktiven Job werden **keine ausgehenden Trigger-Nachrichten** versendet, auch wenn sie korrekt konfiguriert sind.

***

## 9. Best Practices & Hinweise zur Datenkonsistenz

### Risiken bei Fehlkonfiguration

Die unsachgemäße Konfiguration der generischen REST API birgt erhebliche Risiken:

* **Dateninkonsistenzen:** Unvollständige oder fehlerhafte Migrationsdefinitionen können zu falschen oder inkonsistenten Daten in Paledo führen.
* **Unerwünschte Nebeneffekte:** Falsch definierte Filter in Triggern können zu einer Flut ungewollter REST-Aufrufe führen oder wichtige Aufrufe unterdrücken.
* **Referenzfehler:** Eine falsche Verarbeitungsreihenfolge (z.B. bei abhängigen Daten über mehrere Endpunkte) kann zu Fehlern führen.
* **Sicherheitslücken:** Unsichere API-Keys oder fehlerhafte OAuth2-Konfigurationen können unbefugten Zugriff ermöglichen.

### Empfehlungen

* **Nur durch Experten:** Die Konfiguration sollte ausschließlich von Personen mit tiefem Paledo- und Schnittstellen-Know-how durchgeführt werden.
* **Testen, Testen, Testen:** Neue Endpunkte oder Trigger immer zuerst in einer Testumgebung einrichten und ausführlich testen, bevor sie produktiv geschaltet werden.
* **YAML Validierung:** YAML-Spezifikationen vor dem Einfügen in Paledo mit externen Tools (z.B. Swagger Editor) validieren.
* **Logging nutzen:** Die Webservice-Nachrichten regelmäßig überwachen, insbesondere nach Änderungen.
* **Inkrementelle Einführung:** Komplexe Schnittstellen schrittweise aufbauen und testen.
* **Dokumentation:** Konfigurationen und Mappings sorgfältig dokumentieren.

***

## 10. Anwendungsbeispiele aus der Praxis

### Beispiel 1: Anbindung eines Ticketsystems

1. **Eingehend:** Ein externes Ticketsystem (z.B. Jira, ServiceNow) sendet eine neue Störungsmeldung per `POST`-Request an einen Paledo-Endpunkt (`/data/neuesTicket`). Der JSON-Body enthält Ticket-ID, Beschreibung, Melder etc. Eine Migrationsdefinition erstellt daraus eine Paledo-Instandhaltungsmeldung.
2. **Ausgehend:** Ein Trigger in Paledo überwacht die Instandhaltungsmeldungen. Wenn der Status auf "Erledigt" gesetzt wird, löst der Trigger einen `PUT`-Request an das Ticketsystem aus (`/api/tickets/{externeTicketID}`), der den JSON-Body `{"status": "closed"}` enthält, um das Ticket dort zu schließen.

### Beispiel 2: Schadenmeldung aus Fahrzeug-App

1. **Eingehend:** Eine mobile App für Fahrer sendet Schadensmeldungen per `PUT` an den Endpunkt `/data/ovpadschaden`. Der JSON-Body enthält Fahrzeugnummer, Fahrer, Erfassungszeit, eine Liste von Schadenskatalogeinträgen und optional Base64-kodierte Fotos (siehe YAML-Beispiel in 5.3).
2. **Verarbeitung:** Eine Migrationsdefinition liest die Daten, verknüpft sie mit dem Paledo-Equipment (Fahrzeug), legt eine Störmeldung an, hängt Kommentare und die dekodierten Bilder als Dokumente an.

### Beispiel 3: Paledo meldet Statusänderungen

1. **Ausgehend:** Ein Trigger überwacht die Tabelle `BOOrder` (Aufträge). Der Filter prüft auf `[WorkflowState].[Name] = 'Erledigt'`.
2. **Aktion:** Wenn ein Auftrag erledigt wird, sendet der Trigger einen `POST`-Request an ein externes System (z.B. ein Abrechnungssystem) mit einem JSON-Body, der Auftragsnummer, Equipmentnummer und den neuen Status enthält, dynamisch generiert über den Body-Ausdruck.