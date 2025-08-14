# Script de nettoyage des fichiers non essentiels pour le microservice CheckManagement
# Ce script conserve uniquement les fichiers essentiels selon le template

$basePath = Split-Path -Parent $MyInvocation.MyCommand.Path

# Fichiers à conserver dans Data/Dto
$dataDtoKeep = @(
    "PingDto.cs",
    "PingResponseDto.cs",
    "UserInfoToken.cs",
    "DetermineTransferTypeDto.cs"
)

# Fichiers à conserver dans Helper/Model
$helperModelKeep = @(
    "AuditTrailLogger.cs",
    "EnumData.cs",
    "StringValue.cs"
)

# Fichiers à conserver dans Helper/Helper
$helperHelperKeep = @(
    "ApiResponse.cs",
    "BaseUtilities.cs",
    "Configs.cs",
    "Enums.cs",
    "ServiceResponse.cs"
)

# Nettoyage du dossier Data/Dto
$dataDtoPath = Join-Path $basePath "CBS.CheckManagement.Data\Dto"
if (Test-Path $dataDtoPath) {
    # Supprimer tous les fichiers .cs qui ne sont pas dans la liste de conservation
    Get-ChildItem -Path $dataDtoPath -File -Recurse -Filter "*.cs" | 
        Where-Object { $_.Name -notin $dataDtoKeep } | 
        Remove-Item -Force -ErrorAction SilentlyContinue
    
    # Supprimer les dossiers vides
    Get-ChildItem -Path $dataDtoPath -Directory | 
        Where-Object { $_.GetFiles('*.*', 'AllDirectories').Count -eq 0 } | 
        Remove-Item -Recurse -Force -ErrorAction SilentlyContinue
    
    Write-Host "Nettoyage du dossier Data/Dto terminé." -ForegroundColor Green
}

# Nettoyage du dossier Helper/Model
$helperModelPath = Join-Path $basePath "CBS.CheckManagement.Helper\Model"
if (Test-Path $helperModelPath) {
    # Supprimer tous les fichiers .cs qui ne sont pas dans la liste de conservation
    Get-ChildItem -Path $helperModelPath -File -Filter "*.cs" | 
        Where-Object { $_.Name -notin $helperModelKeep } | 
        Remove-Item -Force -ErrorAction SilentlyContinue
    
    Write-Host "Nettoyage du dossier Helper/Model terminé." -ForegroundColor Green
}

# Nettoyage du dossier Helper/Helper
$helperHelperPath = Join-Path $basePath "CBS.CheckManagement.Helper\Helper"
if (Test-Path $helperHelperPath) {
    # Supprimer tous les fichiers .cs qui ne sont pas dans la liste de conservation
    Get-ChildItem -Path $helperHelperPath -File -Filter "*.cs" | 
        Where-Object { $_.Name -notin $helperHelperKeep } | 
        Remove-Item -Force -ErrorAction SilentlyContinue
    
    Write-Host "Nettoyage du dossier Helper/Helper terminé." -ForegroundColor Green
}

Write-Host "\nNettoyage des fichiers non essentiels terminé avec succès!" -ForegroundColor Green
