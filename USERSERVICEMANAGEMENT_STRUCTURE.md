# Arborescence Récursive de UserServiceManagement

Voici la structure de fichiers détaillée du microservice `UserServiceManagement`, formatée avec des icônes comme convenu.

```
📁 CoreServices/UserServiceManagement/
├── 📁 CBS.UserServiceManagement.API/
│   ├── 📄 CBS.UserServiceManagement.API.csproj
│   ├── 📄 CBS.UserServiceManagement.API.http
│   ├── 📁 Controllers/
│   │   ├── 📁 Base/
│   │   │   └── 📄 BaseController.cs
│   │   └── 📁 User/
│   │       └── 📄 UsersController.cs
│   ├── 📁 Helpers/
│   │   ├── 📁 DependencyResolver/
│   │   │   └── 📄 DependencyInjectionExtension.cs
│   │   └── 📁 MapperConfiguation/
│   │       └── 📄 MapperConfig.cs
│   ├── 📁 Middlewares/
│   │   ├── 📁 AuditLog/
│   │   │   └── 📄 AuditLogMiddleware.cs
│   │   ├── 📁 Jwt/
│   │   │   ├── 📄 JWTMiddleware.cs
│   │   │   └── 📄 JwtConfigurationExtension.cs
│   │   └── 📁 Logging/
│   │       └── 📄 RequestResponseLoggingMiddleware.cs
│   ├── 📄 Program.cs
│   ├── 📁 Properties/
│   │   └── 📄 launchSettings.json
│   ├── 📄 Startup.cs
│   ├── 📄 appsettings.Development.json
│   └── 📄 appsettings.json
├── 📁 CBS.UserServiceManagement.Common/
│   ├── 📄 CBS.UserServiceManagement.Common.csproj
│   ├── 📁 GenericRespository/
│   │   ├── 📄 GenericRespository.cs
│   │   └── 📄 IGenericRepository.cs
│   └── 📁 UnitOfWork/
│       ├── 📄 IUnitOfWork.cs
│       └── 📄 UnitOfWork.cs
├── 📁 CBS.UserServiceManagement.Data/
│   ├── 📄 CBS.UserServiceManagement.Data.csproj
│   ├── 📁 Dto/
│   │   ├── 📄 UserDto.cs
│   │   └── 📄 UserInfoToken.cs
│   └── 📁 Entity/
│       ├── 📄 AuditLog.cs
│       ├── 📄 BaseEntity.cs
│       └── 📄 User.cs
├── 📁 CBS.UserServiceManagement.Domain/
│   ├── 📄 CBS.UserServiceManagement.Domain.csproj
│   ├── 📁 Context/
│   │   └── 📄 UserContext.cs
│   └── 📁 Migrations/
│       └── ... (17 files)
├── 📁 CBS.UserServiceManagement.Helper/
│   ├── 📄 CBS.UserServiceManagement.Helper.csproj
│   ├── 📁 DataModel/
│   │   └── ...
│   └── 📁 Helper/
│       └── ...
├── 📁 CBS.UserServiceManagement.MediatR/
│   ├── 📄 CBS.UserServiceManagement.MediatR.csproj
│   ├── 📁 Behaviors/
│   │   └── 📄 ValidationBehavior.cs
│   └── 📁 User/
│       ├── 📁 Commands/
│       │   └── ...
│       ├── 📁 Handlers/
│       │   └── ...
│       └── 📁 Queries/
│           └── ...
└── 📁 CBS.UserServiceManagement.Repository/
    ├── 📄 CBS.UserServiceManagement.Repository.csproj
    └── 📁 User/
        ├── 📄 IUserRepository.cs
        └── 📄 UserRepository.cs
```
