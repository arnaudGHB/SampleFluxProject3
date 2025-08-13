# Directive Maître : Génération d'un Microservice via un Template Configurable (v4 - Master)

## 1. OBJECTIF

Générer la structure complète et le code boilerplate pour un nouveau microservice, en utilisant les paramètres de configuration ci-dessous. Le résultat doit être une solution .NET 8 prête à être compilée et intégrée dans l'écosystème `SampleFluxProject`.

---

## 2. PARAMÈTRES DE CONFIGURATION

*   **`[SERVICE_NAME]`**: Le nom du domaine métier géré par le service (ex: `Portfolio`).
*   **`[SERVICE_NAME_PLURAL]`**: Le nom au pluriel du domaine (ex: `Portfolios`).
*   **`[DATABASE_NAME]`**: Le nom de la base de données SQL pour ce service (ex: `CBSPortfolioDB`).
*   **`[PRIMARY_ENTITY_NAME]`**: Le nom de l'entité principale du service (ex: `Portfolio`).

---

## 3. STRUCTURE ET DÉPENDANCES DE LA SOLUTION

Génère une solution (`.sln`) avec les 7 projets C# suivants. Chaque projet doit cibler **.NET 8.0** (`<TargetFramework>net8.0</TargetFramework>`).

### 1. `CBS.[SERVICE_NAME].API`
-   **Type**: Projet `Microsoft.NET.Sdk.Web`.
-   **Références de Projet**: Doit référencer les 6 autres projets de la solution.
-   **Paquets NuGet**:
    -   `AutoMapper.Extensions.Microsoft.DependencyInjection` (v12.0.1)
    -   `FluentValidation.AspNetCore` (v11.3.0)
    -   `MediatR.Extensions.Microsoft.DependencyInjection` (v11.1.0)
    -   `Microsoft.AspNetCore.Authentication.JwtBearer` (v7.0.11)
    -   `Microsoft.EntityFrameworkCore.Design` (v7.0.13)
    -   `NLog.Web.AspNetCore` (v5.3.5)
    -   `Swashbuckle.AspNetCore` (v6.5.0)

### 2. `CBS.[SERVICE_NAME].MediatR`
-   **Type**: Bibliothèque de classes `Microsoft.NET.Sdk`.
-   **Références de Projet**: `.Data`, `.Domain`, `.Repository`.
-   **Paquets NuGet**:
    -   `AutoMapper` (v12.0.1)
    -   `FluentValidation` (v11.8.0)
    -   `MediatR` (v12.1.1)
    -   `Newtonsoft.Json` (v13.0.3)

### 3. `CBS.[SERVICE_NAME].Domain`
-   **Type**: Bibliothèque de classes `Microsoft.NET.Sdk`.
-   **Références de Projet**: `.Data`.
-   **Paquets NuGet**:
    -   `Microsoft.EntityFrameworkCore.SqlServer` (v7.0.13)

### 4. `CBS.[SERVICE_NAME].Data`
-   **Type**: Bibliothèque de classes `Microsoft.NET.Sdk`.
-   **Références de Projet**: `.Helper`.
-   **Paquets NuGet**:
    -   `MediatR` (v12.1.1)

### 5. `CBS.[SERVICE_NAME].Repository`
-   **Type**: Bibliothèque de classes `Microsoft.NET.Sdk`.
-   **Références de Projet**: `.Common`, `.Data`, `.Domain`.
-   **Paquets NuGet**: Aucun.

### 6. `CBS.[SERVICE_NAME].Common`
-   **Type**: Bibliothèque de classes `Microsoft.NET.Sdk`.
-   **Références de Projet**: `.Data`.
-   **Paquets NuGet**:
    -   `Microsoft.EntityFrameworkCore` (v7.0.13)
    -   `MongoDB.Driver` (v3.1.0) - *Pour le pattern de persistance hybride.*

### 7. `CBS.[SERVICE_NAME].Helper`
-   **Type**: Bibliothèque de classes `Microsoft.NET.Sdk`.
-   **Références de Projet**: `.Data`, et le projet partagé `CBS.APICaller.Helper`.
-   **Paquets NuGet**:
    -   `ClosedXML` (v0.102.2)
    -   `EPPlus` (v7.0.0)
    -   `Microsoft.AspNetCore.Http.Abstractions` (v2.2.0)

---

## 4. FONDATIONS ARCHITECTURALES (CODE BOILERPLATE REQUIS)

Génère les fichiers suivants avec leur logique standard (soft-delete, audit automatique, etc.).

-   **Dans `.Data`**:
    -   `BaseEntity.cs`: Doit contenir `Id`, `CreatedBy`, `CreatedDate`, `ModifiedBy`, `LastModifiedDate`, `IsDeleted`.
-   **Dans `.Helper`**:
    -   `ServiceResponse<T>`: Implémentation standard avec des méthodes Factory.
    -   Classes utilitaires standards: `APICallHelper`, `PathHelper`, `BaseUtilities`, `PinSecurity`, `PagedList`, `Enums`.
-   **Dans `.Common`**:
    -   `IGenericRepository.cs`, `IUnitOfWork.cs`.
    -   Si persistance hybride : `IMongoUnitOfWork.cs`.
-   **Dans `.Repository` ou `.Common`**:
    -   `GenericRepository.cs` (avec logique de soft delete).
    -   `UnitOfWork.cs` (avec logique d'audit dans `SaveChanges()`).
-   **Dans `.API/Controllers/Base`**:
    -   `BaseController.cs` (avec la méthode `ReturnFormattedResponse`).
-   **Dans `.MediatR/PipeLineBehavior`**:
    -   `ValidationBehavior.cs` (avec la logique `FluentValidation`).

---

## 5. CONFIGURATION `Startup.cs` (API)

Génère un `Startup.cs` qui configure le conteneur DI (via `services.AddDependencyInjection()`) et le pipeline de middlewares dans l'ordre critique : `UseRouting`, `UseAuthentication`, `UseMiddleware<JWTMiddleware>`, `UseMiddleware<AuditLogMiddleware>`, `UseAuthorization`, `UseEndpoints`.

---

## 6. TÂCHE DE VALIDATION AUTOMATIQUE

Pour valider le template, génère une fonctionnalité de base pour `[PRIMARY_ENTITY_NAME]` :
1.  Crée l'entité `public class [PRIMARY_ENTITY_NAME] : BaseEntity`.
2.  Crée `Add[PRIMARY_ENTITY_NAME]Command` et son `Add[PRIMARY_ENTITY_NAME]CommandHandler`.
3.  Crée un `[SERVICE_NAME_PLURAL]Controller` avec une action `POST` sécurisée par `[Authorize]`.
4.  Le test final consiste à appeler cet endpoint avec un token JWT valide et à vérifier que l'entité est créée dans la BDD avec les champs d'audit (`CreatedBy`, `CreatedDate`) correctement renseignés.
