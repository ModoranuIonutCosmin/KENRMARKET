using MediatR;
using Microsoft.AspNetCore.Mvc;
using Order.Application.Commands;
using Order.Application.Querries;

namespace Order.API.Controllers;

[ApiVersion("1.0")]
public class OrdersController : BaseController
{
    private readonly IMediator _mediator;

    public OrdersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("createNewOrder")]
    public async Task<IActionResult> CreateNewOrder(CreateNewOrderCommand createNewOrderCommand)
    {
        return Ok(await _mediator.Send(createNewOrderCommand));
    }

    [HttpGet("orders")]
    public async Task<IActionResult> GetOrders(
        [FromQuery] QueryCustomersOrdersCommand queryCustomersOrdersCommand)
    {
        var orders = await _mediator.Send(queryCustomersOrdersCommand);

        if (!orders.Any())
        {
            return NotFound(orders);
        }

        return Ok(orders);
    }


    [HttpGet("{orderId}")]
    public async Task<IActionResult> GetOrder(
        [FromRoute] Guid orderId)
    {
        var order = await _mediator.Send(new QueryOrderByIdCommand(orderId));

        if (order == null)
        {
            return NotFound(order);
        }

        return Ok(order);
    }
}