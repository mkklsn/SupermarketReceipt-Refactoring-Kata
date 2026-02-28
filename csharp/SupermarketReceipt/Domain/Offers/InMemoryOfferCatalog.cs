using System.Collections.Generic;
using SupermarketReceipt.Domain.Products;

namespace SupermarketReceipt.Domain.Offers
{
    public class InMemoryOfferCatalog : IOfferCatalog
    {
        private readonly Dictionary<Product, Offer> _productOfferMap;

        public InMemoryOfferCatalog()
        {
            _productOfferMap = [];
        }

        public void AddOffer(Offer offer)
        {
            _productOfferMap[offer.Product] = offer;
        }

        public Offer GetOffer(Product product)
        {
            return _productOfferMap[product];
        }
    }
}