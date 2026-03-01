using System.Collections.Generic;

using SupermarketReceipt.Domain.Carts;
using SupermarketReceipt.Domain.Offers;
using SupermarketReceipt.Domain.Prices;
using SupermarketReceipt.Domain.Products;
using SupermarketReceipt.Domain.Receipts;

namespace SupermarketReceipt.Application.Checkout
{
    public interface ICheckoutService
    {
        public Receipt Checkout(ShoppingCart shoppingCart);
    }

    public class CheckoutService(IReceiptBuilder receiptBuilder) : ICheckoutService
    {
        private readonly IReceiptBuilder _receiptBuilder = receiptBuilder;

        public Receipt Checkout(ShoppingCart shoppingCart)
        {
            return _receiptBuilder.Build(shoppingCart);
        }
    }
}