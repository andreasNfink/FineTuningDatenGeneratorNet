---
icon: shoe-prints
---

# Step-by-Step

Okay, hier ist eine detaillierte Schritt-für-Schritt-Anleitung zum Import von Arbeitsplätzen (`BOWorkCenter`) aus einer CSV-Datei nach Paledo mithilfe einer Migrationsdefinition.

***

**Anleitung: CSV-Import von Arbeitsplätzen (`BOWorkCenter`)**

**Ziel:** Importieren einer Liste von Arbeitsplätzen aus einer CSV-Datei in das Paledo Business Objekt `BOWorkCenter`, wobei bestehende Arbeitsplätze aktualisiert und neue angelegt werden sollen.

**Voraussetzungen:**

* Zugriff auf das Modul "Migrationsdefinitionen".
* Eine **CSV-Datei** mit den Arbeitsplatzdaten. Die Datei sollte:
  * Eine **Kopfzeile** mit eindeutigen Spaltennamen enthalten (z.B. `Code`, `Name`, `Beschreibung`, `Kostenstelle`).
  * Einen konsistenten **Trennzeichen** verwenden (z.B. Semikolon `;` oder Komma `,`).
  * Pro Zeile die Daten für einen Arbeitsplatz enthalten.
  * Eine Spalte mit einem **eindeutigen Identifikator** für jeden Arbeitsplatz enthalten (z.B. der Arbeitsplatz-Code).

**Beispiel CSV-Datei (`Arbeitsplaetze.csv` mit Semikolon als Trennzeichen):**

```csv
Code;Name;Beschreibung;Kostenstelle
APL-001;Mechanische Werkstatt;Werkstatt für Reparaturen;4711
APL-002;Elektro-Prüfstand;Prüfung elektrischer Komponenten;4712
APL-003;Montageplatz A;Endmontage Baugruppe X;4800
APL-004;Schweißerei;Schweißarbeiten Abteilung Y;4715
```

***

**Schritt 1: Migrationsdefinition erstellen**

1. Navigiere zu **Administration -> Import/Export & Datenmigration -> Importdefinitionen**.
2. Klicke im Ribbon auf **"Neu"**.
3. Vergib einen aussagekräftigen Namen, z.B. `CSV_Arbeitsplatz_Import`.
4. Speichere die leere Definition zunächst ab.

**Schritt 2: Datenquelle konfigurieren (CSV)**

1. Wähle im Kopfbereich als **Standard-Datenprovider** den Typ `CSV-File`.
2. Klicke im Ribbon auf **"Datenstruktur auflösen"**.
3. Im Dialog "Quelle" ist `CSV-File` bereits ausgewählt. Bestätige mit "OK" (oder wähle es ggf. erneut aus).
4. Es öffnet sich ein **Datei-Öffnen-Dialog**. Wähle deine **Beispiel-CSV-Datei** (`Arbeitsplaetze.csv`) aus.

    {% hint style="info" %}
    Paledo liest nun die Kopfzeile der CSV-Datei, um die verfügbaren Spaltennamen zu ermitteln. Es erkennt in der Regel automatisch das verwendete Trennzeichen.
    {% endhint %}
5. Wechsle zum Reiter **"Datenquelle"**. Du solltest nun links eine Tabelle (oft mit dem Dateinamen oder "Table1" benannt) und rechts die Spaltennamen aus deiner CSV-Datei sehen (z.B. `[Code]`, `[Name]`, `[Beschreibung]`, `[Kostenstelle]`).
6. Speichere die Definition erneut.

**Schritt 3: Migrationsziel definieren**

Da wir nur Arbeitsplätze importieren, benötigen wir in der Regel nur ein Migrationsziel.

1. Wechsle zum Reiter **"Migrationsdetails"**.
2. Klicke im oberen Bereich ("Migrationsziele") auf **"Neu"**.
3. Konfiguriere das Migrationsziel:
   * **Position:** `10` (da es das einzige Ziel ist).
   * **Zielobjekt:** Wähle `BOWorkCenter` aus der Liste.
   * **Importverhalten:** Wähle `Voreinstellung nicht leer`. Dies stellt sicher, dass keine Arbeitsplätze angelegt werden, wenn der eindeutige Code in der CSV-Datei fehlt oder leer ist.
   * **Name:** Gib einen einfachen Namen ein, z.B. `Arbeitsplatz`.
   * **Bemerkungen:** (Optional) z.B. "Importiert Arbeitsplätze aus CSV".

**Schritt 4: Zielfelder konfigurieren (Mapping)**

Jetzt werden die Spalten aus der CSV-Datei den Feldern des `BOWorkCenter`-Objekts zugeordnet.

