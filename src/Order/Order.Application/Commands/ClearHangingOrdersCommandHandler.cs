using AutoMapper;
using MediatR;
using Order.Application.Interfaces;

namespace Order.Application.Commands
{
    public class ClearHangingOrdersCommandHandler : IRequestHandler<ClearHangingOrdersCommand>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public ClearHangingOrdersCommandHandler(IOrderRepository orderRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(ClearHangingOrdersCommand request, CancellationToken cancellationToken)
        {
            DateTimeOffset newestHangingOrderDate = DateTimeOffset.UtcNow.Subtract(request.MaxOrderAge);

            List<Domain.Entities.Order> hangingOrders =
                await _orderRepository.GetOldestUnpaidOrdersFrom(newestHangingOrderDate);


            await _orderRepository.RemoveOrders(hangingOrders);

            await _unitOfWork.CommitTransaction();

            return Unit.Value;
        }
    }
}

