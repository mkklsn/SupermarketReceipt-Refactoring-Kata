using SupermarketReceipt.Domain.Products;

namespace SupermarketReceipt.Domain.Offers
{
    /// <summary>
    /// Contains a product and offer type and offer detail
    /// </summary>
    public class Offer(SpecialOfferType offerType, Product product, decimal argument)
    {
        public SpecialOfferType OfferType { get; } = offerType;
        public decimal Argument { get; } = argument;
        public Product Product { get; } = product;
    }
}