# Script de correction de structure pour CheckManagement

# 1. Renommer les fichiers .csproj
Rename-Item -Path "CBS.CheckManagement.Common\CBS.TransactionManagement.Common.csproj" -NewName "CBS.CheckManagement.Common.csproj"
Rename-Item -Path "CBS.CheckManagement.Domain\CBS.TransactionManagement.Domain.csproj" -NewName "CBS.CheckManagement.Domain.csproj"
Rename-Item -Path "CBS.CheckManagement.Helper\CBS.TransactionManagement.Helper.csproj" -NewName "CBS.CheckManagement.Helper.csproj"

# 2. Créer la structure de dossiers pour MediatR
$mediatrBase = "CBS.CheckManagement.MediatR\Features"
New-Item -Path "$mediatrBase\Ping\Commands" -ItemType Directory -Force
New-Item -Path "$mediatrBase\Ping\Queries" -ItemType Directory -Force
New-Item -Path "$mediatrBase\Ping\Handlers" -ItemType Directory -Force

# 3. Déplacer les fichiers MediatR
Move-Item -Path "CBS.CheckManagement.MediatR\Commands\Ping\*" -Destination "$mediatrBase\Ping\Commands\" -Force -ErrorAction SilentlyContinue
Move-Item -Path "CBS.CheckManagement.MediatR\Queries\Ping\*" -Destination "$mediatrBase\Ping\Queries\" -Force -ErrorAction SilentlyContinue
Move-Item -Path "CBS.CheckManagement.MediatR\Handlers\Ping\*" -Destination "$mediatrBase\Ping\Handlers\" -Force -ErrorAction SilentlyContinue

# 4. Nettoyer les dossiers vides
Remove-Item -Path "CBS.CheckManagement.MediatR\Commands" -Recurse -Force -ErrorAction SilentlyContinue
Remove-Item -Path "CBS.CheckManagement.MediatR\Queries" -Recurse -Force -ErrorAction SilentlyContinue
Remove-Item -Path "CBS.CheckManagement.MediatR\Handlers" -Recurse -Force -ErrorAction SilentlyContinue

# 5. Déplacer le DbContext vers Domain
if (Test-Path "CBS.CheckManagement.Data\UserSkillContext.cs") {
    Move-Item -Path "CBS.CheckManagement.Data\UserSkillContext.cs" -Destination "CBS.CheckManagement.Domain\CheckManagementContext.cs" -Force
}

if (Test-Path "CBS.CheckManagement.Data\Migrations") {
    Move-Item -Path "CBS.CheckManagement.Data\Migrations" -Destination "CBS.CheckManagement.Domain\" -Force
}

# 6. Supprimer les entités redondantes
if (Test-Path "CBS.CheckManagement.Domain\Entities") {
    Remove-Item -Path "CBS.CheckManagement.Domain\Entities" -Recurse -Force
}

# 7. Mettre à jour le contenu du DbContext
if (Test-Path "CBS.CheckManagement.Domain\CheckManagementContext.cs") {
    (Get-Content "CBS.CheckManagement.Domain\CheckManagementContext.cs") | 
        ForEach-Object { $_ -replace "UserSkillContext", "CheckManagementContext" } |
        ForEach-Object { $_ -replace "CBS.CheckManagement.Data", "CBS.CheckManagement.Domain" } |
        Set-Content "CBS.CheckManagement.Domain\CheckManagementContext.cs"
}

# 8. Mettre à jour les références dans les projets
$projects = @(
    "CBS.CheckManagement.API\CBS.CheckManagement.API.csproj",
    "CBS.CheckManagement.Data\CBS.CheckManagement.Data.csproj",
    "CBS.CheckManagement.MediatR\CBS.CheckManagement.MediatR.csproj",
    "CBS.CheckManagement.Repository\CBS.CheckManagement.Repository.csproj"
)

foreach ($project in $projects) {
    if (Test-Path $project) {
        $content = Get-Content $project -Raw
        $content = $content -replace "CBS.TransactionManagement", "CBS.CheckManagement"
        $content | Set-Content $project -NoNewline
    }
}

# 9. Mettre à jour les références dans les fichiers .cs
$csFiles = Get-ChildItem -Path . -Recurse -Filter "*.cs" -File | Where-Object { $_.FullName -notlike '*\obj\*' -and $_.FullName -notlike '*\bin\*' }

foreach ($file in $csFiles) {
    try {
        $content = Get-Content $file.FullName -Raw -ErrorAction Stop
        $newContent = $content -replace "CBS.TransactionManagement", "CBS.CheckManagement"
        
        if ($newContent -ne $content) {
            $newContent | Set-Content $file.FullName -NoNewline -Encoding UTF8 -Force
            Write-Host "Mis à jour : $($file.FullName)" -ForegroundColor Cyan
        }
    }
    catch {
        Write-Host "Erreur lors de la lecture/écriture de $($file.FullName): $_" -ForegroundColor Red
    }
}

Write-Host "Structure corrigée avec succès!" -ForegroundColor Green
