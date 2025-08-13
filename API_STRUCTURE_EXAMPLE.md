# Arborescence de la Couche API pour `TransactionManagement`

Voici la structure de fichiers détaillée pour la couche `API` du microservice `TransactionManagement`, formatée comme demandé.

```
📁 CBS.TransactionManagement.API/
├── 📁 AuditLogMiddleware/
│   └── 📄 AuditLogMiddleware.cs
├── 📄 CBS - Backup.TransactionManagement.API.csproj
├── 📄 CBS.TransactionManagement.API.csproj
├── 📄 CBS.TransactionManagement.API.v3.ncrunchproject
├── 📁 Controllers/
│   ├── 📁 Account/
│   │   └── 📄 AccountController.cs
│   ├── 📁 AccountingDayOpening/
│   │   └── 📄 AccountingDayController.cs
│   ├── 📁 Base/
│   │   └── 📄 BaseController.cs
│   ├── 📁 CashCeilingMovement/
│   │   └── 📄 CashCeilingRequestController.cs
│   ├── 📁 CashChangeManagement/
│   │   └── 📄 CashChangeController.cs
│   ├── 📁 CashReplenishment/
│   │   ├── 📄 PrimaryTellerCashReplenishmentController.cs
│   │   └── 📄 SubTellerCashReplenishmentController.cs
│   ├── ... (31 autres répertoires de contrôleurs)
│   ├── 📁 VaultP/
│   │   └── 📄 VaultController.cs
│   ├── 📁 WithdrawalLimits/
│   │   └── 📄 WithdrawalLimitsController.cs
│   └── 📁 WithdrawalNotofy/
│       └── 📄 WithdrawalNotificationController.cs
├── 📁 Helpers/
│   ├── 📄 ArrayModelBinder.cs
│   ├── 📄 CompressUtility.cs
│   ├── 📁 DependencyResolver/
│   │   └── 📄 DependencyInjectionExtension.cs
│   ├── 📁 MapperConfiguation/
│   │   └── 📄 MapperConfig.cs
│   ├── 📁 MappingProfile/
│   │   ├── 📄 AccountProfile.cs
│   │   ├── 📄 AccountingEventProfile.cs
│   │   └── ... (16 autres profils)
│   └── 📄 UnprocessableEntityObjectResult.cs
├── 📁 JWTTokenValidator/
│   ├── 📄 HangfireAuthorizationFilter.cs
│   ├── 📄 JWTMiddleware.cs
│   └── 📄 JwtConfigurationExtension.cs
├── 📁 LoggingMiddleWare/
│   └── 📄 RequestResponseLoggingMiddleware.cs
├── 📄 Program.cs
├── 📁 Properties/
│   └── 📄 launchSettings.json
├── 📄 Startup.cs
├── 📄 appsettings.Development.json
├── 📄 appsettings.json
├── 📄 nlog.config
└── 📁 wwwroot/
    ├── 📁 AccountExports/
    │   └── ... (3 fichiers .xlsx)
    └── 📁 MembersAccountExport/
        └── ... (5 sous-répertoires contenant des fichiers .xlsx)
```
