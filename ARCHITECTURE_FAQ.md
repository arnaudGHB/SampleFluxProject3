# Réponses aux Questions sur l'Architecture

Voici les réponses détaillées à vos questions, basées sur l'analyse complète de l'architecture.

### 1. Authentification des requêtes : Comment les microservices authentifient les requêtes ?
L'authentification est centralisée au niveau de l'**ApiGateway**, qui valide un **JSON Web Token (JWT)** fourni dans l'en-tête `Authorization` de chaque requête entrante. Pour la communication inter-services, le service appelant propage ce même JWT, assurant que le contexte de sécurité est maintenu de bout en bout.

### 2. Sécurité de l’architecture : Quel genre de sécurité est implémenté au niveau des micro services ?
L'architecture implémente une sécurité à plusieurs niveaux :
- **Authentification par Token JWT** : Chaque requête doit avoir un token valide.
- **Autorisation par Rôles/Claims** : L'ApiGateway peut restreindre l'accès à certaines routes en fonction des "claims" (comme le rôle) contenus dans le JWT.
- **Limitation de Débit (Rate Limiting)** : L'ApiGateway protège tous les services contre les attaques par déni de service en limitant le nombre de requêtes par minute.
- **Communication via HTTPS** : Le trafic entre le client, l'ApiGateway et les services internes est configuré pour utiliser HTTPS, chiffrant ainsi les données en transit.

### 3. Communication entre microservices : Comment les microservices dans l’architecture communiquent entre eux ?
Ils communiquent via des **appels HTTP REST directs**. Chaque service utilise une classe `ApiCallerHelper` standardisée qui encapsule `HttpClient` et attache automatiquement le JWT pour s'authentifier auprès des autres services. Bien que le système soit conçu pour utiliser **Consul** pour la découverte de services, l'implémentation actuelle utilise des **URL statiques et hardcodées** dans les fichiers de configuration.

### 4. Validité des tokens : Est-ce que n'importe quel token généré peut fonctionner sur l’architecture ?
**Non.** Un token est considéré comme valide uniquement s'il remplit toutes les conditions suivantes, vérifiées par chaque microservice :
- Il doit être signé avec la **clé secrète** correcte.
- Il doit provenir de l'**émetteur (issuer)** attendu.
- Il doit être destiné à l'**audience** correcte.
- Sa **date d'expiration** ne doit pas être dépassée.
Tout token ne respectant pas l'un de ces critères est immédiatement rejeté.

### 5. Rôle de l'API Gateway : Quelle est la fonction de l'API Gateway ?
L'ApiGateway, construite avec **Ocelot**, agit comme le **point d'entrée unique** de l'écosystème. Ses fonctions sont :
- **Proxy Inverse et Routage** : Acheminer les requêtes publiques vers le microservice interne approprié.
- **Façade de Sécurité** : Centraliser l'application de l'authentification, de l'autorisation et de la limitation de débit.
- **Abstraction** : Masquer la complexité de l'architecture interne derrière une API unifiée pour les clients.

### 6. Niveaux de sécurité : Combien de niveaux de sécurité sont présents sur l’architecture ?
Il y a **trois niveaux de sécurité** clairement identifiables :
1.  **Niveau Périphérique (Edge)** : L'ApiGateway, qui filtre 100% du trafic entrant.
2.  **Niveau Authentification/Autorisation** : La validation systématique du JWT et des droits (claims) qu'il contient.
3.  **Niveau Applicatif** : L'utilisation d'attributs `[Authorize]` directement sur les contrôleurs et les actions dans le code de chaque microservice.

### 7. Couches de services : Combien de couches de services sont présentes dans une structure de microservices sur l’architecture ?
Chaque microservice est structuré selon une architecture cohérente à **7 couches**, représentées par des projets distincts.

### 8. Exposition de l'API : Quelle couche expose l'API aux utilisateurs ?
La couche **API (ou Présentation)** expose l'API. Plus précisément, ce sont les classes `Controller` qui reçoivent les requêtes HTTP et retournent les réponses.

### 9. Fonctions des couches : Décris la fonction de chaque couche.
1.  **API (`*.API`)** : Contient les `Controllers`, la configuration `Startup.cs` et les middlewares. Gère le protocole HTTP.
2.  **MediatR (`*.MediatR`)** : Contient la logique métier principale sous forme de `Commands`, `Queries` et `Handlers` (pattern CQRS).
3.  **Domain (`*.Domain`)** : Contient le `DbContext` (contexte Entity Framework) et les migrations de base de données.
4.  **Data (`*.Data`)** : Contient les définitions des `Entities` (tables) et des `DTOs` (objets de transfert).
5.  **Repository (`*.Repository`)** : Implémente les interfaces de la couche Common pour l'accès aux données (ex: `CustomerRepository`).
6.  **Common (`*.Common`)** : Définit les interfaces communes comme `IGenericRepository` et `IUnitOfWork`.
7.  **Helper (`*.Helper`)** : Contient les classes utilitaires comme `ApiCallerHelper` (communication inter-services) et autres.

### 10. Structure de la couche médiateur : Quelle est la structure de la couche médiateur ?
Elle est organisée par fonctionnalité et suit le pattern CQRS :
- **`Commands`** : Classes qui représentent une intention de modifier l'état du système (ex: `AddCustomerCommand`).
- **`Queries`** : Classes qui représentent une demande de lecture de données (ex: `GetCustomerByIdQuery`).
- **`Handlers`** : Classes qui contiennent la logique pour exécuter un `Command` ou une `Query` spécifique.
- **`PipeLineBehavior`** : Contient la logique transversale, comme le `ValidationBehavior` qui valide automatiquement toutes les requêtes.

### 11. Structure de l'Unit of Work : Quelle est la structure de l'Unit of Work ?
Le `UnitOfWork` encapsule le `DbContext`. Sa caractéristique principale est la surcharge de la méthode `SaveChanges()`. Avant de sauvegarder, cette méthode inspecte toutes les entités modifiées et, si elles héritent de `BaseEntity`, elle **remplit automatiquement les champs d'audit** (`CreatedBy`, `CreatedDate`, etc.) en se basant sur le `UserInfoToken` de l'utilisateur actuel.

### 12. Structure du Generic Repository : Quelle est la structure du Generic Repository ?
Le `GenericRepository<T>` fournit des méthodes CRUD standards. Sa logique la plus importante est l'implémentation du **soft delete**. Lorsqu'une entité héritant de `BaseEntity` est "supprimée", le repository ne la supprime pas physiquement mais positionne simplement son champ `IsDeleted` à `true`. Cela préserve l'historique des données. Il utilise également `AsNoTracking()` pour les lectures afin d'optimiser les performances.
