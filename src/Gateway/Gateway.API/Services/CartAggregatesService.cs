using AutoMapper;
using Gateway.API.Auth.Models.Carts;
using Gateway.API.Interfaces;
using Gateway.API.Models;
using Gateway.Domain.Exceptions;

namespace Gateway.API.Services
{
    public class CartAggregatesService : ICartAggregatesService
    {
        private readonly IProductsService _productsService;
        private readonly ICartService _cartService;
        private readonly IMapper _mapper;

        public CartAggregatesService(IProductsService productsService,
            ICartService cartService, IMapper mapper)
        {
            _productsService = productsService;
            _cartService = cartService;
            this._mapper = mapper;
        }

        public async Task<(bool IsSuccess, dynamic FullCartDetails)> GetFullCartDetails(Guid customerId)
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

        public async Task<(bool IsSuccess, dynamic FullCartDetails)> ModifyCart(Guid customerId,
            UpdateCartRequestDTO newCartContents)
        {
            var productsAvailable = await _productsService.GetProductsAsync();
            var errors = new List<dynamic>();



            List<CartItem> newCartItems = new List<CartItem>(newCartContents.CartItems);


            foreach (var cartItem in newCartContents.CartItems)
            {
                var matchingProduct =
                    productsAvailable.Products.SingleOrDefault(p => p.Id.ToString() == cartItem.ProductId.ToString());

                if (matchingProduct is null)
                {
                    newCartItems.Remove(cartItem);

                    errors.Add(new
                    {
                        exception = new ProductDoesntExistException("Product doesn't exist"),
                        details = new
                        {
                            conflictingProduct = cartItem
                        }
                    });

                    continue;
                }


                if (matchingProduct.Quantity < cartItem.Quantity)
                {
                    errors.Add(new
                    {
                        product = matchingProduct,
                        exception = new InsufficientStockException("Insufficient stock"),
                        details = new
                        {
                            requestedQuantity = cartItem.Quantity,
                            available = matchingProduct.Quantity
                        }
                    });

                    cartItem.Quantity = matchingProduct.Quantity;
                }
            }

            newCartContents.CartItems = newCartItems;

            CartDetails newCart = _mapper.Map<UpdateCartRequestDTO, CartDetails>(newCartContents);

            newCart.CustomerId = customerId;

            var resultingContents = (await _cartService.UpdateCart(customerId, newCart)).CartDetails;

            return (true, new
            {
                errorsOccured = errors.Any(),
                cartDetails = resultingContents,
                errors = errors
            });
        }

    }
}
