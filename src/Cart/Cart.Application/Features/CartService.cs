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

        public async Task<CartDetailsViewModel> GetCartDetails(Guid customerId)
        {
            await _cartRepository.EnsureCartExists(customerId);

            return _mapper.Map<CartDetails,CartDetailsViewModel>(await _cartRepository.GetCartDetails(customerId));
        }

        public async Task<CartDetailsViewModel> AddCartPromocode(Guid customerId, string promocode)
        {
            await _cartRepository.EnsureCartExists(customerId);

            return _mapper.Map<CartDetails, CartDetailsViewModel>(await _cartRepository.SetCartPromocode(customerId, promocode));
        }

        public async Task<CartItemViewModel> AddCartItem(Guid customerId, CartItemViewModel cartItem)
        {
            CartItem cartItemEntity = _mapper.Map<CartItemViewModel, CartItem>(cartItem);

            await _cartRepository.EnsureCartExists(customerId);

            //TODO: Verificat data exista deja si stackat peste cu + quantity

            await _cartRepository.AddCartItem(customerId, cartItemEntity);

            return _mapper.Map<CartItem,CartItemViewModel>(cartItemEntity);
        }

        public async Task<CartDetails> ModifyCart(Guid customerId, CartDetails newCartDetails)
        {
            return await _cartRepository.ModifyCart(customerId, newCartDetails);
        }
    }
}

