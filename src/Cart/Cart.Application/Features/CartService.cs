using AutoMapper;
using Cart.Application.Interfaces;
using Cart.Application.Interfaces.Services;
using Cart.Domain.Entities;
using Cart.Domain.Models;

namespace Cart.Application.Features;

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

        return _mapper.Map<CartDetails, CartDetailsViewModel>(await _cartRepository.GetCartDetails(customerId));
    }

    public async Task<CartDetailsViewModel> AddCartPromocode(Guid customerId, string promocode)
    {
        await _cartRepository.EnsureCartExists(customerId);

        return _mapper.Map<CartDetails, CartDetailsViewModel>(
            await _cartRepository.SetCartPromocode(customerId, promocode));
    }

    public async Task<CartItemDTO> AddCartItem(Guid customerId, CartItemDTO cartItem)
    {
        var cartItemEntity = _mapper.Map<CartItemDTO, CartItem>(cartItem);

        await _cartRepository.EnsureCartExists(customerId);

        var cartDetails = await _cartRepository.GetCartDetails(customerId);

        var existentCartItem = cartDetails.CartItems
            .SingleOrDefault(ci => ci.ProductId.Equals(cartItem.ProductId));

        if (existentCartItem == null)
            await _cartRepository.AddCartItem(customerId, cartItemEntity);
        else
            await _cartRepository.UpdateCartItem(cartDetails.Id, cartItemEntity);

        return _mapper.Map<CartItem, CartItemDTO>(cartItemEntity);
    }

    public async Task<CartDetails> ModifyCart(Guid customerId, CartDetails newCartDetails)
    {
        return await _cartRepository.ModifyCart(customerId, newCartDetails);
    }
}