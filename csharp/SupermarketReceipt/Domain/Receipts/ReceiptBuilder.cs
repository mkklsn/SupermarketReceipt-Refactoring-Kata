using System;
using System.Linq;
using SupermarketReceipt.Domain.Carts;
using SupermarketReceipt.Domain.Offers;
using SupermarketReceipt.Domain.Prices;
using SupermarketReceipt.Domain.Products;

namespace SupermarketReceipt.Domain.Receipts
{
    public interface IReceiptBuilder
    {
        public Receipt Build(ShoppingCart shoppingCart);
    }

    public class ReceiptBuilder : IReceiptBuilder
    {
        private readonly IPriceCatalog _priceCatalog;
        private readonly IOfferCatalog _offerCatalog;
        private readonly IOfferCalculatorFactory _offerCalculatorFactory;

        public ReceiptBuilder(IPriceCatalog priceCatalog, IOfferCatalog offerCatalog, IOfferCalculatorFactory offerCalculatorFactory)
        {
            _priceCatalog = priceCatalog;
            _offerCatalog = offerCatalog;
            _offerCalculatorFactory = offerCalculatorFactory;
        }

        public Receipt Build(ShoppingCart shoppingCart)
        {
            var receipt = new Receipt();

            foreach (var item in shoppingCart.Items)
            {
                var unitPrice = _priceCatalog.GetUnitPrice(item.Product);
                var totalPrice = Math.Round(unitPrice * item.Quantity, 2, MidpointRounding.ToZero);

                receipt.AddItem(item.Product, item.Quantity, unitPrice, totalPrice);
            }

            CalculateDiscounts(receipt);

            return receipt;
        }

        public void CalculateDiscounts(Receipt receipt)
        {
            var groups = receipt.Items.GroupBy(i => i.Product);

            foreach (IGrouping<Product, ReceiptItem> group in groups)
            {
                var offer = _offerCatalog.GetOffer(group.Key);
                if (offer == null) continue;

                var calculator = _offerCalculatorFactory.Build(offer.OfferType);
                if (calculator == null) continue;

                var discount = calculator.CalculateDiscount(group, offer);
                if (discount == null) continue;

                receipt.AddDiscount(discount);
            }
        }
    }
}