using CBS.CheckManagement.Common.UnitOfWork;
using Microsoft.Extensions.DependencyInjection;
using CBS.CheckManagement.Data;
using CBS.CheckManagement.Domain;
using CBS.CheckManagement.Repository;

namespace CBS.CheckManagement.Helper
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
