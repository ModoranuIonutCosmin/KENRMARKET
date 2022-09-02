using Gateway.API.Interfaces;
using Gateway.API.Models;

namespace Gateway.API.Services
{
    public class CartAggregatesService : ICartAggregatesService
    {
        private readonly IProductsService _productsService;
        private readonly ICartService _cartService;

        public CartAggregatesService(IProductsService productsService,
            ICartService cartService)
        {
            _productsService = productsService;
            _cartService = cartService;
        }

        public async Task<(bool IsSuccess, dynamic FullCartDetails)> GetFullCartDetails(string customerId)
        {
            var cartDetails = await _cartService.GetCartDetails(customerId);
            var productsAvailable = await _productsService.GetProductsAsync();


            if (cartDetails.IsOk)
            {
                foreach (CartItem cartItem in cartDetails.CartDetails.CartItems)
                {
                    var product = productsAvailable.Products
                        .FirstOrDefault(p => p.Id == cartItem.ProductId);

                    cartItem.PictureUrl = product != null
                        ? product.PhotoUrl
                        : "dummy.png";
                    cartItem.ProductName = product != null
                        ? product.Name
                        : "Product name is not available!";
                    cartItem.UnitPrice = product != null
                        ? product.Price
                        : cartItem.UnitPrice;
                }

                dynamic result = new
                {
                    CartDetails = cartDetails.CartDetails,
                };

                return (true, result);
            }

            return (false, null);
        }
    }
}
