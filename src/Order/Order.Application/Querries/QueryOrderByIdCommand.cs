using MediatR;

namespace Order.Application.Querries;

public record QueryOrderByIdCommand(Guid OrderId) : IRequest<Domain.Entities.Order>;