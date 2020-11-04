using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WXTechChallenge.Common.ApiClients.Interfaces;
using WXTechChallenge.Common.ApiClients.Responses;
using WXTechChallenge.Common.Dtos.Response;
using WXTechChallenge.Common.Enums;
using WXTechChallenge.Common.Services.Interfaces;

namespace WXTechChallenge.Common.Services
{
    public class SortService : ISortService
    {
        private readonly IWooliesXApiClient _wooliesXApiClient;
        private readonly IMapper _mapper;
        public SortService(IWooliesXApiClient wooliesXApiClient, IMapper mapper)
        {
            _wooliesXApiClient = wooliesXApiClient;
            _mapper = mapper;
        }
        public async Task<List<ProductDto>> GetProducts(SortOption sortOption)
        {
            var products = await _wooliesXApiClient.GetProducts().ConfigureAwait(false);

            if (sortOption != SortOption.Recommended)
            {
                var sortedProducts = sortOption switch
                {
                    SortOption.Low => products.OrderBy(x => x.Price).ToList(),
                    SortOption.High => products.OrderByDescending(x => x.Price).ToList(),
                    SortOption.Ascending => products.OrderBy(x => x.Name).ToList(),
                    SortOption.Descending => products.OrderByDescending(x => x.Name).ToList(),
                    _ => throw new ArgumentException(nameof(sortOption))
                };

                return _mapper.Map<List<ProductDto>>(sortedProducts);
            }

            //Get popular products by quantity
            //Make an empty new list
            //Go through popular products
            //  If they exist in products, put them into the new list
            //Go through the rest of the products list and add them to the end

            var popularProducts = await GetShopperHistorySortedByPopularity().ConfigureAwait(false);

            var popularSortedList = new List<GetProductListResponse>();

            foreach (var popularProduct in popularProducts)
            {
                var product = products.FirstOrDefault(x => x.Name == popularProduct.Name);
                if (product != null)
                {
                    popularSortedList.Add(product);
                }
            }

            foreach (var product in products.Where(product => popularSortedList.All(x => x.Name != product.Name)))
            {
                popularSortedList.Add(product);
            }

            return _mapper.Map<List<ProductDto>>(popularSortedList);
        }

        private async Task<List<GetProductListResponse>> GetShopperHistorySortedByPopularity()
        {
            var shopperHistoryResponse = await _wooliesXApiClient.GetShopperHistory().ConfigureAwait(false);

            return shopperHistoryResponse.SelectMany(x => x.Products).GroupBy(x => x.Name).Select(g => new GetProductListResponse
            {
                Name = g.Key,
                Price = g.First().Price,
                Quantity = g.Sum(x => x.Quantity)
            }).OrderByDescending(x => x.Quantity).ToList();
        }
    }
}
