using Gateway.API.Auth.ExtensionMethods;
using Gateway.API.Auth.Models.Carts;
using Gateway.API.Interfaces;
using Gateway.API.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gateway.API.Controllers
{
    [ApiVersion("1.0")]
    public class CartController: BaseController
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
            Guid customerId = Guid.Parse(User.GetLoggedInUserId<string>());

            var cartDetails = await _cartAggregates.GetFullCartDetails(customerId);

            if (cartDetails.IsSuccess)
            {
                return Ok(cartDetails.FullCartDetails);
            }

            return NotFound();
        }

        [HttpPut]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> UpdateCart(UpdateCartRequestDTO cartDetails)
        {
            Guid customerId = Guid.Parse(User.GetLoggedInUserId<string>());

            var result = await _cartAggregates.ModifyCart(customerId, cartDetails);

            if (result.IsSuccess)
            {
                return Ok(result.FullCartDetails);
            }

            return NotFound();
        }
    }
}
