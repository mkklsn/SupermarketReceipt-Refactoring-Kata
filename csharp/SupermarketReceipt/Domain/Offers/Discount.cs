using SupermarketReceipt.Domain.Products;

namespace SupermarketReceipt.Domain.Offers
{
    /// <summary>
    /// Contains a discount amount for a product
    /// </summary>
    public class Discount(Product product, string description, decimal discountAmount)
    {
        public string Description { get; } = description;
        public decimal DiscountAmount { get; } = discountAmount;
        public Product Product { get; } = product;
    }
}