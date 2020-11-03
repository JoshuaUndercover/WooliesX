using AutoMapper;
using WXTechChallenge.ApiClients.Responses;
using WXTechChallenge.Dtos.Response;

namespace WXTechChallenge.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<GetProductListResponse, ProductDto>();
        }
    }
}
