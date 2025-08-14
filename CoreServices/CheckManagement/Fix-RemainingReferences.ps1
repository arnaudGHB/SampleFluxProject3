# Script pour corriger les références restantes à TransactionManagement dans les fichiers .cs

$basePath = Split-Path -Parent $MyInvocation.MyCommand.Path

# 1. Remplacer les namespaces dans les fichiers .cs
Get-ChildItem -Path $basePath -Include *.cs -Recurse -File | ForEach-Object {
    $content = Get-Content $_.FullName -Raw
    
    # Sauvegarder le contenu original pour la comparaison
    $originalContent = $content
    
    # Remplacer les namespaces et usings
    $content = $content -replace 'namespace\s+CBS\.TransactionManagement\.Dto', 'namespace CBS.CheckManagement.Dto'
    $content = $content -replace 'using\s+CBS\.TransactionManagement\.Dto', 'using CBS.CheckManagement.Dto'
    $content = $content -replace 'using\s+CBS\.TransactionManagement\.Commands', 'using CBS.CheckManagement.Commands'
    $content = $content -replace 'using\s+CBS\.TransactionManagement\.Queries', 'using CBS.CheckManagement.Queries'
    
    # Si le contenu a changé, mettre à jour le fichier
    if ($content -ne $originalContent) {
        Set-Content -Path $_.FullName -Value $content -NoNewline -Force
        Write-Host "Fichier mis à jour : $($_.FullName)" -ForegroundColor Green
    }
}

# 2. Vérifier s'il reste des références à TransactionManagement
$remainingRefs = Get-ChildItem -Path $basePath -Include *.cs -Recurse -File | 
    Select-String -Pattern "TransactionManagement" -List | 
    Select-Object -ExpandProperty Path -Unique

if ($remainingRefs) {
    Write-Host "\nATTENTION : Il reste des références à 'TransactionManagement' dans les fichiers suivants :" -ForegroundColor Red
    $remainingRefs | ForEach-Object { Write-Host "- $_" -ForegroundColor Yellow }
} else {
    Write-Host "\nToutes les références à 'TransactionManagement' ont été supprimées avec succès !" -ForegroundColor Green
}
