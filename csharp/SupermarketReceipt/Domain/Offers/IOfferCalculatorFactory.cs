namespace SupermarketReceipt.Domain.Offers
{
    /// <summary>
    /// Provides an interface to build an offer calculator
    /// </summary>
    public interface IOfferCalculatorFactory
    {
        IOfferCalculator Build(SpecialOfferType specialOfferType);
    }
}