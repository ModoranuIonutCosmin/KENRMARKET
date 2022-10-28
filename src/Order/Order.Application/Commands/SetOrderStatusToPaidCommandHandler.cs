using AutoMapper;
using MediatR;
using Order.Application.DTOs;
using Order.Application.Interfaces;
using Order.Domain.DataModels;
using Order.Domain.Exceptions;

namespace Order.Application.Commands;

public class SetOrderStatusToPaidCommandHandler : IRequestHandler<SetOrderStatusToPaidCommand, OrderDTO>
{
    private readonly IMapper _mapper;
    private readonly IOrderRepository _orderRepository;
    private readonly IUnitOfWork _unitOfWork;

    public SetOrderStatusToPaidCommandHandler(IOrderRepository orderRepository, IMapper mapper, IUnitOfWork unitOfWork)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<OrderDTO> Handle(SetOrderStatusToPaidCommand request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetById(request.OrderId);

        if (order == null)
            throw new OrderDoesntExistException("Order id doesn't identify any order existent in our system.");

        await _orderRepository.SetOrderStatus(request.OrderId, OrderStatus.Paid);

        await _unitOfWork.CommitTransaction();

        return _mapper.Map<Domain.Entities.Order, OrderDTO>(order);
    }
}