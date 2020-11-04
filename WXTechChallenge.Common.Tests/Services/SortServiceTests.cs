using AutoMapper;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WXTechChallenge.Common.ApiClients.Interfaces;
using WXTechChallenge.Common.ApiClients.Responses;
using WXTechChallenge.Common.Dtos.Response;
using WXTechChallenge.Common.Enums;
using WXTechChallenge.Common.Mappings;
using WXTechChallenge.Common.Services;

namespace WXTechChallenge.Common.Tests.Services
{
    [TestFixture]
    public class SortServiceTests
    {
        private Mock<IWooliesXApiClient> _wooliesXApiClient;

        [Test]
        public async Task GetProducts_When_SortOptionIsLow_Should_ReturnSortedPriceLowestFirst()
        {
            var service = GetService();
            var exampleGetProductListResponse = GetExampleGetProductListResponse();
            SetupGetProducts(exampleGetProductListResponse);
            const SortOption sortOption = SortOption.Low;

            var actualResult = await service.GetProducts(sortOption).ConfigureAwait(false);

            var expectedResult = GetProductDtoSortedByPriceLowestFirst();
            Assert.True(AreGetProductListResponseListsEqual(actualResult, expectedResult));
        }

        [Test]
        public async Task GetProducts_When_SortOptionIsHigh_Should_ReturnSortedPriceHighestFirst()
        {
            var service = GetService();
            var exampleGetProductListResponse = GetExampleGetProductListResponse();
            SetupGetProducts(exampleGetProductListResponse);
            const SortOption sortOption = SortOption.High;

            var actualResult = await service.GetProducts(sortOption).ConfigureAwait(false);

            var expectedResult = GetProductDtoSortedByPriceHighestFirst();
            Assert.True(AreGetProductListResponseListsEqual(actualResult, expectedResult));
        }

        [Test]
        public async Task GetProducts_When_SortOptionIsAscending_Should_ReturnSortedNameAlphabetical()
        {
            var service = GetService();
            var exampleGetProductListResponse = GetExampleGetProductListResponse();
            SetupGetProducts(exampleGetProductListResponse);
            const SortOption sortOption = SortOption.Ascending;

            var actualResult = await service.GetProducts(sortOption).ConfigureAwait(false);

            var expectedResult = GetProductDtoSortedByNameAlphabetical();
            Assert.True(AreGetProductListResponseListsEqual(actualResult, expectedResult));
        }

        [Test]
        public async Task GetProducts_When_SortOptionIsDescending_Should_ReturnSortedNameReverseAlphabetical()
        {
            var service = GetService();
            var exampleGetProductListResponse = GetExampleGetProductListResponse();
            SetupGetProducts(exampleGetProductListResponse);
            const SortOption sortOption = SortOption.Descending;

            var actualResult = await service.GetProducts(sortOption).ConfigureAwait(false);

            var expectedResult = GetProductDtoSortedByNameReverseAlphabetical();
            Assert.True(AreGetProductListResponseListsEqual(actualResult, expectedResult));
        }

        [Test]
        public async Task GetProducts_When_SortOptionIsRecommended_Should_ReturnSortedPopularProductsFirst()
        {
            var service = GetService();
            var exampleGetProductListResponse = GetExampleGetProductListResponse();
            SetupGetProducts(exampleGetProductListResponse);
            var exampleGetShopperHistoryResponse = GetExampleGetShopperHistoryResponse();
            SetupGetShopperHistory(exampleGetShopperHistoryResponse);
            const SortOption sortOption = SortOption.Recommended;

            var actualResult = await service.GetProducts(sortOption).ConfigureAwait(false);

            var expectedResult = GetProductDtoSortedByPopular();
            Assert.True(AreGetProductListResponseListsEqual(actualResult, expectedResult));
        }

        #region Setup

        private void SetupGetProducts(List<GetProductListResponse> productList)
        {
            _wooliesXApiClient.Setup(x => x.GetProducts()).ReturnsAsync(productList);
        }

        private void SetupGetShopperHistory(List<GetShopperHistoryResponse> shopperHistory)
        {
            _wooliesXApiClient.Setup(x => x.GetShopperHistory()).ReturnsAsync(shopperHistory);
        }

        #endregion

        #region Getters

        private SortService GetService()
        {
            _wooliesXApiClient = new Mock<IWooliesXApiClient>();
            var mappingConfig = new MapperConfiguration(mc => { mc.AddProfile(new MappingProfile()); });

            return new SortService(_wooliesXApiClient.Object, mappingConfig.CreateMapper());
        }

