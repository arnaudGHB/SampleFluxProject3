using CBS.CheckManagementManagement.Common.GenericRespository;
using CBS.CheckManagementManagement.Common.UnitOfWork;
using CBS.CheckManagementManagement.Data.Entity;
using CBS.CheckManagementManagement.Domain.Context;

namespace CBS.CheckManagementManagement.Repository
{
    public class PingRepository : GenericRepository<Ping, CheckManagementContext>, IPingRepository
    {
        public PingRepository(IUnitOfWork<CheckManagementContext> uow) : base(uow)
        {
        }
    }
}
