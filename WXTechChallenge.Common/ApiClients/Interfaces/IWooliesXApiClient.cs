using System.Collections.Generic;
using System.Threading.Tasks;
using WXTechChallenge.Common.ApiClients.Responses;

namespace WXTechChallenge.Common.ApiClients.Interfaces
{
    public interface IWooliesXApiClient
    {
        Task<List<GetProductListResponse>> GetProducts(string token);
        Task<List<GetShopperHistoryResponse>> GetShopperHistory(string token);
    }
}
