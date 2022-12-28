using AutoMapper;
using Gateway.Domain.Models.Carts;

namespace Gateway.Application.Profiles;

public class CartItemProductAndQuantityToCartItemProfile : Profile
{
    public CartItemProductAndQuantityToCartItemProfile()
    {
        CreateMap<CartItemIdAndQuantity, CartItem>().ReverseMap();
    }
}