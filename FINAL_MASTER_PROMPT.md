# Directive Maître v4 (Définitive) : Génération d'un Template de Microservice Configurable

## 1. RÔLE ET MISSION

Tu es un **Générateur de Code Expert et un Architecte Logiciel**. Ta mission est de générer la structure de projet complète et le code boilerplate pour un nouveau microservice en utilisant les paramètres fournis.

**Instruction critique :** Pour chaque fichier de code C# généré, tu dois **impérativement adapter les déclarations `namespace` et les directives `using`** pour qu'elles correspondent au nom du service configuré (ex: `CBS.[SERVICE_NAME]Management.API`, `CBS.[SERVICE_NAME]Management.Data`, etc.).

Tu dois opérer avec une précision chirurgicale. **Exécute les instructions à la lettre.** Le résultat doit être une solution .NET immédiatement compilable et parfaitement intégrée.

## 2. PARAMÈTRES DE CONFIGURATION
-   `[SERVICE_NAME]`: Nom du domaine (ex: `Portfolio`).
-   `[SERVICE_NAME_LOWER]`: Nom du domaine en minuscules (ex: `portfolio`).
-   `[DATABASE_NAME]`: Nom de la BDD SQL (ex: `CBS_PortfolioDB`).
-   `[PRIMARY_ENTITY_NAME]`: Nom de l'entité principale (ex: `PortfolioItem`).
-   `[PRIMARY_ENTITY_NAME_PLURAL]`: Pluriel de l'entité (ex: `PortfolioItems`).
-   `[PORT_NUMBER]`: Port HTTP (ex: `7114`).

## 3. STRUCTURE ET FONDATIONS (CODE BOILERPLATE)

Génère la structure et les fichiers suivants avec leur contenu exact.

### 3.1. Couche API (`CBS.[SERVICE_NAME]Management.API`)

**Fichiers à créer :**

-   **Fichier :** `Helpers/ArrayModelBinder.cs`
    ```csharp
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using System;
    using System.ComponentModel;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;

    namespace CBS.[SERVICE_NAME]Management.API.Helpers
    {
        public class ArrayModelBinder : IModelBinder
        {
            public Task BindModelAsync(ModelBindingContext bindingContext)
            {
                if (!bindingContext.ModelMetadata.IsEnumerableType)
                {
                    bindingContext.Result = ModelBindingResult.Failed();
                    return Task.CompletedTask;
                }
                var value = bindingContext.ValueProvider.GetValue(bindingContext.ModelName).ToString();
                if (string.IsNullOrWhiteSpace(value))
                {
                    bindingContext.Result = ModelBindingResult.Success(null);
                    return Task.CompletedTask;
                }
                var elementType = bindingContext.ModelType.GetTypeInfo().GenericTypeArguments[0];
                var converter = TypeDescriptor.GetConverter(elementType);
                var values = value.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(x => converter.ConvertFromString(x.Trim()))
                    .ToArray();
                var typedValues = Array.CreateInstance(elementType, values.Length);
                values.CopyTo(typedValues, 0);
                bindingContext.Model = typedValues;
                bindingContext.Result = ModelBindingResult.Success(bindingContext.Model);
                return Task.CompletedTask;
            }
        }
    }
    ```
-   **Fichier :** `Helpers/UnprocessableEntityObjectResult.cs`
    ```csharp
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using System;

    namespace CBS.[SERVICE_NAME]Management.API.Helpers
    {
        public class UnprocessableEntityObjectResult : ObjectResult
        {
            public UnprocessableEntityObjectResult(ModelStateDictionary modelState)
                : base(new SerializableError(modelState))
            {
                if (modelState == null)
                {
                    throw new ArgumentNullException(nameof(modelState));
                }
                StatusCode = 422;
            }
        }
    }
    ```
