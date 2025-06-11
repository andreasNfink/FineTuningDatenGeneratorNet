---
icon: database
---

# Objektstruktur

## Einleitung

Willkommen bei Paledo! Sie nutzen Paledo wahrscheinlich, um die Prozesse Ihres Unternehmens in Bereichen wie Instandhaltung, Qualitätssicherung oder Anlagenmanagement zu optimieren. Paledo zeichnet sich dadurch aus, nahtlose digitale Arbeitsabläufe zu schaffen und Papierkram sowie isolierte Dateninseln zu eliminieren.

Ein Schlüsselaspekt, um die Leistungsfähigkeit von Paledo voll auszuschöpfen – insbesondere bei der Nutzung seiner Konfigurationsmöglichkeiten (wie dem Erstellen von Formularen oder dem Einrichten von Workflows) – ist das Verständnis der grundlegenden Bausteine des Systems. Dies sind die **Geschäftsobjekte**, ihre **Beziehungen** und wie sie in **Geschäftsprozessen** zusammenspielen.

Dieses Kapitel führt Sie durch diese fundamentalen Konzepte. Sie zu verstehen, wird die Konfiguration von Paledo zur perfekten Abbildung Ihrer spezifischen Anforderungen wesentlich einfacher und effektiver machen, auch wenn Sie keinen tiefen technischen Hintergrund haben.

{% hint style="info" %}
**Ziel:** Dieses Kapitel hilft Ihnen, die wesentlichen Datenstrukturen und Prozessabläufe in Paledo zu verstehen, die die Grundlage für eine effektive Low-Code-Konfiguration bilden.
{% endhint %}

## Was sind Geschäftsobjekte in Paledo?

Stellen Sie sich Geschäftsobjekte (oft als "BO" abgekürzt) als digitale Repräsentationen von Dingen, Konzepten oder Dokumenten aus der realen Welt vor, die für Ihre Geschäftsprozesse in Paledo wichtig sind. Sie enthalten spezifische Informationen und interagieren miteinander.

{% hint style="info" %}
**Definition: Geschäftsobjekt (BO)**\
Ein Geschäftsobjekt in Paledo repräsentiert eine eindeutige Entität wie einen Auftrag, eine Anlage, einen Bericht, eine Materialreservierung oder einen Benutzer. Jeder BO-Typ hat spezifische Eigenschaften (Datenfelder) und Funktionen.
{% endhint %}

Basierend auf gängigen Arbeitsabläufen sind hier einige der wichtigsten Geschäftsobjekte, denen Sie in Paledo begegnen werden:

* **Aufträge (BOOrder / BOSAPOrder):** Zentrale Objekte für die Planung und Durchführung von Instandhaltungsarbeiten. Sie können mit SAP-Aufträgen synchronisiert werden.
* **Vorgänge (BOOperation):** Repräsentieren einzelne Arbeitsschritte innerhalb eines Auftrags.
* **Berichte (BORecord):** Dienen zur Dokumentation durchgeführter Arbeiten, oft verknüpft mit spezifischen Anlagen oder Orten über Formulare (`BORecordReport`).
* **Meldungen (BOSAPNotification):** Erfassen Störungen, Probleme oder Anforderungen, oft im Zusammenhang mit Anlagen. Sie können zu Aufträgen führen und mit SAP-Meldungen synchronisiert werden.
* **Anlagen (BOEquipment):** Repräsentieren physische Anlagenteile.
* **Funktionsorte (BOFunctionalLocation):** Repräsentieren funktionale Standorte, an denen Anlagen installiert sein können (Technische Plätze).
* **Materialreservierungen (BOMatReservation):** Werden verwendet, um Materialien zu reservieren, die für Instandhaltungsaufgaben benötigt werden.
* **Firmen (BOCompany):** Repräsentieren externe Firmen wie Lieferanten oder Dienstleister.
* **Katalogeinträge (BOCatalogEntry):** Stellen standardisierte Wertelisten (z.B. Schadenscodes, Maßnahmenarten, Klassifizierungen) bereit, die in vielen anderen Geschäftsobjekten verwendet werden.

