using SupermarketReceipt.Domain.Products;

namespace SupermarketReceipt.Domain.Offers
{
    public class Offer
    {
        public Offer(SpecialOfferType offerType, Product product, decimal argument)
        {
            OfferType = offerType;
            Argument = argument;
            Product = product;
        }

        public SpecialOfferType OfferType { get; }
        public decimal Argument { get; }
        public Product Product { get; }
    }
}