using System.Collections.Generic;
using System.Globalization;

using SupermarketReceipt.Domain.Offers;
using SupermarketReceipt.Domain.Prices;
using SupermarketReceipt.Domain.Products;
using SupermarketReceipt.Domain.Receipts;

namespace SupermarketReceipt.Domain.Carts
{
    public class ShoppingCart
    {
        private static readonly CultureInfo Culture = CultureInfo.CreateSpecificCulture("en-GB");

        private readonly List<CartItem> _items = [];
        public IReadOnlyList<CartItem> Items => _items;

        public void AddItem(Product product, decimal quantity)
            => _items.Add(new CartItem(product, quantity));

        private string PrintPrice(double price)
        {
            return price.ToString("N2", Culture);
        }
    }
}