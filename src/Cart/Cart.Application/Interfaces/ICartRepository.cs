using Cart.Domain.Entities;

namespace Cart.Application.Interfaces;

public interface ICartRepository
{
    Task<CartDetails> GetCartDetails(Guid customerId);
    Task AddCartItem(Guid customerId, CartItem cartItem);
    Task<CartDetails> EnsureCartExists(Guid customerId);
    Task<CartDetails> SetCartPromocode(Guid customerId, string promocode);
    Task<CartDetails> ModifyCart(Guid customerId, CartDetails newCartDetails);
    Task UpdateCartItem(Guid cartId, CartItem newCartItem);
}