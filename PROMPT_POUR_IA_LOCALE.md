# Directive Maître pour IA Locale : Finalisation et Compilation du Microservice `PortfolioManagement`

## 1. CONTEXTE

Un nouveau microservice nommé `PortfolioManagement` a été initialisé au sein de la solution `CBSManagementService`. Le scaffolding de base est terminé :
- Les 7 projets C# (`.csproj`) pour les 7 couches de l'architecture (`API`, `MediatR`, `Domain`, `Data`, `Repository`, `Common`, `Helper`) ont été créés.
- Les fichiers de configuration et de démarrage essentiels (`Program.cs`, `Startup.cs`, `appsettings.json`, `BaseController.cs`, etc.) ont été copiés depuis un service existant et adaptés.
- Le nouveau microservice a été ajouté manuellement au fichier de solution `CBSManagementService.sln`.

Cependant, le code n'est **pas encore compilable**. Il manque quelques fichiers "boilerplate" fondamentaux dont dépendent les fichiers déjà créés.

## 2. MISSION

Votre mission est de **finaliser l'intégration de ce microservice**. Vous devez ajouter les derniers fichiers manquants, puis utiliser les outils de build locaux (`dotnet` ou `msbuild`) pour compiler la solution entière, identifier et corriger toutes les erreurs de compilation jusqu'à ce que la build soit un succès (`Build Succeeded`).

## 3. PLAN D'ACTION DÉTAILLÉ

Veuillez suivre rigoureusement les étapes suivantes.

### Étape 1 : Création des Fichiers "Boilerplate" Manquants

Ces fichiers sont des dépendances critiques pour le code déjà en place.

**1.1. Créez le fichier `BaseEntity.cs`**
- **Emplacement :** `CoreServices/PortfolioManagement/CBS.PortfolioManagement.Data/BaseEntity.cs`
- **Contenu :**
```csharp
using Microsoft.Web.Administration;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CBS.PortfolioManagement.Data
{
    public abstract class BaseEntity
    {
        private DateTime _createdDate;
        public DateTime CreatedDate
        {
            get => _createdDate.ToLocalTime();
            set => _createdDate = value.ToLocalTime();
        }
        public string CreatedBy { get; set; }

        private DateTime _modifiedDate;
        public DateTime ModifiedDate
        {
            get => _modifiedDate.ToLocalTime();
            set => _modifiedDate = value.ToLocalTime();
        }
        public string ModifiedBy { get; set; }
        private DateTime? _deletedDate;
        public DateTime? DeletedDate
        {
            get => _deletedDate?.ToLocalTime();
            set => _deletedDate = value?.ToLocalTime();
        }
        public string? DeletedBy { get; set; }
        [NotMapped]
        public ObjectState ObjectState { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
```

**1.2. Créez le répertoire `Dto` et le fichier `UserInfoToken.cs`**
- **Créez d'abord le répertoire :** `CoreServices/PortfolioManagement/CBS.PortfolioManagement.Data/Dto/`
- **Puis, créez le fichier `UserInfoToken.cs` à cet emplacement :** `CoreServices/PortfolioManagement/CBS.PortfolioManagement.Data/Dto/UserInfoToken.cs`
- **Contenu :**
```csharp
namespace CBS.PortfolioManagement.Dto
{
    public class UserInfoToken
    {
        public string? Id { get; set; }
        public string? Email { get; set; }
        public string? ConnectionId { get; set; }
        public string? Token { get; set; }
        public string? FullName { get; set; }
        public string? BranchID { get; set; }
        public string? BankID { get; set; }
        public string? BranchCode { get; set; }
        public string? BankCode { get; set; }
        public string? BranchName { get; set; }
        public bool IsHeadOffice { get; set; }
    }
}
```

### Étape 2 : Compilation et Débogage

Maintenant que les dépendances les plus évidentes sont résolues, compilez la solution pour trouver et corriger les erreurs restantes.

**2.1. Lancez la compilation**
- Ouvrez un terminal à la racine du projet.
- Exécutez la commande de build :
```bash
dotnet build CBSManagementService.sln
```

**2.2. Analysez et Corrigez les Erreurs**
- La compilation va probablement échouer avec d'autres erreurs (par exemple, des `using` manquants, des références incorrectes, ou d'autres petites dépendances oubliées).
- Lisez attentivement les messages d'erreur du compilateur.
- Pour chaque erreur, utilisez les autres microservices (comme `TransactionManagement` ou `CustomerManagement`) comme **source de vérité** pour comprendre comment la corriger.
- Continuez à lancer la commande `dotnet build` et à corriger les erreurs de manière itérative jusqu'à ce que la compilation réussisse.

### Étape 3 : Validation Finale

**3.1. Assurez-vous que la compilation est un succès**
- La dernière exécution de `dotnet build CBSManagementService.sln` doit se terminer par le message :
  `Build succeeded.`

Une fois cette étape validée, votre mission est terminée. Le microservice `PortfolioManagement` est maintenant une base de code stable et compilable, prête pour le développement de ses fonctionnalités métier.
