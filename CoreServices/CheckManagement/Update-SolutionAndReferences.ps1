# Script pour mettre à jour les références et ajouter les projets à la solution

$basePath = Split-Path -Parent $MyInvocation.MyCommand.Path
$solutionPath = Join-Path $basePath "CheckManagement.sln"

# Liste des projets à ajouter à la solution
$projects = @(
    "CBS.CheckManagement.API\CBS.CheckManagement.API.csproj",
    "CBS.CheckManagement.Common\CBS.CheckManagement.Common.csproj",
    "CBS.CheckManagement.Data\CBS.CheckManagement.Data.csproj",
    "CBS.CheckManagement.Domain\CBS.CheckManagement.Domain.csproj",
    "CBS.CheckManagement.Helper\CBS.CheckManagement.Helper.csproj",
    "CBS.CheckManagement.MediatR\CBS.CheckManagement.MediatR.csproj",
    "CBS.CheckManagement.Repository\CBS.CheckManagement.Repository.csproj"
)

# 1. Mettre à jour les références dans les fichiers .csproj
Write-Host "Mise à jour des références dans les fichiers .csproj..." -ForegroundColor Cyan

foreach ($project in $projects) {
    $projectPath = Join-Path $basePath $project
    if (Test-Path $projectPath) {
        $content = Get-Content $projectPath -Raw
        $content = $content -replace 'TransactionManagement', 'CheckManagement'
        $content | Set-Content $projectPath -NoNewline
        Write-Host "Références mises à jour dans $project" -ForegroundColor Green
    } else {
        Write-Host "Fichier introuvable : $projectPath" -ForegroundColor Yellow
    }
}

# 2. Ajouter les projets à la solution
Write-Host "`nAjout des projets à la solution..." -ForegroundColor Cyan

foreach ($project in $projects) {
    $projectPath = Join-Path $basePath $project
    if (Test-Path $projectPath) {
        dotnet sln $solutionPath add $projectPath
        Write-Host "Projet ajouté à la solution : $project" -ForegroundColor Green
    }
}

Write-Host "`nMise à jour de la solution et des références terminée avec succès !" -ForegroundColor Green
