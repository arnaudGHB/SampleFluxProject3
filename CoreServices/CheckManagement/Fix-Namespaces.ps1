# Script pour corriger les namespaces et usings dans tous les fichiers .cs

$basePath = Split-Path -Parent $MyInvocation.MyCommand.Path
$files = Get-ChildItem -Path $basePath -Include *.cs -Recurse -File

foreach ($file in $files) {
    $content = Get-Content $file.FullName -Raw
    
    # Sauvegarder le contenu original pour la comparaison
    $originalContent = $content
    
    # Remplacer les namespaces et usings
    $content = $content -replace 'CBS\.TransactionManagement\.Helper', 'CBS.CheckManagement.Helper'
    $content = $content -replace 'CBS\.TransactionManagement\.Domain', 'CBS.CheckManagement.Domain'
    $content = $content -replace 'CBS\.TransactionManagement\.Data', 'CBS.CheckManagement.Data'
    $content = $content -replace 'CBS\.TransactionManagement\.MediatR', 'CBS.CheckManagement.MediatR'
    $content = $content -replace 'CBS\.TransactionManagement\.Repository', 'CBS.CheckManagement.Repository'
    $content = $content -replace 'CBS\.TransactionManagement\.API', 'CBS.CheckManagement.API'
    $content = $content -replace 'CBS\.TransactionManagement\.Common', 'CBS.CheckManagement.Common'
    
    # Si le contenu a changé, mettre à jour le fichier
    if ($content -ne $originalContent) {
        Set-Content -Path $file.FullName -Value $content -NoNewline
        Write-Host "Fichier mis à jour : $($file.FullName)" -ForegroundColor Green
    }
}

Write-Host "\nMise à jour des namespaces et usings terminée avec succès !" -ForegroundColor Green
