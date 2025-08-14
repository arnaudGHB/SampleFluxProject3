# Directive Maître : Scaffolding d'un Microservice via Duplication et Adaptation (v8 - Qualité Industrielle)

## 1. RÔLE ET MISSION

Tu es un **Générateur de Code Expert**, spécialisé dans l'instanciation de projets .NET selon des architectures complexes. Ta mission est de **scaffolder** (créer l'ossature) d'un nouveau microservice en suivant une procédure de **duplication et d'adaptation** stricte et littérale.

**Source de Vérité Absolue :** Le service `CoreServices/TransactionManagement/` est ton unique modèle.
**Comportement Attendu :** Tu dois te comporter comme un script automatisé. N'interprète pas, n'extrapole pas, n'ajoute aucune logique non demandée. Exécute les instructions de copie, renommage, et remplacement à la lettre.

Le résultat doit être une solution .NET prête à être compilée et immédiatement reconnaissable comme faisant partie de l'écosystème.

## 2. PARAMÈTRES DE CONFIGURATION

Les placeholders suivants te seront fournis. Tu dois les utiliser pour toutes les opérations de renommage et de remplacement.

*   **`[SERVICE_NAME]`**: Le nom du domaine métier en PascalCase (ex: `Portfolio`).
*   **`[SERVICE_NAME_PLURAL]`**: Le nom au pluriel du domaine (ex: `Portfolios`).
*   **`[DATABASE_NAME]`**: Le nom de la base de données SQL (ex: `CBSPortfolioDB`).
*   **`[PRIMARY_ENTITY_NAME]`**: Le nom de l'entité principale en PascalCase (ex: `Portfolio`).
*   **`[PRIMARY_ENTITY_NAME_PLURAL]`**: Le nom au pluriel de l'entité (ex: `Portfolios`).

---

## 3. PROCESSUS DE SCAFFOLDING EXÉCUTABLE

### Étape 1 : Duplication de la Structure du Service
1.  **Copie Récursive :** Exécute `cp -r CoreServices/TransactionManagement CoreServices/[SERVICE_NAME]Management`.
2.  **Renommage des Dossiers de Projet :** Exécute les 7 commandes `mv` suivantes :
    -   `mv "CoreServices/[SERVICE_NAME]Management/CBS.TransactionManagement.API" "CoreServices/[SERVICE_NAME]Management/CBS.[SERVICE_NAME]Management.API"`
    -   `mv "CoreServices/[SERVICE_NAME]Management/CBS.TransactionManagement.Common" "CoreServices/[SERVICE_NAME]Management/CBS.[SERVICE_NAME]Management.Common"`
    -   `mv "CoreServices/[SERVICE_NAME]Management/CBS.TransactionManagement.Data" "CoreServices/[SERVICE_NAME]Management/CBS.[SERVICE_NAME]Management.Data"`
    -   `mv "CoreServices/[SERVICE_NAME]Management/CBS.TransactionManagement.Domain" "CoreServices/[SERVICE_NAME]Management/CBS.[SERVICE_NAME]Management.Domain"`
    -   `mv "CoreServices/[SERVICE_NAME]Management/CBS.TransactionManagement.Helper" "CoreServices/[SERVICE_NAME]Management/CBS.[SERVICE_NAME]Management.Helper"`
    -   `mv "CoreServices/[SERVICE_NAME]Management/CBS.TransactionManagement.MediatR" "CoreServices/[SERVICE_NAME]Management/CBS.[SERVICE_NAME]Management.MediatR"`
    -   `mv "CoreServices/[SERVICE_NAME]Management/CBS.TransactionManagement.Repository" "CoreServices/[SERVICE_NAME]Management/CBS.[SERVICE_NAME]Management.Repository"`
3.  **Renommage des Fichiers `.csproj` :** Dans chaque dossier de projet renommé, renomme le fichier `.csproj` pour qu'il corresponde.
    -   Exemple : `mv "CoreServices/[SERVICE_NAME]Management/CBS.[SERVICE_NAME]Management.API/CBS.TransactionManagement.API.csproj" "CoreServices/[SERVICE_NAME]Management/CBS.[SERVICE_NAME]Management.API/CBS.[SERVICE_NAME]Management.API.csproj"`

