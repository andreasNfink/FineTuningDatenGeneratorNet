---
icon: file-certificate
---

# Lizenzverwaltung

## 1. Einführung

Willkommen zum Anwenderhandbuch für den Paledo Lizenzmanager. Dieses Modul ist ein zentrales Werkzeug innerhalb des Administrationsbereichs von Paledo und dient der effizienten und rechtskonformen Verwaltung aller Softwarelizenzen. Mit dem Lizenzmanager können Administratoren den Zugriff auf Paledo und seine verschiedenen Module und Funktionen für Benutzer und Geräte präzise steuern.

Ziel dieser Anleitung ist es, Ihnen einen umfassenden Überblick über die Funktionalitäten des Lizenzmanagers zu geben und Sie Schritt für Schritt durch die verschiedenen Verwaltungsaufgaben zu führen. Dies umfasst das Anlegen und Verwalten von Lizenzpools, das Zuweisen von Lizenzen zu Anwendern oder Geräten, den Import von Lizenzen sowie die Handhabung von Modullizenzen.

***

## 2. Systemüberblick und Einordnung des Lizenzmanagers

Paledo ist eine modular aufgebaute Softwarelösung für Instandhaltung und Service Management (CMMS). Das Lizenzmodell von Paledo ist entsprechend differenziert, um den unterschiedlichen Anforderungen und Einsatzszenarien gerecht zu werden. Der Lizenzmanager ist nahtlos in die Benutzeroberfläche (basierend auf Blazor) und das zugrundeliegende DevExpress XAF-Framework integriert. Er ist ein integraler Bestandteil der zentralen Administration von Paledo.

Die Kernaufgabe des Lizenzmanagers besteht darin, den Zugriff auf spezifische Funktionen, Module oder die Software als Ganzes basierend auf den erworbenen Lizenzen zu regeln. Dies gewährleistet nicht nur die Einhaltung lizenzrechtlicher Bestimmungen, sondern ermöglicht auch eine wirtschaftliche und flexible Nutzung der Software entsprechend den Bedürfnissen Ihres Unternehmens.

***

## 3. Lizenztypen und Editionen

Für die Nutzung von Paledo stehen verschiedene Lizenztypen und Lizenzeditionen zur Verfügung. Das Verständnis dieser Konzepte ist grundlegend für die effektive Arbeit mit dem Lizenzmanager. Details dazu sind unter [lizenzkonzept.md](lizenzkonzept.md "mention") beschrieben.

Die Kombination aus Lizenztyp und Edition (z.B. "Standard User License" oder "Professional Machine License") definiert den genauen Nutzungsumfang. Der Lizenzmanager hilft Ihnen, diese verschiedenen Lizenzen zu organisieren und korrekt zuzuweisen.

***

## 4. Lizenzpools verwalten

Ein **Lizenzpool** ist eine logische Gruppierung von kompatiblen Lizenzen. Kompatibel bedeutet in diesem Kontext, dass die Lizenzen denselben Lizenztyp (Benutzer- oder Gerätelizenz) und dieselbe Edition (Basic, Standard, Professional) aufweisen. Lizenzpools sind das zentrale Element zur Organisation und Verteilung Ihrer erworbenen Lizenzen.

### 4.1 Übersicht der Lizenzpool-Ansicht

Die Verwaltung der Lizenzpools erreichen Sie im Administrationsbereich von Paledo über den Menüpunkt (z.B. **Administration → Lizenzen → Lizenzpools** – der genaue Pfad kann je nach Paledo-Version leicht variieren).

<figure><img src="../../.gitbook/assets/Lizenzverwaltung/LizenzverwaltungListeLizenzpools.png" alt=""><figcaption>Paledo Liste der Lizenzpools</figcaption></figure>

In der Listenansicht der Lizenzpools werden typischerweise folgende Informationen pro Pool angezeigt:

* **Poolbeschreibung:** Ein frei wählbarer, aussagekräftiger Name für den Pool (z.B. "Standard Benutzerlizenzen - Instandhaltungsteam A").
* **Edition:** Die Software-Edition der im Pool enthaltenen Lizenzen (z.B. Standard, Professional).
* **Lizenztyp:** Der Typ der im Pool enthaltenen Lizenzen (z.B. Benutzerlizenz, Gerätelizenz).
* **Gültigkeit:** Der Gültigkeitszeitraum der Lizenzen im Pool (oft durch die Lizenz mit dem frühesten Ablaufdatum bestimmt).
* **Quota (Gesamtanzahl):** Die Gesamtzahl der Lizenzen, die diesem Pool zugeordnet sind.
* **Verfügbare Lizenzen:** Die Anzahl der Lizenzen im Pool, die aktuell keinem Lizenznehmer (Benutzer oder Gerät) zugewiesen sind.
* **Automatische Verteilung (Ja/Nein):** Zeigt an, ob für diesen Pool die automatische Lizenzzuweisung aktiviert ist.
* **Priorität:** Ein numerischer Wert, der die Priorität des Pools bei der automatischen Lizenzzuweisung bestimmt (ein kleinerer Wert bedeutet eine höhere Priorität).

### 4.2 Erstellen eines neuen Lizenzpools

