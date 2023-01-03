using System.ComponentModel.DataAnnotations;
using AutoMapper;
using IntegrationEvents.Contracts;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using Order.Application.DTOs;
using Order.Application.Interfaces;
using Order.Domain.DataModels;
using Order.Domain.Exceptions;

namespace Order.Application.Commands;

public class ProcessPaymentSuccessfulCommandHandler : IRequestHandler<ProcessPaymentSuccessfulCommand, OrderDTO>
{
    
    private readonly ILogger<ProcessPaymentSuccessfulCommandHandler> _logger;
    private readonly IPublishEndpoint                                _publishEndpoint;
    private readonly IMapper                                         _mapper;
    private readonly IOrderRepository                                _orderRepository;
    private readonly IUnitOfWork                                     _unitOfWork;

    public ProcessPaymentSuccessfulCommandHandler(IOrderRepository orderRepository,
        ILogger<ProcessPaymentSuccessfulCommandHandler> logger,
        IPublishEndpoint publishEndpoint,
        IMapper mapper, IUnitOfWork unitOfWork)
    {
        _orderRepository      = orderRepository;
        _logger               = logger;
        _publishEndpoint = publishEndpoint;
        _mapper               = mapper;
        _unitOfWork           = unitOfWork;
    }
    
    public async Task<OrderDTO> Handle(ProcessPaymentSuccessfulCommand request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetById(request.OrderId);

        if (order == null)
        {
            throw new OrderDoesntExistException("Order id doesn't identify any order existent in our system.");
        }
        
        _logger.LogInformation("Changing status for orderId={@orderId} to orderStatus={@orderStatus}",
                               request.OrderId, "Paid");

        order.SetOrderStatus(OrderStatus.Paid);

        
        await _unitOfWork.CommitTransaction();

        _logger.LogInformation("Changed status for orderId={@orderId}, to orderStatus={@orderStatus}",
                               request.OrderId,
                               "Paid");

        return _mapper.Map<OrderDTO>(order);
    }
}