using AutoMapper;

namespace CBS.CheckManagementManagement.API.Helpers
{
    public static class MapperConfig
    {
        public static IMapper GetMapperConfigs()
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                // mc.CreateMap<Ping, PingDto>(); // Example
            });
            return mappingConfig.CreateMapper();
        }
    }
}