Um eine strukturierte Lizenzverwaltung zu gewährleisten, können Sie neue Lizenzpools anlegen.

<figure><img src="../../.gitbook/assets/Lizenzverwaltung/LizenzverwaltungPoolErstellen.png" alt=""><figcaption>Paledo Lizenzpool erstellen</figcaption></figure>

1. Navigieren Sie zur Lizenzpool-Verwaltung (z.B. **Administration → Lizenzen → Lizenzpools**).
2. Klicken Sie auf die Schaltfläche **Neu** (oder ein entsprechendes Icon), um das Formular zum Erstellen eines neuen Lizenzpools zu öffnen.
3. Füllen Sie die erforderlichen Felder aus:
   * **Beschreibung:** Geben Sie eine klare und eindeutige Bezeichnung für den Pool ein. Diese Beschreibung hilft Ihnen später, den Pool schnell zu identifizieren (z.B. "Professional User Lizenzen - Key User", "Standard Maschinenlizenzen - Fertigungslinie 1").
   * **Typ und Edition:** Diese Felder werden in der Regel automatisch durch die erste Lizenz bestimmt, die Sie dem Pool hinzufügen. Sie können aber auch vorab ausgewählt werden, um die Kompatibilität sicherzustellen.
   * **Automatisch zuweisen (Checkbox):** Aktivieren Sie diese Option, wenn Lizenzen aus diesem Pool automatisch an Benutzer oder Geräte vergeben werden sollen, die eine entsprechende Lizenz benötigen und noch keine besitzen.
   * **Verteilungspriorität:** Falls "Automatisch zuweisen" aktiviert ist, geben Sie hier einen numerischen Wert ein. Pools mit einem niedrigeren Wert werden bei der automatischen Zuweisung bevorzugt behandelt. Dies ist nützlich, wenn es mehrere passende Pools gibt.
4. Klicken Sie auf **Speichern**, um den Lizenzpool anzulegen. Der Pool ist nun leer und bereit für die Aufnahme von Lizenzen.

### 4.3 Lizenzen zu einem Lizenzpool hinzufügen

