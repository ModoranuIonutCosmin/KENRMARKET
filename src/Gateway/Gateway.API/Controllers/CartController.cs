using Gateway.API.Auth.ExtensionMethods;
using Gateway.API.Interfaces;
using Gateway.API.Models;
using Gateway.Domain.Models.Carts;
using Gateway.Domain.Models.Orders;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gateway.API.Controllers;

[ApiVersion("1.0")]
public class CartController : BaseController
{
    private readonly ICartAggregatesService _cartAggregates;
    private readonly ICartService _cartService;

    public CartController(ICartAggregatesService cartAggregates,
        ICartService cartService)
    {
        _cartAggregates = cartAggregates;
        _cartService = cartService;
    }

    [HttpGet("cartContents")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> GetCartContents()
    {
        var customerId = Guid.Parse(User.GetLoggedInUserId<string>());

        var cartDetails = await _cartAggregates.GetFullCartDetails(customerId);

        if (cartDetails.IsSuccess) return Ok(cartDetails.FullCartDetails);

        return NotFound();
    }

    //TODO: Verificat data exista deja si stackat peste cu + quantity


    [HttpPut("updateCart")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> UpdateCart(UpdateCartRequestDTO cartDetails)
    {
        var customerId = Guid.Parse(User.GetLoggedInUserId<string>());

        var result = await _cartAggregates.ModifyCart(customerId, cartDetails);

        if (result.IsSuccess) return Ok(result.FullCartDetails);

        return NotFound();
    }

    [HttpPost("addItemToCart")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> AddCartItem(CartItemDTO itemToAddDTO)
    {
        var customerId = Guid.Parse(User.GetLoggedInUserId<string>());

        var result = await _cartAggregates.AddItemToCart(customerId, itemToAddDTO);

        if (result.IsSuccess) return Ok(result.CartDetails);

        return NotFound();
    }
    
    
    [HttpPost("checkout")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> Checkout([FromBody]Address shippingAddress)
    {
        var customerId = Guid.Parse(User.GetLoggedInUserId<string>());

        var result = await _cartService.CheckoutCart(customerId, shippingAddress);

        if (result.IsOk) return Created("", result.CartDetails);

        return NotFound();
    }
}