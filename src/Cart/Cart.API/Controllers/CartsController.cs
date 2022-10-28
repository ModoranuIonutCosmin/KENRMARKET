using Cart.API.DTOs;
using Cart.Application.Interfaces.Services;
using Cart.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using CartItemDTO = Cart.Domain.Models.CartItemDTO;

namespace Cart.API.Controllers;

[ApiVersion("1.0")]
public class CartsController : BaseController
{
    private readonly ICartService _cartService;

    public CartsController(ICartService cartService)
    {
        _cartService = cartService;
    }

    [HttpPut("ModifyCart")]
    public async Task<IActionResult> ModifyCart([FromQuery] Guid customerId,
        [FromBody] CartDetails newCartDetails)
    {
        //TODO: 

        var result = await _cartService.ModifyCart(customerId, newCartDetails);

        return Ok(result);
    }

    [HttpPost("AddItemToCart")]
    public async Task<IActionResult> AddItemToCart([FromBody] CartItemDTO cartItem, Guid customerId)
    {
        var result = await _cartService.AddCartItem(customerId, cartItem);

        return Created("", result);
    }


    [HttpGet("Cart")]
    public async Task<IActionResult> GetCartContents(Guid customerId)
    {
        return Ok(await _cartService.GetCartDetails(customerId));
    }

    [HttpPost("Checkout")]
    public async Task<IActionResult> CheckoutCart([FromBody] CheckoutRequestDTO checkoutRequest)
    {
        return Created("", await _cartService.Checkout(checkoutRequest.CustomerId, checkoutRequest.Address));
    }
    
    [HttpDelete("DeleteCart")]
    public async Task<IActionResult> DeleteCartContents(Guid customerId)
    {
        return Ok(await _cartService.DeleteCartContents(customerId));
    }
}