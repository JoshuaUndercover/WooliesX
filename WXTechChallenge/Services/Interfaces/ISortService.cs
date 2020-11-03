using System.Collections.Generic;
using System.Threading.Tasks;
using WXTechChallenge.Dtos.Response;
using WXTechChallenge.Enums;

namespace WXTechChallenge.Services.Interfaces
{
    public interface ISortService
    {
        Task<List<ProductDto>> GetProducts(SortOption sortOption);
    }
}