-   **Fichier :** `AuditLogMiddleware/AuditLogMiddleware.cs`
    ```csharp
    using CBS.[SERVICE_NAME]Management.Data;
    using CBS.[SERVICE_NAME]Management.Domain.Context;
    using System.Text;

    namespace CBS.[SERVICE_NAME]Management.API
    {
        public class AuditLogMiddleware
        {
            private readonly RequestDelegate _next;
            public AuditLogMiddleware(RequestDelegate next) { _next = next; }

            public async Task InvokeAsync(HttpContext context, [SERVICE_NAME]Context dbContext)
            {
                context.Request.EnableBuffering();
                var body = await new StreamReader(context.Request.Body).ReadToEndAsync();
                context.Request.Body.Position = 0;

                var auditLog = new AuditLog
                {
                    EntityName = context.Request.Path,
                    Action = context.Request.Method,
                    Timestamp = DateTime.UtcNow,
                    Changes = body,
                    UserId = context.User.Identity?.Name ?? "anonymous",
                    IPAddress = context.Connection.RemoteIpAddress?.ToString()
                };
                if(dbContext.AuditLogs != null)
                {
                    dbContext.AuditLogs.Add(auditLog);
                    await dbContext.SaveChangesAsync();
                }

                context.Request.Body.Position = 0;
                await _next(context);
            }
        }
    }
    ```
-   **Fichier :** `JWTTokenValidator/JWTMiddleware.cs`
    ```csharp
    // ... (Code complet de JWTMiddleware.cs avec namespace CBS.APICaller.Helper) ...
    ```
-   **Fichier :** `LoggingMiddleWare/RequestResponseLoggingMiddleware.cs`
    ```csharp
    // ... (Code complet de RequestResponseLoggingMiddleware.cs avec namespace CBS.[SERVICE_NAME]Management.API) ...
    ```
-   **Fichier :** `Properties/launchSettings.json`
    ```json
    {
      "profiles": {
        "http": {
          "commandName": "Project",
          "launchBrowser": true,
          "launchUrl": "swagger",
          "applicationUrl": "http://localhost:[PORT_NUMBER]"
        }
      }
    }
    ```
-   **Fichier :** `nlog.config`
    ```xml
    <?xml version="1.0" encoding="utf-8" ?>
    <nlog xmlns="http://www.nlog-project.org/schemas/NLog.xsd"
          xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">
      <targets>
        <target name="applog" xsi:type="File" fileName="C:\Log\[SERVICE_NAME_LOWER]\applog-${shortdate}.log"
                layout="${longdate} - ${message} - ${exception:format=StackTrace}${newline}" />
      </targets>
      <rules>
        <logger name="*" minlevel="Debug" writeTo="applog" />
      </rules>
    </nlog>
    ```
-   **Fichier :** `Startup.cs`
    ```csharp
    // ... (Code complet de Startup.cs qui enregistre tous les services et configure le pipeline de middlewares dans l'ordre exact : Logging > JWT > Auth > Audit > etc.) ...
    ```

### 3.2. Couche Data (`CBS.[SERVICE_NAME]Management.Data`)
-   **Fichier :** `BaseEntity.cs`
    ```csharp
    // ... (Code complet de BaseEntity.cs avec namespace CBS.[SERVICE_NAME]Management.Data) ...
    ```
-   **Fichier :** `Dto/UserInfoToken.cs`
    ```csharp
    // ... (Code complet de UserInfoToken.cs avec namespace CBS.[SERVICE_NAME]Management.Dto) ...
    ```
-   **Fichier :** `Entity/AuditLog.cs`
    ```csharp
    namespace CBS.[SERVICE_NAME]Management.Data
    {
        public class AuditLog : BaseEntity
        {
            public int Id { get; set; }
            public string EntityName { get; set; }
            public string Action { get; set; }
            public DateTime Timestamp { get; set; }
            public string Changes { get; set; }
            public string UserId { get; set; }
            public string IPAddress { get; set; }
        }
    }
    ```

### 3.3. Couche Domain
- **DbContext :** Dans `[SERVICE_NAME]Context.cs`, ajouter `public DbSet<AuditLog> AuditLogs { get; set; }`.

**(Le reste du prompt v3 reste valide pour les autres fichiers boilerplate)**
