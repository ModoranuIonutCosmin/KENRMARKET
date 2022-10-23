using System.ComponentModel.DataAnnotations;
using MediatR;
using Order.Application.DTOs;
using Order.Domain.Models;

namespace Order.Application.Commands
{
    public class SetOrderStatusToPaidCommand: IRequest<OrderDTO>
    {
        [Required]
        public Guid OrderId { get; init; }
    }
}

