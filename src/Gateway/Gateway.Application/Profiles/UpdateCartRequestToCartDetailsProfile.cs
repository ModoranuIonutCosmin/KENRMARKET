using AutoMapper;
using Gateway.API.Auth.Models.Carts;
using Gateway.API.Models;

namespace Gateway.Application.Profiles
{
    public class UpdateCartRequestToCartDetailsProfile : Profile
    {

        public UpdateCartRequestToCartDetailsProfile()
        {
            CreateMap<UpdateCartRequestDTO, CartDetails>();
        }
    }
}

