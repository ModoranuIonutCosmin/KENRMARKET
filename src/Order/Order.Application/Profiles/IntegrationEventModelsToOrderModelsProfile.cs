using AutoMapper;

namespace Order.Application.Profiles;

public class IntegrationEventModelsToOrderModelsProfile : Profile
{
    public IntegrationEventModelsToOrderModelsProfile()
    {
        CreateMap<IntegrationEvents.Models.Address, Order.Domain.Models.Address>().ReverseMap();
        CreateMap<IntegrationEvents.Models.Order, IntegrationEvents.Models.Order>().ReverseMap();
        CreateMap<IntegrationEvents.Models.CartItem, Order.Domain.Models.CartItem>().ReverseMap();
    }
}