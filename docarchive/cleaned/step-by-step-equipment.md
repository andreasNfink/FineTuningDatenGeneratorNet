# Step-by-Step Equipment

**Szenario:** Importieren von Equipment-Stammdaten aus SAP, die über einen RFC-Baustein bereitgestellt werden (angenommen als XML-Struktur mit Segmenten wie `EQUIPMENT_LIST`, `DATA_GENERAL_EXP`, `DATA_SPECIFIC_EXP`). Die Definition soll Equipments anlegen oder aktualisieren und dabei auch Referenzen wie Planergruppe, Objektart und übergeordnete Strukturen (Parent Equipment oder TPL) korrekt auflösen und zuweisen.

***

**Anleitung: SAP Equipmentimport aus XML-Daten**

**Voraussetzungen:**

* Zugriff auf das Paledo-Modul "Migrationsdefinitionen".
* Eine **Beispiel-XML-Datei**, die die Datenstruktur des SAP-Exports widerspiegelt (mit den Segmenten und Feldern wie im ``-Tag des bereitgestellten Exports).
* Verständnis der relevanten Paledo Business Objekte: `BOEquipment`, `BOFunctionalLocation`, `BOPlannerGroup`, `BOComponentType`.
* Ggf. eine definierte Migrationszuordnung ("Mapping") namens 'Objektart' für die Umsetzung von SAP Objektarten auf Paledo ComponentTypes.
* Ggf. eine spezifische Funktion wie `FindFuncLoc` zur TPL-Suche (wie im XML verwendet).

***

**Schritt 1: Neue Migrationsdefinition anlegen**

1. Navigiere zu: **Administration -> Import/Export & Datenmigration -> Importdefinitionen**.
2. Klicke im Ribbon auf **"Neu"**.
3. Gib als **Namen** ein: `SAP_Equipment_Import_RFC` (oder ähnlich wie im XML: `SAP Equipmentimport`).
4. Speichere die leere Definition.

**Schritt 2: Datenquelle konfigurieren**

1. Wähle im Kopfbereich als **Standard-Datenprovider** `Xml-File` (basierend auf der Annahme, dass die SAP-Daten als XML vorliegen).
   * _Hinweis: Der XML-Export zeigt "Spreadsheet", was aber im Kontext der komplexen Struktur unwahrscheinlich ist. Wir folgen der Struktur für XML._
2. Klicke im Ribbon auf **"Datenstruktur auflösen"**.
3. Wähle im Dialog "Quelle" `Xml-File` aus und bestätige.
4. Wähle im Datei-Dialog deine **Beispiel-XML-Datei** aus.
5. Wechsle zum Reiter **"Datenquelle"**. Überprüfe, ob die Segmente (`EQUIPMENT_LIST`, `DATA_GENERAL_EXP`, `DATA_SPECIFIC_EXP` etc.) und deren Felder korrekt erkannt wurden. Der **Hauptdatenmember** (aus dem XML abgeleitet) ist `EQUIPMENT_LIST`. Diesen ggf. im Feld `DataMember` der Definition eintragen.
6. Speichere die Definition.

**Schritt 3: Migrationsziele definieren (Reihenfolge beachten!)**

Wechsle zum Reiter **"Migrationsdetails"**. Erstelle die folgenden Ziele in der exakten Reihenfolge ihrer `Position` aus der XML-Datei:

1. **Ziel 1: Planergruppe (Referenz)**
   * **Position:** `1`
   * **Zielobjekt:** `SynX.Xaf.Paledo.SAP.BusinessObjects.BOPlannerGroup`
   * **Importverhalten:** `Voreinstellung nicht leer` (Default `0` im XML entspricht oft diesem oder Standard)
   * **Name:** `Planergruppeobjekt`
2. **Ziel 2: Objektart Default (Fallback)**
   * **Position:** `3`
   * **Zielobjekt:** `SynX.Xaf.Paledo.Core.BusinessObjects.Component.BOComponentType`
   * **Importverhalten:** `Voreinstellung nicht leer`
   * **Name:** `ObjektartDefault`
3. **Ziel 3: Objektart (Mapping)**
   * **Position:** `4`
   * **Zielobjekt:** `SynX.Xaf.Paledo.Core.BusinessObjects.Component.BOComponentType`
   * **Importverhalten:** `Voreinstellung nicht leer`
   * **Name:** `Objektart`
4. **Ziel 4: Parent Equipment (Referenz)**
   * **Position:** `5`
   * **Zielobjekt:** `SynX.Xaf.Paledo.Core.BusinessObjects.Component.BOEquipment`
   * **Importverhalten:** `Voreinstellung nicht leer`
   * **Name:** `EquipmentParent`
5. **Ziel 5: Parent TPL (Referenz)**
   * **Position:** `6`
   * **Zielobjekt:** `SynX.Xaf.Paledo.Core.BusinessObjects.Component.BOFunctionalLocation`
   * **Importverhalten:** `Voreinstellung nicht leer`
   * **Name:** `TPLParent`
