using MediatR;
using Order.Application.DTOs;

namespace Order.Application.Querries;

public class QueryOrderByIdCommand : IRequest<Domain.Entities.Order>
{
    public Guid OrderId { get; init; }
}