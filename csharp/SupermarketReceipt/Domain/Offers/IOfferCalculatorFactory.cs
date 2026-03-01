namespace SupermarketReceipt.Domain.Offers
{
    public interface IOfferCalculatorFactory
    {
        IOfferCalculator Build(SpecialOfferType specialOfferType);
    }
}