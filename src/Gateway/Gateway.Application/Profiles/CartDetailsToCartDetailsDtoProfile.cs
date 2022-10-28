using AutoMapper;
using Gateway.Domain.Models.Carts;

namespace Gateway.Application.Profiles;

public class CartDetailsToCartDetailsDTOProfile : Profile
{
    public CartDetailsToCartDetailsDTOProfile()
    {
        CreateMap<CartDetails, CartDetailsDTO>().ReverseMap();
    }
}