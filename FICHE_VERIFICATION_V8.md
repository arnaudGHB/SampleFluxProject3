# Fiche de Vérification de Conformité (v8 - Stratégie de Duplication)

Ce document sert à valider que le scaffolding d'un nouveau microservice a été réalisé en suivant correctement les étapes de la directive **`MASTER_PROMPT_V8_DUPLICATION.md`**.

## Paramètres Utilisés pour la Vérification
*   **`[SERVICE_NAME]`**: ______________
*   **`[DATABASE_NAME]`**: ______________
*   **`[PORT_NUMBER]`**: ______________
*   **`[PRIMARY_ENTITY_NAME]`**: `Ping` (par défaut pour la validation)

---

## Checklist de Vérification des Étapes

### Étape 1 : Duplication et Renommage de la Structure
- [ ] Le répertoire `CoreServices/[SERVICE_NAME]Management` a été créé.
- [ ] Le contenu de `CoreServices/TransactionManagement` y a été copié.
- [ ] Les 7 sous-dossiers de projet ont été renommés de `...TransactionManagement...` à `...[SERVICE_NAME]Management...`.
- [ ] Les 7 fichiers `.csproj` à l'intérieur de ces dossiers ont été renommés correctement.

### Étape 2 : Adaptation du Contenu des Fichiers de Projet (`.csproj`)
- [ ] Dans tous les `.csproj` du nouveau service, le texte `TransactionManagement` a été remplacé par `[SERVICE_NAME]Management`.
- [ ] Les sections `<Compile Remove="..." />` superflues ont été supprimées des fichiers `.csproj`.

### Étape 3 : Nettoyage et Simplification du Code Source
- [ ] **Contrôleurs :** Le dossier `API/Controllers/` ne contient que le sous-dossier `Base/` avec `BaseController.cs`.
- [ ] **MediatR :** Le dossier `MediatR/` ne contient que le sous-dossier `PipeLineBehavior/` avec `ValidationBehavior.cs`.
- [ ] **Entités :** Le dossier `Data/Entity/` est vide (avant la tâche de validation).
- [ ] **DTOs :** Le dossier `Data/Dto/` est vide.
- [ ] **Repositories :** Le dossier `Repository/` est vide.
- [ ] **Startup.cs :** La méthode `AddDependencyInjection` a été nettoyée et ne contient que l'enregistrement de `IUnitOfWork`.
- [ ] **appsettings.json :** La chaîne de connexion a été renommée `DefaultConnection` et utilise `[DATABASE_NAME]`. Le port Consul utilise `[PORT_NUMBER]`.
- [ ] **launchSettings.json :** L'URL de l'application utilise `[PORT_NUMBER]`.

### Étape 4 : Intégration à la Solution Globale (`.sln`)
- [ ] Un nouveau dossier de solution `[SERVICE_NAME]Management` a été ajouté au fichier `.sln`.
- [ ] Les 7 nouveaux projets `.csproj` ont été ajoutés au fichier `.sln`.
- [ ] Les 7 nouveaux projets sont correctement configurés dans la section `ProjectConfigurationPlatforms`.
- [ ] Les 7 nouveaux projets sont correctement imbriqués sous le nouveau dossier de solution dans la section `NestedProjects`.

### Étape 5 : Tâche de Validation Automatique ("Ping")
- [ ] L'entité `Ping.cs` et le DTO `PingDto.cs` ont été créés.
- [ ] Le `DbSet<Ping> Pings` a été ajouté au `[SERVICE_NAME]Context.cs`.
- [ ] Le `PingsController.cs` a été créé et contient une action `POST`.
- [ ] `IPingRepository` a été enregistré dans `DependencyInjectionExtension.cs`.
- [ ] Le mapping pour `Ping` <-> `PingDto` a été ajouté à `MapperConfig.cs`.
- [ ] **Test Final :** La commande `dotnet build CBSManagementService.sln` s'exécute sans aucune erreur.
