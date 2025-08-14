# Directive Maître Définitive : Génération d'un Template de Microservice

## 1. RÔLE ET MISSION

Tu es un **Générateur de Code Expert**. Ta mission est de générer la **structure complète et le code boilerplate** pour un nouveau microservice en utilisant les paramètres fournis. Pour chaque fichier de code C# généré, tu dois **impérativement adapter les déclarations `namespace` et les directives `using`** pour qu'elles correspondent au nom du service configuré. Exécute les instructions à la lettre. Le résultat doit être une solution .NET immédiatement compilable.

## 2. PARAMÈTRES DE CONFIGURATION
-   `[SERVICE_NAME]`: Nom du domaine (ex: `Portfolio`).
-   `[SERVICE_NAME_LOWER]`: Nom du domaine en minuscules (ex: `portfolio`).
-   `[DATABASE_NAME]`: Nom de la BDD SQL (ex: `CBS_PortfolioDB`).
-   `[PRIMARY_ENTITY_NAME]`: Nom de l'entité principale (ex: `PortfolioItem`).
-   `[PRIMARY_ENTITY_NAME_PLURAL]`: Pluriel de l'entité (ex: `PortfolioItems`).
-   `[PORT_NUMBER]`: Port HTTP (ex: `7114`).

## 3. STRUCTURE DE LA SOLUTION

Génère 7 projets C# ciblant `.NET 8.0` avec les dépendances exactes suivantes.

#### **1. CBS.[SERVICE_NAME]Management.API**
-   **Type:** `Microsoft.NET.Sdk.Web`
-   **Packages NuGet:** `AutoMapper` (v12.0.1), `FluentValidation.AspNetCore` (v11.3.0), `MediatR` (v12.1.1), `Microsoft.AspNetCore.Mvc.NewtonsoftJson` (v7.0.13), `Microsoft.EntityFrameworkCore.Design` (v7.0.13), `NLog.Web.AspNetCore` (v5.3.5), `Serilog.AspNetCore` (v7.0.0), `Swashbuckle.AspNetCore` (v6.5.0)
-   **Références Projet:** Les 6 autres projets de ce service, plus `CBS.APICaller.Helper`, `CBS.CustomLog.Logger`, `CBS.ServicesDelivery.Service`.

#### **2. CBS.[SERVICE_NAME]Management.MediatR**
-   **Type:** `Microsoft.NET.Sdk`
-   **Packages NuGet:** `AutoMapper` (v12.0.1), `FluentValidation` (v11.8.0), `MediatR` (v12.1.1), `Newtonsoft.Json` (v13.0.3), `NLog.Web.AspNetCore` (v5.3.5)
-   **Références Projet:** `.Data`, `.Domain`, `.Repository`

#### **3. CBS.[SERVICE_NAME]Management.Domain**
-   **Type:** `Microsoft.NET.Sdk`
-   **Packages NuGet:** `Microsoft.EntityFrameworkCore.SqlServer` (v7.0.13), `Microsoft.EntityFrameworkCore.Tools` (v7.0.13)
-   **Références Projet:** `.Data`

#### **4. CBS.[SERVICE_NAME]Management.Data**
-   **Type:** `Microsoft.NET.Sdk`
-   **Packages NuGet:** `MediatR` (v12.1.1), `Microsoft.Web.Administration` (v11.1.0)
-   **Références Projet:** `.Helper`

#### **5. CBS.[SERVICE_NAME]Management.Repository**
-   **Type:** `Microsoft.NET.Sdk`
-   **Packages NuGet:** `AutoMapper` (v12.0.1), `MediatR` (v12.1.1), `System.Linq.Dynamic.Core` (v1.4.4)
-   **Références Projet:** `.Common`, `.Domain`

#### **6. CBS.[SERVICE_NAME]Management.Common**
-   **Type:** `Microsoft.NET.Sdk`
-   **Packages NuGet:** `Microsoft.EntityFrameworkCore` (v7.0.13), `MongoDB.Driver` (v3.1.0)
-   **Références Projet:** `.Data`

