using CBS.CheckManagement.Common.GenericRespository;
using CBS.CheckManagement.Common.UnitOfWork;
using CBS.CheckManagement.Data;
using CBS.CheckManagement.Data.Entity;

namespace CBS.CheckManagement.Repository
{
    public class PingRepository : GenericRepository<Ping, CheckManagementContext>, IPingRepository
    {
        public PingRepository(IUnitOfWork<CheckManagementContext> unitOfWork) : base(unitOfWork)
        {
        }
    }
}
