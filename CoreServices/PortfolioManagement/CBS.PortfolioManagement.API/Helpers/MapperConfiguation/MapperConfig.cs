using AutoMapper;

namespace CBS.PortfolioManagement.API
{
    public static class MapperConfig
    {
        public static IMapper GetMapperConfigs()
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                // Mapping profiles for PortfolioManagement will be added here.
            });
            return mappingConfig.CreateMapper();
        }
    }
}
