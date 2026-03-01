using System.Linq;
using SupermarketReceipt.Domain.Products;
using SupermarketReceipt.Domain.Receipts;

namespace SupermarketReceipt.Domain.Offers
{
    public interface IOfferCalculator
    {
        Discount CalculateDiscount(IGrouping<Product, ReceiptItem> group, Offer offer);
    }
}