using System;
using SupermarketReceipt.Domain.Products;

namespace SupermarketReceipt.Domain.Carts
{
    /// <summary>
    /// An item in the <see cref="ShoppingCart"/>
    /// </summary>
    public class CartItem
    {
        public Product Product { get; }
        public decimal Quantity { get; }

        public CartItem(Product product, decimal quantity)
        {
            Product = product ?? throw new ArgumentNullException(nameof(product));
            if (quantity <= 0) throw new ArgumentOutOfRangeException(nameof(quantity));
            Quantity = quantity;
        }
    }
}