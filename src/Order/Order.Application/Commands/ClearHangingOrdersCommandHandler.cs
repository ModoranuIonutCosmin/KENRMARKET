using MediatR;
using Microsoft.Extensions.Logging;
using Order.Application.Interfaces;

namespace Order.Application.Commands;

public class ClearHangingOrdersCommandHandler : IRequestHandler<ClearHangingOrdersCommand>
{
    private readonly ILogger<ClearHangingOrdersCommandHandler> _logger;
    private readonly IOrderRepository                          _orderRepository;
    private readonly IUnitOfWork                               _unitOfWork;

    public ClearHangingOrdersCommandHandler(IOrderRepository orderRepository,
        ILogger<ClearHangingOrdersCommandHandler> logger,
        IUnitOfWork unitOfWork)
    {
        _orderRepository = orderRepository;
        _logger          = logger;
        _unitOfWork      = unitOfWork;
    }

    public async Task<Unit> Handle(ClearHangingOrdersCommand request, CancellationToken cancellationToken)
    {
        var newestHangingOrderDate = DateTimeOffset.UtcNow.Subtract(request.MaxOrderAge);

        var hangingOrders =
            await _orderRepository.GetOldestUnpaidOrdersFrom(newestHangingOrderDate);


        await _orderRepository.RemoveOrders(hangingOrders);

        await _unitOfWork.CommitTransaction();

        return Unit.Value;
    }
}