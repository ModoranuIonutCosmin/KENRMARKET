using Cart.Domain.Entities;
using Cart.Domain.Models;

namespace Cart.Application.Interfaces.Services
{
    public interface ICartService
    {
        Task<CartDetailsViewModel> GetCartDetails(string customerId);
        Task<CartDetailsViewModel> AddCartPromocode(string customerId, string promocode);
        Task<CartItemViewModel> AddCartItem(string customerId, CartItemViewModel cartItem);
    }
}

