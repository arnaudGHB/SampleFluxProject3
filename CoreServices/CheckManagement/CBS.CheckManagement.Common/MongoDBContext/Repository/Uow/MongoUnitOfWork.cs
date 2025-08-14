using CBS.CheckManagement.Common.DBConnection;
using CBS.CheckManagement.Common.Repository.Generic;

namespace CBS.CheckManagement.Common.Repository.Uow
{
    public class MongoUnitOfWork : IMongoUnitOfWork
    {
        private readonly IMongoDbConnection _mongoDbConnection;
        private readonly Dictionary<Type, object> _repositories;

        public MongoUnitOfWork(IMongoDbConnection mongoDbConnection)
        {
            _mongoDbConnection = mongoDbConnection;
            _repositories = new Dictionary<Type, object>();
        }

        public IMongoGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : class
        {
            var entityType = typeof(TEntity);

            if (!_repositories.ContainsKey(entityType))
            {
                var repository = new MongoGenericRepository<TEntity>(_mongoDbConnection);
                _repositories[entityType] = repository;
            }

            return (IMongoGenericRepository<TEntity>)_repositories[entityType];
        }

        public void Dispose()
        {
            // Dispose resources (if necessary)
        }
    }
}
