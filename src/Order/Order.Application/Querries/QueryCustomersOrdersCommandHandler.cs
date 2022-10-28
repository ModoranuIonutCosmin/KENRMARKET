using AutoMapper;
using MediatR;
using Order.Application.DTOs;
using Order.Application.Interfaces;

namespace Order.Application.Querries;

public class QueryCustomersOrdersCommandHandler : IRequestHandler<QueryCustomersOrdersCommand, List<OrderDTO>>
{
    private readonly IMapper _mapper;
    private readonly IOrderRepository _orderRepository;

    public QueryCustomersOrdersCommandHandler(IMapper mapper,
        IOrderRepository orderRepository)
    {
        _mapper = mapper;
        _orderRepository = orderRepository;
    }


    public async Task<List<OrderDTO>> Handle(QueryCustomersOrdersCommand request, CancellationToken cancellationToken)
    {
        var orders = await _orderRepository.GetOrdersForUser(request.CustomerId);

        return _mapper.Map<List<Domain.Entities.Order>, List<OrderDTO>>(orders);
    }
}