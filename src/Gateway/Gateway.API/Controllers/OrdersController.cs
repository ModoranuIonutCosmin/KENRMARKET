using Gateway.API.Auth.ExtensionMethods;
using Gateway.API.Interfaces;
using Gateway.API.Services;
using Gateway.Domain.Models.Products;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gateway.API.Controllers;

[ApiVersion("1.0")]
public class OrdersController : BaseController
{
    private readonly IOrdersService _ordersService;

    public OrdersController(IOrdersService ordersService)
    {
        _ordersService = ordersService;
    }

    [HttpGet]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> Orders()
    {
        var customerId = Guid.Parse(User.GetLoggedInUserId<string>());

        var ordersStatus = await _ordersService.GetOrders(customerId);

        if (ordersStatus.isOk)
        {
            return Ok(ordersStatus.orderDetails);
        }

        return NotFound();
    }
}