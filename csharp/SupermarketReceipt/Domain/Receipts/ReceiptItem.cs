using SupermarketReceipt.Domain.Products;

namespace SupermarketReceipt.Domain.Receipts
{
    /// <summary>
    /// An item in the <see cref="Receipt"/>
    /// </summary>
    public class ReceiptItem(Product p, decimal quantity, decimal price, decimal totalPrice)
    {
        public Product Product { get; } = p;
        public decimal Price { get; } = price;
        public decimal TotalPrice { get; } = totalPrice;
        public decimal Quantity { get; } = quantity;
    }
}