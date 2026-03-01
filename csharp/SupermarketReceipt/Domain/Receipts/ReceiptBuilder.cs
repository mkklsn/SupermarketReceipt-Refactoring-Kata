using System;
using System.Linq;
using SupermarketReceipt.Domain.Carts;
using SupermarketReceipt.Domain.Offers;
using SupermarketReceipt.Domain.Prices;
using SupermarketReceipt.Domain.Products;

namespace SupermarketReceipt.Domain.Receipts
{
    /// <summary>
    /// Provides an interface for building a <see cref="Receipt"/> 
    /// </summary>
    public interface IReceiptBuilder
    {
        public Receipt Build(ShoppingCart shoppingCart);
    }

    /// <summary>
    /// Builds a <see cref="Receipt"/> 
    /// </summary>
    public class ReceiptBuilder(IPriceCatalog priceCatalog, IOfferCatalog offerCatalog, IOfferCalculatorFactory offerCalculatorFactory) : IReceiptBuilder
    {
        private readonly IPriceCatalog _priceCatalog = priceCatalog;
        private readonly IOfferCatalog _offerCatalog = offerCatalog;
        private readonly IOfferCalculatorFactory _offerCalculatorFactory = offerCalculatorFactory;

        public Receipt Build(ShoppingCart shoppingCart)
        {
            var receipt = new Receipt();

            foreach (var item in shoppingCart.Items)
            {
                var unitPrice = _priceCatalog.GetUnitPrice(item.Product.Id);
                var totalPrice = Math.Round(unitPrice * item.Quantity, 2, MidpointRounding.ToZero);

                receipt.AddItem(item.Product, item.Quantity, unitPrice, totalPrice);
            }

            ApplyDiscountIfAny(receipt);

            return receipt;
        }

        public void ApplyDiscountIfAny(Receipt receipt)
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