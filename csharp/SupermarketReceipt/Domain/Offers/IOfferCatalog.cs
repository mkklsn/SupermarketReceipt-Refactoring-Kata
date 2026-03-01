using SupermarketReceipt.Domain.Products;

namespace SupermarketReceipt.Domain.Offers
{
    /// <summary>
    /// Provides an interface for storing and retrieving offers
    /// </summary>
    public interface IOfferCatalog
    {
        void AddOffer(Offer offer);
        Offer GetOffer(Product product);
    }
}