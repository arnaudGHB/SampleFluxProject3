using CBS.CheckManagement.Common.Repository.Generic;

namespace CBS.CheckManagement.Common.Repository.Uow
{
    public interface IMongoUnitOfWork
    {
        void Dispose();
        IMongoGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : class;
    }
}