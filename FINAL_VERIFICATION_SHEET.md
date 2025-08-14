# Fiche de Vérification de Conformité v4 (Finale) : Microservice `CheckManagement`

Ce document sert à valider que le microservice `CheckManagement` a été généré en stricte conformité avec les directives du **`FINAL_MASTER_PROMPT.md`**.

## 1. Paramètres de Configuration Appliqués

- **`[SERVICE_NAME]`**: `CheckManagement`
- **`[SERVICE_NAME_LOWER]`**: `checkmanagement`
- **`[DATABASE_NAME]`**: `CBSCheckManagementDB`
- **`[PRIMARY_ENTITY_NAME]`**: `CheckManagement`
- **`[PRIMARY_ENTITY_NAME_PLURAL]`**: `CheckManagements`
- **`[PORT_NUMBER]`**: (ex: `7115`)

---

## 2. Checklist de Conformité Exhaustive

#### **Projet 1 : `CBS.CheckManagementManagement.API`**
- [ ] **Dossier `AuditLogMiddleware` :**
    - [ ] Le dossier existe.
    - [ ] Contient `AuditLogMiddleware.cs`.
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
- [ ] **Fichiers à la racine de l'API :**
    - [ ] `nlog.config` existe.
    - [ ] `appsettings.json` et `appsettings.Development.json` existent.
- [ ] **Fichier `Startup.cs` :**
    - [ ] Le pipeline `Configure` contient `app.UseMiddleware<AuditLogMiddleware>();` au bon endroit.

#### **Projet 2 : `CBS.CheckManagementManagement.Data`**
- [ ] **Fichier `BaseEntity.cs` :**
    - [ ] Existe à la racine du projet `Data/`.
- [ ] **Fichier `AuditLog.cs` :**
    - [ ] Le fichier `Entity/AuditLog.cs` existe.
- [ ] **DbContext :**
    - [ ] Le `CheckManagementContext.cs` dans le projet Domain contient `public DbSet<AuditLog> AuditLogs { get; set; }`.


---

## 3. Structure de Fichiers Attendue (Récursive, Finale et Complète)

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
└── CBS.CheckManagementManagement.Common/
    ├── GenericRespository/
    │   ├── GenericRepository.cs
    │   └── IGenericRepository.cs
    ├── MongoDBContext/
    ├── UnitOfWork/
    │   ├── IUnitOfWork.cs
    │   └── UnitOfWork.cs
    └── CBS.CheckManagementManagement.Common.csproj
└── CBS.CheckManagementManagement.Data/
    ├── Dto/
    │   ├── CheckManagementDto.cs
    │   └── UserInfoToken.cs
    ├── Entity/
    │   ├── AuditLog.cs
    │   └── CheckManagement.cs
    ├── BaseEntity.cs
    └── CBS.CheckManagementManagement.Data.csproj
└── CBS.CheckManagementManagement.Domain/
    ├── Context/
    │   └── CheckManagementContext.cs
    ├── Migrations/
    │   └── ...
    └── CBS.CheckManagementManagement.Domain.csproj
└── CBS.CheckManagementManagement.Helper/
    ├── Helper/
    │   └── ServiceResponse.cs
    └── CBS.CheckManagementManagement.Helper.csproj
└── CBS.CheckManagementManagement.MediatR/
    ├── Behaviors/
    │   └── ValidationBehavior.cs
    └── CheckManagement/
        ├── Commands/
        │   └── CreateCheckManagementCommand.cs
        ├── Handlers/
        │   └── CreateCheckManagementCommandHandler.cs
        └── Validators/
            └── CreateCheckManagementCommandValidator.cs
    └── CBS.CheckManagementManagement.MediatR.csproj
└── CBS.CheckManagementManagement.Repository/
    ├── ICheckManagementRepository.cs
    ├── CheckManagementRepository.cs
    └── CBS.CheckManagementManagement.Repository.csproj
```
