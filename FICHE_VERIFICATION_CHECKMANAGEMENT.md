# Fiche de Vérification de Conformité : Microservice `CheckManagement`

Ce document sert à valider que le microservice `CheckManagement` a été généré en stricte conformité avec les directives du `MASTER_TEMPLATE_PROMPT.md`.

## 1. Paramètres de Configuration Appliqués

Vérifiez que les valeurs suivantes ont été utilisées dans tout le code généré :
- **`[SERVICE_NAME]`**: `CheckManagement`
- **`[SERVICE_NAME_LOWER]`**: `checkmanagement`
- **`[DATABASE_NAME]`**: `CBSCheckManagementDB`
- **`[PRIMARY_ENTITY_NAME]`**: `CheckManagement`
- **`[PRIMARY_ENTITY_NAME_PLURAL]`**: `CheckManagements`
- **`[PORT_NUMBER]`**: (ex: `7115` ou autre port non utilisé)

---

## 2. Checklist de Conformité Structurelle

#### **Projet 1 : `CBS.CheckManagementManagement.API`**
- [ ] **Nom du projet correct :** `CBS.CheckManagementManagement.API.csproj`
- [ ] **Type de SDK correct :** `Microsoft.NET.Sdk.Web`
- [ ] **Packages NuGet (versions exactes) :**
    - [ ] `AutoMapper` (v12.0.1)
    - [ ] `FluentValidation.AspNetCore` (v11.3.0)
    - [ ] `MediatR` (v12.1.1)
    - [ ] `Microsoft.AspNetCore.Mvc.NewtonsoftJson` (v7.0.13)
    - [ ] `Microsoft.EntityFrameworkCore.Design` (v7.0.13)
    - [ ] `NLog.Web.AspNetCore` (v5.3.5)
    - [ ] `Serilog.AspNetCore` (v7.0.0)
    - [ ] `Swashbuckle.AspNetCore` (v6.5.0)
- [ ] **Références Projet :**
    - [ ] Référence vers `CBS.CheckManagementManagement.Common`
    - [ ] Référence vers `CBS.CheckManagementManagement.Data`
    - [ ] Référence vers `CBS.CheckManagementManagement.Domain`
    - [ ] Référence vers `CBS.CheckManagementManagement.Helper`
    - [ ] Référence vers `CBS.CheckManagementManagement.MediatR`
    - [ ] Référence vers `CBS.CheckManagementManagement.Repository`
    - [ ] Référence vers `CBS.APICaller.Helper`
    - [ ] Référence vers `CBS.CustomLog.Logger`
    - [ ] Référence vers `CBS.ServicesDelivery.Service`

#### **Projet 2 : `CBS.CheckManagementManagement.MediatR`**
- [ ] **Nom du projet correct :** `CBS.CheckManagementManagement.MediatR.csproj`
- [ ] **Packages NuGet (versions exactes) :**
    - [ ] `AutoMapper` (v12.0.1)
    - [ ] `FluentValidation` (v11.8.0)
    - [ ] `MediatR` (v12.1.1)
    - [ ] `Newtonsoft.Json` (v13.0.3)
    - [ ] `NLog.Web.AspNetCore` (v5.3.5)
- [ ] **Références Projet :**
    - [ ] `Data`, `Domain`, `Repository`

---

## 3. Checklist de Conformité du Boilerplate

#### **Couche Data**
- [ ] **Fichier `BaseEntity.cs` :**
    - [ ] Existe dans `Data/`
    - [ ] Namespace est `CBS.CheckManagementManagement.Data`
    - [ ] Contient les propriétés `CreatedBy`, `CreatedDate`, `ModifiedBy`, `ModifiedDate`, `IsDeleted`, etc.
- [ ] **Fichier `UserInfoToken.cs` :**
    - [ ] Existe dans `Data/Dto/`
    - [ ] Namespace est `CBS.CheckManagementManagement.Dto`

#### **Couche Common**
- [ ] **Fichier `IGenericRepository.cs` & `GenericRepository.cs` :**
    - [ ] Existent dans `Common/GenericRepository/`
    - [ ] Namespace est `CBS.CheckManagementManagement.Common.GenericRepository`
    - [ ] `GenericRepository` implémente la logique de soft-delete (`IsDeleted = true`).
- [ ] **Fichier `IUnitOfWork.cs` & `UnitOfWork.cs` :**
    - [ ] Existent dans `Common/UnitOfWork/`
    - [ ] Namespace est `CBS.CheckManagementManagement.Common.UnitOfWork`
    - [ ] `UnitOfWork.cs` contient la logique pour peupler les champs d'audit de `BaseEntity`.

