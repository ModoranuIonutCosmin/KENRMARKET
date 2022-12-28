using AutoMapper;
using IntegrationEvents.Models;
using OrderItem = Order.Domain.Entities.OrderItem;

namespace Order.Application.Profiles;

public class IntegrationEventModelsToOrderModelsProfile : Profile
{
    public IntegrationEventModelsToOrderModelsProfile()
    {
        CreateMap<Address, Domain.Models.Address>().ReverseMap();
        CreateMap<Domain.Entities.Order, IntegrationEvents.Models.Order>().ReverseMap();
        CreateMap<OrderItem, IntegrationEvents.Models.OrderItem>().ReverseMap();
        CreateMap<CartItem, Domain.Models.CartItem>().ReverseMap();
    }
}