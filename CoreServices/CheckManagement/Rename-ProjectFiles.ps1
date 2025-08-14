# Script pour renommer les fichiers de projet et mettre à jour les références

$basePath = Split-Path -Parent $MyInvocation.MyCommand.Path

# 1. Renommer le fichier de projet MediatR
$mediatrOldPath = Join-Path $basePath "CBS.CheckManagement.MediatR\CBS.TransactionManagement.MediatR.csproj"
$mediatrNewPath = Join-Path $basePath "CBS.CheckManagement.MediatR\CBS.CheckManagement.MediatR.csproj"

if (Test-Path $mediatrOldPath -and -not (Test-Path $mediatrNewPath)) {
    Rename-Item -Path $mediatrOldPath -NewName "CBS.CheckManagement.MediatR.csproj" -Force
    Write-Host "Fichier MediatR renommé avec succès." -ForegroundColor Green
}

# 2. Renommer le fichier de projet Repository
$repoOldPath = Join-Path $basePath "CBS.CheckManagement.Repository\CBS.TransactionManagement.Repository.csproj"
$repoNewPath = Join-Path $basePath "CBS.CheckManagement.Repository\CBS.CheckManagement.Repository.csproj"

if (Test-Path $repoOldPath -and -not (Test-Path $repoNewPath)) {
    Rename-Item -Path $repoOldPath -NewName "CBS.CheckManagement.Repository.csproj" -Force
    Write-Host "Fichier Repository renommé avec succès." -ForegroundColor Green
}

# 3. Mettre à jour les références dans les fichiers .csproj
$projects = @(
    "CBS.CheckManagement.API\CBS.CheckManagement.API.csproj",
    "CBS.CheckManagement.Common\CBS.TransactionManagement.Common.csproj",
    "CBS.CheckManagement.Repository\CBS.CheckManagement.Repository.csproj",
    "CBS.CheckManagement.MediatR\CBS.CheckManagement.MediatR.csproj"
)

foreach ($project in $projects) {
    $projectPath = Join-Path $basePath $project
    if (Test-Path $projectPath) {
        $content = Get-Content $projectPath -Raw
        $content = $content -replace 'TransactionManagement', 'CheckManagement'
        $content | Set-Content $projectPath -NoNewline
        Write-Host "Références mises à jour dans $project" -ForegroundColor Cyan
    }
}

Write-Host "\nMise à jour des noms de fichiers et références terminée avec succès!" -ForegroundColor Green
