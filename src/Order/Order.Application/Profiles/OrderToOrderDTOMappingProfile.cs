using AutoMapper;
using Order.Application.DTOs;

namespace Order.Application.Profiles;

public class OrderToOrderDtoMappingProfile : Profile
{
    public OrderToOrderDtoMappingProfile()
    {
        CreateMap<Domain.Entities.Order, OrderDTO>();
    }
}