# Structure de Fichiers Définitive pour un Nouveau Microservice

Voici la structure de fichiers complète et récursive attendue pour un nouveau microservice (ex: `[SERVICE_NAME]` = `Portfolio`) après exécution de la directive `MASTER_PROMPT_V8_DUPLICATION.md`.

```
CoreServices/PortfolioManagement/
├── CBS.PortfolioManagement.API/
│   ├── Controllers/
│   │   ├── BaseController.cs
│   │   └── PingsController.cs
│   ├── Helpers/
│   │   ├── DependencyResolver/
│   │   │   └── DependencyInjectionExtension.cs
│   │   ├── MapperConfiguation/
│   │   │   └── MapperConfig.cs
│   │   ├── ArrayModelBinder.cs
│   │   └── UnprocessableEntityObjectResult.cs
│   ├── JWTTokenValidator/
│   │   └── JWTMiddleware.cs
│   ├── LoggingMiddleWare/
│   │   └── RequestResponseLoggingMiddleware.cs
│   ├── Properties/
│   │   └── launchSettings.json
│   ├── AuditLogMiddleware/
│   │   └── AuditLogMiddleware.cs
│   ├── appsettings.Development.json
│   ├── appsettings.json
│   ├── nlog.config
│   ├── Program.cs
│   ├── Startup.cs
│   └── CBS.PortfolioManagement.API.csproj
│
├── CBS.PortfolioManagement.Common/
│   ├── GenericRespository/
│   │   ├── GenericRespository.cs
│   │   └── IGenericRepository.cs
│   ├── MongoDBContext/
│   ├── UnitOfWork/
│   │   ├── IUnitOfWork.cs
│   │   └── UnitOfWork.cs
│   └── CBS.PortfolioManagement.Common.csproj
│
├── CBS.PortfolioManagement.Data/
│   ├── Dto/
│   │   ├── PingDto.cs
│   │   └── UserInfoToken.cs
│   ├── Entity/
│   │   ├── AuditLog.cs
│   │   └── Ping.cs
│   ├── BaseEntity.cs
│   └── CBS.PortfolioManagement.Data.csproj
│
├── CBS.PortfolioManagement.Domain/
│   ├── Context/
│   │   └── PortfolioContext.cs
│   ├── Migrations/
│   │   └── (dossier contenant les migrations EF Core)
│   └── CBS.PortfolioManagement.Domain.csproj
│
├── CBS.PortfolioManagement.Helper/
│   ├── Helper/
│   │   └── ServiceResponse.cs
│   └── CBS.PortfolioManagement.Helper.csproj
│
├── CBS.PortfolioManagement.MediatR/
│   ├── PipeLineBehavior/
│   │   └── ValidationBehavior.cs
│   └── Ping/
│       ├── Commands/
│       │   └── AddPingCommand.cs
│       └── Handlers/
│           └── AddPingCommandHandler.cs
│   └── CBS.PortfolioManagement.MediatR.csproj
│
└── CBS.PortfolioManagement.Repository/
    ├── IPingRepository.cs
    ├── PingRepository.cs
    └── CBS.PortfolioManagement.Repository.csproj
```
