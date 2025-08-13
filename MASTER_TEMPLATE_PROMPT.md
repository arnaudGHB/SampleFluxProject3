# Directive Maître : Scaffolding d'un Microservice par Duplication (v7 - Master Final)

## 1. OBJECTIF

Ta mission est de **scaffolder** un nouveau microservice en suivant une procédure de **duplication et d'adaptation** stricte. Tu ne dois **rien générer ou créer à partir de zéro**, sauf si explicitement demandé dans la tâche de validation. Tu dois te comporter comme un script automatisé.

## 2. SOURCE DE VÉRITÉ

Le service de référence pour toute duplication est **`CoreServices/TransactionManagement/`**.

## 3. PARAMÈTRES DE CONFIGURATION

*   **`[SERVICE_NAME]`**: Le nom du domaine métier (ex: `Portfolio`).
*   **`[SERVICE_NAME_PLURAL]`**: Le nom au pluriel du domaine (ex: `Portfolios`).
*   **`[DATABASE_NAME]`**: Le nom de la base de données SQL (ex: `CBSPortfolioDB`).
*   **`[PRIMARY_ENTITY_NAME]`**: Le nom de l'entité principale (ex: `Portfolio`).
*   **`[PRIMARY_ENTITY_NAME_PLURAL]`**: Le nom au pluriel de l'entité (ex: `Portfolios`).

---

## 4. PROCESSUS DE SCAFFOLDING EXÉCUTABLE

### Étape 1 : Duplication de la Structure du Service
1.  Exécute la commande de copie récursive : `cp -r CoreServices/TransactionManagement CoreServices/[SERVICE_NAME]Management`
2.  Renomme le répertoire principal du projet API : `mv CoreServices/[SERVICE_NAME]Management/CBS.TransactionManagement.API CoreServices/[SERVICE_NAME]Management/CBS.[SERVICE_NAME]Management.API`
3.  Renomme les 6 autres répertoires de projet de la même manière (`.Common`, `.Data`, `.Domain`, `.Helper`, `.MediatR`, `.Repository`).
4.  Dans chaque répertoire de projet renommé, renomme le fichier `.csproj` correspondant. (ex: `mv CBS.TransactionManagement.API.csproj CBS.[SERVICE_NAME]Management.API.csproj`).

### Étape 2 : Adaptation du Contenu des Fichiers
Pour chaque fichier `.cs` et `.csproj` dans le répertoire `CoreServices/[SERVICE_NAME]Management/`, effectue les opérations de "rechercher-remplacer" suivantes (en respectant la casse) :

1.  `TransactionManagement` -> `[SERVICE_NAME]Management`
2.  `TransactionContext` -> `[SERVICE_NAME]Context`
3.  `Transaction` -> `[PRIMARY_ENTITY_NAME]` (Attention : Remplacer uniquement les instances qui font référence à l'entité `Transaction`, pas le mot commun).
4.  `Transactions` -> `[PRIMARY_ENTITY_NAME_PLURAL]`

### Étape 3 : Nettoyage et Simplification du Code
Supprime tous les fichiers de code non essentiels pour ne garder que le boilerplate de base. Exécute les commandes de suppression suivantes :

1.  **Contrôleurs**: `rm -r CoreServices/[SERVICE_NAME]Management/CBS.[SERVICE_NAME]Management.API/Controllers/*`, puis recrée le dossier `Base` et le fichier `BaseController.cs` en copiant l'original.
2.  **MediatR**: `rm -r CoreServices/[SERVICE_NAME]Management/CBS.[SERVICE_NAME]Management.MediatR/*`, puis recrée le dossier `PipeLineBehavior` et le fichier `ValidationBehavior.cs`.
3.  **Entités**: `rm CoreServices/[SERVICE_NAME]Management/CBS.[SERVICE_NAME]Management.Data/Entity/*`, puis recrée les fichiers `BaseEntity.cs` et `AuditLog.cs`.
4.  **DTOs**: `rm CoreServices/[SERVICE_NAME]Management/CBS.[SERVICE_NAME]Management.Data/Dto/*`, puis recrée le fichier `UserInfoToken.cs`.
5.  **Repositories**: `rm CoreServices/[SERVICE_NAME]Management/CBS.[SERVICE_NAME]Management.Repository/*`, puis recrée les implémentations de `GenericRepository.cs` et `UnitOfWork.cs` et leurs interfaces dans `.Common`.

### Étape 4 : Ajustement Final des Références de Projet
Dans chaque fichier `*.csproj` du nouveau service, vérifie et corrige les `<ProjectReference>` pour qu'ils pointent vers les nouveaux noms de projet (ex: `<ProjectReference Include="..\CBS.[SERVICE_NAME]Management.Data\CBS.[SERVICE_NAME]Management.Data.csproj" />`).

---

## 5. TÂCHE DE VALIDATION AUTOMATIQUE

Pour valider le template, implémente une fonctionnalité "Ping" de base :
1.  **Entité**: Crée le fichier `Ping.cs` dans `.Data/Entity` avec `public class Ping : BaseEntity { public string Message { get; set; } }`.
2.  **DbContext**: Ajoute `public DbSet<Ping> Pings { get; set; }` au `[SERVICE_NAME]Context.cs`.
3.  **MediatR**: Crée le dossier `Ping` dans `.MediatR` et y ajoute `AddPingCommand.cs` et `AddPingCommandHandler.cs`. Le handler doit utiliser l'Unit of Work pour sauvegarder l'entité.
4.  **Contrôleur**: Crée `PingsController.cs` dans `.API/Controllers`, héritant de `BaseController`, avec une action `POST` sécurisée par `[Authorize]` qui déclenche la commande.
5.  Le test final est de s'assurer que la solution compile sans erreur. L'appel à l'endpoint doit (théoriquement) fonctionner et créer une entrée en base de données avec les champs d'audit corrects.
