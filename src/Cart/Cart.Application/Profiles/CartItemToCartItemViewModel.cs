using AutoMapper;
using Cart.Domain.Entities;
using Cart.Domain.Models;

namespace Cart.Application.Profiles;

public class CartItemToCartItemViewModel : Profile
{
    public CartItemToCartItemViewModel()
    {
        CreateMap<CartItemDTO, CartItem>()
            .ReverseMap();
    }
}