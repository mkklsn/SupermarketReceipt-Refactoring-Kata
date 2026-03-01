using SupermarketReceipt.Domain.Products;

namespace SupermarketReceipt.Domain.Receipts
{
    /// <summary>
    /// An item in the <see cref="Receipt"/>
    /// </summary>
    public class ReceiptItem
    {
        public ReceiptItem(Product p, decimal quantity, decimal price, decimal totalPrice)
        {
            Product = p;
            Quantity = quantity;
            Price = price;
            TotalPrice = totalPrice;
        }

        public Product Product { get; }
        public decimal Price { get; }
        public decimal TotalPrice { get; }
        public decimal Quantity { get; }
    }
}