using System.Collections.Generic;

using SupermarketReceipt.Domain.Offers;
using SupermarketReceipt.Domain.Products;

namespace SupermarketReceipt.Domain.Receipts
{
    /// <summary>
    /// A container for a collection of <see cref="ReceiptItem"/> and <see cref="Discount"/>
    /// </summary>
    public class Receipt
    {
        private readonly List<Discount> _discounts = [];
        private readonly List<ReceiptItem> _items = [];

        public List<ReceiptItem> Items => [.. _items];

        public decimal GetTotalPrice()
        {
            var total = 0.0m;
            foreach (var item in _items) total += item.TotalPrice;
            foreach (var discount in _discounts) total += discount.DiscountAmount;
            return total;
        }

        public void AddItem(Product p, decimal quantity, decimal price, decimal totalPrice)
        {
            _items.Add(new ReceiptItem(p, quantity, price, totalPrice));
        }

        public void AddDiscount(Discount discount)
        {
            _discounts.Add(discount);
        }

        public IEnumerable<Discount> GetDiscounts()
        {
            return _discounts;
        }
    }
}