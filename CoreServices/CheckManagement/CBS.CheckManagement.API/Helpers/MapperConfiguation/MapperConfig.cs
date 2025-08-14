using AutoMapper;
using CBS.CheckManagement.API;
using CBS.CheckManagement.API.Helpers.MappingProfile;
using CBS.CheckManagement.Data;

namespace CBS.CheckManagement.API
{
    public static class MapperConfig
    {
        public static IMapper GetMapperConfigs()
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new PingProfile());
            });
            return mappingConfig.CreateMapper();
        }
    }
}
