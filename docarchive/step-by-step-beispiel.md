---
icon: shoe-prints
---

# Step-by-Step

**Zielsetzung:** Ein externes MES-System soll Stammdaten und ggf. Produktionsdaten zu Turbinen an Paledo senden können. Paledo soll diese Daten über einen dedizierten REST-Endpunkt empfangen und zur Aktualisierung oder Neuanlage von Turbinen-Objekten (z.B. `BOEquipment` für die Turbine selbst, `BOFuncLoc` für den Standort) verwenden.

**Szenario:** Das MES sendet JSON-Daten per HTTP PUT-Request an einen Paledo-Endpunkt. Die Authentifizierung erfolgt über einen API-Key im Header. Die Verarbeitung der Daten in Paledo soll über eine Migrationsdefinition erfolgen.

***

## Voraussetzungen

Bevor Sie beginnen, stellen Sie sicher, dass folgende Voraussetzungen erfüllt sind (gemäß Abschnitt 2 der Hauptanleitung):

1. **Know-how:** Sie verfügen über tiefgreifende Kenntnisse in Paledo, insbesondere:
   * Paledo Datenmodell (speziell `BOEquipment`, `BOFuncLoc` oder äquivalente Objekte für Turbinen).
   * Paledo Migrationsdefinitionen (Erstellung und Funktionsweise).
   * REST-Prinzipien, HTTP, JSON und YAML.
   * API-Key-Authentifizierung.
2. **Paledo-System:**
   * Paledo Interface Toolkit (PIT) ist lizenziert und aktiv.
   * Netzwerkzugriff vom MES-System zum Paledo-Server ist gewährleistet.
   * Der Paledo-Server ist für eingehende HTTPS-Anfragen auf dem entsprechenden Port erreichbar.
3. **Vorbereitete Migrationsdefinition:** Eine Paledo-Migrationsdefinition **muss bereits existieren**, die den erwarteten JSON-Body des MES verarbeiten und die Daten korrekt auf die Paledo-Objekte (`BOEquipment`, `BOFuncLoc`) mappen kann. Nennen wir diese fiktive Definition **`MD_MES_Turbine_Import`**. Die Erstellung dieser Definition ist _nicht_ Teil dieses Tutorials, aber eine zwingende Voraussetzung.

***

## Schritt 1: Navigieren zur REST-Schnittstellenkonfiguration

1. Öffnen Sie den Paledo Client.
2. Navigieren Sie im Menü zu:`Administration → Webschnittstelle → REST-Schnittstelle Konfiguration`\
   (Siehe Abschnitt 4 der Hauptanleitung)

***

## Schritt 2: Neuen REST-Endpunkt anlegen

1. In der Maskenübersicht "REST-Schnittstelle Konfiguration" klicken Sie in der oberen Ribbon-Leiste auf die Schaltfläche **"Neu"**.\
   (Siehe Abschnitt 5.1 der Hauptanleitung)
2. Es öffnet sich die Detailmaske für einen neuen REST-Endpunkt.

***

## Schritt 3: Basisdaten des Endpunkts konfigurieren

Füllen Sie die folgenden Felder im oberen Bereich der Detailmaske aus (Siehe Abschnitt 5.2 der Hauptanleitung):

1. **Endpunkt-Name:** Geben Sie einen eindeutigen, technischen Namen für den Endpunkt ein. Dieser wird Teil der URL.
   * _Beispiel:_ `MES_Turbine_Data`
   * Die resultierende URL wird etwa so aussehen: `https://<IhrPaledoHost>:<Port>/data/MES_Turbine_Data`
2. **API Key:** Generieren Sie einen sicheren, zufälligen API-Schlüssel (z.B. eine lange Zeichenkette oder UUID) und fügen Sie ihn hier ein. Diesen Schlüssel muss das MES-System später im `X-API-Key` HTTP-Header mitsenden.
   * _Beispiel:_ `a3b8e4f1-z9d7-4c6a-b1e0-d9f2a1b3c4d5` (Dies ist nur ein Beispiel, verwenden Sie einen eigenen sicheren Schlüssel!)
3. **Ist aktiv:** Setzen Sie hier vorerst **keinen** Haken, bis die gesamte Konfiguration abgeschlossen und getestet ist. Sie aktivieren den Endpunkt später.
4. **Direkte Verarbeitung:** Entscheiden Sie über den Verarbeitungsmodus (Siehe Abschnitt 5.4 der Hauptanleitung):
   * **Haken gesetzt (Synchron):** Das MES erhält erst eine Antwort, wenn Paledo die Datenverarbeitung (via Migrationsdefinition) abgeschlossen hat. Gut für sofortiges Feedback, kann aber bei vielen Daten oder komplexer Logik länger dauern.
   * **Haken nicht gesetzt (Asynchron):** Paledo bestätigt den Empfang sofort (z.B. HTTP 202), die eigentliche Verarbeitung erfolgt im Hintergrund durch einen Server Job. Besser für Entkopplung und zeitaufwändige Prozesse.
   * _Für dieses Tutorial wählen wir:_ **Haken nicht gesetzt (Asynchron)**.

