using AutoMapper;
using Payments.Application.DTOs;
using Payments.Domain.Models;

namespace Payments.Application.Profiles;

public class OrderToOrderDtoMappingProfile : Profile
{
    public OrderToOrderDtoMappingProfile()
    {
        CreateMap<Order, OrderDTO>();
    }
}