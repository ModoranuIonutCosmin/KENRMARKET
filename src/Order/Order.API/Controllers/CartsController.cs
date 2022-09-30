using Microsoft.AspNetCore.Mvc;
using Order.Application.Interfaces.Services;
using Order.Domain.Models;

namespace Order.API.Controllers
{
    [ApiVersion("1.0")]
    public class CartsController : BaseController 
    {
        private readonly ICartService _cartService;

        public CartsController(ICartService cartService)
        {
            this._cartService = cartService;
        }

        [HttpPost("order")]
        public async Task<IActionResult> GetCartContents(string customerId)
        {
            return Ok(await _cartService.GetCartDetails(customerId));
        }
    }
}