#### **Couche API**
- [ ] **Fichier `BaseController.cs` :**
    - [ ] Existe dans `API/Controllers/`
    - [ ] Namespace est `CBS.CheckManagementManagement.API.Controllers`
    - [ ] Contient la méthode `ReturnFormattedResponse`.
- [ ] **Fichier `Startup.cs` :**
    - [ ] Enregistre `CheckManagementContext`.
    - [ ] Enregistre MediatR, AutoMapper, FluentValidation, et le `ValidationBehavior`.
    - [ ] Injecte `UserInfoToken` en `Scoped`.
- [ ] **Fichier `appsettings.json` :**
    - [ ] `ConnectionStrings.DefaultConnection` utilise la BDD `CBSCheckManagementDB`.
    - [ ] `JwtSettings.Audience` est `CBS.CheckManagementManagement`.
    - [ ] `ConsulConfig.serviceName` est `checkmanagementService`.

---

## 4. Checklist de la Tâche de Validation "Ping"

- [ ] **Entité :** Le fichier `Data/Entity/CheckManagement.cs` existe, hérite de `BaseEntity` et a une propriété `Name`.
- [ ] **DTO :** Le fichier `Data/Dto/CheckManagementDto.cs` existe.
- [ ] **DbContext :** Le fichier `Domain/Context/CheckManagementContext.cs` existe et contient `public DbSet<CheckManagement> CheckManagements { get; set; }`.
- [ ] **Repository :** Les fichiers `ICheckManagementRepository.cs` et `CheckManagementRepository.cs` existent dans `Repository/`.
- [ ] **MediatR Command :** `MediatR/CheckManagement/Commands/CreateCheckManagementCommand.cs` existe.
- [ ] **MediatR Handler :** `MediatR/CheckManagement/Handlers/CreateCheckManagementCommandHandler.cs` existe et utilise `IUnitOfWork`.
- [ ] **Controller :** Le fichier `API/Controllers/CheckManagementsController.cs` existe, hérite de `BaseController` et a une action `POST`.
- [ ] **Injection de Dépendances :** `DependencyInjectionExtension.cs` enregistre `ICheckManagementRepository`.
- [ ] **Compilation :** La commande `dotnet build CBSManagementService.sln` s'exécute **sans aucune erreur**.

---

## 5. Structure de Fichiers Attendue (Récursive)

Utilisez cette arborescence comme référence visuelle pour valider la structure générée.

```
CoreServices/CheckManagementManagement/
├── CBS.CheckManagementManagement.API/
│   ├── Controllers/
│   │   ├── BaseController.cs
│   │   └── CheckManagementsController.cs
│   ├── Helpers/
│   │   ├── DependencyInjectionExtension.cs
│   │   └── MapperConfig.cs
│   ├── appsettings.json
│   ├── Program.cs
│   ├── Startup.cs
│   └── CBS.CheckManagementManagement.API.csproj
├── CBS.CheckManagementManagement.Common/
│   ├── GenericRepository/
│   │   ├── GenericRepository.cs
│   │   └── IGenericRepository.cs
│   ├── UnitOfWork/
│   │   ├── IUnitOfWork.cs
│   │   └── UnitOfWork.cs
│   └── CBS.CheckManagementManagement.Common.csproj
├── CBS.CheckManagementManagement.Data/
│   ├── Dto/
│   │   ├── CheckManagementDto.cs
│   │   └── UserInfoToken.cs
│   ├── Entity/
│   │   └── CheckManagement.cs
│   ├── BaseEntity.cs
│   └── CBS.CheckManagementManagement.Data.csproj
├── CBS.CheckManagementManagement.Domain/
│   ├── Context/
│   │   └── CheckManagementContext.cs
│   ├── Migrations/
│   │   └── ... (fichiers de migration générés)
│   └── CBS.CheckManagementManagement.Domain.csproj
├── CBS.CheckManagementManagement.Helper/
│   ├── Helper/
│   │   └── ServiceResponse.cs
│   └── CBS.CheckManagementManagement.Helper.csproj
├── CBS.CheckManagementManagement.MediatR/
│   ├── Behaviors/
│   │   └── ValidationBehavior.cs
│   └── CheckManagement/
│       ├── Commands/
│       │   └── CreateCheckManagementCommand.cs
│       ├── Handlers/
│       │   └── CreateCheckManagementCommandHandler.cs
│       └── Validators/
│           └── CreateCheckManagementCommandValidator.cs
│   └── CBS.CheckManagementManagement.MediatR.csproj
└── CBS.CheckManagementManagement.Repository/
    ├── ICheckManagementRepository.cs
    ├── CheckManagementRepository.cs
    └── CBS.CheckManagementManagement.Repository.csproj
```
