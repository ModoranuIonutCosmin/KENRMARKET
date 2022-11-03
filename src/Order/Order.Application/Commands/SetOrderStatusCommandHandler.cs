using AutoMapper;
using MediatR;
using Order.Application.DTOs;
using Order.Application.Interfaces;
using Order.Domain.DataModels;
using Order.Domain.Exceptions;

namespace Order.Application.Commands;

public class SetOrderStatusCommandHandler : IRequestHandler<SetOrderStatusCommand, OrderDTO>
{
    private readonly IMapper _mapper;
    private readonly IOrderRepository _orderRepository;
    private readonly IUnitOfWork _unitOfWork;

    public SetOrderStatusCommandHandler(IOrderRepository orderRepository, IMapper mapper, IUnitOfWork unitOfWork)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<OrderDTO> Handle(SetOrderStatusCommand request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetById(request.OrderId);

        if (order == null)
            throw new OrderDoesntExistException("Order id doesn't identify any order existent in our system.");
        
        
        //TODO: Beautify
        await _orderRepository.SetOrderStatus(request.OrderId, request.OrderStatus);

        await _unitOfWork.CommitTransaction();

        return _mapper.Map<Domain.Entities.Order, OrderDTO>(order);
    }
}