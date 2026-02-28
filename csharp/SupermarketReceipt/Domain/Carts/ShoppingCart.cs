using System.Collections.Generic;
using System.Globalization;
using SupermarketReceipt.Domain.Products;

namespace SupermarketReceipt.Domain.Carts
{
    public class ShoppingCart
    {
        private readonly List<CartItem> _items = [];
        public IReadOnlyList<CartItem> Items => _items;

        public void AddItem(Product product, decimal quantity)
            => _items.Add(new CartItem(product, quantity));
    }
}