## Wie Geschäftsobjekte miteinander in Beziehung stehen

Geschäftsobjekte existieren selten isoliert. Sie sind miteinander verbunden und bilden die Struktur Ihrer Daten und Prozesse. Das Verständnis dieser Beziehungen ist entscheidend.

Zum Beispiel:

* Ein `Auftrag` (BOOrder) **enthält** typischerweise einen oder mehrere `Vorgänge` (BOOperation).
* Ein `Vorgang` (BOOperation) kann zur Dokumentation zu einem `Bericht` (BORecord) **führen**.
* Ein `Bericht` (BORecord) ist oft mit einer `Anlage` (BOEquipment) oder einem `Funktionsort` (BOFunctionalLocation) **verknüpft**.
* Eine `Meldung` (BOSAPNotification) kann die Erstellung eines `Auftrags` (BOOrder) **auslösen**.
* Eine `Materialreservierung` (BOMatReservation) **gehört zu** einem `Auftrag` (BOOrder) oder `Vorgang` (BOOperation) und **bezieht sich auf** ein `Material` (BOMaterial).

Diese Beziehungen definieren, wie Informationen durch Paledo fließen und wie verschiedene Teile Ihres Arbeitsablaufs miteinander verbunden sind.

## Beziehungen visualisieren: Wichtige Diagramme

Mermaid-Diagramme helfen, diese Strukturen und Beziehungen zu visualisieren. Die folgenden Diagramme illustrieren Schlüsselbereiche innerhalb von Paledo:

### 1. Anlagenhierarchie und Strukturierung

Dieses Diagramm zeigt, wie technische Objekte wie Anlagen (`BOEquipment`) und Technische Plätze (`BOFunctionalLocation`) organisiert sind, potenziell hierarchisch, und wie sie mit anderen Informationen wie Herstellern (`BOCompany`) oder verantwortlichen Personen (`BOPaledoUser`) verknüpft sind.

```mermaid
classDiagram
    BOComponent <|-- BOEquipment
    BOComponent <|-- BOFunctionalLocation
    BOComponent o-- BOComponent : Parent
    BOComponent o-- BOComponentType
    BOComponent o-- BOCatalogEntry : ConstructionType
    BOComponent o-- BOProductionArea
    BOComponent o-- BOCompany : Manufacturer
    BOComponent o-- BOPaledoUser : ResponsiblePerson
    BOComponent o-- "0..*" BOComponentKPI
    BOComponentKPI o-- BOComponentProductionTime

    class BOComponent {
        +string Name
        +string Equipment
        +string Technischer_Platz
        +string Description
        +bool AvailableForPMActions
        +bool IsSAPComponent
        +string Location
        +string Room
    }

    class BOEquipment {
        +BOFunctionalLocation FuncLoc
        +IList<BORecord> Records
    }

    class BOFunctionalLocation {
        +IList<BORecord> Records
        +IList<BOEquipment> Equipments
    }

    class BOComponentType {
        +string Name
        +string Code
        +DXImage Icon
        +DXImage FuncLocIcon
    }

    class BOProductionArea {
        +string Name
        +string Code
        +BOOrgUnit ResponsibleOrgUnit
    }

    class BOComponentKPI {
        +int Year
        +int Month
        +double MTBF
        +double MTTR
        +bool IsActual
    }
```

### 2. Auftrags- und Instandhaltungsprozesse

Dieses Diagramm konzentriert sich auf den zentralen `BOOrder` (und seine SAP-spezifische Variante `BOSAPOrder`) und zeigt, wie er mit `BOOperation` (Arbeitsschritten), Anlagen (`BOEquipment`, `BOFunctionalLocation`), Organisationseinheiten (`BOOrgUnit`) sowie potenziell Materialreservierungen und Workflow-Status verknüpft ist.

