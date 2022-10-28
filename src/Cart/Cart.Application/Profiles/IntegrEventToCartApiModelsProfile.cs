using AutoMapper;

namespace Cart.Application.Profiles;

public class IntegrEventToCartApiModelsProfile : Profile
{
    public IntegrEventToCartApiModelsProfile()
    {
        CreateMap<Domain.Entities.CartItem, IntegrationEvents.Models.CartItem>();
    }
}