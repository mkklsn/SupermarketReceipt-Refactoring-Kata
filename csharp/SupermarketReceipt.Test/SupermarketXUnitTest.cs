
using System.Threading.Tasks;
using VerifyXunit;
using Xunit;

using SupermarketReceipt.Domain.Carts;
using SupermarketReceipt.Domain.Offers;
using SupermarketReceipt.Domain.Prices;
using SupermarketReceipt.Domain.Products;
using SupermarketReceipt.Domain.Receipts;
using SupermarketReceipt.Application.Checkout;

namespace SupermarketReceipt.Test
{
    [UsesVerify]
    public class SupermarketXUnitTest
    {
        private IPriceCatalog _catalog;
        private IOfferCatalog _offerCatalog;
        private ICheckoutService _checkoutService;
        private IReceiptBuilder _receiptBuilder;
        private ShoppingCart _theCart;
        private Product _toothbrush;
        private Product _rice;
        private Product _apples;
        private Product _cherryTomatoes;

        public SupermarketXUnitTest()
        {
            _catalog = new InMemoryPriceCatalog();
            _offerCatalog = new InMemoryOfferCatalog();
            _receiptBuilder = new ReceiptBuilder(_catalog);
            _checkoutService = new CheckoutService(_receiptBuilder);
            _theCart = new ShoppingCart();

            _toothbrush = new Product("toothbrush", ProductUnit.Each);
            _catalog.AddProduct(_toothbrush, 0.99m);
            _rice = new Product("rice", ProductUnit.Each);
            _catalog.AddProduct(_rice, 2.99m);
            _apples = new Product("apples", ProductUnit.Kilo);
            _catalog.AddProduct(_apples, 1.99m);
            _cherryTomatoes = new Product("cherry tomato box", ProductUnit.Each);
            _catalog.AddProduct(_cherryTomatoes, 0.69m);

        }

        [Fact]
        public Task an_empty_shopping_cart_should_cost_nothing()
        {
            Receipt receipt = _checkoutService.Checkout(_theCart);
            return Verifier.Verify(new ReceiptPrinter(40).PrintReceipt(receipt));
        }

        [Fact]
        public Task one_normal_item()
        {
            _theCart.AddItem(_toothbrush, 1.0m);
            Receipt receipt = _checkoutService.Checkout(_theCart);
            return Verifier.Verify(new ReceiptPrinter(40).PrintReceipt(receipt));
        }

        [Fact]
        public Task two_normal_items()
        {
            _theCart.AddItem(_toothbrush, 1.0m);
            _theCart.AddItem(_rice, 1.0m);
            Receipt receipt = _checkoutService.Checkout(_theCart);
            return Verifier.Verify(new ReceiptPrinter(40).PrintReceipt(receipt));
        }

        [Fact]
        public Task buy_two_get_one_free()
        {
            _theCart.AddItem(_toothbrush, 1.0m);
            _theCart.AddItem(_toothbrush, 1.0m);
            _theCart.AddItem(_toothbrush, 1.0m);
            _offerCatalog.AddOffer(new Offer(SpecialOfferType.ThreeForTwo, _toothbrush, _catalog.GetUnitPrice(_toothbrush)));
            Receipt receipt = _checkoutService.Checkout(_theCart);
            return Verifier.Verify(new ReceiptPrinter(40).PrintReceipt(receipt));
        }

        [Fact]
        public Task buy_five_get_one_free()
        {
            _theCart.AddItem(_toothbrush, 1.0m);
            _theCart.AddItem(_toothbrush, 1.0m);
            _theCart.AddItem(_toothbrush, 1.0m);
            _theCart.AddItem(_toothbrush, 1.0m);
            _theCart.AddItem(_toothbrush, 1.0m);
            _offerCatalog.AddOffer(new Offer(SpecialOfferType.ThreeForTwo, _toothbrush, _catalog.GetUnitPrice(_toothbrush)));
            Receipt receipt = _checkoutService.Checkout(_theCart);
            return Verifier.Verify(new ReceiptPrinter(40).PrintReceipt(receipt));
        }

        [Fact]
        public Task loose_weight_product()
        {
            _theCart.AddItem(_apples, 0.5m);
            Receipt receipt = _checkoutService.Checkout(_theCart);
            return Verifier.Verify(new ReceiptPrinter(40).PrintReceipt(receipt));
        }

        [Fact]
        public Task percent_discount()
        {
            _theCart.AddItem(_rice, 1.0m);
            _offerCatalog.AddOffer(new Offer(SpecialOfferType.TenPercentDiscount, _rice, 10.0m));
            Receipt receipt = _checkoutService.Checkout(_theCart);
            return Verifier.Verify(new ReceiptPrinter(40).PrintReceipt(receipt));
        }

        [Fact]
        public Task xForY_discount()
        {
            _theCart.AddItem(_cherryTomatoes, 1.0m);
            _theCart.AddItem(_cherryTomatoes, 1.0m);
            _offerCatalog.AddOffer(new Offer(SpecialOfferType.TwoForAmount, _cherryTomatoes, 0.99m));
            Receipt receipt = _checkoutService.Checkout(_theCart);
            return Verifier.Verify(new ReceiptPrinter(40).PrintReceipt(receipt));
        }

        [Fact]
        public Task FiveForY_discount()
        {
            _theCart.AddItem(_apples, 5.0m);
            _offerCatalog.AddOffer(new Offer(SpecialOfferType.FiveForAmount, _apples, 6.99m));
            Receipt receipt = _checkoutService.Checkout(_theCart);
            return Verifier.Verify(new ReceiptPrinter(40).PrintReceipt(receipt));
        }

        [Fact]
        public Task FiveForY_discount_withSix()
        {
            _theCart.AddItem(_apples, 6.0m);
            _offerCatalog.AddOffer(new Offer(SpecialOfferType.FiveForAmount, _apples, 6.99m));
            Receipt receipt = _checkoutService.Checkout(_theCart);
            return Verifier.Verify(new ReceiptPrinter(40).PrintReceipt(receipt));
        }

        [Fact]
        public Task FiveForY_discount_withSixteen()
        {
            _theCart.AddItem(_apples, 16.0m);
            _offerCatalog.AddOffer(new Offer(SpecialOfferType.FiveForAmount, _apples, 6.99m));
            Receipt receipt = _checkoutService.Checkout(_theCart);
            return Verifier.Verify(new ReceiptPrinter(40).PrintReceipt(receipt));
        }

        [Fact]
        public Task FiveForY_discount_withFour()
        {
            _theCart.AddItem(_apples, 4.0m);
            _offerCatalog.AddOffer(new Offer(SpecialOfferType.FiveForAmount, _apples, 6.99m));
            Receipt receipt = _checkoutService.Checkout(_theCart);
            return Verifier.Verify(new ReceiptPrinter(40).PrintReceipt(receipt));
        }
    }
}