```mermaid
classDiagram
    BOOrder <|-- BOSAPOrder
    BOOrder o-- BOOrgUnit : WorkCenter
    BOOrder o-- BOEquipment : Equipment
    BOOrder o-- BOFunctionalLocation : FuncLoc
    BOOrder o-- BOWorkflowState
    BOOrder o-- BOCatalogEntry : ActionCode
    BOOrder o-- BOCompany : ExternalCompany
    BOOrder o-- BOPaledoUser : ResponsiblePerson
    BOOrder *-- "0..*" BOOperation
    BOOrder *-- "0..*" BOMatReservation
    BOOrder *-- "0..*" BOMatMovement
    BOOrder *-- "0..*" BORecordComponentRelation

    BOOperation o-- BOOrgUnit : WorkCenter
    BOOperation o-- BOEvent
    BOOperation o-- BOWorkflowState
    BOOperation o-- BOCatalogEntry : Category
    BOOperation o-- BOCatalogEntry : ControlKey
    BOOperation *-- "0..*" BORecord

    BOSAPOrder o-- BOSAPOrderType
    BOSAPOrder *-- "0..*" BOSAPNotification : HeaderNotification

    class BOOrder {
        +string Number
        +string SAPOrderNo
        +DateTime BasicStart
        +DateTime BasicEnd
        +string Subject
        +string Description
        +OrderStatus OrderStatus
        +bool IsOrderTemplate
    }

    class BOSAPOrder {
        +NotificationStatus PaledoStatus
        +SAPObjectSource SAPObjectSource
        +bool RequiresSAPUpdate
        +string SAPOrderTypeTxt
    }

    class BOOperation {
        +string Number
        +string ShortDescription
        +string Description
        +double PlannedHours
        +int Progress
    }

    class BOWorkflowState {
        +string Name
        +string Code
        +bool AllowTimeConfirmation
        +bool AllowMaterialMovements
    }
```

### 3. Ereignisse, Berichte und Meldungen

Dieses Diagramm illustriert, wie Ereignisse (`BOEvent`), einschließlich SAP-Meldungen (`BOSAPNotification`), mit Anlagen, Arbeitsplätzen und insbesondere wie sie mit der Dokumentation (`BORecord`) und spezifischen Formularen (`BORecordReport`) zusammenhängen.

```mermaid
classDiagram
    BOEvent <|-- BOSAPNotification
    BOEvent o-- BOEquipment : Equipment
    BOEvent o-- BOFunctionalLocation : FuncLoc
    BOEvent o-- BOEventTask
    BOEvent o-- BOOrgUnit : WorkCenter
    BOEvent o-- BOProductionArea
    BOEvent *-- "0..*" BORecord
    BOEvent *-- "0..*" BOEventEventTaskRelation

    BOEventTask o-- BOTemplateRevisionReport

    BORecord o-- BOEvent
    BORecord o-- BOOperation
    BORecord o-- BOEquipment : Equipment
    BORecord o-- BOFunctionalLocation : FuncLoc
    BORecord o-- BOWorkflowState
    BORecord o-- BOCatalogEntry : Group
    BORecord *-- "0..*" BORecordReport
    BORecord *-- "0..*" BORecordComponentRelation

    BOSAPNotification o-- BONotificationType
    BOSAPNotification o-- BOSAPOrder : CauseOrder
    BOSAPNotification *-- "0..*" BOSAPTechnicalConfirmation

    BOSAPNotification <|-- BOSAPFaultNotification
    BOSAPNotification <|-- BOSAPRequirementNotification
    BOSAPFaultNotification o-- BOSAPOrder : Order
    BOSAPFaultNotification o-- BOCatalogEntry : DamageCode

    class BOEvent {
        +string Subject
        +DateTime StartOn
        +DateTime EndOn
        +string Description
        +bool SimpleMode
        +bool AllowMultipleEventTasks
    }

    class BOSAPNotification {
        +string SAPNotificationNo
        +string Number
        +NotificationStatus NotificationStatus
        +bool RequiresSAPUpdate
        +DateTime Date
    }

    class BORecord {
        +string Name
        +string Description
        +RecordRating Rating
        +bool Approved
        +bool Documented
        +bool IsTemplate
        +string Components
    }

    class BOEventTask {
        +string Name
        +string Description
        +bool HideInSelection
        +bool RequiresQualification
    }

    class BORecordReport {
        +DateTime DocumentDate
        +string Comments
        +DateTime ExaminationDate
    }
```

