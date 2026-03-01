using System.Linq;
using SupermarketReceipt.Domain.Products;
using SupermarketReceipt.Domain.Receipts;

namespace SupermarketReceipt.Domain.Offers
{
    /// <summary>
    /// Provides an interface to calculate a discount
    /// </summary>
    public interface IOfferCalculator
    {
        Discount CalculateDiscount(IGrouping<Product, ReceiptItem> group, Offer offer);
    }
}