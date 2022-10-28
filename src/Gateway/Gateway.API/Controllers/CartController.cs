using Gateway.API.Auth.ExtensionMethods;
using Gateway.API.Interfaces;
using Gateway.Domain.Models.Carts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gateway.API.Controllers;

[ApiVersion("1.0")]
public class CartController : BaseController
{
    private readonly ICartAggregatesService _cartAggregates;

    public CartController(ICartAggregatesService cartAggregates)
    {
        _cartAggregates = cartAggregates;
    }

    [HttpGet]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> GetCartContents()
    {
        var customerId = Guid.Parse(User.GetLoggedInUserId<string>());

        var cartDetails = await _cartAggregates.GetFullCartDetails(customerId);

        if (cartDetails.IsSuccess) return Ok(cartDetails.FullCartDetails);

        return NotFound();
    }

    //TODO: Verificat data exista deja si stackat peste cu + quantity


    [HttpPut]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> UpdateCart(UpdateCartRequestDTO cartDetails)
    {
        var customerId = Guid.Parse(User.GetLoggedInUserId<string>());

        var result = await _cartAggregates.ModifyCart(customerId, cartDetails);

        if (result.IsSuccess) return Ok(result.FullCartDetails);

        return NotFound();
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> AddCartItem(CartItemDTO itemToAddDTO)
    {
        var customerId = Guid.Parse(User.GetLoggedInUserId<string>());

        var result = await _cartAggregates.AddItemToCart(customerId, itemToAddDTO);

        if (result.IsSuccess) return Ok(result.CartDetails);

        return NotFound();
    }
}