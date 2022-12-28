using Cart.Application.Interfaces.Base;
using Cart.Domain.Entities;

namespace Cart.Application.Interfaces;

public interface ICartRepository : IRepository<CartDetails, Guid>
{
    Task<CartDetails> GetCartDetails(Guid customerId);
    Task              AddCartItem(Guid customerId, CartItem cartItem);
    Task              EnsureCartExists(Guid customerId);
    Task<CartDetails> SetCartPromocode(Guid customerId, string promocode);
    Task<CartDetails> ModifyCart(Guid customerId, CartDetails newCartDetails);
    Task              UpdateCartItem(Guid cartId, CartItem newCartItem);
    Task<CartDetails> DeleteCartContents(Guid customerId);
}