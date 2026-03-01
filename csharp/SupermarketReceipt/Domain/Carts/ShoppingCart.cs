using System.Collections.Generic;
using SupermarketReceipt.Domain.Products;

namespace SupermarketReceipt.Domain.Carts
{
    /// <summary>
    /// A container for a collection of <see cref="CartItem"/>
    /// </summary>
    public class ShoppingCart
    {
        private readonly List<CartItem> _items = [];
        public IReadOnlyList<CartItem> Items => _items;

        public void AddItem(Product product, decimal quantity)
            => _items.Add(new CartItem(product, quantity));
    }
}