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

            return shopperHistoryResponse.SelectMany(x => x.Products).GroupBy(x => x.Name).Select(g => new GetProductListResponse
            {
                Name = g.Key,
                Price = g.First().Price,
                Quantity = g.Sum(x => x.Quantity)
            }).OrderByDescending(x => x.Quantity).ToList();
        }
    }
}