{% hint style="warning" %}
**Wichtig:** Für `GET`-Anfragen und solche, die direkt Daten in der Serverantwort zurückliefern sollen, muss der Haken bei `Direkte Verarbeitung` gesetzt sein.
{% endhint %}

***

## Schritt 4: YAML-Spezifikation definieren

1. Wechseln Sie in der Detailmaske zum Tab **"Spezifikation"**.
2. Geben Sie hier die OpenAPI 3.x YAML-Definition ein, die den erwarteten Request vom MES beschreibt. Diese muss mindestens die verwendete HTTP-Methode (hier `PUT`), den erwarteten JSON-Body (Schema) und die Sicherheitsanforderung (API Key) definieren.\
    (Siehe Abschnitte 5.2 und 5.3 der Hauptanleitung)

    _Beispiel YAML für Turbinendaten (vereinfacht):_

    ```yaml
    openapi: 3.1.0
    info:
      title: Paledo MES Turbine API
      version: '1.0'
      description: API zur Übermittlung von Turbinen-Stammdaten vom MES an Paledo.
    components:
      securitySchemes:
        ApiKeyAuth:
          type: apiKey
          in: header
          name: X-API-Key # Muss exakt dem Header entsprechen, den Paledo erwartet
    paths:
      /: # Der Pfad relativ zu /data/MES_Turbine_Data
        put:
          summary: Turbinendaten anlegen oder aktualisieren
          operationId: put-turbine-data
          security:
            - ApiKeyAuth: [] # Verweist auf das definierte Security Scheme
          requestBody:
            description: JSON-Objekt mit Turbinen-Stammdaten.
            required: true
            content:
              application/json:
                schema:
                  type: object
                  required:
                    - turbine_id_mes # Eindeutige ID aus dem MES
                    - serial_number
                    - location_code # Standortkennung
                  properties:
                    turbine_id_mes:
                      type: string
                      description: Eindeutige ID der Turbine im MES.
                      example: "TURBINE-007"
                    serial_number:
                      type: string
                      description: Seriennummer der Turbine.
                      example: "SN-987654321"
                    location_code:
                      type: string
                      description: Code des technischen Platzes / Standorts.
                      example: "SITE-A/HALL-3/POS-12"
                    manufacturer:
                      type: string
                      description: Hersteller der Turbine.
                      example: "ACME Turbines Inc."
                    model:
                      type: string
                      description: Turbinenmodell.
                      example: "WindMaster 5000"
                    commissioning_date:
                      type: string
                      format: date
                      description: Inbetriebnahmedatum (YYYY-MM-DD).
                      example: "2023-10-26"
                    operating_hours:
                      type: number
                      format: float
                      description: Aktuelle Betriebsstunden.
                      example: 1578.5
          responses:
            '202': # Da wir asynchron gewählt haben, ist 202 Accepted eine passende Antwort
              description: Anfrage erfolgreich angenommen, Verarbeitung erfolgt asynchron.
            '400':
              description: Ungültige Anfrage (z.B. fehlende Pflichtfelder, falsches Format).
            '401':
              description: Authentifizierung fehlgeschlagen (API Key fehlt oder ist falsch).
            '500':
              description: Interner Serverfehler bei der Annahme der Anfrage.

    ```

3. Stellen Sie sicher, dass die YAML-Syntax korrekt ist.

***

## Schritt 5: Migrationsdefinition zuweisen

1. Zurück im oberen Bereich der Detailmaske, klicken Sie auf das Dropdown-Feld **"Migrationsdefinition"**.
2. Wählen Sie die vorbereitete Migrationsdefinition **`MD_MES_Turbine_Import`** aus der Liste aus.\
   (Siehe Abschnitt 5.2 der Hauptanleitung)

***

## Schritt 6: Endpunkt speichern

1. Überprüfen Sie alle Eingaben nochmals sorgfältig.
2. Klicken Sie in der Ribbon-Leiste auf **"Speichern"** oder **"Speichern und Schließen"**.\
   (Siehe Abschnitt 5.2 der Hauptanleitung)

***

## Schritt 7: Server Job für asynchrone Verarbeitung prüfen (Wichtig!)

Da wir die asynchrone Verarbeitung gewählt haben (Checkbox "Direkte Verarbeitung" ist _nicht_ gesetzt), müssen Sie sicherstellen, dass der zuständige Server Job läuft:

1. Navigieren Sie zu `Administration → Server Jobs & Monitoring → Server Jobs`.
2. Suchen Sie den Job mit dem Typ **`Webservice Message Service`**.
3. Stellen Sie sicher, dass dieser Job **"Ist Aktiv"** ist und ein sinnvolles **"Intervall (s)"** (z.B. 30 oder 60 Sekunden) konfiguriert ist.\
   (Siehe Abschnitt 8.1 der Hauptanleitung)
