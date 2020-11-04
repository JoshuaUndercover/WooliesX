using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WXTechChallenge.Common.ApiClients.Interfaces;
using WXTechChallenge.Common.ApiClients.Responses;
using WXTechChallenge.Common.Dtos.Request;
using WXTechChallenge.Common.Settings;

namespace WXTechChallenge.Common.ApiClients
{
    public class WooliesXApiClient : HttpClient, IWooliesXApiClient
    {
        private readonly WooliesXApiSettings _settings;
        public WooliesXApiClient(WooliesXApiSettings settings)
        {
            _settings = settings;

            if (string.IsNullOrEmpty(_settings.Token))
            {
                throw new ArgumentNullException(nameof(settings));
            }
        }
        public async Task<List<GetProductListResponse>> GetProducts()
        {
            var url = _settings.BaseUrl + $"/api/resource/products?token={_settings.Token}";

            var response = await GetAsync(url).ConfigureAwait(false);

            response.EnsureSuccessStatusCode();

            var responseJson = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            return JsonConvert.DeserializeObject<List<GetProductListResponse>>(responseJson);
        }

        public async Task<List<GetShopperHistoryResponse>> GetShopperHistory()
        {
            var url = _settings.BaseUrl + $"/api/resource/shopperHistory?token={_settings.Token}";

            var response = await GetAsync(url).ConfigureAwait(false);

            response.EnsureSuccessStatusCode();

            var responseJson = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            return JsonConvert.DeserializeObject<List<GetShopperHistoryResponse>>(responseJson);
        }

        public async Task<decimal> GetTotal(TrolleyRequest trolleyRequest)
        {
            var requestJson = JsonConvert.SerializeObject(trolleyRequest);

            var content = new StringContent(requestJson, Encoding.UTF8, "application/json");
            
            var url = _settings.BaseUrl + $"/api/resource/trolleyCalculator?token={_settings.Token}";

            var response = await PostAsync(url, content).ConfigureAwait(false);

            response.EnsureSuccessStatusCode();

            var responseJson = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            return JsonConvert.DeserializeObject<decimal>(responseJson);
        }
    }
}