### 4. Materialwirtschaft und Beschaffung

Dieses Diagramm detailliert die Objekte der Materialwirtschaft, mit Fokus auf Materialreservierungen (`BOMatReservation`), Materialien (`BOMaterial`), Materialbewegungen (`BOMatMovement`) und Bestellanforderungen (`BORequisitionPosition`).

```mermaid
classDiagram
    BOMatReservation o-- BOMaterial
    BOMatReservation o-- BOEquipment : Equipment
    BOMatReservation o-- BOOrder
    BOMatReservation o-- BOOperation
    BOMatReservation o-- BOWorkflowState
    BOMatReservation o-- BOCatalogEntry : UKN_MaterialGroup
    BOMatReservation *-- "0..*" BOMatMovement

    BOMaterial o-- BOCompany : Vendor
    BOMaterial o-- BOCatalogEntry : MaterialGroup
    BOMaterial o-- BOQuantityUnit

    BORequisitionPosition o-- BOOrder
    BORequisitionPosition o-- BOOperation
    BORequisitionPosition o-- BOMatReservation
    BORequisitionPosition o-- BOCatalogEntry : ItemCategory
    BORequisitionPosition o-- BOCatalogEntry : MaterialGroup
    BORequisitionPosition o-- BOCatalogEntry : PurchasingGroup
    BORequisitionPosition o-- BOCatalogEntry : Account
    BORequisitionPosition o-- BOCatalogEntry : CostCenter

    class BOMatReservation {
        +string ReservationNumber
        +double RequiredCount
        +double WithdrawnCount
        +DateTime WithdrawalTime
        +bool ReservedInSAP
        +bool MaterialPurchasing
        +bool IsUnknownMaterial
    }

    class BOMaterial {
        +string MaterialNo
        +string Description
        +double Amount
        +string StorageLocation
        +bool HandledInBatches
        +string AlternativeName
    }

    class BOMatMovement {
        +DateTime MovementDate
        +double Quantity
        +string StorageLocation
        +bool MovementToSystem
        +string SAPMaterialDocument
    }

    class BORequisitionPosition {
        +string RequisitionNo
        +string ItemNo
        +double Quantity
        +DateTime DeliveryDate
        +string Description
        +string RequestedBy
    }

    class BOQuantityUnit {
        +string Code
        +string Name
        +string DimensionType
    }
```

### 5. Organisationsstruktur und Verantwortlichkeiten

Dieses Diagramm zeigt, wie Organisationseinheiten (`BOOrgUnit`, `BOWorkCenter`, `BOPlannerGroup`), Benutzer (`BOPaledoUser`) und externe Firmen (`BOCompany`) strukturiert und verknüpft sind, einschließlich Aspekten wie Qualifikationen und Schichten (`BOShift`).

