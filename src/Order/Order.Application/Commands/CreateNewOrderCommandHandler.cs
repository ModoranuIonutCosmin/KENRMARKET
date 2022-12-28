using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Order.Application.DTOs;
using Order.Application.Interfaces;
using Order.Domain.DataModels;

namespace Order.Application.Commands;

public class CreateNewOrderCommandHandler : IRequestHandler<CreateNewOrderCommand, OrderDTO>
{
    private readonly ILogger<CreateNewOrderCommandHandler> _logger;
    private readonly IMapper                               _mapper;
    private readonly IOrderRepository                      _orderRepository;
    private readonly IUnitOfWork                           _unitOfWork;

    public CreateNewOrderCommandHandler(IOrderRepository orderRepository,
        IMapper mapper,
        ILogger<CreateNewOrderCommandHandler> logger,
        IUnitOfWork unitOfWork)
    {
        _orderRepository = orderRepository;
        _mapper          = mapper;
        _logger          = logger;
        _unitOfWork      = unitOfWork;
    }

    public async Task<OrderDTO> Handle(CreateNewOrderCommand request, CancellationToken cancellationToken)
    {
        var newOrder = new Domain.Entities.Order(request.BuyerId, request.Address);

        //TODO: Throttle la numarul de ordere concurente.
        _logger.LogInformation("Creating a new order for buyerId={@buyerId}, with cartItemCount={@count}",
                               request.BuyerId,
                               request.CartItems.Count);

        request.CartItems
               .ForEach(ci => newOrder.AddOrderItem(ci.ProductId, ci.ProductName, ci.Quantity,
                                                    ci.UnitPrice, ci.PictureUrl));

        newOrder.SetOrderStatus(OrderStatus.PendingValidation);

        await _orderRepository.AddNewOrder(newOrder);

        await _unitOfWork.CommitTransaction();

        _logger.LogInformation("Created order for buyerId={@buyerId}, orderId={@orderId}  with cartItemCount={@count}",
                               request.BuyerId,
                               newOrder.Id,
                               request.CartItems.Count);

        return _mapper.Map<Domain.Entities.Order, OrderDTO>(newOrder);
    }
}