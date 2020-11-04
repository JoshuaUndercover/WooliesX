using System.Collections.Generic;
using System.Threading.Tasks;
using WXTechChallenge.Common.Dtos.Response;
using WXTechChallenge.Common.Enums;

namespace WXTechChallenge.Common.Services.Interfaces
{
    public interface ISortService
    {
        Task<List<ProductDto>> GetProducts(SortOption sortOption);
    }
}