```mermaid
classDiagram
    BOOrgUnit <|-- BOWorkCenter
    BOOrgUnit <|-- BOPlannerGroup
    BOOrgUnit o-- BOOrgUnit : Parent
    BOOrgUnit o-- BOCompany
    BOOrgUnit *-- "0..*" BOShift

    BOPaledoUser o-- BOOrgUnit : OrganisationalUnit
    BOPaledoUser *-- "0..*" BOUserQualificationRelation
    BOPaledoUser *-- "0..*" BOProductionAreaPaledoUserRelation

    BOCompany o-- BOCatalogEntry : Category
    BOCompany o-- BOPaledoUser : MainContact
    BOCompany *-- "0..*" BOCompanyQualificationRelation

    BOCompanyQualificationRelation o-- BOCompanyQualification
    BOUserQualificationRelation o-- BOQualification
    BOProductionAreaPaledoUserRelation o-- BOProductionArea

    class BOOrgUnit {
        +string Name
        +string Code
        +string Description
        +string EMail
    }

    class BOWorkCenter {
        +string SAPWorkCenterNo
        +BOPlannerGroup PlannerGroup
        +bool IsExternalCompany
        +bool CanReport
    }

    class BOPlannerGroup {
        +string SAPPlannerGroupNo
    }

    class BOPaledoUser {
        +string LoginName
        +string FirstName
        +string SurName
        +string PersonalNumber
        +string EMail
        +PaledoUserType UserType
    }

    class BOCompany {
        +string Name
        +string Identifier
        +string Street
        +string City
        +string PhoneNumber
        +bool IsManufacturer
        +bool IsSupplier
        +bool IsServiceProvider
    }

    class BOShift {
        +string Name
        +string Code
        +TimeSpan StartTime
        +TimeSpan EndTime
        +bool Monday
        +bool Tuesday
        +bool Wednesday
        +bool Thursday
        +bool Friday
        +bool Saturday
        +bool Sunday
    }
```

### 6. Kataloge und Konfigurationen

Kataloge (`BOCatalog`) und ihre Einträge (`BOCatalogEntry`) sind fundamental für Standardisierung und Konfiguration. Dieses Diagramm zeigt ihre hierarchische Struktur und wie sie mehrsprachige Einträge (`BOLocalizationEntry`) unterstützen.

```mermaid
classDiagram
    BOCatalog *-- "0..*" BOCatalogEntry
    BOCatalogEntry o-- BOCatalog
    BOCatalogEntry o-- BOCatalogEntry : ParentCatalogEntry
    BOCatalogEntry *-- "0..*" BOCatalogEntry : SubCatalogEntries
    BOCatalogEntry *-- "0..*" BOLocalizationEntry
    BOCatalogEntry *-- "0..*" BOCatalogEntryRelation

    class BOCatalog {
        +string Name
        +string SystemCatalogName
        +string Description
        +bool RelatedEntries
        +bool AdvancedOptions
        +bool ShowFullName
        +string FullNameSeperator
        +bool StructureNodeSelectable
    }

    class BOCatalogEntry {
        +string Name
        +string Description
        +string FullName
        +string ExternalKey
        +string ExternalKey2
        +double NumericValue
        +StandardRating Rating
    }

    class BOCatalogEntryRelation {
        +BOCatalogEntry Parent
        +BOCatalogEntry Child
    }

    class BOLocalizationEntry {
        +string Language
        +string PropertyName
        +string TranslationString
    }
```

### 7. Zeit- und Statusbestätigungen

Dieses Diagramm zeigt, wie Zeit- und Arbeitsbestätigungen (`BOSAPConfirmation` und Varianten) erfasst werden, die auf Vorgänge, Meldungen oder Berichte zurückverweisen und Benutzer, Arbeitsplätze und Schichten involvieren. Es beinhaltet auch oft erfasste technische Details (`BOSAPTechnicalConfirmation`).

```mermaid
classDiagram
    BOSAPConfirmation <|-- BOSAPOperationConfirmation
    BOSAPConfirmation <|-- BOSAPNotificationConfirmation
    BOSAPConfirmation <|-- BOSAPRecordConfirmation

    BOSAPConfirmation o-- BOShift
    BOSAPConfirmation o-- BOWorkCenter
    BOSAPConfirmation o-- BOPaledoUser

    BOSAPOperationConfirmation o-- BOSAPOperation
    BOSAPNotificationConfirmation o-- BOSAPNotification
    BOSAPRecordConfirmation o-- BORecord

    class BOSAPConfirmation {
        +string SAPConfirmationNo
        +string Number
        +double ActualTime
        +DateTime StartDateTime
        +DateTime EndDateTime
        +DateTime PostingDate
        +string Comment
        +bool FinalConfirmation
        +bool WasSynched
        +string ActivityType
        +string WageType
    }

    class BOSAPOperationConfirmation {
        +BOSAPOperation Operation
    }

    class BOSAPNotificationConfirmation {
        +BOSAPNotification Notification
    }

    class BOSAPRecordConfirmation {
        +BORecord Record
    }

    class BOSAPTechnicalConfirmation {
        +string CauseCode
        +string ActivityCode
        +string Symptoms
        +string Cause
        +string Remedy
        +DateTime StartDateTime
        +DateTime EndDateTime
    }
```

