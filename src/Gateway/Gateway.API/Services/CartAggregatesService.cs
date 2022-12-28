using AutoMapper;
using Gateway.Application.Interfaces;
using Gateway.Domain.Exceptions;
using Gateway.Domain.Models.Carts;

namespace Gateway.API.Services;

public class CartAggregatesService : ICartAggregatesService
{
    private readonly ICartService     _cartService;
    private readonly IMapper          _mapper;
    private readonly IProductsService _productsService;

    public CartAggregatesService(IProductsService productsService,
        ICartService cartService, IMapper mapper)
    {
        _productsService = productsService;
        _cartService     = cartService;
        _mapper          = mapper;
    }

    public async Task<(bool IsSuccess, dynamic FullCartDetails)> GetFullCartDetails(Guid customerId)
    {
        var cartDetails = await _cartService.GetCartDetails(customerId);

        if (cartDetails.IsOk)
        {
            dynamic result = new CartDetailsFullDto(_mapper.Map<CartDetails, CartDetailsDTO>(cartDetails.CartDetails));

            return (true, result);
        }

        return (false, null);
    }

    public async Task<(bool IsSuccess, dynamic CartDetails)> AddItemToCart(Guid customerId,
        CartItemIdAndQuantity cartItemDto)
    {
        var productStatus = await _productsService.GetProductByIdAsync(cartItemDto.ProductId);
        var cartStatus    = await _cartService.GetCartDetails(customerId);

        var product = productStatus.Product;
        var cart    = cartStatus.CartDetails;

        var errors = new List<CartActionError>();

        if (!productStatus.IsOk || productStatus.Product == null ||
            !cartStatus.IsOk || cartStatus.CartDetails == null ||
            cartItemDto.Quantity == 0)
        {
            return (false, $"{productStatus.ErrorMessage ?? ""}{cartStatus.ErrorMessage ?? ""}");
        }

        var existentCartItem = cart.CartItems.SingleOrDefault(ci => ci.ProductId.Equals(cartItemDto.ProductId));

        if (existentCartItem == null)
        {
            existentCartItem = _mapper.Map<CartItemIdAndQuantity, CartItem>(cartItemDto);

            cart.CartItems.Add(existentCartItem);
        }
        else
        {
            existentCartItem.Quantity += cartItemDto.Quantity;
        }

        existentCartItem.ProductName    = product.Name;
        existentCartItem.PictureUrl     = product.PhotoUrl ?? "dummy.png";
        existentCartItem.UnitPrice      = product.Price;
        existentCartItem.AddedAt        = product.AddedDate;
        existentCartItem.Id             = Guid.Empty;
        existentCartItem.CartCustomerId = customerId;

        if (product.Quantity < existentCartItem.Quantity)
        {
            if (product.Quantity == 0)
            {
                cart.CartItems.Remove(existentCartItem);
            }
            else
            {
                existentCartItem.Quantity = product.Quantity;
            }


            errors.Add(
                       new CartActionError(
                                           new InsufficientStockException("Insufficient stock"),
                                           new CartActionInsufficientStocksDetailsDto(product.Id,
                                            product.Quantity,
                                            cartItemDto.Quantity)
                                          ));
        }

        cartStatus = await _cartService.UpdateCart(customerId, cart);


        return (cartStatus.IsOk,
                new CartUpdateStatusDto(errors.Any(), cartStatus.CartDetails, errors));
    }

    public async Task<(bool IsSuccess, dynamic FullCartDetails)> ModifyCart(Guid customerId,
        UpdateCartRequestDTO newCartContents)
    {
        var errors            = new List<CartActionError>();
        var productsAvailable = await _productsService.GetProductsAsync();
        var requestedProducts = newCartContents.CartItems;

        if (!productsAvailable.IsOk)
        {
            throw new HttpRequestException("Couldn't query products service");
        }

        requestedProducts.ForEach(e =>
        {
            if (!productsAvailable.Products.Any(p => p.Id.Equals(e.ProductId)) ||
                e.Quantity == 0)
            {
                errors.Add(new
                               CartActionError(new ProductDoesntExistException("Product doesn't exist or the requested quantity is 0"),
                                               new CartActionProductDoesntExistErrorDetailsDto(e.ProductId)));
            }
        });

        requestedProducts.RemoveAll(e => !productsAvailable.Products.Any(p => p.Id.Equals(e.ProductId)) ||
                                         e.Quantity == 0);

        var cartItems = productsAvailable.Products.Join(requestedProducts, pa => pa.Id, rp => rp.ProductId,
                                                        (pa, rp) =>
                                                        {
                                                            var quantity = rp.Quantity;

                                                            if (pa.Quantity < rp.Quantity)
                                                            {
                                                                errors.Add(
                                                                           new CartActionError(
                                                                                new
                                                                                    InsufficientStockException("Insufficient stock"),
                                                                                new
                                                                                    CartActionInsufficientStocksDetailsDto(pa.Id,
                                                                                     pa.Quantity,
                                                                                     rp.Quantity)
                                                                               ));

                                                                quantity = pa.Quantity;
                                                            }


                                                            return new CartItem
                                                                   {
                                                                       ProductId   = pa.Id,
                                                                       Id          = Guid.Empty,
                                                                       Quantity    = quantity,
                                                                       AddedAt     = DateTimeOffset.UtcNow,
                                                                       PictureUrl  = pa.PhotoUrl,
                                                                       ProductName = pa.Name,
                                                                       UnitPrice   = pa.Price
                                                                   };
                                                        });


        var cartDetails = new CartDetails
                          {
                              CartItems  = cartItems.ToList(),
                              CustomerId = customerId,
                              Promocode  = newCartContents.Promocode
                          };

        var cartStatus = await _cartService.UpdateCart(customerId, cartDetails);

        if (cartStatus.IsOk)
        {
            return (cartStatus.IsOk,
                    new CartUpdateStatusDto(errors.Any(), cartStatus.CartDetails, errors));
        }

        return (false, cartStatus.ErrorMessage);
    }
}