using SupermarketReceipt.Domain.Prices;

namespace SupermarketReceipt.Domain.Offers
{
    /// <summary>
    /// Creates an offer calculator based on offer type
    /// </summary>
    public class OfferCalculatorFactory(IPriceCatalog priceCatalog) : IOfferCalculatorFactory
    {
        private readonly IPriceCatalog _priceCatalog = priceCatalog;

        public IOfferCalculator Build(SpecialOfferType specialOfferType)
        {
            return specialOfferType switch
            {
                SpecialOfferType.ThreeForTwo => new ThreeForTwoOfferCalculator(_priceCatalog),
                SpecialOfferType.TenPercentDiscount => new TenPercentDiscountCalculator(_priceCatalog),
                SpecialOfferType.TwoForAmount => new TwoForAmountOfferCalculator(_priceCatalog),
                SpecialOfferType.FiveForAmount => new FiveForAmountOfferCalculator(_priceCatalog),
                _ => null,
            };
        }
    }
}