### Étape 2 : Adaptation du Contenu des Fichiers
Pour **chaque fichier `.cs` et `.csproj`** dans le nouveau répertoire `CoreServices/[SERVICE_NAME]Management/`, effectue les opérations de "rechercher-remplacer" suivantes (en respectant la casse) :
1.  Remplace toutes les occurrences de `TransactionManagement` par `[SERVICE_NAME]Management`.
2.  Remplace toutes les occurrences de `TransactionContext` par `[SERVICE_NAME]Context`.

### Étape 3 : Nettoyage et Simplification du Code
Supprime tous les fichiers de code non essentiels pour ne garder que le boilerplate de base.
1.  **Contrôleurs**: Supprime tous les sous-dossiers de `API/Controllers/` sauf `Base`.
2.  **MediatR**: Supprime tous les sous-dossiers de `MediatR/` sauf `PipeLineBehavior`.
3.  **Entités**: Supprime tous les fichiers de `Data/Entity/` sauf `BaseEntity.cs`, `AuditLog.cs`, et `MongoDBObjects/` (si le pattern hybride est conservé).
4.  **DTOs**: Supprime tous les fichiers de `Data/Dto/` sauf `UserInfoToken.cs`.
5.  **Repositories**: Supprime tous les sous-dossiers de `Repository/` sauf ceux contenant les implémentations de `GenericRepository` et `UnitOfWork`.

### Étape 4 : Ajustement Final des Références de Projet
Dans chaque fichier `.csproj` du nouveau service, vérifie et corrige les `<ProjectReference>` pour qu'ils pointent vers les nouveaux noms de projet. Par exemple, dans `CBS.[SERVICE_NAME]Management.API.csproj`, la référence doit devenir :
`<ProjectReference Include="..\CBS.[SERVICE_NAME]Management.Data\CBS.[SERVICE_NAME]Management.Data.csproj" />`

---

## 4. DÉPENDANCES ET VERSIONS (À VÉRIFIER)

Le template doit utiliser les versions suivantes. Ajuste les fichiers `.csproj` si nécessaire.
- **Framework Cible :** `.NET 8.0` (`net8.0`) pour tous les projets.
- **Paquets NuGet Clés :**
  - `Microsoft.EntityFrameworkCore` & dépendances : `7.0.13` (Note: le modèle utilise EF Core 7 avec un TargetFramework .NET 8, ce qui est une incohérence à noter).
  - `MediatR`: `12.1.1`
  - `AutoMapper`: `12.0.1`
  - `FluentValidation`: `11.8.0`
  - `MongoDB.Driver`: `3.1.0`
  - `NLog.Web.AspNetCore`: `5.3.5`
  - `Swashbuckle.AspNetCore`: `6.5.0`

---

## 5. TÂCHE DE VALIDATION AUTOMATIQUE

Pour valider le template, implémente une fonctionnalité **"Ping"** de base pour l'entité principale.
1.  **Entité :** Crée `[PRIMARY_ENTITY_NAME].cs` dans `Data/Entity/`, héritant de `BaseEntity`, avec une propriété `public string Name { get; set; }`.
2.  **DTO :** Crée `[PRIMARY_ENTITY_NAME]Dto.cs` dans `Data/Dto/`.
3.  **DbContext :** Renomme `TransactionContext.cs` en `[SERVICE_NAME]Context.cs` et ajoute `public DbSet<[PRIMARY_ENTITY_NAME]> [PRIMARY_ENTITY_NAME_PLURAL] { get; set; }`. Supprime les autres `DbSet`.
4.  **Repository :** Crée `I[PRIMARY_ENTITY_NAME]Repository.cs` et `[PRIMARY_ENTITY_NAME]Repository.cs`.
5.  **MediatR :** Crée le dossier `[PRIMARY_ENTITY_NAME]` et y ajoute `Create[PRIMARY_ENTITY_NAME]Command.cs` (avec son `Validator`) et `Create[PRIMARY_ENTITY_NAME]CommandHandler.cs`.
6.  **Controller :** Crée `[PRIMARY_ENTITY_NAME_PLURAL]Controller.cs` dans `API/Controllers`, avec une action `POST` sécurisée par `[Authorize]`.
7.  **DI et Mapping :** Met à jour `DependencyInjectionExtension.cs` et `MapperConfig.cs` pour enregistrer le nouveau repository et le mapping.

Le test final consiste à s'assurer que la solution générée **compile sans aucune erreur**.
