using System.ComponentModel.DataAnnotations;
using MediatR;
using Order.Application.DTOs;
using Order.Domain.DataModels;

namespace Order.Application.Commands;

public class SetOrderStatusCommand : IRequest<OrderDTO>
{
    [Required] public Guid OrderId { get; init; }
    [Required] public OrderStatus OrderStatus { get; init; }
}