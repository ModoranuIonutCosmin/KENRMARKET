using AutoMapper;
using Gateway.Domain.Models.Carts;

namespace Gateway.Application.Profiles;

public class UpdateCartRequestToCartDetailsProfile : Profile
{
    public UpdateCartRequestToCartDetailsProfile()
    {
        CreateMap<UpdateCartRequestDTO, CartDetails>();
    }
}