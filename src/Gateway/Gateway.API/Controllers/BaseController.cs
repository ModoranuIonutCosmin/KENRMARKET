using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Gateway.API.Controllers;

[Route("api/{version:apiVersion}/[controller]")]
[ApiController]
public class BaseController : ControllerBase
{


}
