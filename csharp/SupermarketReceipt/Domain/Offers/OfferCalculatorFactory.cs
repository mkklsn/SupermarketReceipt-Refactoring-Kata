using SupermarketReceipt.Domain.Prices;

namespace SupermarketReceipt.Domain.Offers
{
    public class OfferCalculatorFactory : IOfferCalculatorFactory
    {
        private readonly IPriceCatalog _priceCatalog;

        public OfferCalculatorFactory(IPriceCatalog priceCatalog)
        {
            _priceCatalog = priceCatalog;
        }

        public IOfferCalculator Build(SpecialOfferType specialOfferType)
        {
            switch (specialOfferType)
            {
                case SpecialOfferType.ThreeForTwo:
                    return new ThreeForTwoOfferCalculator(_priceCatalog);
                case SpecialOfferType.TenPercentDiscount:
                    return new TenPercentDiscountCalculator(_priceCatalog);
                case SpecialOfferType.TwoForAmount:
                    return new TwoForAmountOfferCalculator(_priceCatalog);
                case SpecialOfferType.FiveForAmount:
                    return new FiveForAmountOfferCalculator(_priceCatalog);
                default:
                    return null;
            }
        }
    }
}