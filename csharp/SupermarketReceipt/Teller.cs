using System.Collections.Generic;

using SupermarketReceipt.Domain.Carts;
using SupermarketReceipt.Domain.Offers;
using SupermarketReceipt.Domain.Prices;
using SupermarketReceipt.Domain.Products;

namespace SupermarketReceipt
{
    public class Teller
    {
        private readonly IPriceCatalog _catalog;
        private readonly Dictionary<Product, Offer> _offers = new Dictionary<Product, Offer>();

        public Teller(IPriceCatalog catalog)
        {
            _catalog = catalog;
        }

        public void AddSpecialOffer(SpecialOfferType offerType, Product product, double argument)
        {
            _offers[product] = new Offer(offerType, product, argument);
        }

        public Receipt ChecksOutArticlesFrom(ShoppingCart theCart)
        {
            var receipt = new Receipt();
            var productQuantities = theCart.GetItems();
            foreach (var pq in productQuantities)
            {
                var p = pq.Product;
                var quantity = pq.Quantity;
                var unitPrice = _catalog.GetUnitPrice(p);
                var price = quantity * unitPrice;
                receipt.AddProduct(p, quantity, unitPrice, price);
            }

            theCart.HandleOffers(receipt, _offers, _catalog);

            return receipt;
        }
    }
}