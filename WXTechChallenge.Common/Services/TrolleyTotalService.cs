using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WXTechChallenge.Common.ApiClients.Interfaces;
using WXTechChallenge.Common.Dtos.Request;
using WXTechChallenge.Common.Services.Interfaces;

namespace WXTechChallenge.Common.Services
{
    public class TrolleyTotalService : ITrolleyTotalService
    {
        private readonly IWooliesXApiClient _wooliesXApiClient;

        public TrolleyTotalService(IWooliesXApiClient wooliesXApiClient)
        {
            _wooliesXApiClient = wooliesXApiClient;
        }

        public async Task<decimal> GetTotal(TrolleyRequest trolleyRequest)
        {
            return await _wooliesXApiClient.GetTotal(trolleyRequest).ConfigureAwait(false);
        }

        public decimal GetTotalInternal(TrolleyRequest trolleyRequest)
        {
            //Quantities is what is in your cart
            //Product is a list of the products and their original price
            //Specials are a list of possible specials, sometimes involving the requirement of multiple items together which then all get sold for total
            //All items in quantities must be listed in each special, products which aren't in the special are still set to quantity 0

            //If you have a product in quantities that isn't in the product list, it's free

            if (trolleyRequest.Products == null || trolleyRequest.Products.Count == 0)
            {
                throw new ArgumentException(nameof(trolleyRequest) + " no products are specified in request");
            }

            if (trolleyRequest.Quantities == null || trolleyRequest.Quantities.Count == 0)
            {
                throw new ArgumentException(nameof(trolleyRequest) + " no products are specified in request");
            }

            //TODO: validate the data

            var values = new List<decimal>();

            GetTotalRecursive(values, trolleyRequest.Specials, trolleyRequest.Products, trolleyRequest.Quantities, 0);

            var lowest = values.Min();

            return lowest;
        }

        private void GetTotalRecursive(List<decimal> values, List<Special> specials, List<Product> products, List<ProductQuantity> remainingQuantities, decimal currentTotal)
        {
            //Add the case where no specials are used
            values.Add(currentTotal + CalculateRemainingCost(products, remainingQuantities));

            foreach (var special in specials)
            {
                if (!CanUseSpecial(special, remainingQuantities))
                {
                    continue;
                }

                //Clone the list before passing into next level
                var remainingQuantitiesCopy = remainingQuantities.Select(x => (ProductQuantity)x.Clone()).ToList();

                //Remove the quantity of items required by the special
                foreach (var specialQuantity in special.Quantities)
                {
                    remainingQuantitiesCopy
                        .First(x => x.Name == specialQuantity.Name).Quantity -= specialQuantity.Quantity;
                }

                //Take the rest of the items and continue
                GetTotalRecursive(values, specials, products, remainingQuantitiesCopy, currentTotal + special.Total);
            }
        }

        [DebuggerStepThrough]
        private bool CanUseSpecial(Special special, List<ProductQuantity> remainingQuantities)
        {
            //go through each of the special quantities
                //Make sure there is that many in remainingQuantities
                //If there isn't return false
            //return true

            foreach (var specialQuantity in special.Quantities)
            {
                var remainingQuantity = remainingQuantities.FirstOrDefault(x => x.Name == specialQuantity.Name);

                if (remainingQuantity == null || remainingQuantity.Quantity < specialQuantity.Quantity)
                {
                    return false;
                }
            }

            return true;
        }

        private decimal CalculateRemainingCost(List<Product> products, List<ProductQuantity> remainingQuantities)
        {
            decimal total = 0;
            foreach (var productQuantity in remainingQuantities)
            {
                var product = products.First(x => x.Name == productQuantity.Name);
                total += productQuantity.Quantity * product.Price;
            }

            return total;
        }
    }
}
