using SupermarketReceipt.Domain.Products;

namespace SupermarketReceipt.Domain.Offers
{
    public interface IOfferCatalog
    {
        void AddOffer(Offer offer);
        Offer GetOffer(Product product);
    }
}