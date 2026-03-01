using System;
using System.Collections.Generic;

using SupermarketReceipt.Domain.Products;

namespace SupermarketReceipt.Domain.Prices
{
    /// <summary>
    /// In memory catalog that contains a products and prices
    /// </summary>
    public class InMemoryPriceCatalog : IPriceCatalog
    {
        private readonly Dictionary<Guid, decimal> _prices = [];
        private readonly Dictionary<Guid, Product> _products = [];

        public void AddProduct(Product product, decimal price)
        {
            _products.Add(product.Id, product);
            _prices.Add(product.Id, price);
        }

        public decimal GetUnitPrice(Guid productId)
        {
            return _prices[productId];
        }
    }
}