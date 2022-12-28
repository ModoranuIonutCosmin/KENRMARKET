using Gateway.API.Auth.ExtensionMethods;
using Gateway.Application.Interfaces;
using Gateway.Domain.Exceptions;
using Gateway.Domain.Models.Checkout;
using Gateway.Domain.Models.Orders;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gateway.API.Controllers;

[ApiVersion("1.0")]
public class OrdersController : BaseController
{
    private readonly IOrdersAggregatesService _ordersAggregatesService;
    private readonly IOrdersService           _ordersService;

    public OrdersController(IOrdersService ordersService, IOrdersAggregatesService ordersAggregatesService)
    {
        _ordersService           = ordersService;
        _ordersAggregatesService = ordersAggregatesService;
    }

    [HttpGet("orders")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(statusCode: StatusCodes.Status200OK, type: typeof(List<Order>))]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> Orders()
    {
        var customerId = Guid.Parse(User.GetLoggedInUserId<string>());

        var ordersStatus = await _ordersService.GetOrders(customerId);

        if (ordersStatus.isOk)
        {
            return Ok(ordersStatus.ordersDetails);
        }

        return NotFound();
    }

    [HttpGet("order/{id}")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(statusCode: StatusCodes.Status200OK, type: typeof(Order))]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> Orders([FromRoute] Guid id)
    {
        var customerId = Guid.Parse(User.GetLoggedInUserId<string>());

        var orderStatus = await _ordersService.GetSpecificOrder(id);

        if (orderStatus.isOk && orderStatus.orderDetails.BuyerId.Equals(customerId))
        {
            return Ok(orderStatus.orderDetails);
        }

        return NotFound();
    }


    [HttpPost("checkoutSession")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(statusCode: StatusCodes.Status201Created, type: typeof(CheckoutSession))]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> GetPaymentLinkForCheckoutSession([FromBody] Guid orderId)
    {
        var customerId = Guid.Parse(User.GetLoggedInUserId<string>());

        try
        {
            var checkoutSessionStatus
                = await _ordersAggregatesService.GetCheckoutSessionForOrder(customerId, orderId);

            if (checkoutSessionStatus.IsSuccess)
            {
                return Ok(checkoutSessionStatus.CheckoutSession);
            }

            return NotFound();
        }
        catch (StockForOrderNotValidatedException)
        {
            return StatusCode(StatusCodes.Status403Forbidden);
        }
    }
}