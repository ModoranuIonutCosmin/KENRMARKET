using Gateway.API.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Gateway.API.Controllers
{
    [ApiVersion("1.0")]
    public class CheckoutController: BaseController
    {
        private readonly ICartAggregatesService _cartAggregates;

        public CheckoutController(ICartAggregatesService cartAggregates)
        {
            _cartAggregates = cartAggregates;
        }
        [HttpGet]
        public async Task<IActionResult> GetCartContents()
        {
            string customerId = "acbcb196-0d48-4865-9417-eddb9c1b5ce0";

            var cartDetails = await _cartAggregates.GetFullCartDetails(customerId);

            if (cartDetails.IsSuccess)
            {
                return Ok(cartDetails.FullCartDetails);
            }

            return NotFound();
        }
    }
}
