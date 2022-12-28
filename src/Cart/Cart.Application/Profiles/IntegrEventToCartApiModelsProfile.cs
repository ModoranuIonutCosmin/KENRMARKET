using AutoMapper;
using Cart.Domain.Entities;

namespace Cart.Application.Profiles;

public class IntegrEventToCartApiModelsProfile : Profile
{
    public IntegrEventToCartApiModelsProfile()
    {
        CreateMap<CartItem, IntegrationEvents.Models.CartItem>();
    }
}