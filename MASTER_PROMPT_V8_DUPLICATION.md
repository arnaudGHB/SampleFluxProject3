# Directive Maître : Scaffolding d'un Microservice par Duplication (v8 - Corrigé et Finalisé)

## 1. OBJECTIF
Ta mission est de **scaffolder** un nouveau microservice en suivant une procédure de **duplication et d'adaptation** stricte. Tu dois te comporter comme un script automatisé de haute précision.

## 2. SOURCE DE VÉRITÉ
Le service de référence pour toute duplication est **`CoreServices/TransactionManagement/`**.

## 3. PARAMÈTRES DE CONFIGURATION
*   **`[SERVICE_NAME]`**: Le nom du domaine métier (ex: `Portfolio`).
*   **`[DATABASE_NAME]`**: Le nom de la base de données SQL (ex: `CBSPortfolioDB`).
*   **`[PORT_NUMBER]`**: Le port HTTP pour le service (ex: `7114`).
*   **`[PRIMARY_ENTITY_NAME]`**: Le nom de l'entité de validation (ex: `Ping`).
*   **`[PRIMARY_ENTITY_NAME_PLURAL]`**: Le nom au pluriel de l'entité (ex: `Pings`).

---

## 4. PROCESSUS DE SCAFFOLDING EXÉCUTABLE

### Étape 1 : Duplication et Renommage de la Structure
1.  **Copie Récursive :** Exécute `cp -r CoreServices/TransactionManagement CoreServices/[SERVICE_NAME]Management`.
2.  **Renommage des Dossiers de Projet :** Pour chaque sous-dossier dans `CoreServices/[SERVICE_NAME]Management/` qui commence par `CBS.TransactionManagement`, remplace `TransactionManagement` par `[SERVICE_NAME]Management`.
    *   Exemple : `mv CoreServices/[SERVICE_NAME]Management/CBS.TransactionManagement.API CoreServices/[SERVICE_NAME]Management/CBS.[SERVICE_NAME]Management.API`
    *   Répète pour les 7 projets (`.API`, `.Common`, `.Data`, `.Domain`, `.Helper`, `.MediatR`, `.Repository`).
3.  **Renommage des Fichiers `.csproj` :** Dans chaque dossier de projet renommé, renomme le fichier `.csproj` pour qu'il corresponde au nouveau nom du projet.
    *   Exemple : `mv CBS.[SERVICE_NAME]Management.API/CBS.TransactionManagement.API.csproj CBS.[SERVICE_NAME]Management.API/CBS.[SERVICE_NAME]Management.API.csproj`

### Étape 2 : Adaptation du Contenu des Fichiers de Projet
Pour chaque fichier `*.csproj` dans le nouveau service, effectue les actions suivantes :
1.  **Rechercher-Remplacer :** Remplace toutes les occurrences de `TransactionManagement` par `[SERVICE_NAME]Management`. Cela mettra à jour les `<ProjectReference>`.
2.  **Nettoyage des Références Inutiles :** Ouvre chaque `.csproj` et supprime toutes les lignes `<Compile Remove="..." />` qui font référence à des fichiers de l'ancien service.

### Étape 3 : Nettoyage et Simplification du Code Source
1.  **Suppression Ciblée :** Supprime tous les sous-dossiers dans les répertoires suivants, **SAUF** les dossiers de boilerplate spécifiés :
    *   Dans `...API/Controllers/`, supprime tout sauf le dossier `Base`.
    *   Dans `...API/Helpers/`, supprime tout sauf les dossiers `DependencyResolver` et `MapperConfiguation`, et les fichiers `ArrayModelBinder.cs`, `UnprocessableEntityObjectResult.cs`.
    *   Dans `...Data/Dto/`, supprime tout.
    *   Dans `...Data/Entity/`, supprime tout.
    *   Dans `...MediatR/`, supprime tout sauf le dossier `PipeLineBehavior`.
    *   Dans `...Repository/`, supprime tout.
2.  **Adaptation des Fichiers Restants :** Dans tous les fichiers `.cs` restants, effectue les opérations de "rechercher-remplacer" suivantes :
    *   `TransactionManagement` -> `[SERVICE_NAME]Management`
    *   `TransactionContext` -> `[SERVICE_NAME]Context`
3.  **Simplification de `Startup.cs` :** Ouvre `...API/Startup.cs`. Dans la méthode `AddDependencyInjection`, supprime toutes les lignes `services.AddScoped<...>()` **sauf** la ligne `services.AddScoped(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));`.
4.  **Configuration `appsettings.json` :** Ouvre `...API/appsettings.json`. Modifie la clé `Conn:POSDbConnectionString` en `Conn:DefaultConnection` et utilise `[DATABASE_NAME]` comme nom de base de données. Modifie `ConsulConfig:servicePort` pour utiliser `[PORT_NUMBER]`.
5.  **Configuration `launchSettings.json` :** Ouvre `...API/Properties/launchSettings.json`. Modifie `profiles:http:applicationUrl` pour utiliser `[PORT_NUMBER]`.

### Étape 4 : Intégration à la Solution Globale
1.  **Lecture du Fichier `.sln` :** Lis le contenu de `CBSManagementService.sln`.
2.  **Création des GUIDs :** "Génère" 8 nouveaux GUIDs uniques en prenant les GUIDs existants de `TransactionManagement` et en changeant le dernier caractère.
3.  **Ajout des Projets :** Ajoute un nouveau `Project` de type "Solution Folder" pour `[SERVICE_NAME]Management` et les 7 `Project` pour les `.csproj` du nouveau service.
4.  **Configuration du Build :** Ajoute les 7 nouveaux projets à la section `GlobalSection(ProjectConfigurationPlatforms)`.
5.  **Configuration de l'Arborescence :** Ajoute les 7 nouveaux projets à la section `GlobalSection(NestedProjects)` pour les lier au nouveau dossier de solution.

---

## 5. TÂCHE DE VALIDATION AUTOMATIQUE
1.  **Entité & DTO**: Crée `Ping.cs` dans `.Data/Entity` et `PingDto.cs` dans `.Data/Dto`.
    ```csharp
    // Dans Ping.cs
    namespace CBS.[SERVICE_NAME]Management.Data.Entity {
        public class Ping : BaseEntity { public int Id { get; set; } public string Message { get; set; } }
    }
    ```
2.  **DbContext**: Ajoute `public DbSet<Ping> Pings { get; set; }` au `[SERVICE_NAME]Context.cs`.
3.  **MediatR**: Crée `AddPingCommand.cs` et `AddPingCommandHandler.cs`.
    ```csharp
    // Dans AddPingCommandHandler.cs
    // ...
    var entity = new Ping { Message = request.Message };
    _repository.Add(entity);
    await _uow.SaveAsync(_userInfoToken);
    // ...
    ```
4.  **Contrôleur**: Crée `PingsController.cs` avec une action `POST`.
5.  **DI & Mapping**: Met à jour `DependencyInjectionExtension.cs` pour enregistrer `IPingRepository` et `MapperConfig.cs` pour mapper `Ping` et `PingDto`.
6.  **Compilation Finale**: Exécute `dotnet build CBSManagementService.sln`. La compilation doit réussir.
