using AutoMapper;
using CBS.CheckManagement.Data.Dto;
using CBS.CheckManagement.Data.Entity;

namespace CBS.CheckManagement.API.Helpers.MappingProfile
{
    public class PingProfile : Profile
    {
        public PingProfile()
        {
            CreateMap<Ping, PingDto>().ReverseMap();
        }
    }
}