1. Stelle sicher, dass das Migrationsziel "Arbeitsplatz" (von Schritt 3) oben ausgewählt ist.
2. Klicke im unteren Bereich ("Zielfelder") auf **"Neu"** für jede benötigte Zuordnung.

    **(A) Eindeutiger Identifikator (Code):**

    * **Priorität:** `10`
    * **Zielfeld:** Wähle `Code` (das Feld für den Arbeitsplatz-Code in Paledo).
    * **Wertausdruck:** Gib den Spaltennamen aus deiner CSV für den Code ein, z.B. `[Code]`.
    * **Filterausdruck:** Gib **denselben** Ausdruck wie im Wertausdruck ein: `[Code]`. **Dies ist entscheidend für die Erkennung vorhandener Arbeitsplätze!**
    * **Bemerkungen:** (Optional) Eindeutige Identifikation.

    **(B) Name des Arbeitsplatzes:**

    * **Priorität:** `20`
    * **Zielfeld:** Wähle `Name`.
    * **Wertausdruck:** Gib den Spaltennamen aus deiner CSV für den Namen ein, z.B. `[Name]`.
    * **Filterausdruck:** **Leer lassen** (Der Name dient nicht zur Identifikation).
    * **Bemerkungen:** (Optional) Bezeichnung des Arbeitsplatzes.

    **(C) Beschreibung (Optional):**

    * **Priorität:** `30`
    * **Zielfeld:** Wähle `Description`.
    * **Wertausdruck:** Gib den Spaltennamen aus deiner CSV für die Beschreibung ein, z.B. `[Beschreibung]`.
    * **Filterausdruck:** **Leer lassen**.
    * **Bemerkungen:** (Optional) Detaillierte Beschreibung.

    **(D) Kostenstelle (Optional):**

    * **Priorität:** `40`
    * **Zielfeld:** Wähle das entsprechende Feld in `BOWorkCenter` für die Kostenstelle (z.B. `CostCenter`, der genaue Name kann abweichen).
    * **Wertausdruck:** Gib den Spaltennamen aus deiner CSV für die Kostenstelle ein, z.B. `[Kostenstelle]`.
    * **Filterausdruck:** **Leer lassen**.
    * **Bemerkungen:** (Optional) Zugeordnete Kostenstelle.

{% hint style="warning" %}
Achte genau auf die **Groß- und Kleinschreibung** der Spaltennamen in den Wertausdrücken (z.B. `[Code]` vs. `[code]`). Sie müssen exakt mit den Namen in der CSV-Kopfzeile übereinstimmen.
{% endhint %}

**Schritt 5: Testen und Debuggen**

1. **Speichere** die gesamte Migrationsdefinition.
2. Führe eine **manuelle Migration** durch, indem du auf **"Migration durchführen"** klickst.
3. Im folgenden Dialog musst du erneut die **konkrete CSV-Datei auswählen**, die importiert werden soll (diesmal kann es die vollständige Datei sein, nicht nur die Beispieldatei). Bestätige mit "OK".
4. Beobachte den **Fortschrittsdialog**. Achte auf Erfolgs- oder Fehlermeldungen.
5. **Überprüfe das Ergebnis** in Paledo:
   * Wurden neue Arbeitsplätze korrekt angelegt?
   * Wurden bestehende Arbeitsplätze (falls welche mit den Codes aus der CSV schon existierten) aktualisiert?
   * Sind alle Felder korrekt befüllt?
6. Bei Fehlern:
   * Prüfe die Fehlermeldung im Dialog oder im Paledo Log.
   * Verwende **Haltepunkte** (z.B. auf dem Zielfeld `Code`), um die Werte während des Imports zu inspizieren.
   * Kontrolliere die CSV-Datei auf Formatierungsfehler (Trennzeichen, Anführungszeichen, leere Zeilen).
   * Stelle sicher, dass die Spaltennamen in den Ausdrücken exakt mit der CSV-Kopfzeile übereinstimmen.

**Schritt 6: Automatisierung (Optional)**

Wenn die CSV-Datei regelmäßig (z.B. täglich in einem bestimmten Ordner) bereitgestellt wird, kann der Import automatisiert werden:

1. Erstelle einen **Server Job** vom Typ **`Migrations-Service-Task`**.
2. Verknüpfe den Job mit deiner Migrationsdefinition (`CSV_Arbeitsplatz_Import`).
3. Konfiguriere die **Parameter** des Jobs, z.B.:
   * Einen **festen Pfad** zur CSV-Datei oder einen **Ordner**, der überwacht werden soll.
   * Ggf. Parameter zum Verschieben oder Löschen der Datei nach erfolgreichem Import.
4. Stelle das **Intervall** ein und **aktiviere** den Job.

***

Damit ist der Import von Arbeitsplätzen aus einer CSV-Datei konfiguriert und kann manuell oder automatisch ausgeführt werden.
