using Microsoft.AspNetCore.Mvc;

namespace Products.API.Controllers;

[Route("api/{version:apiVersion}/[controller]")]
[ApiController]
public class BaseController : ControllerBase
{
}