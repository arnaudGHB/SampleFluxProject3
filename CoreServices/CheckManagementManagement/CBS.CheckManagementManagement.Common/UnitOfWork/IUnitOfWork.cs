using CBS.CheckManagementManagement.Dto;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace CBS.CheckManagementManagement.Common.UnitOfWork
{
    public interface IUnitOfWork<TContext>
        where TContext : DbContext
    {
        int Save();
        Task<int> SaveAsync();
        Task<int> SaveAsync(UserInfoToken userInfoToken);
        TContext Context { get; }
    }
}
