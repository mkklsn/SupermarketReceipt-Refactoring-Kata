using System.Linq;
using SupermarketReceipt.Domain.Prices;
using SupermarketReceipt.Domain.Products;
using SupermarketReceipt.Domain.Receipts;

namespace SupermarketReceipt.Domain.Offers
{
    /// <summary>
    /// Calculates a discount based on a percentage of a product's price
    /// </summary>
    public class TenPercentDiscountCalculator : IOfferCalculator
    {
        private readonly IPriceCatalog _priceCatalog;

        public TenPercentDiscountCalculator(IPriceCatalog priceCatalog)
        {
            _priceCatalog = priceCatalog;
        }

        public Discount CalculateDiscount(IGrouping<Product, ReceiptItem> group, Offer offer)
        {
            var totalQuantity = group.Sum(x => x.Quantity);
            var unitPrice = _priceCatalog.GetUnitPrice(group.Key.Id);

            var totalDiscount = totalQuantity * unitPrice * -0.1m;
            var discount = new Discount(group.Key, (int)offer.Argument + "% off", totalDiscount);

            return discount;
        }
    }
}