using Cart.Domain.Entities;

namespace Cart.Application.Interfaces
{
    public interface ICartRepository
    {
        Task<CartDetails> GetCartDetails(string customerId);
        Task AddCartItem(CartItem cartItem);
        Task<CartDetails> EnsureCartExists(string customerId);

        Task<CartDetails> SetCartPromocode(string customerId, string promocode);
    }
}

