# Script complet pour corriger tous les namespaces et usings dans les fichiers .cs

$basePath = Split-Path -Parent $MyInvocation.MyCommand.Path
$files = Get-ChildItem -Path $basePath -Include *.cs -Recurse -File

$replacements = @{
    'CBS\\.TransactionManagement\\.Helper' = 'CBS.CheckManagement.Helper'
    'CBS\\.TransactionManagement\\.Domain' = 'CBS.CheckManagement.Domain'
    'CBS\\.TransactionManagement\\.Data' = 'CBS.CheckManagement.Data'
    'CBS\\.TransactionManagement\\.MediatR' = 'CBS.CheckManagement.MediatR'
    'CBS\\.TransactionManagement\\.Repository' = 'CBS.CheckManagement.Repository'
    'CBS\\.TransactionManagement\\.API' = 'CBS.CheckManagement.API'
    'CBS\\.TransactionManagement\\.Common' = 'CBS.CheckManagement.Common'
    'CBS\\.TransactionManagement\\.Dto' = 'CBS.CheckManagement.Dto'
    'CBS\\.TransactionManagement\\.Commands' = 'CBS.CheckManagement.Commands'
    'CBS\\.TransactionManagement\\.Queries' = 'CBS.CheckManagement.Queries'
}

$filesUpdated = 0

foreach ($file in $files) {
    $content = Get-Content $file.FullName -Raw
    $originalContent = $content
    
    # Appliquer tous les remplacements
    foreach ($key in $replacements.Keys) {
        $content = $content -replace $key, $replacements[$key]
    }
    
    # Si le contenu a changé, mettre à jour le fichier
    if ($content -ne $originalContent) {
        Set-Content -Path $file.FullName -Value $content -NoNewline -Force
        Write-Host "Fichier mis à jour : $($file.FullName)" -ForegroundColor Green
        $filesUpdated++
    }
}

# Vérifier s'il reste des références à TransactionManagement
$remainingRefs = Select-String -Path "$basePath\**\*.cs" -Pattern "TransactionManagement" -List | Select-Object -ExpandProperty Path -Unique

if ($remainingRefs) {
    Write-Host "\nATTENTION : Il reste des références à 'TransactionManagement' dans les fichiers suivants :" -ForegroundColor Red
    $remainingRefs | ForEach-Object { Write-Host "- $_" -ForegroundColor Yellow }
} else {
    Write-Host "\nToutes les références à 'TransactionManagement' ont été supprimées avec succès !" -ForegroundColor Green
}

Write-Host "\nRécapitulatif :" -ForegroundColor Cyan
Write-Host "- Fichiers analysés : $($files.Count)" -ForegroundColor Cyan
Write-Host "- Fichiers mis à jour : $filesUpdated" -ForegroundColor Cyan
Write-Host "- Références restantes : $($remainingRefs.Count)" -ForegroundColor Cyan
