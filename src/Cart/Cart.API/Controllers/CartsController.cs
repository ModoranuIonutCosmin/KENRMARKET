using AutoMapper;
using Cart.API.DTOs;
using Cart.Application.Interfaces.Services;
using Cart.Domain.Exceptions;
using Cart.Domain.Models;
using IntegrationEvents.Models;
using Microsoft.AspNetCore.Mvc;

namespace Cart.API.Controllers;

[ApiVersion("1.0")]
public class CartsController : BaseController
{
    private readonly ICartService _cartService;
    private readonly IMapper      _mapper;

    public CartsController(ICartService cartService, IMapper mapper)
    {
        _cartService = cartService;
        _mapper      = mapper;
    }

    [HttpPut("modifyCart")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(statusCode: StatusCodes.Status200OK, type: typeof(CartDetails))]
    public async Task<IActionResult> ModifyCart([FromQuery] Guid customerId,
        [FromBody] CartDetailsDto newCartDetails)
    {
        //TODO: 

        var result = await _cartService.ModifyCart(customerId, newCartDetails);

        return Ok(result);
    }

    [HttpPost("addItemToCart")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(statusCode: StatusCodes.Status201Created, type: typeof(CartDetails))]
    public async Task<IActionResult> AddItemToCart([FromBody] ProductQuantity cartItem, Guid customerId)
    {
        var result = await _cartService.AddCartItem(customerId, cartItem);

        return Created("", result);
    }


    [HttpGet("cart")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(statusCode: StatusCodes.Status200OK, type: typeof(CartDetails))]
    public async Task<IActionResult> GetCartContents(Guid customerId)
    {
        return Ok(await _cartService.GetCartDetails(customerId));
    }

    [HttpPost("checkout")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(statusCode: StatusCodes.Status202Accepted, type: typeof(CartDetails))]
    public async Task<IActionResult> CheckoutCart([FromBody] CheckoutRequestDTO checkoutRequest)
    {
        try
        {
            var result = await _cartService.Checkout(checkoutRequest.CustomerId, checkoutRequest.Address);

            return Accepted("", result);
        }
        catch (CheckoutFailedCartEmptyException)
        {
            return BadRequest("You can't checkout with an empty cart");
        }
    }

    [HttpDelete("deleteCart")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteCartContents(Guid customerId)
    {
        var result = await _cartService.DeleteCartContents(customerId);

        return NoContent();
    }
}