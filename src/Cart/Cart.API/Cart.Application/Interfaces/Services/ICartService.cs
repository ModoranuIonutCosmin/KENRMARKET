using Cart.Domain.Entities;
using Cart.Domain.Models;

namespace Cart.Application.Interfaces.Services
{
    public interface ICartService
    {
        Task<CartDetails> GetCartDetails(string customerId);
        Task<CartDetails> AddCartPromocode(string customerId, string promocode);
        Task<CartItem> AddCartItem(string customerId, CartItemViewModel cartItem);
    }
}

