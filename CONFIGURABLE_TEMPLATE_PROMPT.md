# Directive Maître : Génération d'un Microservice via un Template Configurable

## 1. OBJECTIF

Générer la structure complète et le code boilerplate pour un nouveau microservice, en utilisant les paramètres de configuration ci-dessous. Le résultat doit être un ensemble de 7 projets C# prêts à être compilés et intégrés dans l'écosystème `SampleFluxProject`.

---

## 2. PARAMÈTRES DE CONFIGURATION

*   **`[SERVICE_NAME]`**: Le nom du domaine métier géré par le service (ex: `Portfolio`).
*   **`[SERVICE_NAME_PLURAL]`**: Le nom au pluriel du domaine (ex: `Portfolios`).
*   **`[DATABASE_NAME]`**: Le nom de la base de données SQL pour ce service (ex: `CBSPortfolioDB`).
*   **`[PRIMARY_ENTITY_NAME]`**: Le nom de l'entité principale du service (ex: `Portfolio`).

---

## 3. STRUCTURE DE LA SOLUTION (LES 7 COUCHES)

Tu dois générer une solution (`.sln`) contenant les 7 projets C# suivants. Remplace `[SERVICE_NAME]` par sa valeur.

1.  **`CBS.[SERVICE_NAME].API`**: Projet ASP.NET Core API.
2.  **`CBS.[SERVICE_NAME].MediatR`**: Bibliothèque de classes .NET pour la logique métier.
3.  **`CBS.[SERVICE_NAME].Domain`**: Bibliothèque de classes .NET pour le DbContext et les migrations.
4.  **`CBS.[SERVICE_NAME].Data`**: Bibliothèque de classes .NET pour les Entities et DTOs.
5.  **`CBS.[SERVICE_NAME].Repository`**: Bibliothèque de classes .NET pour l'implémentation des repositories.
6.  **`CBS.[SERVICE_NAME].Common`**: Bibliothèque de classes .NET pour les interfaces de persistance.
7.  **`CBS.[SERVICE_NAME].Helper`**: Bibliothèque de classes .NET pour les utilitaires.

---

## 4. FONDATIONS ARCHITECTURALES (GÉNÉRATION DE CODE)

Génère les fichiers suivants dans les projets correspondants.

-   **Dans `.Data`**:
    -   **`BaseEntity.cs`**: Doit contenir `Id`, `CreatedBy`, `CreatedDate`, `ModifiedBy`, `LastModifiedDate`, `IsDeleted`.
    -   Crée une entité principale `public class [PRIMARY_ENTITY_NAME] : BaseEntity { ... }`.

-   **Dans `.Helper`**:
    -   **`ServiceResponse<T>`**: Implémentation standard avec des méthodes Factory (`ReturnSuccess`, `Return409`, etc.).
    -   **`APICallHelper.cs`**, **`PathHelper.cs`**, **`BaseUtilities.cs`**, **`PinSecurity.cs`**, **`PagedList.cs`**.

-   **Dans `.Common`**:
    -   **`IGenericRepository.cs`** et **`IUnitOfWork.cs`**.
    -   **Optionnel (si MongoDB est requis)**: Crée un dossier `MongoDBContext` avec `IMongoUnitOfWork.cs` et `IMongoGenericRepository.cs`.

-   **Dans `.Repository` (ou `.Common`)**:
    -   **`GenericRepository.cs`**: Doit implémenter le **soft delete**.
    -   **`UnitOfWork.cs`**: Doit surcharger `SaveChanges()` pour **peupler automatiquement les champs d'audit**.

-   **Dans `.API/Controllers/Base`**:
    -   **`BaseController.cs`**: Doit contenir la méthode `ReturnFormattedResponse`.

-   **Dans `.MediatR/PipeLineBehavior`**:
    -   **`ValidationBehavior.cs`**: Doit implémenter `IPipelineBehavior<,>` et utiliser `FluentValidation`.

---

## 5. CONFIGURATION `Startup.cs` (API)

Génère un `Startup.cs` configuré pour :
1.  **DI Container**: Enregistrer le `[SERVICE_NAME]Context`, MediatR, AutoMapper, FluentValidation. Utilise une méthode d'extension `services.AddDependencyInjection()`.
2.  **Base de Données**: Configurer la connexion à la base de données `[DATABASE_NAME]`.
3.  **JWT Bearer Auth**: Configurer l'authentification JWT.
4.  **Pipeline de Middlewares (Ordre Critique)**: `UseRouting`, `UseAuthentication`, `UseMiddleware<JWTMiddleware>`, `UseMiddleware<AuditLogMiddleware>`, `UseAuthorization`, `UseEndpoints`.

---

## 6. TÂCHE DE VALIDATION AUTOMATIQUE

Pour valider la génération, crée une fonctionnalité `[PRIMARY_ENTITY_NAME]` de base :
1.  Ajoute les `DbSet` nécessaires au `[SERVICE_NAME]Context`.
2.  Crée un `Add[PRIMARY_ENTITY_NAME]Command` et son `Add[PRIMARY_ENTITY_NAME]CommandHandler`.
3.  Crée un `[SERVICE_NAME_PLURAL]Controller` avec une action `POST` sécurisée par `[Authorize]` qui déclenche la commande.
4.  Le test final consiste à appeler cet endpoint avec un token JWT valide et à vérifier que l'entité est créée dans la base de données avec les champs d'audit (`CreatedBy`, `CreatedDate`) correctement renseignés.
