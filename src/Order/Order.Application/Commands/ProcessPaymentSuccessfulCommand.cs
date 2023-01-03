using System.ComponentModel.DataAnnotations;
using MediatR;
using Order.Application.DTOs;

namespace Order.Application.Commands;

public class ProcessPaymentSuccessfulCommand : IRequest<OrderDTO>
{
    [Required] public Guid OrderId { get; init; }
}