# Script de nettoyage des fichiers non essentiels pour le microservice CheckManagement
# Ce script supprime les dossiers et fichiers non nécessaires selon le template

$basePath = Split-Path -Parent $MyInvocation.MyCommand.Path

# Dossiers à conserver dans Data/Entity
$dataEntityKeep = @(
    "BaseEntity",
    "AuditLog",
    "Ping"
)

# Dossiers à conserver dans API/Controllers
$apiControllersKeep = @(
    "Base",
    "PingsController.cs"
)

# Dossiers à conserver dans MediatR
$mediatrKeep = @(
    "PipeLineBehavior",
    "Ping"
)

# Nettoyage du projet Data
$dataEntityPath = Join-Path $basePath "CBS.CheckManagement.Data\Entity"
if (Test-Path $dataEntityPath) {
    Get-ChildItem -Path $dataEntityPath -Directory | Where-Object { $_.Name -notin $dataEntityKeep } | Remove-Item -Recurse -Force -ErrorAction SilentlyContinue
    Write-Host "Nettoyage du dossier Data/Entity terminé." -ForegroundColor Green
}

# Nettoyage du projet API/Controllers
$apiControllersPath = Join-Path $basePath "CBS.CheckManagement.API\Controllers"
if (Test-Path $apiControllersPath) {
    # Supprimer tous les contrôleurs sauf ceux dans la liste de conservation
    Get-ChildItem -Path $apiControllersPath -File -Recurse | Where-Object { 
        $_.Name -notin $apiControllersKeep -and 
        -not ($_.Directory.Name -eq "Base" -and $_.Name -eq "BaseController.cs")
    } | Remove-Item -Force -ErrorAction SilentlyContinue
    
    # Supprimer les dossiers vides
    Get-ChildItem -Path $apiControllersPath -Directory | 
        Where-Object { $_.GetFiles('*.*', 'AllDirectories').Count -eq 0 } | 
        Remove-Item -Recurse -Force -ErrorAction SilentlyContinue
    
    Write-Host "Nettoyage du dossier API/Controllers terminé." -ForegroundColor Green
}

# Nettoyage du projet MediatR
$mediatrPath = Join-Path $basePath "CBS.CheckManagement.MediatR"
if (Test-Path $mediatrPath) {
    # Supprimer tous les dossiers sauf ceux dans la liste de conservation
    Get-ChildItem -Path $mediatrPath -Directory | 
        Where-Object { $_.Name -notin $mediatrKeep } | 
        Remove-Item -Recurse -Force -ErrorAction SilentlyContinue
    
    Write-Host "Nettoyage du dossier MediatR terminé." -ForegroundColor Green
}

# Nettoyage du projet Repository
$repositoryPath = Join-Path $basePath "CBS.TransactionManagement.Repository"
if (Test-Path $repositoryPath) {
    # Supprimer tous les dossiers sauf GenericRepository et UnitOfWork
    Get-ChildItem -Path $repositoryPath -Directory | 
        Where-Object { $_.Name -notin @("GenericRepository", "UnitOfWork") } | 
        Remove-Item -Recurse -Force -ErrorAction SilentlyContinue
    
    # Renommer le dossier du projet si nécessaire
    $newRepositoryPath = Join-Path $basePath "CBS.CheckManagement.Repository"
    if (-not (Test-Path $newRepositoryPath)) {
        Rename-Item -Path $repositoryPath -NewName "CBS.CheckManagement.Repository" -Force
    }
    
    Write-Host "Nettoyage du dossier Repository terminé." -ForegroundColor Green
}

# Mise à jour des références dans les fichiers .csproj
$projects = @(
    "CBS.CheckManagement.API\CBS.CheckManagement.API.csproj",
    "CBS.CheckManagement.Data\CBS.CheckManagement.Data.csproj",
    "CBS.CheckManagement.MediatR\CBS.TransactionManagement.MediatR.csproj"
)

foreach ($project in $projects) {
    $projectPath = Join-Path $basePath $project
    if (Test-Path $projectPath) {
        $content = Get-Content $projectPath -Raw
        $content = $content -replace 'TransactionManagement', 'CheckManagement'
        $content | Set-Content $projectPath -NoNewline
        Write-Host "Mise à jour des références dans $project" -ForegroundColor Cyan
    }
}

Write-Host "\nNettoyage terminé avec succès!" -ForegroundColor Green
