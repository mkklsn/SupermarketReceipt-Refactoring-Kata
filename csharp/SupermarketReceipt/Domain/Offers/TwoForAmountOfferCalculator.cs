using System;
using System.Linq;
using SupermarketReceipt.Domain.Prices;
using SupermarketReceipt.Domain.Products;
using SupermarketReceipt.Domain.Receipts;

namespace SupermarketReceipt.Domain.Offers
{
    public class TwoForAmountOfferCalculator : IOfferCalculator
    {
        private readonly IPriceCatalog _priceCatalog;

        public TwoForAmountOfferCalculator(IPriceCatalog priceCatalog)
        {
            _priceCatalog = priceCatalog;
        }

        public Discount CalculateDiscount(IGrouping<Product, ReceiptItem> group, Offer offer)
        {
            var totalQuantity = group.Sum(x => x.Quantity);
            var unitPrice = _priceCatalog.GetUnitPrice(group.Key);

            if (totalQuantity < 2) return null;
            var offerSetCount = Math.Round(totalQuantity / 2, MidpointRounding.ToZero);
            var totalDiscount = (offerSetCount * offer.Argument) - (totalQuantity * unitPrice);

            var discount = new Discount(group.Key, "2 for " + offer.Argument, totalDiscount);

            return discount;
        }
    }
}