#### **7. CBS.[SERVICE_NAME]Management.Helper**
-   **Type:** `Microsoft.NET.Sdk`
-   **Packages NuGet:** `Microsoft.AspNetCore.Http` (v2.2.2), `Microsoft.Extensions.Configuration.Abstractions` (v7.0.0), `MongoDB.Driver` (v3.1.0)
-   **Références Projet:** `CBS.APICaller.Helper`

## 4. FICHIERS BOILERPLATE À GÉNÉRER

### **4.1. Couche Data**

-   **Fichier:** `CoreServices/[SERVICE_NAME]Management/CBS.[SERVICE_NAME]Management.Data/BaseEntity.cs`
    ```csharp
    using Microsoft.Web.Administration;
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    namespace CBS.[SERVICE_NAME]Management.Data
    {
        public abstract class BaseEntity
        {
            private DateTime _createdDate;
            public DateTime CreatedDate { get => _createdDate.ToLocalTime(); set => _createdDate = value.ToLocalTime(); }
            public string CreatedBy { get; set; }
            private DateTime _modifiedDate;
            public DateTime ModifiedDate { get => _modifiedDate.ToLocalTime(); set => _modifiedDate = value.ToLocalTime(); }
            public string ModifiedBy { get; set; }
            private DateTime? _deletedDate;
            public DateTime? DeletedDate { get => _deletedDate?.ToLocalTime(); set => _deletedDate = value?.ToLocalTime(); }
            public string? DeletedBy { get; set; }
            [NotMapped]
            public ObjectState ObjectState { get; set; }
            public bool IsDeleted { get; set; } = false;
        }
    }
    ```

### **4.2. Couche API**

-   **Fichier:** `CoreServices/[SERVICE_NAME]Management/CBS.[SERVICE_NAME]Management.API/Properties/launchSettings.json`
    ```json
    {
      "profiles": {
        "http": {
          "commandName": "Project",
          "launchBrowser": true,
          "launchUrl": "swagger",
          "applicationUrl": "http://localhost:[PORT_NUMBER]",
          "environmentVariables": {
            "ASPNETCORE_ENVIRONMENT": "Development"
          }
        }
      }
    }
    ```
-   **Fichier:** `CoreServices/[SERVICE_NAME]Management/CBS.[SERVICE_NAME]Management.API/appsettings.json`
    ```json
    {
      "ConnectionStrings": {
        "DefaultConnection": "Server=localhost;Database=[DATABASE_NAME];User Id=youruser;Password=yourpassword;TrustServerCertificate=True;"
      },
      "JwtSettings": {
        "key": "YOUR_SUPER_SECRET_KEY_THAT_IS_LONG_ENOUGH",
        "issuer": "https://identity.cbs.com/",
        "audience": "CBS.[SERVICE_NAME]Management"
      },
      "Logging": {
        "LogLevel": {
          "Default": "Information",
          "Microsoft.AspNetCore": "Warning"
        }
      },
      "AllowedHosts": "*"
    }
    ```

## 5. TÂCHE DE VALIDATION AUTOMATIQUE

1.  **Entité:** Crée `[PRIMARY_ENTITY_NAME].cs` dans `Data/Entity/`, héritant de `BaseEntity`, avec `public string Name { get; set; }`.
2.  **DTO:** Crée `[PRIMARY_ENTITY_NAME]Dto.cs` dans `Data/Dto/`.
3.  **DbContext:** Crée `[SERVICE_NAME]Context.cs` dans `Domain/Context/` héritant de `DbContext`. Ajoute `public DbSet<[PRIMARY_ENTITY_NAME]> [PRIMARY_ENTITY_NAME_PLURAL] { get; set; }`.
4.  **Migrations:** Exécute `dotnet ef migrations add InitialCreate` et `dotnet ef database update`.
5.  **Repository:** Crée `I[PRIMARY_ENTITY_NAME]Repository.cs` et `[PRIMARY_ENTITY_NAME]Repository.cs`.
6.  **MediatR:** Crée la `Create[PRIMARY_ENTITY_NAME]Command.cs` avec son `Validator` et son `CommandHandler`.
7.  **Controller:** Crée `[PRIMARY_ENTITY_NAME_PLURAL]Controller.cs` avec une action `POST`.
8.  **DI & Mapping:** Met à jour `DependencyInjectionExtension.cs` et `MapperConfig.cs`.
9.  **Compilation:** La solution doit compiler sans erreur.