        private static List<GetProductListResponse> GetExampleGetProductListResponse()
        {
            return new List<GetProductListResponse>()
            {
                new GetProductListResponse()
                {
                    Name = "B",
                    Price = 200.00M,
                    Quantity = 2
                },
                new GetProductListResponse()
                {
                    Name = "A",
                    Price = 50.00M,
                    Quantity = 5
                },
                new GetProductListResponse()
                {
                    Name = "C",
                    Price = 100.00M,
                    Quantity = 1
                }
            };
        }

        private static List<GetShopperHistoryResponse> GetExampleGetShopperHistoryResponse()
        {
            return new List<GetShopperHistoryResponse>()
            {
                new GetShopperHistoryResponse()
                {
                    CustomerId = 123,
                    Products = new List<GetProductListResponse>()
                    {
                        new GetProductListResponse()
                        {
                            Name = "A",
                            Quantity = 2
                        },
                        new GetProductListResponse()
                        {
                            Name = "B",
                            Quantity = 3
                        },
                        new GetProductListResponse()
                        {
                            Name = "F",
                            Quantity = 1
                        }
                    }
                },
                new GetShopperHistoryResponse()
                {
                    CustomerId = 23,
                    Products = new List<GetProductListResponse>()
                    {
                        new GetProductListResponse()
                        {
                            Name = "A",
                            Quantity = 2
                        },
                        new GetProductListResponse()
                        {
                            Name = "B",
                            Quantity = 3
                        },
                        new GetProductListResponse()
                        {
                            Name = "F",
                            Quantity = 1
                        }
                    }
                },
                new GetShopperHistoryResponse()
                {
                    CustomerId = 23,
                    Products = new List<GetProductListResponse>()
                    {
                        new GetProductListResponse()
                        {
                            Name = "C",
                            Quantity = 2
                        },
                        new GetProductListResponse()
                        {
                            Name = "F",
                            Quantity = 2
                        }
                    }
                },
                new GetShopperHistoryResponse()
                {
                    CustomerId = 23,
                    Products = new List<GetProductListResponse>()
                    {
                        new GetProductListResponse()
                        {
                            Name = "A",
                            Quantity = 4
                        },
                        new GetProductListResponse()
                        {
                            Name = "B",
                            Quantity = 4
                        },
                        new GetProductListResponse()
                        {
                            Name = "C",
                            Quantity = 3
                        }
                    }
                }
            };
        }

        private static List<ProductDto> GetProductDtoSortedByPriceLowestFirst()
        {
            return new List<ProductDto>()
            {
                new ProductDto()
                {
                    Name = "A",
                    Price = 50.00M,
                },
                new ProductDto()
                {
                    Name = "C",
                    Price = 100.00M,
                },
                new ProductDto()
                {
                    Name = "B",
                    Price = 200.00M,
                }
            };
        }

        private static List<ProductDto> GetProductDtoSortedByPriceHighestFirst()
        {
            return new List<ProductDto>()
            {
                new ProductDto()
                {
                    Name = "B",
                    Price = 200.00M,
                },
                new ProductDto()
                {
                    Name = "C",
                    Price = 100.00M,
                },
                new ProductDto()
                {
                    Name = "A",
                    Price = 50.00M,
                }
            };
        }

        private static List<ProductDto> GetProductDtoSortedByNameAlphabetical()
        {
            return new List<ProductDto>()
            {
                new ProductDto()
                {
                    Name = "A",
                },
                new ProductDto()
                {
                    Name = "B",
                },
                new ProductDto()
                {
                    Name = "C",
                }
            };
        }

        private static List<ProductDto> GetProductDtoSortedByNameReverseAlphabetical()
        {
            return new List<ProductDto>()
            {
                new ProductDto()
                {
                    Name = "C",
                },
                new ProductDto()
                {
                    Name = "B",
                },
                new ProductDto()
                {
                    Name = "A",
                }
            };
        }

        private static List<ProductDto> GetProductDtoSortedByPopular()
        {
            return new List<ProductDto>()
            {
                new ProductDto()
                {
                    Name = "B",
                },
                new ProductDto()
                {
                    Name = "A",
                },
                new ProductDto()
                {
                    Name = "C",
                }
            };
        }

        #endregion

        #region AssertHelpers

        private static bool AreGetProductListResponseListsEqual(List<ProductDto> a, List<ProductDto> b)
        {
            if (a.Count != b.Count)
            {
                return false;
            }

            return !a.Where((t, i) => t.Name != b[i].Name).Any();
        }

        #endregion
    }
}
