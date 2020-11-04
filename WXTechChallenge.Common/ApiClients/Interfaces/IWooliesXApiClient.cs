using System.Collections.Generic;
using System.Threading.Tasks;
using WXTechChallenge.Common.ApiClients.Responses;
using WXTechChallenge.Common.Dtos.Request;

namespace WXTechChallenge.Common.ApiClients.Interfaces
{
    public interface IWooliesXApiClient
    {
        Task<List<GetProductListResponse>> GetProducts();
        Task<List<GetShopperHistoryResponse>> GetShopperHistory();
        Task<decimal> GetTotal(TrolleyRequest trolleyRequest);
    }
}