4. Falls der Job nicht aktiv ist, aktivieren Sie ihn und speichern Sie die Änderung. Ansonsten werden die empfangenen Nachrichten nicht verarbeitet!

***

## Schritt 8: Endpunkt aktivieren

1. Gehen Sie zurück zur Übersicht der REST-Schnittstellenkonfiguration (`Administration → Webschnittstelle → REST-Schnittstelle Konfiguration`).
2. Suchen Sie Ihren neu erstellten Endpunkt `MES_Turbine_Data`.
3. Setzen Sie den Haken in der Spalte **"Ist aktiv"** direkt in der Liste oder öffnen Sie die Detailmaske erneut und setzen Sie dort den Haken bei "Ist aktiv" und speichern Sie.\
   (Siehe Abschnitte 5.1 und 5.2 der Hauptanleitung)

Der Endpunkt ist nun bereit, Anfragen vom MES zu empfangen.

***

## Schritt 9: Testen des Endpunkts

1. Verwenden Sie ein externes Tool wie `Postman`, `curl` oder ein Testskript, um einen HTTP PUT-Request an die Endpunkt-URL zu senden (z.B. `https://<IhrPaledoHost>:<Port>/data/MES_Turbine_Data`).
2. Stellen Sie sicher, dass der Request folgende Merkmale hat:

    * Methode: `PUT`
    * Header `Content-Type`: `application/json`
    * Header `X-API-Key`: Der von Ihnen in Schritt 3 konfigurierte API-Schlüssel.
    * Request Body: Ein JSON-Objekt, das der in Schritt 4 definierten YAML-Spezifikation entspricht._Beispiel JSON-Body:_

    ```json
    {
      "turbine_id_mes": "TURBINE-008",
      "serial_number": "SN-112233445",
      "location_code": "SITE-B/HALL-1/POS-01",
      "manufacturer": "ACME Turbines Inc.",
      "model": "WindMaster 5000",
      "commissioning_date": "2024-01-15",
      "operating_hours": 250.0
    }
    ```

3. Senden Sie den Request ab. Erwarten Sie eine HTTP-Antwort (z.B. `202 Accepted`, wenn asynchron konfiguriert).

***

## Schritt 10: Überwachung und Fehlerbehebung

1. Navigieren Sie in Paledo zu `Administration → Webschnittstelle → Webservice-Nachrichten`.\
   (Siehe Abschnitt 7 der Hauptanleitung)
2. Suchen Sie nach dem Log-Eintrag für Ihre Testanfrage (Filter nach Datum/Uhrzeit oder Kennung `MES_Turbine_Data`).
3. Prüfen Sie den **Status**:
   * **Wartend (Initial):** Wenn asynchron, sollte die Nachricht kurz hier erscheinen, bevor der Server Job sie aufnimmt.
   * **Successfully processed:** Die Verarbeitung durch die Migrationsdefinition war erfolgreich. Überprüfen Sie die entsprechenden Paledo-Objekte (`BOEquipment`/`BOFuncLoc`), ob die Daten korrekt angelegt/aktualisiert wurden.
   * **Failed:** Die Verarbeitung ist fehlgeschlagen. Die Zeile ist rot markiert.
     * Öffnen Sie die Detailansicht der Nachricht (Doppelklick).
     * Lesen Sie die **Fehlermeldung**. Diese stammt oft aus der Migrationsdefinition (z.B. "Objekt nicht gefunden", "Pflichtfeld fehlt", "Datentyp-Konflikt").
     * Überprüfen Sie den **Inhalt** (JSON-Body) der Nachricht auf Fehler.
     * Korrigieren Sie das Problem (z.B. in der Migrationsdefinition `MD_MES_Turbine_Import` oder im gesendeten JSON).
     * Nach der Korrektur können Sie die fehlgeschlagene Nachricht markieren und über **"Retry processing"** erneut zur Verarbeitung anstoßen.

***

## Wichtige Hinweise (Siehe Abschnitt 9 der Hauptanleitung)

* **Testumgebung:** Führen Sie diese Schritte immer zuerst in einer dedizierten Paledo-Testumgebung durch.
* **Datenkonsistenz:** Stellen Sie sicher, dass die Logik in Ihrer Migrationsdefinition robust ist, um Dubletten oder inkonsistente Daten zu vermeiden (z.B. korrekte Identifizierung bestehender Objekte anhand der `turbine_id_mes`).
* **Sicherheit:** Behandeln Sie den API Key vertraulich. Erwägen Sie ggf. weitere Sicherheitsmaßnahmen (IP-Whitelisting auf Netzwerkebene).
* **Performance:** Bei sehr hohem Datenvolumen kann die asynchrone Verarbeitung und die Performance der Migrationsdefinition entscheidend sein.

***

Sie haben nun erfolgreich einen eingehenden REST-Endpunkt in Paledo konfiguriert, um Daten von einem externen MES-System zu empfangen.