6. **Ziel 6: Equipment (Hauptobjekt)**
   * **Position:** `7`
   * **Zielobjekt:** `SynX.Xaf.Paledo.Core.BusinessObjects.Component.BOEquipment`
   * **Importverhalten:** `Standard`
   * **Name:** `Equipment`

**Speichere** die Definition nach dem Anlegen der Ziele.

_Hinweis: Die Pfeile im Diagramm deuten Abhängigkeiten an, die sich aus den Zielfeld-Referenzen ergeben._

***

**Schritt 4: Zielfelder konfigurieren**

Konfiguriere nun die Feldzuweisungen für jedes Migrationsziel gemäß der XML-Vorlage:

1. **Für Ziel "Planergruppeobjekt":**
   * **Priorität:** `1`
   * **Zielfeld:** `Code`
   * **Wertausdruck:** `[DATA_GENERAL_EXP.PLANGROUP]`
   * **Filterausdruck:** `[DATA_GENERAL_EXP.PLANGROUP]`
2. **Für Ziel "ObjektartDefault":**
   * **Priorität:** `1`
   * **Zielfeld:** `Name`
   * **Wertausdruck:** `GetMappedValue('Objektart', '')` (Sucht Default-Wert aus Mapping 'Objektart')
   * **Filterausdruck:** `GetMappedValue('Objektart', '')`
3. **Für Ziel "Objektart":**
   * **Priorität:** `1`
   * **Zielfeld:** `Name`
   * **Wertausdruck:** `GetMappedValue('Objektart', [DATA_GENERAL_EXP.OBJECTTYPE])` (Mapped SAP-Typ)
   * **Filterausdruck:** `GetMappedValue('Objektart', [DATA_GENERAL_EXP.OBJECTTYPE])`
   * **Priorität:** `2`
   * **Zielfeld:** `IsEquipmentType`
   * **Wertausdruck:** `true` (Markiert dies als Equipment-Typ)
   * **Priorität:** `3`
   * **Zielfeld:** ``
   * **Wertausdruck:** `Iif(IsNullOrEmpty([DATA_SPECIFIC_EXP.READ_SUPEQ]) And IsNullOrEmpty([DATA_SPECIFIC_EXP.READ_FLOC]),'Equipment', '')` (Wenn kein Parent (EQ oder TPL) vorhanden, springe direkt zum Haupt-Equipment Ziel 7)
   * **Priorität:** `4`
   * **Zielfeld:** ``
   * **Wertausdruck:** `Iif(IsNullOrEmpty([DATA_SPECIFIC_EXP.READ_SUPEQ]), 'TPLParent','EquipmentParent')` (Wenn kein Parent-EQ, springe zu TPL-Suche (Pos 6), sonst zu EQ-Suche (Pos 5))
4. **Für Ziel "EquipmentParent":**
   * **Priorität:** `1`
   * **Zielfeld:** `Equipment`
   * **Wertausdruck:** `SAPNumber([DATA_SPECIFIC_EXP.READ_SUPEQ])`
   * **Filterausdruck:** `SAPNumber([DATA_SPECIFIC_EXP.READ_SUPEQ])`
   * **Priorität:** `2`
   * **Zielfeld:** ``
   * **Wertausdruck:** `Iif(IsNullOrEmpty([.ComponentType]), '' , 'Equipment')` (Prüft, ob gefundenes Parent-EQ einen Typ hat, sonst Abbruch?, springt sonst zu Ziel 7)
   * **Priorität:** `3`
   * **Zielfeld:** `ComponentType`
   * **Wertausdruck:** `[]` (Weist dem Parent ggf. Default-Typ zu?) - _Logik hier prüfen_
   * **Priorität:** `4`
   * **Zielfeld:** ``
   * **Wertausdruck:** `'Equipment'` (Springe immer zu Ziel 7, nachdem Parent-EQ verarbeitet wurde)
5. **Für Ziel "TPLParent":**
   * **Priorität:** `1`
   * **Zielfeld:** _(Leer lassen)_
   * **Wertausdruck:** _(Leer lassen)_
   * **Filterausdruck:** `FindFuncLoc([DATA_SPECIFIC_EXP.READ_FLOC], 'Technischer Platz', 'SAPObjectKey', true)` (Sucht TPL über spezielle Funktion)
   * **Priorität:** `2`
   * **Zielfeld:** ``
   * **Wertausdruck:** `Iif(IsNullOrEmpty([.ComponentType]), '' , 'Equipment')` (Prüft, ob gefundener TPL einen Typ hat, springt sonst zu Ziel 7)
   * **Priorität:** `3`
   * **Zielfeld:** `ComponentType`
   * **Wertausdruck:** `[]` (Weist TPL ggf. Default-Typ zu?) - _Logik hier prüfen_
   * **Priorität:** `4`
   * **Zielfeld:** ``
   * **Wertausdruck:** `'Equipment'` (Springe immer zu Ziel 7, nachdem Parent-TPL verarbeitet wurde)
