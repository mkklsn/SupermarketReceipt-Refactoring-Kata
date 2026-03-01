using System;
using System.Linq;
using SupermarketReceipt.Domain.Prices;
using SupermarketReceipt.Domain.Products;
using SupermarketReceipt.Domain.Receipts;

namespace SupermarketReceipt.Domain.Offers
{
    public class ThreeForTwoOfferCalculator : IOfferCalculator
    {
        private readonly IPriceCatalog _priceCatalog;

        public ThreeForTwoOfferCalculator(IPriceCatalog priceCatalog)
        {
            _priceCatalog = priceCatalog;
        }

        public Discount CalculateDiscount(IGrouping<Product, ReceiptItem> group, Offer offer)
        {
            var totalQuantity = group.Sum(x => x.Quantity);
            var unitPrice = _priceCatalog.GetUnitPrice(group.Key);

            var offerSetCount = Math.Round(totalQuantity / 3, MidpointRounding.ToZero);
            var totalDiscount = offerSetCount * unitPrice * -1m;

            var discount = new Discount(group.Key, "3 for 2", totalDiscount);

            return discount;
        }
    }
}