## Geschäftsobjekte in Aktion: Kernprozesse

Diese Geschäftsobjekte und ihre Beziehungen ermöglichen zentrale Geschäftsprozesse in Paledo. Hier sind einige typische Beispiele:

### 1. Instandhaltungszyklus (Ende-zu-Ende)

Ein gängiger Instandhaltungsworkflow beinhaltet die Interaktion mehrerer Geschäftsobjekte:

1. Ein Problem wird über eine **Meldung** (BOSAPNotification) gemeldet.
2. Dies löst die Erstellung eines **Auftrags** (BOOrder) mit geplanten **Vorgängen** (BOOperation) aus.
3. Benötigte **Materialien** werden mittels `BOMatReservation` reserviert.
4. Die Arbeit wird ausgeführt, potenziell unter Erfassung von **Zeitbestätigungen** (BOSAPConfirmation).
5. Die abgeschlossene Arbeit wird in **Berichten** (BORecord) dokumentiert.

{% hint style="info" %}
Dieser Zyklus repräsentiert einen typischen Ablauf von der Problemidentifikation bis zur Lösung und Dokumentation, der oft in Paledo konfigurierbar ist.
{% endhint %}

### 2. Materialversorgungszyklus

Die Sicherstellung der Materialverfügbarkeit umfasst:

1. Erstellung von **Materialreservierungen** (BOMatReservation), oft verknüpft mit Aufträgen oder Vorgängen.
2. Bei Bedarf Generierung von **Bestellanforderungen** (BORequisitionPosition).
3. Dokumentation der tatsächlichen Entnahme oder des Zugangs von Materialien mittels **Materialbewegungen** (BOMatMovement).

### 3. Anlagenlebenszyklusmanagement

Die Verwaltung von Anlagen über ihren gesamten Lebenszyklus beinhaltet:

1. Erfassung von **Anlagen** (BOEquipment) und **Funktionsorten** (BOFunctionalLocation / Technischen Plätzen).
2. Dokumentation der Historie, Inspektionen und Ereignisse mittels **Berichten** (BORecord).
3. Planung und Ausführung der Instandhaltung über **Aufträge** (BOOrder).
4. Erfassung von Problemen und Störungen mittels **Meldungen** (BOSAPNotification).

### 4. Visualisierung eines Störungsbehebungs-Workflows

Dieses Sequenzdiagramm zeigt einen typischen Interaktionsfluss bei der Bearbeitung einer gemeldeten Störung:

```mermaid
sequenceDiagram
    actor Techniker
    participant Meldung as BOSAPNotification
    participant Auftrag as BOSAPOrder
    participant Vorgang as BOOperation
    participant Material as BOMatReservation
    participant Bericht as BORecord
    participant Zeiterfassung as BOSAPConfirmation

    Techniker->>Meldung: Erfasst Störung
    Meldung->>Meldung: Speichert Schaden (DamageCode)
    Meldung->>Auftrag: Erzeugt Instandhaltungsauftrag
    Auftrag->>Vorgang: Erzeugt Arbeitsschritte
    Vorgang->>Material: Reserviert benötigte Materialien
    Techniker->>Material: Entnimmt Material
    Techniker->>Vorgang: Führt Arbeiten durch
    Techniker->>Bericht: Dokumentiert Arbeiten
    Techniker->>Zeiterfassung: Erfasst Arbeitszeiten
    Auftrag->>Meldung: Meldet Arbeiten abgeschlossen

```

