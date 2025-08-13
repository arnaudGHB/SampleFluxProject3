# Directive Maître : Création d'un Template de Microservice pour l'Écosystème `SampleFluxProject` (v2 - Révisé)

## 1. OBJECTIF

Créer un template de projet Visual Studio (`dotnet new`) pour un nouveau microservice qui s'intègre de manière transparente et cohérente dans l'architecture existante de `SampleFluxProject`. Ce template doit incarner **tous** les patterns, conventions et structures standards observés dans l'écosystème.

## 2. STRUCTURE DE LA SOLUTION (LES 7 COUCHES)

Votre solution (`.sln`) doit contenir les 7 projets C# suivants (`CBS.{NomDuService}.{Couche}`) :
1.  **`.API`**: Point d'entrée ASP.NET Core (Controllers, `Startup.cs`, Middlewares).
2.  **`.MediatR`**: Logique métier (Commands, Queries, Handlers, `ValidationBehavior`).
3.  **`.Domain`**: Contexte de la base de données (`DbContext`, Migrations).
4.  **`.Data`**: Modèles de données (`Entities` héritant de `BaseEntity`, `DTOs`).
5.  **`.Repository`**: Implémentations concrètes des interfaces de repository.
6.  **`.Common`**: Interfaces de persistance (`IGenericRepository`, `IUnitOfWork`, et optionnellement pour MongoDB).
7.  **`.Helper`**: Librairie d'utilitaires standards.

## 3. FONDATIONS ARCHITECTURALES (CODE BOILERPLATE REQUIS)

Les fichiers suivants sont **obligatoires** et constituent le "génome" d'un service.

-   **`BaseEntity.cs` (dans `.Data`)**:
    -   Doit contenir les champs d'audit : `Id`, `CreatedBy`, `CreatedDate`, `ModifiedBy`, `LastModifiedDate`, `IsDeleted`.

-   **`ServiceResponse<T>` (dans `.Helper`)**:
    -   Copie de l'implémentation existante pour standardiser les réponses d'API.

-   **`BaseController.cs` (dans `.API/Controllers/Base`)**:
    -   Doit hériter de `ControllerBase` et fournir la méthode `ReturnFormattedResponse(ServiceResponse<T> result)`.

-   **`ValidationBehavior.cs` (dans `.MediatR/PipeLineBehavior`)**:
    -   Doit implémenter `IPipelineBehavior<TRequest, TResponse>` pour intégrer `FluentValidation` dans le pipeline MediatR.

### 3.1. Couche de Persistance SQL (Obligatoire)

-   **`IGenericRepository.cs` et `IUnitOfWork.cs` (dans `.Common`)**: Définition des interfaces.
-   **`GenericRepository.cs` et `UnitOfWork.cs` (dans `.{CoucheDeVotreChoix}, typiquement .Common ou .Repository`)**:
    -   `GenericRepository` **doit** implémenter le **soft delete**.
    -   `UnitOfWork` **doit** surcharger `SaveChanges()` pour **peupler automatiquement les champs d'audit** de `BaseEntity`.

### 3.2. Gestion de la Persistance Hybride (Optionnel mais Standard)

-   Si le service nécessite une persistance NoSQL, le pattern suivant doit être appliqué :
-   **`IMongoUnitOfWork.cs` et `IMongoGenericRepository.cs` (dans `.Common/MongoDBContext`)**: Créer un sous-dossier pour les interfaces MongoDB.
-   **Implémentations (dans `.Common` ou `.Repository`)**: Fournir les implémentations concrètes de ces interfaces.

## 4. PROJET HELPER (`*.Helper`)

Le projet `.Helper` n'est pas minimaliste. Il doit contenir une copie des classes utilitaires standards suivantes, observées dans les services matures :
-   `APICallHelper.cs`: Pour la communication inter-services.
-   `PathHelper.cs`: Pour centraliser la gestion des URLs depuis `appsettings.json`.
-   `BaseUtilities.cs`: Fonctions communes (génération d'ID, conversion de dates, etc.).
-   `PinSecurity.cs`: Logique de hashage et de validation des PINs.
-   `PagedList.cs` & `ResourceParameters.cs`: Utilitaires pour la pagination.
-   `Enums.cs`: Énumérations communes à travers l'application.
-   `EmailHelper.cs` / `SmsHelper.cs`: Si le service doit envoyer des communications.

## 5. CONFIGURATION `Startup.cs` (API)

Le `Startup.cs` doit être configuré pour :
1.  **DI Container**: Enregistrer le `DbContext`, MediatR, AutoMapper, FluentValidation, et les repositories via une méthode d'extension `services.AddDependencyInjection()`.
2.  **JWT Bearer Auth**: Configurer l'authentification JWT en lisant les `JwtSettings`.
3.  **Pipeline de Middlewares (Ordre Critique)**:
    1.  `UseRouting`
    2.  `UseAuthentication`
    3.  `UseMiddleware<JWTMiddleware>` (Middleware personnalisé qui lit le token et peuple `UserInfoToken`)
    4.  `UseMiddleware<AuditLogMiddleware>`
    5.  `UseAuthorization`
    6.  `UseEndpoints`

## 6. EXEMPLE DE TÂCHE DE VALIDATION

Pour valider que le template est correctement implémenté, créez une fonctionnalité "Ping" :
1.  Créez une entité `Ping` héritant de `BaseEntity`.
2.  Créez un `AddPingCommand` et son `AddPingCommandHandler`.
3.  Le handler doit simplement sauvegarder une nouvelle entité `Ping`.
4.  Créez un `PingController` avec une action `POST` sécurisée par `[Authorize]`.
5.  Testez l'endpoint avec un token JWT valide. Le test est réussi si une nouvelle ligne est ajoutée à la table `Pings` avec les champs `CreatedBy` et `CreatedDate` automatiquement et correctement renseignés.
