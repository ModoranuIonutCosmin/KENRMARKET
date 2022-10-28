using AutoMapper;
using MediatR;
using Order.Application.DTOs;
using Order.Application.Interfaces;
using Order.Domain.DataModels;

namespace Order.Application.Commands;

public class CreateNewOrderCommandHandler : IRequestHandler<CreateNewOrderCommand, OrderDTO>
{
    private readonly IMapper _mapper;
    private readonly IOrderRepository _orderRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateNewOrderCommandHandler(IOrderRepository orderRepository, IMapper mapper, IUnitOfWork unitOfWork)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<OrderDTO> Handle(CreateNewOrderCommand request, CancellationToken cancellationToken)
    {
        var newOrder = new Domain.Entities.Order(request.BuyerId, request.Address);

        //TODO: Throttle la numarul de ordere concurente.
        request.CartItems
            .ForEach(ci => newOrder.AddOrderItem(ci.Id, ci.ProductName, ci.Quantity,
                ci.UnitPrice, ci.PictureUrl));
        
        newOrder.SetOrderStatus(OrderStatus.PendingValidation);

        await _orderRepository.AddNewOrder(newOrder);

        await _unitOfWork.CommitTransaction();

        return _mapper.Map<Domain.Entities.Order, OrderDTO>(newOrder);
    }
}