Nachdem ein Lizenzpool erstellt wurde, müssen ihm die eigentlichen Lizenzen hinzugefügt werden. Diese Lizenzen müssen zuvor in Paledo importiert worden sein (siehe Abschnitt [Lizenzen importieren](./#lizenzen-importieren)).

1. Öffnen Sie den gewünschten Lizenzpool aus der Listenansicht per Doppelklick oder über eine entsprechende "Bearbeiten"-Aktion.
2. Innerhalb der Detailansicht des Lizenzpools finden Sie einen Reiter oder Bereich namens **Zuweisbare Lizenzen** (oder ähnlich).
3. Klicken Sie auf die Schaltfläche **Add License to Pool** (oder "Lizenzen hinzufügen").

<figure><img src="../../.gitbook/assets/Lizenzverwaltung/LizenzverwaltungLizenzenZuweisen.png" alt=""><figcaption>Paledo Lizenz zu Pool hinzufügen</figcaption></figure>

4. Es öffnet sich ein Dialogfenster, das eine Liste aller verfügbaren (d.h. noch keinem anderen Pool zugewiesenen und nicht vergebenen) Lizenzen anzeigt.

<figure><img src="../../.gitbook/assets/Lizenzverwaltung/LizenzverwaltungLizenzenZuweisenDIalog.png" alt=""><figcaption>Paledo Liste aller verfügbaren Lizenzen</figcaption></figure>

5. **Wichtig:** Das System filtert diese Liste in der Regel automatisch, sodass nur Lizenzen angezeigt werden, die hinsichtlich Typ und Edition mit dem aktuellen Pool kompatibel sind. Wenn der Pool beispielsweise für "Standard User Licenses" konfiguriert ist, können Sie keine "Professional Machine Licenses" hinzufügen.
6. Wählen Sie die gewünschten Lizenzen aus der Liste aus (Mehrfachauswahl ist üblicherweise möglich).
7. Bestätigen Sie Ihre Auswahl. Die ausgewählten Lizenzen werden nun dem Pool hinzugefügt und erhöhen dessen Quota.

### 4.4 Lizenzen aus einem Lizenzpool entfernen

Es kann vorkommen, dass Sie Lizenzen aus einem Pool entfernen müssen, beispielsweise um sie einem anderen Pool zuzuweisen oder weil sie nicht mehr benötigt werden.

1. Öffnen Sie den betreffenden Lizenzpool.
2. Navigieren Sie zum Reiter/Bereich **Zuweisbare Lizenzen**.
3. Wählen Sie die Lizenz oder die Lizenzen aus, die Sie aus dem Pool entfernen möchten.
4. Klicken Sie auf die Schaltfläche **Remove License from Pool** (oder "Lizenzen aus Pool entfernen").

<figure><img src="../../.gitbook/assets/Lizenzverwaltung/LizenzverwaltungLizenzEntfernen.png" alt=""><figcaption>Paledo Lizenz aus Pool entfernen</figcaption></figure>

5. **Wichtig:** Eine Lizenz kann nur dann aus einem Pool entfernt werden, wenn sie aktuell **nicht** einem Lizenznehmer (Benutzer oder Gerät) zugewiesen ist. Sollte die Lizenz noch in Gebrauch sein, muss sie zuerst vom Lizenznehmer entfernt werden (siehe Abschnitt [Entfernung oder Wechsel von Lizenzen bei Lizenznehmern](./#entfernung-oder-wechsel-von-lizenzen)).
6. Bestätigen Sie die Aktion. Die Lizenzen werden aus dem Pool entfernt und stehen wieder zur Verfügung, um beispielsweise einem anderen Pool hinzugefügt zu werden.

***

## 5. Lizenznehmer verwalten (Zuweisung von Lizenzen)

Ein **Lizenznehmer** ist entweder ein Paledo-Benutzerkonto (im Falle einer Benutzerlizenz) oder ein registriertes Gerät (im Falle einer Gerätelizenz). Die Verwaltung der Lizenznehmer erfolgt typischerweise direkt im Kontext eines Lizenzpools.

### 5.1 Zuweisung einer Lizenz per Drag & Drop

Eine der komfortabelsten Methoden zur Zuweisung von Lizenzen in Paledo ist die Drag & Drop-Funktionalität.

1. Öffnen Sie den Lizenzpool, aus dem Sie eine Lizenz zuweisen möchten.
2. Innerhalb der Detailansicht des Lizenzpools finden Sie einen Reiter oder Bereich namens **Lizenznehmer** (oder "Zugewiesene Lizenzen", "Benutzer/Geräte"). Dieser Bereich ist oft zweigeteilt:
   * Eine Liste der verfügbaren, potenziellen Lizenznehmer (z.B. alle Benutzer, die noch keine Lizenz dieses Typs/dieser Edition haben, oder alle registrierten Geräte).
   * Eine Liste der Lizenznehmer, denen bereits eine Lizenz aus diesem Pool zugewiesen wurde.
3. Identifizieren Sie den gewünschten Benutzer oder das Gerät aus der Liste der verfügbaren Lizenznehmer.
4. Klicken Sie auf den Eintrag des Benutzers/Geräts, halten Sie die Maustaste gedrückt und ziehen Sie (Drag) den Eintrag in den Bereich, der die bereits zugewiesenen Lizenznehmer anzeigt, oder direkt auf den Pool selbst (je nach UI-Design).
5. Lassen Sie die Maustaste los (Drop).
6. **Validierungslogik:** Bevor die Zuweisung endgültig erfolgt, führt Paledo im Hintergrund eine Validierung durch:
   * **Gültigkeit des Pools:** Ist der Pool aktiv und enthält er verfügbare Lizenzen?
   * **Kompatibilität:** Passt der Lizenznehmertyp zum Pooltyp? (z.B. kann ein Benutzer nur eine Benutzerlizenz aus einem Benutzerlizenz-Pool erhalten).
   * **Edition:** Ist die Edition des Pools für den vorgesehenen Zweck ausreichend oder gewünscht?
   * **Doppelte Zuweisung:** Hat der Benutzer/das Gerät bereits eine gleichwertige oder höherwertige Lizenz? (Das System könnte hier eine Warnung anzeigen oder die Zuweisung verhindern).
7. Bei erfolgreicher Validierung wird dem Benutzer/Gerät eine Lizenz aus dem Pool zugewiesen. Die Anzahl der verfügbaren Lizenzen im Pool verringert sich entsprechend.

### 5.2 Entfernung oder Wechsel von Lizenzen bei Lizenznehmern

Lizenzen können von Benutzern oder Geräten entfernt oder auf andere Pools (und somit potenziell andere Lizenztypen oder Editionen) gewechselt werden.

**Lizenz entfernen:**

1. Öffnen Sie den Lizenzpool, dem der Lizenznehmer aktuell zugewiesen ist.
2. Navigieren Sie zum Reiter/Bereich **Lizenznehmer**.
3. Suchen Sie den Benutzer oder das Gerät in der Liste der zugewiesenen Lizenznehmer.
4. Wählen Sie den Eintrag aus und verwenden Sie eine entsprechende Aktion wie **Lizenz entziehen**, **Entfernen** oder ein Papierkorb-Symbol.
5. Bestätigen Sie die Aktion. Die Lizenz wird dem Pool wieder als verfügbar gutgeschrieben.
   * **Anwendungsfall:** Ein Mitarbeiter verlässt das Unternehmen, ein Gerät wird außer Betrieb genommen.

**Lizenz wechseln (Upgrade/Downgrade/Typwechsel):**

Ein direkter "Wechsel" ist oft ein Prozess aus Entfernen und neu Zuweisen, kann aber durch Drag & Drop zwischen Pools vereinfacht werden:

1. Öffnen Sie die Ansicht, in der sowohl der Quell-Pool (mit der aktuell zugewiesenen Lizenz) als auch der Ziel-Pool (mit der gewünschten neuen Lizenz) sichtbar sind, oder öffnen Sie beide Pools in separaten Ansichten, falls das UI dies unterstützt.
2. Identifizieren Sie den Lizenznehmer im Quell-Pool.
3. Ziehen Sie den Lizenznehmer per Drag & Drop vom Quell-Pool in den Ziel-Pool.
4. Paledo führt im Hintergrund die notwendigen Schritte aus:
   * Entfernt die Lizenz aus dem Quell-Pool vom Lizenznehmer.
   * Weist eine Lizenz aus dem Ziel-Pool dem Lizenznehmer zu.
   * Auch hier greift die Validierungslogik (Kompatibilität, Verfügbarkeit im Ziel-Pool).
   * **Anwendungsfall:** Ein Benutzer benötigt eine höhere Edition (z.B. von Standard auf Professional) oder wechselt von einer projektbezogenen Gerätelizenz zu einer persönlichen Benutzerlizenz.

**Wichtig:** Eine Lizenz kann in der Regel nur entfernt werden, wenn sie nicht aktiv genutzt wird (z.B. der Benutzer ist nicht angemeldet oder das Gerät führt keine Paledo-Operationen aus, die diese Lizenz erfordern). Ggf. muss der Benutzer abgemeldet werden.

***

## 6. Automatische Lizenzzuweisung

Die automatische Lizenzzuweisung ist eine mächtige Funktion, um den administrativen Aufwand bei der Lizenzvergabe zu minimieren. Wenn aktiviert, kann Paledo Benutzern oder Geräten, die auf lizenzpflichtige Funktionen zugreifen möchten und noch keine passende Lizenz besitzen, automatisch eine Lizenz aus einem dafür vorgesehenen Pool zuweisen.

### 6.1 Aktivierung und Konfiguration

Die automatische Lizenzzuweisung wird auf Ebene des einzelnen Lizenzpools konfiguriert:

1. Öffnen Sie den Lizenzpool, für den Sie die automatische Zuweisung aktivieren möchten.
2. Suchen Sie in den Einstellungen des Pools die Option **Automatisch zuweisen** (oder ähnlich) und aktivieren Sie die entsprechende Checkbox.
3. **Verteilungspriorität:** Weisen Sie dem Pool eine **Priorität** zu. Dies ist ein numerischer Wert (z.B. 1, 10, 100). Pools mit einem _kleineren_ Wert haben eine _höhere_ Priorität.
   * **Beispiel:** Pool A hat Priorität 10, Pool B hat Priorität 20. Benötigt ein Benutzer eine Lizenz, die in beiden Pools verfügbar wäre, wird Paledo versuchen, die Lizenz zuerst aus Pool A zu beziehen. Dies ist nützlich, um z.B. teurere Lizenzen (Professional) nur dann automatisch zu vergeben, wenn keine günstigeren (Standard) mehr verfügbar sind, oder um bestimmte Abteilungen zu bevorzugen.

### 6.2 Funktionsweise der automatischen Zuweisung

Wenn ein Benutzer eine Aktion in Paledo ausführt oder ein Modul öffnet, für das eine bestimmte Lizenz (Typ und ggf. Edition) erforderlich ist, prüft das System:

1. **Vorhandene Lizenz:** Besitzt der Benutzer (oder das Gerät) bereits eine gültige, manuell zugewiesene Lizenz, die den Zugriff erlaubt?
   * Wenn ja: Zugriff wird gewährt.
2. **Keine passende manuelle Lizenz:** Wenn keine direkt zugewiesene Lizenz vorhanden ist, prüft das System, ob es Lizenzpools gibt, die:
   * den benötigten Lizenztyp und die benötigte Edition enthalten.
   * die Option "Automatisch zuweisen" aktiviert haben.
   * noch verfügbare Lizenzen haben.
3. **Priorisierte Auswahl:** Gibt es mehrere solcher Pools, wählt das System den Pool mit der höchsten Priorität (kleinster Zahlenwert).
4. **Automatische Zuweisung:** Ist ein passender Pool gefunden, wird dem Benutzer/Gerät automatisch eine Lizenz aus diesem Pool zugewiesen. Der Zugriff wird gewährt. Die Anzahl der verfügbaren Lizenzen im Pool verringert sich.
5. **Keine Lizenz verfügbar:** Kann keine Lizenz zugewiesen werden (weder manuell noch automatisch), wird der Zugriff verweigert, und es erscheint eine entsprechende Meldung. Dies wird auch protokolliert (siehe [Protokolle und Lizenzverstöße](./#protokolle-und-lizenzverstoesse)).

Diese Automatik vereinfacht die Administration erheblich, da nicht jeder Lizenzbedarf manuell gedeckt werden muss, insbesondere in dynamischen Umgebungen oder bei vielen Anwendern.

***

## 7. Lizenzen importieren

Bevor Lizenzen in Pools organisiert und Benutzern oder Geräten zugewiesen werden können, müssen sie in das Paledo-System importiert werden. Lizenzen werden typischerweise vom Softwarehersteller (Paledo GmbH oder Vertriebspartner) in Form einer digitalen Lizenzdatei (häufig im XML-Format) bereitgestellt.

Der Importvorgang gestaltet sich wie folgt:

1. Navigieren Sie zum zentralen Lizenzverwaltungsbereich in Paledo (z.B. **Administration → Lizenzen → Lizenzverwaltung** – der genaue Pfad kann variieren).
2. Suchen Sie nach einer Schaltfläche oder Menüoption mit der Bezeichnung **Lizenzen importieren** (oder "Import License File").

<figure><img src="../../.gitbook/assets/Lizenzverwaltung/LizenzverwaltungLizenzenimportieren.png" alt=""><figcaption>Paledo Lizenzen importieren</figcaption></figure>

3. Klicken Sie auf diese Schaltfläche. Es öffnet sich ein Dateiauswahldialog.
4. Wählen Sie die von Paledo erhaltene Lizenzdatei (z.B. `licenses.xml`) von Ihrem lokalen System oder einem Netzlaufwerk aus.
5. Bestätigen Sie die Auswahl.
6. **Verarbeitung und Pool-Zuweisungsvorschlag:** Das System liest die Lizenzdatei ein und validiert die enthaltenen Lizenzen.
   * Jede Lizenz enthält Informationen über ihren Typ (Benutzer/Gerät/Modul), ihre Edition (Basic/Standard/Professional), ihre Gültigkeit und eine eindeutige ID.
   * Basierend auf der Edition und dem Typ der importierten Lizenzen kann Paledo Ihnen **automatisch Vorschläge zur Zuordnung zu bestehenden oder neu zu erstellenden Lizenzpools** machen. Wenn beispielsweise fünf "Standard User Licenses" importiert werden und bereits ein Pool für "Standard User Licenses" existiert, könnte das System vorschlagen, diese direkt dorthin zu verschieben. Existiert kein passender Pool, könnte das System die Erstellung eines neuen Pools vorschlagen.
7. Überprüfen Sie die importierten Lizenzen und die Zuweisungsvorschläge. Nehmen Sie ggf. Anpassungen vor.
8. Schließen Sie den Importvorgang ab. Die Lizenzen sind nun im System bekannt und können über die Lizenzpools weiter verwaltet werden.

Es ist ratsam, Lizenzdateien sicher aufzubewahren, auch nachdem sie importiert wurden.

***

## 8. Modullizenzen und systemweite Freischaltungen

Neben den Benutzer- und Gerätelizenzen, die den Zugriff einzelner Entitäten steuern, gibt es in Paledo auch **Modullizenzen**. Diese Lizenzen sind nicht an spezifische Benutzer oder Geräte gebunden, sondern schalten bestimmte Funktionalitäten oder ganze Module für die gesamte Paledo-Installation oder für bestimmte Serverkomponenten frei.

**Verwaltung von Modullizenzen:**

* Modullizenzen werden ebenfalls über den Lizenzimport (siehe Abschnitt [Lizenzen importieren](./#lizenzen-importieren)) in das System eingespielt.
* Es gibt oft einen eigenen Bereich oder Reiter im Lizenzmanager, z.B. **Modullizenzen** oder **Systemweite Lizenzen**, der eine Übersicht über alle erworbenen und aktiven Modullizenzen bietet.
* Hier wird angezeigt, welche Module (z.B. "Erweiterte Berichterstattung", "Mobile Instandhaltung", "KI-Analyse") lizenziert sind und ggf. deren Gültigkeitsdauer.

<figure><img src="../../.gitbook/assets/Lizenzverwaltung/LizenzverwaltungModullizenzen.png" alt=""><figcaption>Paledo Reiter Modullizenzen</figcaption></figure>

**Funktionsweise:**

* Ein **Modulschlüssel** oder eine spezifische Modullizenz bestimmt, welche übergeordneten Features und Programmteile in Paledo freigeschaltet sind.
* Die Verfügbarkeit dieser Funktionen für einen Endanwender hängt dann oft von einer Kombination ab:
  1. Das Modul muss systemweit durch eine Modullizenz freigeschaltet sein.
  2. Der zugreifende Benutzer (oder das Gerät) muss über eine passende Benutzer-/Gerätelizenz verfügen, deren Edition den Zugriff auf die Funktionen dieses Moduls erlaubt.
  3. **Beispiel:** Das Modul "Strategiemanagement" ist durch eine Modullizenz systemweit aktiviert. Ein Benutzer mit einer "Professional User License" kann auf dieses Modul zugreifen, ein Benutzer mit einer "Basic User License" jedoch nicht, auch wenn das Modul prinzipiell da ist.
* Es ist wichtig, dass die Edition der Client-Lizenz (Benutzer/Gerät) zur Edition des Servers bzw. des lizenzierten Moduls passt, um Inkompatibilitäten zu vermeiden.

Die Verwaltung von Modullizenzen ist in der Regel weniger kleinteilig als die von Benutzer-/Gerätelizenzen, da sie global wirken. Es geht primär um die Übersicht und die Sicherstellung, dass alle benötigten Module korrekt lizenziert und aktiv sind.

***

## 9. Protokolle und Umgang mit Lizenzverstößen

Eine transparente Nachverfolgung von Lizenzaktivitäten und das Erkennen von potenziellen Lizenzproblemen sind wichtige Aspekte der Lizenzverwaltung. Paledo bietet hierfür Protokollierungsfunktionen.

### 9.1 Lizenzprotokolle einsehen

Der Lizenzmanager oder ein zugehöriger Bereich in der Administration führt Protokolle über lizenzrelevante Ereignisse. Diese Protokolle (Logs) dokumentieren typischerweise:

* **Zeitpunkt:** Wann hat das Ereignis stattgefunden?
* **Modul/Funktion:** Auf welches Paledo-Modul oder welche Funktion wurde zugegriffen oder versucht zuzugreifen?
* **Lizenznehmer:** Welcher Benutzer oder welches Gerät war involviert?
* **Lizenzdetails:** Welche Lizenz wurde verwendet oder angefordert (Typ, Edition)?
* **Ergebnis:** War der Zugriff erfolgreich oder ist ein Fehler aufgetreten (z.B. Lizenz nicht vorhanden, Lizenz abgelaufen)?
* **Art der Zuweisung:** Wurde eine Lizenz manuell oder automatisch zugewiesen?

Diese Protokolle sind wertvoll für:

* **Fehlersuche:** Bei Zugriffsproblemen können die Logs erste Hinweise auf die Ursache geben.
* **Compliance-Überprüfung:** Nachweis der korrekten Lizenznutzung.
* **Nutzungsanalyse:** Erkennen, welche Lizenzen stark genutzt werden und wo möglicherweise Engpässe bestehen oder Lizenzen optimiert werden können.

Den Zugriff auf die Lizenzprotokolle finden Sie üblicherweise im Administrationsbereich unter einem Menüpunkt wie "Lizenzprotokolle", "Audit Trail" oder als Teil der Detailansicht von Lizenzpools oder einzelnen Lizenzen.

### 9.2 Umgang mit Lizenzverstößen

Ein Lizenzverstoß tritt auf, wenn ein Benutzer oder ein Gerät versucht, auf eine Funktion oder ein Modul von Paledo zuzugreifen, für das keine gültige Lizenz vorhanden oder zugewiesen ist. Typische Ursachen für Lizenzverstöße sind:

* **Keine Lizenz vorhanden:** Dem Benutzer/Gerät wurde keine Lizenz zugewiesen, und es konnte auch keine automatisch zugewiesen werden (z.B. weil alle Lizenzen in den Pools vergeben sind).
* **Nicht ausreichende Edition:** Der Benutzer/das Gerät besitzt zwar eine Lizenz, aber deren Edition (z.B. Basic) reicht nicht aus, um auf die gewünschte Funktion (z.B. eine Professional-Funktion) zuzugreifen.
* **Lizenz abgelaufen:** Die zugewiesene Lizenz oder die Lizenzen im Pool sind nicht mehr gültig.
* **Lizenztyp-Konflikt:** Es wird versucht, mit einer Gerätelizenz auf eine Funktion zuzugreifen, die eine Benutzerlizenz erfordert (oder umgekehrt), falls solche strikten Prüfungen implementiert sind.

<figure><img src="../../.gitbook/assets/Lizenzverwaltung/LizenzverwaltungLizenzverstoeße.png" alt=""><figcaption>Paledo Liste der Lizenzverstöße</figcaption></figure>

**Systemreaktion und Protokollierung:**

* Im Falle eines Lizenzverstoßes wird dem Benutzer der Zugriff auf die angeforderte Funktion verweigert. In der Regel erhält der Benutzer eine entsprechende Fehlermeldung.
* Der Vorfall wird detailliert im Lizenzprotokoll vermerkt, inklusive der genauen Fehlerursache (z.B. "Lizenz nicht gefunden", "Edition nicht ausreichend").

Als Administrator sollten Sie die Lizenzprotokolle regelmäßig auf solche Verstöße prüfen, um:

* Lizenzengpässe frühzeitig zu erkennen und ggf. zusätzliche Lizenzen zu beschaffen.
* Fehlkonfigurationen bei der Lizenzzuweisung zu korrigieren.
* Benutzer über die korrekte Nutzung und die Grenzen ihrer Lizenzen aufzuklären.

***

## 10. Spezialfälle und Best Practices

Eine effektive Lizenzverwaltung geht über die reine Bedienung des Lizenzmanagers hinaus. Hier einige Überlegungen zu Spezialfällen und bewährten Praktiken:

* **Offline-Nutzung (z.B. Tablets im Außendienst):**
  * Für Geräte, die häufig oder dauerhaft ohne Netzwerkverbindung zu Paledo betrieben werden (z.B. Tablets von Servicetechnikern), sind **Professional Maschinenlizenzen** oft die beste Wahl. Diese können so konfiguriert sein, dass sie für einen bestimmten Zeitraum offline gültig bleiben, ohne ständige Verifizierung mit dem Server. Klären Sie die genauen Offline-Fähigkeiten mit dem Paledo-Support.
* **Produktionshallen und Schichtbetrieb:**
  * Hier eignen sich **Gerätelizenzen** (Machine Licenses) hervorragend. Ein Terminal oder ein Industrie-PC kann mit einer Gerätelizenz ausgestattet werden, und Mitarbeiter verschiedener Schichten können sich an diesem Gerät anmelden und Paledo nutzen, ohne dass jeder eine eigene Benutzerlizenz benötigt.
* **Cross-Plattform-Anwendung (Web, Desktop-Client, Mobile App):**
  * Wenn Mitarbeiter Paledo auf verschiedenen Plattformen nutzen (z.B. am Desktop-Client im Büro, per Webbrowser von unterwegs und mit der mobilen App beim Kunden), sind **Benutzerlizenzen** (User Licenses) in der Regel am sinnvollsten, da sie an die Person und nicht an ein spezifisches Gerät gebunden sind.
* **Lizenzen deaktivieren statt löschen:**
  * Wenn Lizenzen (z.B. durch Ablauf oder Austausch) nicht mehr aktiv genutzt werden, sollten sie im System eher als "deaktiviert" oder "archiviert" markiert werden, anstatt sie vollständig zu löschen. Dies hilft bei der Nachverfolgung des Lizenzbestands und bei Audits. Die genauen Mechanismen hierfür hängen von Paledo ab.
* **Regelmäßige Pool-Überwachung und -Pflege:**
  * Überwachen Sie regelmäßig die Auslastung Ihrer Lizenzpools.
  * Achten Sie auf das Ablaufdatum von Lizenzen. Paledo könnte Warnungen ausgeben, wenn Lizenzen bald ablaufen. Planen Sie rechtzeitig die Erneuerung oder den Ersatz.
  * Passen Sie Poolgrößen und automatische Zuweisungsregeln an veränderte Nutzungsanforderungen an.
* **Klare Benennung von Lizenzpools:**
  * Verwenden Sie sprechende Namen für Ihre Lizenzpools (z.B. "Std\_User\_Instandhaltung\_WerkA", "Pro\_Machine\_Fertigungslinie3"). Dies erleichtert die Administration erheblich.
* **Dokumentation Ihrer Lizenzstrategie:**
  * Halten Sie intern fest, warum Sie bestimmte Lizenztypen und -pools für welche Benutzergruppen oder Anwendungsfälle verwenden. Dies hilft bei der Einarbeitung neuer Administratoren und bei späteren Anpassungen.

***

## 11. Technische Grundlagen (für Administratoren)

Dieser Abschnitt richtet sich primär an technisch versierte Administratoren, die ein tiefergehendes Verständnis der Architektur des Paledo Lizenzmanagers wünschen.

* **Softwarearchitektur:**
  * Der Paledo Lizenzmanager ist typischerweise auf modernen Technologien wie **.NET (z.B. .NET 6 oder neuer)** aufgebaut.
  * Die Benutzeroberfläche für Administratoren ist oft als Webanwendung mit **Blazor** realisiert, während die Kernlogik und Datenmodelle im **DevExpress XAF (eXpressApp Framework)** verankert sein können. XAF bietet eine robuste Grundlage für die Erstellung von Geschäftsanwendungen.
* **Datenpersistenz:**
  * Die Lizenzinformationen (Lizenzen, Pools, Zuweisungen) werden in der Paledo-Datenbank gespeichert. Als Object-Relational Mapper (ORM) kommt häufig **XPO (eXpress Persistent Objects)** von DevExpress zum Einsatz, das die Abbildung von .NET-Objekten auf Datenbanktabellen übernimmt.
* **Wichtige Softwareklassen (konzeptionell):**
  * `BOAssignableLicense` (oder ähnlich): Repräsentiert eine einzelne, zuweisbare Lizenz mit ihren Eigenschaften (Typ, Edition, Gültigkeit, ID). "BO" steht oft für Business Object.
  * `BOLicensePool` (oder ähnlich): Stellt einen Lizenzpool dar, der eine Sammlung von `BOAssignableLicense`-Objekten verwaltet und Einstellungen für die automatische Zuweisung enthält.
  * `ILicenseManager` (oder ähnlich als Interface/Service-Klasse): Kapselt die zentrale Geschäftslogik für die Lizenzprüfung, -zuweisung und -verwaltung. Hier werden Anfragen validiert und Lizenzoperationen durchgeführt.
* **Sicherheitsaspekte:**
  * Lizenzdateien und die darin enthaltenen Schlüssel sind kryptographisch gesichert, um Manipulationen zu verhindern.
  * Für die Verifikation von Lizenzen und die Sicherung sensibler Daten können **FIPS-konforme Hashing-Algorithmen** und Verschlüsselungsverfahren zum Einsatz kommen.
* **Integration:**
  * Der Lizenzmanager ist eng mit dem Benutzer- und Geräteverwaltungsmodul von Paledo sowie mit den einzelnen Fachmodulen verzahnt, um Lizenzprüfungen bei jedem relevanten Zugriff durchführen zu können.

Ein Verständnis dieser Grundlagen kann bei der Fehleranalyse oder bei der Planung komplexer Lizenzszenarien hilfreich sein. Für detaillierte technische Informationen ist die offizielle Paledo-Entwicklerdokumentation oder der Paledo-Support zu konsultieren.

***

## 12. Beispiele für Lizenzstrategien nach Unternehmensform

Die optimale Lizenzstrategie hängt stark von der Struktur und den Arbeitsweisen Ihres Unternehmens ab. Hier einige beispielhafte Ansätze:

### Produktionsunternehmen mit Schichtbetrieb

* **Herausforderung:** Viele Mitarbeiter teilen sich wenige Arbeitsplätze/Geräte in der Produktion über mehrere Schichten hinweg.
* **Strategie:**
  * **Gerätebasierte Lizenzpools (Machine Licenses):** Für Terminals in der Werkshalle oder an den Produktionslinien werden Gerätelizenzen verwendet.
  * **Edition nach Bedarf:** Standard-Lizenzen für normale Werker, ggf. Professional-Lizenzen für Schichtleiter-Terminals mit erweiterten Analysefunktionen.
  * **Automatische Verteilung:** Kann genutzt werden, falls es unterschiedliche Geräte-Pools gibt (z.B. nach Abteilung).
  * **Prioritäten:** Abteilungsbasierte Prioritäten können sinnvoll sein, wenn Lizenzen knapp werden.
  * **Benutzerlizenzen:** Für Planer, Meister und Management, die von ihren Büro-PCs oder Laptops zugreifen.

### Dienstleistungsunternehmen mit Außendienst (z.B. Servicetechniker)

* **Herausforderung:** Mitarbeiter sind mobil, nutzen oft Laptops oder Tablets und benötigen auch offline Zugriff.
* **Strategie:**
  * **Benutzerlizenz-Pools (User Licenses):** Jeder Techniker erhält eine persönliche Benutzerlizenz.
  * **Edition:** Oft Professional-Lizenzen, um Offline-Fähigkeiten und den vollen Funktionsumfang mobil sicherzustellen.
  * **Dynamische Freigabe bei Inaktivität (falls unterstützt):** Wenn ein Techniker Paledo für längere Zeit nicht aktiv nutzt, könnte die Lizenz (falls technisch so vorgesehen) temporär freigegeben und für andere verfügbar gemacht werden (Floating-Aspekt). Dies ist jedoch ein fortgeschrittenes Szenario.
  * **Gerätelizenzen:** Ggf. für zentrale Geräte im Servicebüro, die von Innendienstmitarbeitern geteilt werden.

### Konzern mit mehreren Standorten und zentraler IT

* **Herausforderung:** Heterogene Nutzergruppen, unterschiedliche Anforderungen pro Standort, Wunsch nach zentraler Kontrolle und Kostenoptimierung.
* **Strategie:**
  * **Standortbasierte oder abteilungsbasierte Lizenzpools:** Erstellung von Pools, die spezifisch den Bedürfnissen einzelner Standorte oder großer Abteilungen entsprechen (z.B. "User\_Standard\_Standort\_Muenchen", "Machine\_Pro\_Forschung\_Berlin").
  * **Zentrale Lizenzverteilung und -überwachung:** Die IT-Abteilung verwaltet alle Pools zentral, überwacht die Auslastung und beschafft Lizenzen bedarfsgerecht.
  * **Kombinierte Typen/Editionen:** Einsatz einer Mischung aus Benutzer- und Gerätelizenzen sowie verschiedenen Editionen, um Kosten zu optimieren und gleichzeitig allen Nutzergruppen die benötigten Funktionen bereitzustellen.
  * **Hohe Nutzung der automatischen Zuweisung mit Prioritäten:** Um die Administration zu vereinfachen und sicherzustellen, dass teurere Lizenzen nur bei Bedarf vergeben werden.

Diese Beispiele dienen als Anregung. Es ist wichtig, die spezifischen Prozesse und Anforderungen Ihres Unternehmens genau zu analysieren, um die passgenaue Lizenzstrategie mit Paledo zu entwickeln.

***

## 13. Zusammenfassung

Der Paledo Lizenzmanager ist ein essenzielles Werkzeug für Administratoren, um die Softwarenutzung im Unternehmen effektiv, rechtskonform und wirtschaftlich zu gestalten. Durch seine umfassenden Funktionen unterstützt er Sie bei:

* **Kontrolle und Compliance:** Sicherstellung, dass nur ordnungsgemäß lizenzierte Benutzer und Geräte auf Paledo und seine Funktionen zugreifen.
* **Kostenoptimierung:** Durch bedarfsgerechte Zuweisung und die Möglichkeit, Lizenztypen (Benutzer vs. Gerät) und Editionen passend zum Anwendungsfall zu wählen, können unnötige Lizenzkosten vermieden werden.
* **Flexibilität:** Anpassung der Lizenzverteilung an sich ändernde organisatorische Anforderungen durch einfaches Verwalten von Pools und Zuweisungen.
* **Automatisierung:** Reduzierung des administrativen Aufwands durch die intelligente automatische Lizenzzuweisung.
* **Transparenz:** Nachvollziehbarkeit der Lizenzvergabe und -nutzung durch Protokollierungsfunktionen.
* **Benutzerfreundlichkeit:** Eine moderne Benutzeroberfläche, oft mit Funktionen wie Drag & Drop, erleichtert die tägliche Verwaltungsarbeit.
* **Technische Integrität:** Validierungslogiken und Sicherheitsmechanismen gewährleisten einen robusten und zuverlässigen Betrieb.

Durch die sorgfältige Konfiguration und Nutzung des Lizenzmanagers tragen Sie maßgeblich zu einem reibungslosen und effizienten Einsatz von Paledo in Ihrem Unternehmen bei. Wir hoffen, dieses Handbuch unterstützt Sie dabei, alle Möglichkeiten des Paledo Lizenzmanagers optimal auszuschöpfen.

***
