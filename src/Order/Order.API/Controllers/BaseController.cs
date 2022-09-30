using Microsoft.AspNetCore.Mvc;

namespace Order.API.Controllers;

[Route("api/{version:apiVersion}/[controller]")]
[ApiController]
public class BaseController : ControllerBase
{
}
