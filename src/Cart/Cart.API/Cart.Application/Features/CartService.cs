using AutoMapper;
using Cart.Application.Interfaces;
using Cart.Application.Interfaces.Services;
using Cart.Domain.Entities;
using Cart.Domain.Models;

namespace Cart.Application.Features
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;
        private readonly IMapper _mapper;

        public CartService(ICartRepository cartRepository,
            IMapper mapper)
        {
            _cartRepository = cartRepository;
            _mapper = mapper;
        }

        public async Task<CartDetails> GetCartDetails(string customerId)
        {
            await _cartRepository.EnsureCartExists(customerId);

            return await _cartRepository.GetCartDetails(customerId);
        }

        public async Task<CartDetails> AddCartPromocode(string customerId, string promocode)
        {
            await _cartRepository.EnsureCartExists(customerId);

            return await _cartRepository.SetCartPromocode(customerId, promocode);
        }

        public async Task<CartItem> AddCartItem(string customerId, CartItemViewModel cartItem)
        {
            CartItem cartItemEntity = _mapper.Map<CartItemViewModel, CartItem>(cartItem);

            await _cartRepository.EnsureCartExists(customerId);

            cartItemEntity.CartCustomerId = customerId;

            await _cartRepository.AddCartItem(cartItemEntity);

            return cartItemEntity;
        }


    }
}

