using Microsoft.AspNetCore.Mvc;

namespace Cart.API.Controllers;

[Route("api/{version:apiVersion}/[controller]")]
[ApiController]
public class BaseController : ControllerBase
{
}