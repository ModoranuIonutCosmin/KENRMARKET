using AutoMapper;
using Cart.Application.Interfaces;
using Cart.Application.Interfaces.Services;
using Cart.Domain.Exceptions;
using Cart.Domain.Models;
using IntegrationEvents.Contracts;
using IntegrationEvents.Models;
using MassTransit;
using CartDetails = Cart.Domain.Entities.CartDetails;
using CartItem = Cart.Domain.Entities.CartItem;

namespace Cart.Application.Features;

public class CartService : ICartService
{
    private readonly ICartRepository _cartRepository;
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly IMapper _mapper;

    public CartService(ICartRepository cartRepository,
        IPublishEndpoint publishEndpoint,
        IMapper mapper)
    {
        _cartRepository = cartRepository;
        _publishEndpoint = publishEndpoint;
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

    public async Task<CartDetails> Checkout(Guid customerId, Address address)
    {

        var cartDetails = await _cartRepository.GetCartDetails(customerId);

        if (!cartDetails.CartItems.Any())
        {
            throw new CheckoutFailedCartEmptyException("Cart is empty, can't checkout!");
        }

        await _publishEndpoint
            .Publish(new CheckoutAcceptedIntegrationEvent(customerId,
                _mapper.Map<List<IntegrationEvents.Models.CartItem>>(cartDetails.CartItems),
                address
                ));
        
        return cartDetails;
    }
    
    public async Task<CartDetails> DeleteCartContents(Guid customerId)
    {
        return await _cartRepository.DeleteCartContents(customerId);
    }
}