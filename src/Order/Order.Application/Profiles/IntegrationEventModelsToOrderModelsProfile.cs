using AutoMapper;

namespace Order.Application.Profiles;

public class IntegrationEventModelsToOrderModelsProfile : Profile
{
    public IntegrationEventModelsToOrderModelsProfile()
    {
        CreateMap<IntegrationEvents.Models.Address, Order.Domain.Models.Address>().ReverseMap();
        CreateMap<Domain.Entities.Order, IntegrationEvents.Models.Order>().ReverseMap();
        CreateMap<Domain.Entities.OrderItem, IntegrationEvents.Models.OrderItem>().ReverseMap();
        CreateMap<IntegrationEvents.Models.CartItem, Order.Domain.Models.CartItem>().ReverseMap();
    }
}