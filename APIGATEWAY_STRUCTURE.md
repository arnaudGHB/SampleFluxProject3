# Arborescence Récursive de l'ApiGateway

Voici la structure de fichiers détaillée du microservice `ApiGateway`, formatée avec des icônes pour une meilleure lisibilité.

```
📁 ApiGateway/
|-- 📁 Database
|   `-- 📁 CBS.DataContex
|       `-- 📁 CBS.DataContext
|           |-- 📄 CBS.Gateway.DataContext.csproj
|           |-- 📄 CBS.Gateway.DataContext.v3.ncrunchproject
|           |-- 📁 Context
|           |   `-- 📄 LoggingDbContext.cs
|           |-- 📁 DBConnection
|           |   `-- 📄 MongoDbConnection.cs
|           |-- 📁 Entity
|           |   `-- 📄 RequestResponseLog.cs
|           `-- 📁 Repository
|               |-- 📁 Generic
|               |   |-- 📄 GenericRepository.cs
|               |   `-- 📄 IMongoGenericRepository.cs
|               `-- 📁 Uow
|                   |-- 📄 IMongoUnitOfWork.cs
|                   `-- 📄 MongoUnitOfWork.cs
`-- 📁 Public
    |-- 📄 CBS.Gateway.API.csproj
    |-- 📄 CBS.Gateway.API.v3.ncrunchproject
    |-- 📁 Config
    |   |-- 📄 AlterUpstream.cs
    |   |-- 📄 CustomSecurityHeader.cs
    |   `-- 📄 PathHelper.cs
    |-- 📁 Controllers
    |   `-- 📄 HomeController.cs
    |-- 📁 JTWMiddleware
    |   |-- 📄 CorrelationIdMiddleware.cs
    |   |-- 📄 CustomJwtAuthExtension.cs
    |   |-- 📄 CustomJwtSecurityTokenHandler.cs
    |   |-- 📄 CustomTokenValidator.cs
    |   `-- 📄 TokenValidationMiddleware.cs
    |-- 📄 JwtSettings.cs
    |-- 📁 LogginMiddleWare
    |   `-- 📄 RequestResponseLoggingMiddleware.cs
    |-- 📄 Program.cs
    |-- 📁 Properties
    |   `-- 📄 launchSettings.json
    |-- 📁 Routes
    |   |-- 📄 ocelot.Client.api.json
    |   |-- 📄 ocelot.SwaggerEndPoints.json
    |   `-- 📄 ocelot.global.json
    |-- 📄 Startup.cs
    |-- 📄 appsettings.Development.json
    |-- 📄 appsettings.json
    |-- 📄 libman.json
    |-- 📄 ocelot - Copy.Development.json
    |-- 📄 ocelot - Copy.json
    |-- 📄 ocelot - Copy.local.json
    |-- 📄 ocelot.Development.json
    |-- 📄 ocelot.SwaggerEndPoints.Development.json
    |-- 📄 ocelot.SwaggerEndPoints.json
    |-- 📄 ocelot.SwaggerEndPoints.local.json
    |-- 📄 ocelot.json
    `-- 📄 ocelot.local.json
```
