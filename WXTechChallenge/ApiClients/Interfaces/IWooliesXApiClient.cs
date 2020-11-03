using System.Collections.Generic;
using System.Threading.Tasks;
using WXTechChallenge.ApiClients.Responses;

namespace WXTechChallenge.ApiClients.Interfaces
{
    public interface IWooliesXApiClient
    {
        Task<List<GetProductListResponse>> GetProducts(string token);
        Task<List<GetShopperHistoryResponse>> GetShopperHistory(string token);
    }
}
