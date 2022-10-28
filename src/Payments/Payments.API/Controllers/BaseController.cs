using Microsoft.AspNetCore.Mvc;

namespace Payments.API.Controllers;

[Route("api/{version:apiVersion}/[controller]")]
[ApiController]
public class BaseController : ControllerBase
{
}