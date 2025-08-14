# Script pour corriger les références à TransactionManagement dans les fichiers MappingProfile

$basePath = Join-Path (Split-Path -Parent $MyInvocation.MyCommand.Path) "CBS.CheckManagement.API\Helpers\MappingProfile"

# Vérifier si le dossier existe
if (-not (Test-Path $basePath)) {
    Write-Host "Le dossier MappingProfile n'existe pas : $basePath" -ForegroundColor Red
    exit 1
}

# Définir les remplacements à effectuer
$replacements = @{
    'using CBS\.TransactionManagement\.Command;' = 'using CBS.CheckManagement.Command;'
    'using CBS\.TransactionManagement\.MemberAccountConfiguration\.Commands;' = 'using CBS.CheckManagement.MemberAccountConfiguration.Commands;'
    'using CBS\.TransactionManagement\.CashCeilingMovement\.Commands;' = 'using CBS.CheckManagement.CashCeilingMovement.Commands;'
    'using CBS\.TransactionManagement\.CashOutThirdPartyP\.Commands;' = 'using CBS.CheckManagement.CashOutThirdPartyP.Commands;'
    'using CBS\.TransactionManagement\.DailyTellerP\.Commands;' = 'using CBS.CheckManagement.DailyTellerP.Commands;'
    'using CBS\.TransactionManagement\.OldLoanConfiguration\.Commands;' = 'using CBS.CheckManagement.OldLoanConfiguration.Commands;'
    'using CBS\.TransactionManagement\.otherCashIn\.Commands;' = 'using CBS.CheckManagement.otherCashIn.Commands;'
}

# Parcourir tous les fichiers .cs dans le dossier MappingProfile
Get-ChildItem -Path $basePath -Filter "*.cs" -Recurse | ForEach-Object {
    $filePath = $_.FullName
    $content = Get-Content $filePath -Raw
    $originalContent = $content
    
    # Appliquer les remplacements
    foreach ($pattern in $replacements.Keys) {
        $content = $content -replace $pattern, $replacements[$pattern]
    }
    
    # Si le contenu a changé, mettre à jour le fichier
    if ($content -ne $originalContent) {
        Set-Content -Path $filePath -Value $content -NoNewline -Force
        Write-Host "Fichier mis à jour : $filePath" -ForegroundColor Green
    }
}

# Vérifier s'il reste des références à TransactionManagement
$remainingRefs = Select-String -Path "$basePath\*.cs" -Pattern "TransactionManagement" -List | 
    Select-Object -ExpandProperty Path -Unique

if ($remainingRefs) {
    Write-Host "\nATTENTION : Il reste des références à 'TransactionManagement' dans les fichiers suivants :" -ForegroundColor Red
    $remainingRefs | ForEach-Object { Write-Host "- $_" -ForegroundColor Yellow }
} else {
    Write-Host "\nToutes les références à 'TransactionManagement' ont été supprimées des fichiers MappingProfile !" -ForegroundColor Green
}
