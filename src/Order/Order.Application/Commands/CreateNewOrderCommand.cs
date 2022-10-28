using System.ComponentModel.DataAnnotations;
using MediatR;
using Order.Application.DTOs;
using Order.Domain.Models;

namespace Order.Application.Commands;

public class CreateNewOrderCommand : IRequest<OrderDTO>
{
    [Required] public Guid BuyerId { get; init; }

    [Required] public Address Address { get; init; }

    [Required] public List<CartItem> CartItems { get; init; }
}