using AutoMapper;
using Gateway.API.Exceptions;
using Gateway.API.Interfaces;
using Gateway.Domain.Exceptions;
using Gateway.Domain.Models.Carts;
using Gateway.Domain.Models.Orders;

namespace Gateway.API.Services;

public class OrdersAggregatesService : IOrdersAggregatesService
{
    private readonly IOrdersService _ordersService;
    private readonly IPaymentsService _paymentsService;
    private readonly IMapper _mapper;

    public OrdersAggregatesService(IOrdersService ordersService,
        IPaymentsService paymentsService, IMapper mapper)
    {
        _ordersService = ordersService;
        _paymentsService = paymentsService;
        _mapper = mapper;
    }

    public async Task<(bool IsSuccess, dynamic CheckoutSession)> GetCheckoutSessionForOrder(Guid customerId, Guid orderId)
    {
        var orderDetails = await _ordersService.GetSpecificOrder(orderId);

        if (orderDetails.orderDetails == null || !orderDetails.orderDetails.BuyerId.Equals(customerId))
        {
            throw new UnauthorizedAccessException("You can't access a order that isn't made by you!");
        }
        
        if (orderDetails.isOk)
        {

            if (orderDetails.orderDetails.OrderStatus != OrderStatus.StocksValidationAccepted)
            {
                throw new StockForOrderNotValidatedException("Stock wasn't validated yet, for this order.");
            }

            var checkoutSession 
                = await _paymentsService.CreateCheckoutSession(orderDetails.orderDetails);

            if (checkoutSession.isOk)
            {
                return (true, checkoutSession.checkoutSession);
            }
        }

        return (false, null);
    }
}