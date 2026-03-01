using System;
using System.Linq;
using SupermarketReceipt.Domain.Prices;
using SupermarketReceipt.Domain.Products;
using SupermarketReceipt.Domain.Receipts;

namespace SupermarketReceipt.Domain.Offers
{
    /// <summary>
    /// Calculates a discount based on a product and quantity (5) constraint
    /// </summary>
    public class FiveForAmountOfferCalculator(IPriceCatalog priceCatalog) : IOfferCalculator
    {
        private readonly IPriceCatalog _priceCatalog = priceCatalog;

        public Discount CalculateDiscount(IGrouping<Product, ReceiptItem> group, Offer offer)
        {
            var totalQuantity = group.Sum(x => x.Quantity);
            var unitPrice = _priceCatalog.GetUnitPrice(group.Key.Id);

            if (totalQuantity < 5) return null;
            var offerSetCount = Math.Round(totalQuantity / 5, MidpointRounding.ToZero);
            var totalDiscount = (totalQuantity * unitPrice) - (offerSetCount * offer.Argument) - (totalQuantity % 5 * unitPrice);

            var discount = new Discount(group.Key, "5 for " + offer.Argument, totalDiscount * -1m);

            return discount;
        }
    }
}