using AutoMapper;
using Cart.Domain.Entities;
using Cart.Domain.Models;

namespace Cart.Application.Profiles;

public class CartDetailsToCartDetailsViewModel : Profile
{
    public CartDetailsToCartDetailsViewModel()
    {
        CreateMap<CartDetailsViewModel, CartDetails>()
            .ReverseMap();
    }
}