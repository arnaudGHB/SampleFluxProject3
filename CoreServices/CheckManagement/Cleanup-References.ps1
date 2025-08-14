# Script de nettoyage des références inutilisées

# 1. Nettoyage des packages NuGet inutilisés
$projects = Get-ChildItem -Path . -Filter "*.csproj" -Recurse | Select-Object -ExpandProperty FullName

foreach ($project in $projects) {
    Write-Host "Nettoyage des packages inutilisés dans $project" -ForegroundColor Cyan
    
    # Liste des packages utilisés
    $usedPackages = @()
    $projectDir = Split-Path $project -Parent
    $csFiles = Get-ChildItem -Path $projectDir -Filter "*.cs" -Recurse | Select-Object -ExpandProperty FullName
    
    foreach ($file in $csFiles) {
        try {
            $content = Get-Content $file -Raw -ErrorAction Stop
            $matches = [regex]::Matches($content, "using\s+([^\s;]+);")
            foreach ($match in $matches) {
                $usedPackages += $match.Groups[1].Value
            }
        } catch {
            Write-Host "  - Erreur lors de la lecture de $file" -ForegroundColor Red
        }
    }
    
    # Vérification des packages référencés
    try {
        $projectContent = Get-Content $project -Raw -ErrorAction Stop
        $packageRefs = [regex]::Matches($projectContent, "<PackageReference\s+Include=\"([^\"]+)\"\s+Version=\"([^\"]+)\"\s*/>")
        
        foreach ($packageRef in $packageRefs) {
            $packageName = $packageRef.Groups[1].Value
            $isUsed = $false
            
            foreach ($used in $usedPackages) {
                if ($used -like "*$($packageName.Split('.')[-1])*") {
                    $isUsed = $true
                    break
                }
            }
            
            if (-not $isUsed) {
                Write-Host "  - Suppression du package inutilisé: $packageName" -ForegroundColor Yellow
                $projectContent = $projectContent -replace [regex]::Escape($packageRef.Value), ""
            }
        }
        
        # Sauvegarder les modifications
        $projectContent | Set-Content $project -NoNewline -Encoding UTF8 -Force
    } catch {
        Write-Host "  - Erreur lors du traitement de $project" -ForegroundColor Red
    }
}

# 2. Nettoyage des références de projet inutiles
foreach ($project in $projects) {
    Write-Host "Vérification des références de projet dans $project" -ForegroundColor Cyan
    
    try {
        $projectContent = Get-Content $project -Raw -ErrorAction Stop
        $projectDir = Split-Path $project -Parent
        
        $projectRefs = [regex]::Matches($projectContent, "<ProjectReference\s+Include=\"([^\"]+)\"\s*/>")
        
        foreach ($ref in $projectRefs) {
            $refPath = [System.IO.Path]::Combine($projectDir, $ref.Groups[1].Value)
            $refProjectName = [System.IO.Path]::GetFileNameWithoutExtension($refPath)
            
            $isUsed = $false
            $csFiles = Get-ChildItem -Path $projectDir -Filter "*.cs" -Recurse | Select-Object -ExpandProperty FullName
            
            foreach ($file in $csFiles) {
                try {
                    $content = Get-Content $file -Raw -ErrorAction Stop
                    if ($content -match $refProjectName) {
                        $isUsed = $true
                        break
                    }
                } catch {
                    Write-Host "    - Erreur lors de la lecture de $file" -ForegroundColor Red
                }
            }
            
            if (-not $isUsed) {
                Write-Host "  - Suppression de la référence inutilisée: $refProjectName" -ForegroundColor Yellow
                $projectContent = $projectContent -replace [regex]::Escape($ref.Value), ""
            }
        }
        
        # Sauvegarder les modifications
        $projectContent | Set-Content $project -NoNewline -Encoding UTF8 -Force
    } catch {
        Write-Host "  - Erreur lors du traitement de $project" -ForegroundColor Red
    }
}

# 3. Nettoyage des using inutiles
Get-ChildItem -Path . -Filter "*.cs" -Recurse | ForEach-Object {
    $file = $_.FullName
    try {
        $content = Get-Content $file -Raw -ErrorAction Stop
        
        # Supprimer les lignes using vides
        $newContent = $content -replace "(?m)^\s*using\s+[^;]+;\s*$(\r?\n)(?=\s*using\s+[^;]+;)", ""
        
        if ($newContent -ne $content) {
            Write-Host "Nettoyage des using inutiles dans $file" -ForegroundColor Cyan
            $newContent | Set-Content $file -NoNewline -Encoding UTF8 -Force
        }
    } catch {
        Write-Host "  - Erreur lors du traitement de $file" -ForegroundColor Red
    }
}

Write-Host "Nettoyage terminé avec succès!" -ForegroundColor Green
