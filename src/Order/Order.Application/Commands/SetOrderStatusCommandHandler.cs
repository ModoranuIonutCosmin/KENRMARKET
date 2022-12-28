using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Order.Application.DTOs;
using Order.Application.Interfaces;
using Order.Domain.Exceptions;

namespace Order.Application.Commands;

public class SetOrderStatusCommandHandler : IRequestHandler<SetOrderStatusCommand, OrderDTO>
{
    private readonly ILogger<SetOrderStatusCommandHandler> _logger;
    private readonly IMapper                               _mapper;
    private readonly IOrderRepository                      _orderRepository;
    private readonly IUnitOfWork                           _unitOfWork;

    public SetOrderStatusCommandHandler(IOrderRepository orderRepository,
        ILogger<SetOrderStatusCommandHandler> logger,
        IMapper mapper, IUnitOfWork unitOfWork)
    {
        _orderRepository = orderRepository;
        _logger          = logger;
        _mapper          = mapper;
        _unitOfWork      = unitOfWork;
    }

    public async Task<OrderDTO> Handle(SetOrderStatusCommand request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetById(request.OrderId);

        if (order == null)
        {
            throw new OrderDoesntExistException("Order id doesn't identify any order existent in our system.");
        }

        _logger.LogInformation("Changing status for orderId={@orderId} to orderStatus={@orderStatus}",
                               request.OrderId,
                               request.OrderStatus.ToString());

        //TODO: Beautify
        await _orderRepository.SetOrderStatus(request.OrderId, request.OrderStatus);

        await _unitOfWork.CommitTransaction();

        _logger.LogInformation("Changed status for orderId={@orderId}, to orderStatus={@orderStatus}",
                               request.OrderId,
                               request.OrderStatus.ToString());

        return _mapper.Map<Domain.Entities.Order, OrderDTO>(order);
    }
}