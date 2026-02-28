using System;
using System.Linq;
using SupermarketReceipt.Domain.Carts;
using SupermarketReceipt.Domain.Offers;
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
        private readonly IOfferCatalog _offerCatalog;

        public ReceiptBuilder(IPriceCatalog priceCatalog, IOfferCatalog offerCatalog)
        {
            _priceCatalog = priceCatalog;
            _offerCatalog = offerCatalog;
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

            foreach (var group in groups)
            {
                var offer = _offerCatalog.GetOffer(group.Key);
                if (offer == null) continue;

                var totalQuantity = group.Sum(x => x.Quantity);
                var unitPrice = _priceCatalog.GetUnitPrice(group.Key);
                decimal totalDiscount;
                Discount discount;
                switch (offer.OfferType)
                {
                    case SpecialOfferType.ThreeForTwo:
                        var offerSetCount = Math.Round(totalQuantity / 3, MidpointRounding.ToZero);
                        totalDiscount = offerSetCount * unitPrice * -1m;

                        discount = new Discount(group.Key, "3 for 2", totalDiscount);
                        receipt.AddDiscount(discount);
                        break;
                    case SpecialOfferType.TenPercentDiscount:
                        totalDiscount = totalQuantity * unitPrice * -0.1m;

                        discount = new Discount(group.Key, (int)offer.Argument + "% off", totalDiscount);
                        receipt.AddDiscount(discount);
                        break;
                }
            }
        }
    }
}