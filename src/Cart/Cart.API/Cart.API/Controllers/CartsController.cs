using Cart.Application.Interfaces.Services;
using Cart.Domain.Entities;
using Cart.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Cart.API.Controllers
{
    [ApiVersion("1.0")]
    public class CartsController : BaseController 
    {
        private readonly ICartService _cartService;

        public CartsController(ICartService cartService)
        {
            this._cartService = cartService;
        }

        [HttpPost]
        public async Task<IActionResult> AddItemToCart([FromBody] CartItemViewModel cartItem, string customerId)
        {
            var result = await _cartService.AddCartItem(customerId, cartItem);

            return Created("", result);
        }

        [HttpGet("Cart")]
        public async Task<IActionResult> GetCartContents(string customerId)
        {
            return Ok(await _cartService.GetCartDetails(customerId));
        }
    }
}
