﻿using AutoMapper;
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
    private readonly ICartRepository  _cartRepository;
    private readonly IMapper          _mapper;
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly IUnitOfWork      _unitOfWork;

    public CartService(ICartRepository cartRepository,
        IUnitOfWork unitOfWork,
        IPublishEndpoint publishEndpoint,
        IMapper mapper)
    {
        _cartRepository  = cartRepository;
        _unitOfWork      = unitOfWork;
        _publishEndpoint = publishEndpoint;
        _mapper          = mapper;
    }

    public async Task<CartDetailsDto> GetCartDetails(Guid customerId)
    {
        await _cartRepository.EnsureCartExists(customerId);

        await _unitOfWork.CommitTransaction();

        return _mapper.Map<CartDetails, CartDetailsDto>(await _cartRepository.GetCartDetails(customerId));
    }

    public async Task<CartDetailsDto> AddCartPromocode(Guid customerId, string promocode)
    {
        await _cartRepository.EnsureCartExists(customerId);

        await _unitOfWork.CommitTransaction();

        var cartDetails = await _cartRepository.SetCartPromocode(customerId, promocode);

        await _unitOfWork.CommitTransaction();

        return _mapper.Map<CartDetails, CartDetailsDto>(
                                                        cartDetails);
    }

    public async Task<CartItemDTO> AddCartItem(Guid customerId, ProductQuantity cartItem)
    {
        var cartItemEntity = _mapper.Map<ProductQuantity, CartItem>(cartItem);

        await _cartRepository.EnsureCartExists(customerId);

        await _unitOfWork.CommitTransaction();

        if (cartItem.Quantity <= 0)
        {
            throw new InvalidCartItemQuantityValueException("Invalid cart item value");
        }

        var cartDetails = await _cartRepository.GetCartDetails(customerId);

        var existentCartItem = cartDetails.CartItems
                                          .SingleOrDefault(ci => ci.ProductId.Equals(cartItem.ProductId));

        if (existentCartItem == null)
        {
            await _cartRepository.AddCartItem(customerId, cartItemEntity);
        }
        else
        {
            await _cartRepository.UpdateCartItem(cartDetails.Id, cartItemEntity);
        }

        await _unitOfWork.CommitTransaction();

        return _mapper.Map<CartItem, CartItemDTO>(cartItemEntity);
    }

    public async Task<CartDetails> ModifyCart(Guid customerId, CartDetailsDto newCartDetails)
    {
        var cartDetails = _mapper.Map<CartDetailsDto, CartDetails>(newCartDetails);
        cartDetails = await _cartRepository.ModifyCart(customerId, cartDetails);

        await _unitOfWork.CommitTransaction();

        return cartDetails;
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
                                                          _mapper
                                                              .Map<List<IntegrationEvents.Models.CartItem>>(cartDetails
                                                                  .CartItems),
                                                          address
                                                         ));

        await _unitOfWork.CommitTransaction();

        return cartDetails;
    }

    public async Task<CartDetails> DeleteCartContents(Guid customerId)
    {
        var cartContents = await _cartRepository.DeleteCartContents(customerId);

        await _unitOfWork.CommitTransaction();

        return cartContents;
    }
}