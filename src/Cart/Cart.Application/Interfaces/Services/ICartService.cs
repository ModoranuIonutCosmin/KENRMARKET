using Cart.Domain.Models;
using IntegrationEvents.Models;
using CartDetails = Cart.Domain.Entities.CartDetails;

namespace Cart.Application.Interfaces.Services;

public interface ICartService
{
    Task<CartDetailsViewModel> GetCartDetails(Guid customerId);
    Task<CartDetailsViewModel> AddCartPromocode(Guid customerId, string promocode);
    Task<CartItemDTO> AddCartItem(Guid customerId, CartItemDTO cartItem);
    Task<CartDetails> ModifyCart(Guid customerId, CartDetails newCartDetails);
    Task<CartDetails> DeleteCartContents(Guid customerId);
    Task<CartDetails> Checkout(Guid customerId, Address address);
}