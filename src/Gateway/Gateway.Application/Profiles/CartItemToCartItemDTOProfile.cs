using AutoMapper;
using Gateway.Domain.Models.Carts;

namespace Gateway.Application.Profiles;

public class CartItemToCartItemDTOProfile : Profile
{
    public CartItemToCartItemDTOProfile()
    {
        CreateMap<CartItem, CartItemDTO>().ReverseMap();
    }
}