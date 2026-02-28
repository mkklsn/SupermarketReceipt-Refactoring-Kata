using SupermarketReceipt.Domain.Products;

namespace SupermarketReceipt.Domain.Prices
{
    public interface IPriceCatalog
    {
        void AddProduct(Product product, decimal price);

        decimal GetUnitPrice(Product product);
    }
}