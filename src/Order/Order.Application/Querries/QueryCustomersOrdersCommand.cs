using MediatR;
using Order.Application.DTOs;

namespace Order.Application.Querries;

public class QueryCustomersOrdersCommand : IRequest<List<OrderDTO>>
{
    public Guid CustomerId { get; init; }
}