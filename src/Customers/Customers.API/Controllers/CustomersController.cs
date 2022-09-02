using Customers.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Customers.API.Controllers
{
    [ApiVersion("1.0")]
    public class CustomersController : BaseController 
    {
        private readonly ICustomersService _customersService;

        public CustomersController(ICustomersService customersService)
        {
            this._customersService = customersService;
        }

        [HttpGet()]
        public async Task<IActionResult> GetAllCustomersDetails()
        {
            return Ok(await _customersService.GetAllCustomersDetails());
        }

        [HttpGet("{customerId}")]
        public async Task<IActionResult> GetSpecificCustomerDetails([FromRoute]Guid customerId)
        {
            return Ok(await _customersService.GetSpecificCustomerDetails(customerId));
        }
    }
}
