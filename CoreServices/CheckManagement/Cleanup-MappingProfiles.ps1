# Script pour nettoyer les profils de mapping non essentiels
$mappingProfilePath = Join-Path $PSScriptRoot "CBS.CheckManagement.API\Helpers\MappingProfile"

# Liste des fichiers à supprimer
$filesToRemove = @(
    "AccountProfile.cs",
    "AccountingEventProfile.cs",
    "CashReplenishmentPrimaryTellerProfile.cs",
    "CashReplenishmentProfile.cs",
    "CloseFeeParameterProfile.cs",
    "ConfigsProfile.cs",
    "CurrencyNotesProfile.cs",
    "DailyProvisionProfile.cs",
    "DepositLimitProfile.cs",
    "EntryFeeParameterProfile.cs",
    "ManagementFeeParameterProfile.cs",
    "ReopenFeeParameterProfile.cs",
    "SavingProductProfile.cs",
    "TellerHistoryProfile.cs",
    "TellerProfile.cs",
    "TransactionProfile.cs",
    "TransferLimitsProfile.cs",
    "WithdrawalLimitsProfile.cs"
)

# Suppression des fichiers
foreach ($file in $filesToRemove) {
    $filePath = Join-Path $mappingProfilePath $file
    if (Test-Path $filePath) {
        Remove-Item $filePath -Force
        Write-Host "Supprimé : $file" -ForegroundColor Red
    } else {
        Write-Host "Non trouvé : $file" -ForegroundColor Yellow
    }
}

Write-Host "Nettoyage des profils de mapping terminé." -ForegroundColor Green