6. **Für Ziel "Equipment" (Hauptziel):**
   * **Priorität:** `1` (Identifikation)
   * **Zielfeld:** `Equipment`
   * **Wertausdruck:** `SAPNumber([EQUIPMENT])`
   * **Filterausdruck:** `SAPNumber([EQUIPMENT])`
   * **Priorität:** `2`
   * **Zielfeld:** `ComponentType`
   * **Wertausdruck:** `[]` (Referenziert den gemappten Typ aus Ziel 3)
   * **Priorität:** `3`
   * **Zielfeld:** `Name`
   * **Wertausdruck:** `[DATA_GENERAL_EXP.DESCRIPT]`
   * **Priorität:** `4`
   * **Zielfeld:** `Hersteller`
   * **Wertausdruck:** `[DATA_GENERAL_EXP.MANFACTURE]`
   * **Priorität:** `5`
   * **Zielfeld:** `PlannerGroup`
   * **Wertausdruck:** `[]` (Referenziert Planergruppe aus Ziel 1)
   * **Priorität:** `6`
   * **Zielfeld:** `Baujahr`
   * **Wertausdruck:** `[DATA_GENERAL_EXP.CONSTYEAR]`
   * **Priorität:** `7`
   * **Zielfeld:** `Kostenstelle`
   * **Wertausdruck:** `[DATA_GENERAL_EXP.COSTCENTER]`
   * **Priorität:** `9`
   * **Zielfeld:** `Hersteller Teil Nr`
   * **Wertausdruck:** `[DATA_GENERAL_EXP.MANPARNO]`
   * **Priorität:** `10`
   * **Zielfeld:** `Hersteller Serial Nr`
   * **Wertausdruck:** `[DATA_GENERAL_EXP.MANSERNO]`
   * **Priorität:** `11`
   * **Zielfeld:** `Leergewicht`
   * **Wertausdruck:** `[DATA_GENERAL_EXP.OBJ_WEIGHT]`
   * **Priorität:** `12`
   * **Zielfeld:** `Typbezeichnung`
   * **Wertausdruck:** `[DATA_GENERAL_EXP.MANMODEL]`
   * **Priorität:** `13`
   * **Zielfeld:** `SubNumber` (SAP Anlagennr.)
   * **Wertausdruck:** `[DATA_GENERAL_EXP.SUB_NUMBER]`
   * **Priorität:** `14` (Parent-Zuweisung!)
   * **Zielfeld:** `Parent`
   * **Wertausdruck:** `IIF(IsNull([]), [], [])` (Weist Parent EQ zu, wenn gefunden, sonst Parent TPL)
   * **Priorität:** `15`
   * **Zielfeld:** `SAPObjectKey` (Feld zur Speicherung der TPL-Referenz aus SAP)
   * **Wertausdruck:** `[DATA_SPECIFIC_EXP.READ_FLOC]`
   * **Priorität:** `16`
   * **Zielfeld:** `Inventar Nr`
   * **Wertausdruck:** `[DATA_GENERAL_EXP.INVENTORY]`
   * _(Füge weitere Felder nach Bedarf hinzu)_

**Speichere** die Definition nach Abschluss der Feldzuweisungen.

***

**Schritt 5: Globale Optionen konfigurieren**

1. Wechsle zum Reiter **"Optionen"**.
2. Setze ggf. die Option **"Stornierte Einträge ignorieren"** auf `true` (wie im XML).
3. Trage einen **Kommentar** ein, z.B. "Importiert SAP Equipments inkl. Hierarchie (Parent EQ/TPL), Planergruppe und Objektart aus RFC-XML."
4. Speichere die Definition.

***

**Schritt 6: Testen und Ausführen**

1. **Manueller Test:** Führe die Migration über **"Migration durchführen"** mit deiner Test-XML-Datei aus.
2. **Ergebnis prüfen:** Kontrolliere in Paledo, ob Equipments korrekt angelegt/aktualisiert wurden und ob die Referenzen (Typ, Planergruppe, Parent) stimmen.
3. **Debugging:** Nutze **Haltepunkte**, insbesondere bei den Zielen für Parents (`EquipmentParent`, `TPLParent`) und bei der finalen Parent-Zuweisung im Hauptziel `Equipment`, um die Logik und die gefundenen Objekte zu prüfen.

***

**Schritt 7: Automatisierung**

1. Erstelle einen **Server Job** (Typ `SAP Migration Task` oder `Migrations-Service-Task`, je nachdem, wie die XML-Daten bereitgestellt werden).
2. Verknüpfe den Job mit der Definition `SAP_Equipment_Import_RFC`.
3. Konfiguriere Job-Parameter (Intervall, SAP-Details/Dateipfad etc.).
4. Aktiviere den Job für den regelmäßigen Import.

***

Diese Anleitung bildet die komplexe Logik des XML-Exports nach. Besonderes Augenmerk liegt auf der korrekten Reihenfolge der Ziele, der Verwendung von Referenzen (`[]`), speziellen Funktionen (`SAPNumber`, `GetMappedValue`, `FindFuncLoc`) und der bedingten Zuweisung der Parent-Struktur.