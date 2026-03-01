using System;
using System.Linq;
using SupermarketReceipt.Domain.Prices;
using SupermarketReceipt.Domain.Products;
using SupermarketReceipt.Domain.Receipts;

namespace SupermarketReceipt.Domain.Offers
{
    public class FiveForAmountOfferCalculator : IOfferCalculator
    {
        private readonly IPriceCatalog _priceCatalog;

        public FiveForAmountOfferCalculator(IPriceCatalog priceCatalog)
        {
            _priceCatalog = priceCatalog;
        }

        public Discount CalculateDiscount(IGrouping<Product, ReceiptItem> group, Offer offer)
        {
            var totalQuantity = group.Sum(x => x.Quantity);
            var unitPrice = _priceCatalog.GetUnitPrice(group.Key);

            if (totalQuantity < 5) return null;
            var offerSetCount = Math.Round(totalQuantity / 5, MidpointRounding.ToZero);
            var totalDiscount = (totalQuantity * unitPrice) - (offerSetCount * offer.Argument) - (totalQuantity % 5 * unitPrice);

            var discount = new Discount(group.Key, "5 for " + offer.Argument, totalDiscount * -1m);

            return discount;
        }
    }
}