## Warum dies für die Paledo-Konfiguration wichtig ist

Das Verständnis dieser Kernkonzepte – Geschäftsobjekte, ihre Eigenschaften, ihre Beziehungen und wie sie an Prozessen teilnehmen – ist **essenziell**, wenn Sie Paledo mit seinen Low-Code-Werkzeugen konfigurieren. Hier sind die Gründe:

* **Workflow-Design:** Zu wissen, dass ein `Auftrag` `Vorgänge` enthält, ermöglicht es Ihnen, mehrstufige Instandhaltungsaufgaben korrekt in Ihren Workflows zu strukturieren.
* **Formularerstellung:** Beim Erstellen digitaler Formulare (oft verknüpft mit `BORecord`), stellt das Verständnis, auf welches BO sich das Formular bezieht (z.B. `BOEquipment`, `BOFunctionalLocation`, `BOOperation`), sicher, dass Sie die richtigen Daten im korrekten Kontext erfassen und angemessen verknüpfen können.
* **Datenauswahl & Anzeige:** Die Konfiguration von Dropdown-Listen, Suchfeldern oder Berichtspalten basiert auf dem Wissen, welche `BOCatalogEntry`-Listen zu verwenden sind oder welche Felder auf einem verknüpften BO existieren (z.B. Anzeigen des `Name` der zugehörigen `BOEquipment` auf einem `BORecord`).
* **Integrations-Setup:** Wenn Sie mit SAP integrieren, ist das Wissen um das Mapping zwischen `BOSAPOrder` und SAP-Aufträgen oder `BOSAPNotification` und SAP-Meldungen entscheidend für die korrekte Konfiguration der Datensynchronisation.
* **Prozesslogik:** Das Definieren von Regeln oder Automatisierungen (z.B. das automatische Erstellen eines `Auftrags` aus einem bestimmten Typ von `Meldung`) erfordert das Verständnis der Beziehung und des typischen Flusses zwischen diesen BOs.

{% hint style="warning" %}
**Konfigurationsfallen vermeiden:** Ein Missverständnis dieser Beziehungen kann zu Konfigurationen führen, die nicht wie erwartet funktionieren, zu falscher Datenerfassung oder zu fehlerhaften Workflows. Der Versuch beispielsweise, einen `BORecord` direkt mit einem `BOMaterial` zu verknüpfen, ist möglicherweise keine Standardbeziehung und führt zu Fehlern, wenn Sie ein Formular konfigurieren, das diese Verknüpfung ohne die korrekten Zwischenobjekte (wie `BOMatReservation`, verknüpft mit einem `Auftrag`/`Vorgang`, der dann mit dem `BORecord` verknüpft ist) erwartet.
{% endhint %}

Indem Sie Zeit investieren, um diese Kernstrukturen zu verstehen, befähigen Sie sich selbst, robustere, effizientere und genauere digitale Prozesse in Paledo zu erstellen.

## Zusammenfassung: Wichtige Erkenntnisse

* **Geschäftsobjekte (BOs)** sind die fundamentalen Bausteine in Paledo und repräsentieren reale Entitäten wie Aufträge, Anlagen, Berichte usw.
* **Beziehungen** definieren, wie diese BOs verbunden sind und interagieren, und bilden die Struktur Ihrer Daten und Prozesse.
* **Geschäftsprozesse** zeigen, wie BOs in typischen Arbeitsabläufen wie Instandhaltungszyklen oder Materialmanagement zusammenarbeiten.
* **Visuelle Diagramme** (wie die Mermaid-Diagramme) helfen, diese komplexen Strukturen zu verstehen.
* **Konfigurationsmächtigkeit:** Das Verständnis dieser Konzepte ist entscheidend für die effektive Konfiguration von Paledos Formularen, Workflows, Berichten und Integrationen mithilfe seiner Low-Code-Funktionen.
