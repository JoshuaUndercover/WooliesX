using AutoMapper;
using WXTechChallenge.Common.ApiClients.Responses;
using WXTechChallenge.Common.Dtos.Response;

namespace WXTechChallenge.Common.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<GetProductListResponse, ProductDto>();
        }
    }
}
