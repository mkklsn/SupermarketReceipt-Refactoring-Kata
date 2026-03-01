using SupermarketReceipt.Domain.Products;

namespace SupermarketReceipt.Domain.Prices
{
    /// <summary>
    /// Provides an interface to stores product and price
    /// </summary>
    public interface IPriceCatalog
    {
        void AddProduct(Product product, decimal price);

        decimal GetUnitPrice(Product product);
    }
}