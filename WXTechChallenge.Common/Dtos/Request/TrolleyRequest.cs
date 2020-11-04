using System;
using System.Collections.Generic;

namespace WXTechChallenge.Common.Dtos.Request
{
    public class TrolleyRequest
    {
        public List<Product> Products { get; set; }
        public List<Special> Specials { get; set; }
        public List<ProductQuantity> Quantities { get; set; }
    }

    public class Product
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
    }

    public class Special
    {
        public List<ProductQuantity> Quantities { get; set; }
        public decimal Total { get; set; }
    }

    public class ProductQuantity : ICloneable
    {
        public string Name { get; set; }
        public int Quantity { get; set; }

        public object Clone()
        {
            return new ProductQuantity {Name = Name, Quantity = Quantity};
        }
    }
}
