using Cart.Domain.Entities;
using Cart.Domain.Models;

namespace Cart.Application.Interfaces.Services
{
    public interface ICartService
    {
        Task<CartDetailsViewModel> GetCartDetails(Guid customerId);
        Task<CartDetailsViewModel> AddCartPromocode(Guid customerId, string promocode);
        Task<CartItemViewModel> AddCartItem(Guid customerId, CartItemViewModel cartItem);
        Task<CartDetails> ModifyCart(Guid customerId, CartDetails newCartDetails);
    }
}

