using SupermarketReceipt.Domain.Products;

namespace SupermarketReceipt.Domain.Offers
{
    /// <summary>
    /// Contains a discount amount for a product
    /// </summary>
    public class Discount
    {
        public Discount(Product product, string description, decimal discountAmount)
        {
            Product = product;
            Description = description;
            DiscountAmount = discountAmount;
        }

        public string Description { get; }
        public decimal DiscountAmount { get; }
        public Product Product { get; }
    }
}