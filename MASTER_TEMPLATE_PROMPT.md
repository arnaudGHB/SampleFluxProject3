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

**Structure Complète :**
```
CBS.[SERVICE_NAME]Management.API/
├── AuditLogMiddleware/
│   └── AuditLogMiddleware.cs
├── Controllers/
│   └── BaseController.cs
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
├── appsettings.json
├── appsettings.Development.json
├── nlog.config
├── Program.cs
└── Startup.cs
```

**Contenu des fichiers :**

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
                dbContext.AuditLogs.Add(auditLog);
                await dbContext.SaveChangesAsync();

                context.Request.Body.Position = 0;
                await _next(context);
            }
        }
    }
    ```
-   **Fichier :** `Startup.cs` (Version finale)
    ```csharp
    // ... (Code de Startup.cs qui inclut maintenant le pipeline complet dans Configure) ...
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        // ...
        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseMiddleware<RequestResponseLoggingMiddleware>();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseMiddleware<JWTMiddleware>();
        app.UseMiddleware<AuditLogMiddleware>(); // <-- Ajout crucial
        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
    }
    ```
-   **(Les autres fichiers de l'API et des autres couches sont à générer comme défini dans la v3 du prompt)**

### 3.2. Couche Data (`CBS.[SERVICE_NAME]Management.Data`)
-   **Ajouter `AuditLog.cs`**
-   **Fichier :** `Data/Entity/AuditLog.cs`
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
- **DbContext :** Ajouter `public DbSet<AuditLog> AuditLogs { get; set; }` au `[SERVICE_NAME]Context.cs`.

**(Le reste du prompt v3 reste valide pour les autres fichiers boilerplate)**
