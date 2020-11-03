using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WXTechChallenge.ApiClients.Interfaces;
using WXTechChallenge.ApiClients.Responses;
using WXTechChallenge.Dtos.Response;
using WXTechChallenge.Enums;
using WXTechChallenge.Services.Interfaces;
using WXTechChallenge.Settings;

namespace WXTechChallenge.Services
{
    public class SortService : ISortService
    {
        private readonly IWooliesXApiClient _wooliesXApiClient;
        private readonly WooliesXApiSettings _settings;
        private readonly IMapper _mapper;
        public SortService(IWooliesXApiClient wooliesXApiClient, WooliesXApiSettings settings, IMapper mapper)
        {
            _wooliesXApiClient = wooliesXApiClient;
            _settings = settings;
            _mapper = mapper;
        }
        public async Task<List<ProductDto>> GetProducts(SortOption sortOption)
        {
            List<GetProductListResponse> sortedProducts;
            if (sortOption == SortOption.Recommended)
            {
                sortedProducts = await GetShopperHistorySortedByPopularity(_settings.Token).ConfigureAwait(false);
            }
            else
            {
                var products = await _wooliesXApiClient.GetProducts(_settings.Token).ConfigureAwait(false);

                sortedProducts = sortOption switch
                {
                    SortOption.Low => products.OrderBy(x => x.Price).ToList(),
                    SortOption.High => products.OrderByDescending(x => x.Price).ToList(),
                    SortOption.Ascending => products.OrderBy(x => x.Name).ToList(),
                    SortOption.Descending => products.OrderByDescending(x => x.Name).ToList(),
                    _ => throw new System.NotImplementedException(),
                };
            }

            return _mapper.Map<List<ProductDto>>(sortedProducts);
        }

        private async Task<List<GetProductListResponse>> GetShopperHistorySortedByPopularity(string token)
        {
            var shopperHistoryResponse = await _wooliesXApiClient.GetShopperHistory(token).ConfigureAwait(false);

            var products = shopperHistoryResponse.SelectMany(x => x.Products).ToList();

            Dictionary<string, decimal> productPopularities = new Dictionary<string, decimal>();

            foreach(var product in products)
            {
                if(!productPopularities.ContainsKey(product.Name))
                {
                    productPopularities[product.Name] = 0;
                }

                productPopularities[product.Name] += product.Quantity;
            }

            return productPopularities.OrderByDescending(x => x.Value).Select(x =>
            {
                var first = products.First(p => p.Name == x.Key);
                first.Quantity = x.Value;
                return first;
            }).ToList();
        }
    }
}
