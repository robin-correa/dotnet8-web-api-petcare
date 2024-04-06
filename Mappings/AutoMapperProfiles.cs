using AutoMapper;

using dotnet8_web_api_petcare.Dtos.Services;
using dotnet8_web_api_petcare.Models.Domains;

namespace dotnet8_web_api_petcare.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Service, GetServiceDto>().ReverseMap();
            CreateMap<CreateServiceDto, Service>().ReverseMap();
            CreateMap<UpdateServiceDto, Service>().ReverseMap();
        }
    }
}
