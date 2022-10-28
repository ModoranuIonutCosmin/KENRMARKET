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

    [HttpPost("order")]
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
            return NotFound();
        }
        
        return Ok(orders);
    }
}