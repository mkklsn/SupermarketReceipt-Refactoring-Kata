using SupermarketReceipt.Domain.Carts;
using SupermarketReceipt.Domain.Prices;

namespace SupermarketReceipt.Domain.Receipts
{
    public interface IReceiptBuilder
    {
        public Receipt Build(ShoppingCart shoppingCart);
    }

    public class ReceiptBuilder : IReceiptBuilder
    {
        private readonly IPriceCatalog _priceCatalog;

        public ReceiptBuilder(IPriceCatalog priceCatalog)
        {
            _priceCatalog = priceCatalog;
        }

        public Receipt Build(ShoppingCart shoppingCart)
        {
            var receipt = new Receipt();

            foreach (var item in shoppingCart.Items)
            {
                var unitPrice = _priceCatalog.GetUnitPrice(item.Product);
                var totalPrice = unitPrice * item.Quantity;

                receipt.AddItem(item.Product, item.Quantity, unitPrice, totalPrice);
            }

            return receipt;
        }
    }
}