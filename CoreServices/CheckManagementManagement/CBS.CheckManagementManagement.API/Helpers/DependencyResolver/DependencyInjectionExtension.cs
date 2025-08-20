using CBS.CheckManagementManagement.Common.UnitOfWork;
using CBS.CheckManagementManagement.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace CBS.CheckManagementManagement.API.Helpers
{
    public static class DependencyInjectionExtension
    {
        public static void AddDependencyInjection(this IServiceCollection services)
        {
            services.AddScoped(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));
            services.AddScoped<IPingRepository, PingRepository>();
        }
    }
}
