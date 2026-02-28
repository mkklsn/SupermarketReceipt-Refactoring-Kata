using SupermarketReceipt.Domain.Products;

namespace SupermarketReceipt.Domain.Prices
{
    public interface IPriceCatalog
    {
        void AddProduct(Product product, double price);

        double GetUnitPrice(Product product);
    }
}