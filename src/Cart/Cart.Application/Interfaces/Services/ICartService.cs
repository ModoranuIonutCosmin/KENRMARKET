using Cart.Domain.Models;
using IntegrationEvents.Models;
using CartDetails = Cart.Domain.Entities.CartDetails;

namespace Cart.Application.Interfaces.Services;

public interface ICartService
{
    Task<CartDetailsDto> GetCartDetails(Guid customerId);
    Task<CartDetailsDto> AddCartPromocode(Guid customerId, string promocode);
    Task<CartItemDTO>    AddCartItem(Guid customerId, ProductQuantity cartItem);
    Task<CartDetails>    ModifyCart(Guid customerId, CartDetailsDto newCartDetails);
    Task<CartDetails>    DeleteCartContents(Guid customerId);
    Task<CartDetails>    Checkout(Guid customerId, Address address);
}