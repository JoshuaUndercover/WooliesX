using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using WXTechChallenge.Common.ApiClients.Interfaces;
using WXTechChallenge.Common.ApiClients.Responses;
using WXTechChallenge.Common.Settings;

namespace WXTechChallenge.Common.ApiClients
{
    public class WooliesXApiClient : HttpClient, IWooliesXApiClient
    {
        private readonly WooliesXApiSettings _settings;
        public WooliesXApiClient(WooliesXApiSettings settings)
        {
            _settings = settings;
        }
        public async Task<List<GetProductListResponse>> GetProducts(string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                throw new ArgumentNullException(nameof(token));
            }

            var url = _settings.BaseUrl + $"/api/resource/products?token={token}";

            var response = await GetAsync(url).ConfigureAwait(false);

            response.EnsureSuccessStatusCode();

            var responseJson = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            return JsonConvert.DeserializeObject<List<GetProductListResponse>>(responseJson);
        }

        public async Task<List<GetShopperHistoryResponse>> GetShopperHistory(string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                throw new ArgumentNullException(nameof(token));
            }

            var url = _settings.BaseUrl + $"/api/resource/shopperHistory?token={token}";

            var response = await GetAsync(url).ConfigureAwait(false);

            response.EnsureSuccessStatusCode();

            var responseJson = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            return JsonConvert.DeserializeObject<List<GetShopperHistoryResponse>>(responseJson);
        }
    }
}
