using Microsoft.EntityFrameworkCore;

namespace CBS.CheckManagement.Common.UnitOfWork
{
    public interface IDbContextFactory<TContext> where TContext : DbContext
    {
        TContext CreateDbContext();
    }
}