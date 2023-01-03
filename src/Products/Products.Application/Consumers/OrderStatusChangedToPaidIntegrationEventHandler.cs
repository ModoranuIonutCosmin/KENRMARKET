using IntegrationEvents.Base;
using IntegrationEvents.Contracts;
using Microsoft.Extensions.Logging;
using Products.Application.Interfaces.Services;

namespace Products.Application.Consumers;

// public class
//     OrderStatusChangedToPaidIntegrationEventHandler : IntegrationEventHandler<OrderStatusChangedToPaidIntegrationEvent>
// {
//     private readonly ILogger<OrderStatusChangedToPaidIntegrationEventHandler> _logger;
//     private readonly IProductsService _productsService;
//
//     public OrderStatusChangedToPaidIntegrationEventHandler(IProductsService productsService,
//         ILogger<OrderStatusChangedToPaidIntegrationEventHandler> logger)
//     {
//         _productsService = productsService;
//         _logger = logger;
//     }
//
//     public override async Task Handle(OrderStatusChangedToPaidIntegrationEvent @event)
//     {
//         //TODO: Mutat in fisierul lui
//         //TODO: Outbox commit
//         // _logger.LogInformation("Deducting stock for  in orderId={@orderId}", @event.OrderId);
//         //
//         // var deductions = @event.Order.OrderItems
//         //                             .Select(o => (o.ProductId, o.Quantity)).ToList();
//         //
//         // await _productsService.DeductStocksForProducts(deductions);
//         //
//         // _logger.LogInformation("Deducted stock for orderId={@orderId}", @event.OrderId);
//     }
// }