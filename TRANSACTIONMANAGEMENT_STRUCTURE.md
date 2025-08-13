# Arborescence Récursive de TransactionManagement

Voici la structure de fichiers détaillée du microservice `TransactionManagement`, formatée avec des icônes pour une meilleure lisibilité.

```
📁 CoreServices/TransactionManagement/
├── 📁 CBS.TransactionManagement.API/
│   ├── 📁 AuditLogMiddleware/
│   │   └── 📄 AuditLogMiddleware.cs
│   ├── 📄 CBS - Backup.TransactionManagement.API.csproj
│   ├── 📄 CBS.TransactionManagement.API.csproj
│   ├── 📄 CBS.TransactionManagement.API.v3.ncrunchproject
│   ├── 📁 Controllers/
│   │   ├── 📁 Account/
│   │   │   └── 📄 AccountController.cs
│   │   ├── 📁 AccountingDayOpening/
│   │   │   └── 📄 AccountingDayController.cs
│   │   ├── 📁 Base/
│   │   │   └── 📄 BaseController.cs
│   │   ├── 📁 CashCeilingMovement/
│   │   │   └── 📄 CashCeilingRequestController.cs
│   │   ├── 📁 CashChangeManagement/
│   │   │   └── 📄 CashChangeController.cs
│   │   ├── 📁 CashReplenishment/
│   │   │   ├── 📄 PrimaryTellerCashReplenishmentController.cs
│   │   │   └── 📄 SubTellerCashReplenishmentController.cs
│   │   ├── 📁 ChargesWaivedP/
│   │   │   └── 📄 ChargesWaivedController.cs
│   │   ├── 📁 CloseFeeParameter/
│   │   │   └── 📄 CloseFeeParameterController.cs
│   │   ├── 📁 Config/
│   │   │   └── 📄 ConfigController.cs
│   │   ├── 📁 DailyStatisticBoard/
│   │   │   └── 📄 GeneralDailyDashboardController.cs
│   │   ├── 📁 DailyTellerP/
│   │   │   └── 📄 DailyTellerController.cs
│   │   ├── 📁 DepositLimits/
│   │   │   └── 📄 DepositLimitsController.cs
│   │   ├── 📁 EntryFeeParameter/
│   │   │   └── 📄 EntryFeeParameterController.cs
│   │   ├── 📁 FeeP/
│   │   │   └── 📄 FeeController.cs
│   │   ├── 📁 FeePolicyP/
│   │   │   └── 📄 FeePolicyController.cs
│   │   ├── 📁 FileDownloadInfoP/
│   │   │   └── 📄 BatchFileDownloadController.cs
│   │   ├── 📁 HolyDayP/
│   │   │   └── 📄 HolyDayController.cs
│   │   ├── 📁 HolyDayRecurringP/
│   │   │   └── 📄 HolyDayRecurringController.cs
│   │   ├── 📁 ManagementFeeParameter/
│   │   │   └── 📄 ManagementFeeParameterController.cs
│   │   ├── 📁 MemberAccountSetting/
│   │   │   ├── 📄 MemberAccountActivationController.cs
│   │   │   └── 📄 MemberAccountActivationPolicyController.cs
│   │   ├── 📁 MemberNoneCashOperationP/
│   │   │   └── 📄 MemberNoneCashOperationController.cs
│   │   ├── 📁 MobileMoney/
│   │   │   └── 📄 MobileMoneyCashTopupController.cs
│   │   ├── 📁 OldLoanConfiguration/
│   │   │   └── 📄 OldLoanConfigurationController.cs
│   │   ├── 📁 OtherCashinP/
│   │   │   └── 📄 OtherTransactionController.cs
│   │   ├── 📁 RemittanceP/
│   │   │   └── 📄 RemittanceController.cs
│   │   ├── 📁 ReopenFeeParameter/
│   │   │   └── 📄 ReopenFeeParameterController.cs
│   │   ├── 📁 SalaryFiles/
│   │   │   └── 📄 SalaryProcessingFileController.cs
│   │   ├── 📁 SalaryManagement/
│   │   │   ├── 📁 SalaryAnalysisP/
│   │   │   │   └── 📄 SalaryAnalysisController.cs
│   │   │   ├── 📁 SalaryExecutionP/
│   │   │   │   └── 📄 SalaryExecutionController.cs
│   │   │   ├── 📁 SalaryUploadedModelP/
│   │   │   │   └── 📄 SalaryUploadController.cs
│   │   │   └── 📁 StandingOrderP/
│   │   │       └── 📄 StandingOrderController.cs
│   │   ├── 📁 SavingProduct/
│   │   │   └── 📄 SavingProductController.cs
│   │   ├── 📁 SavingProductFeeP/
│   │   │   └── 📄 SavingProductFeeController.cs
│   │   ├── 📁 Teller/
│   │   │   └── 📄 TellerController.cs
│   │   ├── 📁 TellerOperationsP/
│   │   │   └── 📄 TellerOperationsController.cs
│   │   ├── 📁 ThirdParty/
│   │   │   └── 📄 ThirdPartyOperationsController.cs
│   │   ├── 📁 Transaction/
│   │   │   └── 📄 TransactionController.cs
│   │   ├── 📁 TransactionReversalP/
│   │   │   └── 📄 TransactionReversalController.cs
│   │   ├── 📁 TransactionTrackerAccountingData/
│   │   │   └── 📄 TransactionTrackerAccountingController.cs
│   │   ├── 📁 TransferLimits/
│   │   │   └── 📄 TransferLimitsController.cs
│   │   ├── 📁 TransferM/
│   │   │   └── 📄 TransferController.cs
│   │   ├── 📁 VaultP/
│   │   │   └── 📄 VaultController.cs
│   │   ├── 📁 WithdrawalLimits/
│   │   │   └── 📄 WithdrawalLimitsController.cs
│   │   └── 📁 WithdrawalNotofy/
│   │       └── 📄 WithdrawalNotificationController.cs
│   ├── 📁 Helpers/
│   │   ├── 📄 ArrayModelBinder.cs
│   │   ├── 📄 CompressUtility.cs
│   │   ├── 📁 DependencyResolver/
│   │   │   └── 📄 DependencyInjectionExtension.cs
│   │   ├── 📁 MapperConfiguation/
│   │   │   └── 📄 MapperConfig.cs
│   │   ├── 📁 MappingProfile/
│   │   │   ├── 📄 AccountProfile.cs
│   │   │   ├── ... (16 more profiles)
│   │   │   └── 📄 WithdrawalLimitsProfile.cs
│   │   └── 📄 UnprocessableEntityObjectResult.cs
│   ├── 📁 JWTTokenValidator/
│   │   ├── 📄 HangfireAuthorizationFilter.cs
│   │   ├── 📄 JWTMiddleware.cs
│   │   └── 📄 JwtConfigurationExtension.cs
│   ├── 📁 LoggingMiddleWare/
│   │   └── 📄 RequestResponseLoggingMiddleware.cs
│   ├── 📄 Program.cs
│   ├── 📁 Properties/
│   │   └── 📄 launchSettings.json
│   ├── 📄 Startup.cs
│   ├── 📄 appsettings.Development.json
│   ├── 📄 appsettings.json
│   ├── 📄 nlog.config
│   └── 📁 wwwroot/
│       ├── 📁 AccountExports/
│       │   └── ... (3 files)
│       └── 📁 MembersAccountExport/
│           └── ... (5 subdirectories)
├── 📁 CBS.TransactionManagement.Common/
│   ├── ... (3 files)
│   ├── 📁 GenericRespository/
│   │   ├── 📄 GenericRespository.cs
│   │   └── 📄 IGenericRepository.cs
│   ├── 📁 MongoDBContext/
│   │   ├── 📁 DBConnection/
│   │   │   └── 📄 MongoDbConnection.cs
│   │   └── 📁 Repository/
│   │       └── ...
│   └── 📁 UnitOfWork/
│       ├── 📄 IDbContextFactory.cs
│       ├── 📄 IUnitOfWork.cs
│       └── 📄 UnitOfWork.cs
├── 📁 CBS.TransactionManagement.Data/
│   ├── 📄 BaseEntity.cs
│   ├── ... (3 files)
│   ├── 📁 Dto/
│   │   └── ... (48 subdirectories)
│   ├── 📁 Entity/
│   │   └── ... (42 subdirectories)
│   └── 📁 Queries/
│       └── 📄 GetAllLoanQueries.cs
├── 📁 CBS.TransactionManagement.Domain/
│   ├── ... (3 files)
│   ├── 📁 Context/
│   │   ├── 📄 DatabaseInitializerService.cs
│   │   └── 📄 TransactionContext.cs
│   └── 📁 Migrations/
│       └── ... (many migration files)
├── 📁 CBS.TransactionManagement.Helper/
│   ├── ... (3 files)
│   ├── 📁 Helper/
│   │   └── ... (21 files)
│   └── 📁 Model/
│       └── ... (3 files)
├── 📁 CBS.TransactionManagement.MediatR/
│   └── ... (40+ subdirectories for features)
└── 📁 CBS.TransactionManagement.Repository/
    └── ... (40+ subdirectories for repositories)
```
