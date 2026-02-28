using System.Collections.Generic;

using SupermarketReceipt.Domain.Products;

namespace SupermarketReceipt.Domain.Prices
{
    public class InMemoryPriceCatalog : IPriceCatalog
    {
        private readonly Dictionary<string, decimal> _prices = [];
        private readonly Dictionary<string, Product> _products = [];

        public void AddProduct(Product product, decimal price)
        {
            _products.Add(product.Name, product);
            _prices.Add(product.Name, price);
        }

        public decimal GetUnitPrice(Product p)
        {
            return _prices[p.Name];
        }
    }
}