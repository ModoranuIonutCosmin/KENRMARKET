﻿using Gateway.API.Auth.ExtensionMethods;
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
    private readonly IOrdersAggregatesService _ordersAggregatesService;

    public OrdersController(IOrdersService ordersService, IOrdersAggregatesService ordersAggregatesService)
    {
        _ordersService = ordersService;
        _ordersAggregatesService = ordersAggregatesService;
    }

    [HttpGet]
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
    
    
    [HttpGet]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> GetPaymentLinkForCheckoutSession(Guid orderId)
    {
        var customerId = Guid.Parse(User.GetLoggedInUserId<string>());

        var checkoutSessionStatus 
            = await _ordersAggregatesService.GetCheckoutSessionForOrder(customerId, orderId);

        if (checkoutSessionStatus.IsSuccess)
        {
            return Ok(checkoutSessionStatus.CheckoutSession);
        }

        return NotFound();
    }
}