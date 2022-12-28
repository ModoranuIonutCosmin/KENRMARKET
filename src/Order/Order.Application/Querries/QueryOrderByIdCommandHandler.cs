using AutoMapper;
using MediatR;
using Order.Application.Interfaces;

namespace Order.Application.Querries;

public class QueryOrderByIdCommandHandler : IRequestHandler<QueryOrderByIdCommand, Domain.Entities.Order>
{
    private readonly IMapper          _mapper;
    private readonly IOrderRepository _orderRepository;

    public QueryOrderByIdCommandHandler(IMapper mapper,
        IOrderRepository orderRepository)
    {
        _mapper          = mapper;
        _orderRepository = orderRepository;
    }


    public async Task<Domain.Entities.Order> Handle(QueryOrderByIdCommand request,
        CancellationToken cancellationToken)
    {
        return await _orderRepository.GetById(request.OrderId);
    }
}