# Fiche de Vérification de Conformité v4 (Définitive) : Microservice `CheckManagement`

Ce document sert à valider que le microservice `CheckManagement` a été généré en stricte conformité avec les directives du **`MASTER_TEMPLATE_PROMPT.md` (v4)**.

## 1. Checklist de Conformité de la Couche API

#### **Projet : `CBS.CheckManagementManagement.API`**

- [ ] **Dossier `AuditLogMiddleware` :**
    - [ ] Le dossier existe.
    - [ ] Contient `AuditLogMiddleware.cs`.
    - [ ] Le namespace et le code sont adaptés pour `CheckManagement`.
- [ ] **Dossier `Properties` :**
    - [ ] Le dossier existe.
    - [ ] Contient `launchSettings.json`.
- [ ] **Dossier `JWTTokenValidator` :**
    - [ ] Le dossier existe.
    - [ ] Contient `JWTMiddleware.cs`.
- [ ] **Dossier `LoggingMiddleWare` :**
    - [ ] Le dossier existe.
    - [ ] Contient `RequestResponseLoggingMiddleware.cs`.
- [ ] **Dossier `Helpers` :**
    - [ ] Le dossier `Helpers/DependencyResolver` existe et contient `DependencyInjectionExtension.cs`.
    - [ ] Le dossier `Helpers/MapperConfiguation` existe et contient `MapperConfig.cs`.
    - [ ] Le fichier `Helpers/ArrayModelBinder.cs` existe.
    - [ ] Le fichier `Helpers/UnprocessableEntityObjectResult.cs` existe.
- [ ] **Fichier `Startup.cs` :**
    - [ ] Le pipeline `Configure` contient `app.UseMiddleware<AuditLogMiddleware>();` au bon endroit.

---
## 2. Checklist de Conformité de la Couche Data
- [ ] **Fichier `AuditLog.cs` :**
    - [ ] Le fichier `Data/Entity/AuditLog.cs` existe.
- [ ] **DbContext :**
    - [ ] Le `CheckManagementContext.cs` contient `public DbSet<AuditLog> AuditLogs { get; set; }`.

---

## 3. Structure de Fichiers Attendue (Récursive, v4 Finale)

```
CoreServices/CheckManagementManagement/
└── CBS.CheckManagementManagement.API/
    ├── AuditLogMiddleware/
    │   └── AuditLogMiddleware.cs
    ├── Controllers/
    │   ├── BaseController.cs
    │   └── CheckManagementsController.cs
    ├── Helpers/
    │   ├── DependencyResolver/
    │   │   └── DependencyInjectionExtension.cs
    │   ├── MapperConfiguation/
    │   │   └── MapperConfig.cs
    │   ├── ArrayModelBinder.cs
    │   └── UnprocessableEntityObjectResult.cs
    ├── JWTTokenValidator/
    │   └── JWTMiddleware.cs
    ├── LoggingMiddleWare/
    │   └── RequestResponseLoggingMiddleware.cs
    ├── Properties/
    │   └── launchSettings.json
    ├── appsettings.Development.json
    ├── appsettings.json
    ├── nlog.config
    ├── Program.cs
    ├── Startup.cs
    └── CBS.CheckManagementManagement.API.csproj
(et ainsi de suite pour les autres projets...)
```
