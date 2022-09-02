using Microsoft.AspNetCore.Mvc;

namespace Gateway.API.Controllers;

[Route("api/{version:apiVersion}/[controller]")]
[ApiController]
public class BaseController : ControllerBase
{
}
