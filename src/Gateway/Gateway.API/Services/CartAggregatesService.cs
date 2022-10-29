using AutoMapper;
using Gateway.API.Interfaces;
using Gateway.Domain.Exceptions;
using Gateway.Domain.Models.Carts;

namespace Gateway.API.Services;

public class CartAggregatesService : ICartAggregatesService
{
    private readonly ICartService _cartService;
    private readonly IMapper _mapper;
    private readonly IProductsService _productsService;

    public CartAggregatesService(IProductsService productsService,
        ICartService cartService, IMapper mapper)
    {
        _productsService = productsService;
        _cartService = cartService;
        _mapper = mapper;
    }

    public async Task<(bool IsSuccess, dynamic FullCartDetails)> GetFullCartDetails(Guid customerId)
    {
        var cartDetails = await _cartService.GetCartDetails(customerId);

        if (cartDetails.IsOk)
        {
            dynamic result = new
            {
                CartDetails = _mapper.Map<CartDetails, CartDetailsDTO>(cartDetails.CartDetails)
            };

            return (true, result);
        }

        return (false, null);
    }

    public async Task<(bool IsSuccess, dynamic CartDetails)> AddItemToCart(Guid customerId, CartItemDTO cartItemDto)
    {
        var productStatus = await _productsService.GetProductByIdAsync(cartItemDto.ProductId);
        var cartStatus = await _cartService.GetCartDetails(customerId);

        var product = productStatus.Product;
        var cart = cartStatus.CartDetails;

        var errors = new List<dynamic>();

        if (!productStatus.IsOk || productStatus.Product == null ||
            !cartStatus.IsOk || cartStatus.CartDetails == null)
            return (false, null);

        var existentCartItem = cart.CartItems.SingleOrDefault(ci => ci.ProductId.Equals(cartItemDto.ProductId));

        if (existentCartItem == null)
        {
            existentCartItem = _mapper.Map<CartItemDTO, CartItem>(cartItemDto);

            cart.CartItems.Add(existentCartItem);
        }

        existentCartItem.ProductName = product.Name;
        existentCartItem.PictureUrl = product.PhotoUrl ?? "dummy.png";

        existentCartItem.UnitPrice = product.Price;
        existentCartItem.AddedAt = product.AddedDate;
        existentCartItem.Quantity = existentCartItem.Quantity + cartItemDto.Quantity;
        existentCartItem.Id = Guid.Empty;

        if (product.Quantity < existentCartItem.Quantity)
        {
            existentCartItem.Quantity = product.Quantity;

            errors.Add(new
            {
                product,
                exception = new InsufficientStockException("Insufficient stock"),
                details = new
                {
                    requestedQuantity = existentCartItem.Quantity + cartItemDto.Quantity,
                    available = product.Quantity
                }
            });
        }

        cartStatus = await _cartService.UpdateCart(customerId, cart);


        return (cartStatus.IsOk, new
        {
            hasErrors = errors.Any(),
            cartDetails = cartStatus.CartDetails,
            errors
        });
    }

    public async Task<(bool IsSuccess, dynamic FullCartDetails)> ModifyCart(Guid customerId,
        UpdateCartRequestDTO newCartContents)
    {
        var productsAvailable = await _productsService.GetProductsAsync();
        var errors = new List<dynamic>();

        var newCartItems = new List<CartItemDTO>(newCartContents.CartItems);


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

            cartItem.PictureUrl = matchingProduct.PhotoUrl ?? "dummy.png";
            cartItem.UnitPrice = matchingProduct.Price;
            cartItem.ProductName = matchingProduct.Name;
        }

        newCartContents.CartItems = newCartItems;

        var newCart = _mapper.Map<UpdateCartRequestDTO, CartDetails>(newCartContents);

        newCart.CustomerId = customerId;


        //TODO: Studiat (si pictureUrl trebuie)
        newCart.CartItems.ForEach(ci => ci.Id = Guid.Empty);

        var resultingContents = (await _cartService.UpdateCart(customerId, newCart)).CartDetails;

        return (true, new
        {
            errorsOccured = errors.Any(),
            cartDetails = resultingContents,
            errors
        });
    }
}