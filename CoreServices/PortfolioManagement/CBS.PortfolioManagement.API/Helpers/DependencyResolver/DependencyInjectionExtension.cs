using CBS.PortfolioManagement.Common.UnitOfWork;

namespace CBS.PortfolioManagement.API.Helpers
{
    public static class DependencyInjectionExtension
    {
        public static void AddDependencyInjection(this IServiceCollection services)
        {
            services.AddScoped(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));

            // Other dependencies (repositories, services) will be added here as they are created.
        }
    }
}
