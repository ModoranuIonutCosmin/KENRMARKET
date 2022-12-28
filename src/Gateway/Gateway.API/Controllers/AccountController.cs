using System.Security.Authentication;
using Gateway.Application.Interfaces;
using Gateway.Domain.Models.Auth;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Gateway.API.Controllers;

[ApiController]
[ApiVersion("1.0")]
public class AccountController : BaseController
{
    private readonly IConfiguration             _configuration;
    private readonly IUserPasswordResetService  _passwordResetService;
    private readonly IUserAuthenticationService _userAuthenticationService;

    public AccountController(
        IConfiguration configuration,
        IUserAuthenticationService userAuthenticationService,
        IUserPasswordResetService passwordResetService
    )
    {
        _configuration             = configuration;
        _userAuthenticationService = userAuthenticationService;
        _passwordResetService      = passwordResetService;
    }

    [HttpPost("register")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(statusCode: StatusCodes.Status201Created, type: typeof(RegisterUserDataModelResponse))]
    public async Task<IActionResult> RegisterAsync(
        [FromBody] RegisterUserDataModelRequest registerData)
    {
        var frontEndUrl = (Environment.GetEnvironmentVariable("FrontEndUrl") ??
                           _configuration["FrontEnd:URL"]) +
                          _configuration["FrontEnd:ConfirmationRoute"];

        try
        {
            var registrationResult = await _userAuthenticationService.RegisterAsync(registerData, frontEndUrl);

            return Created($"/user/{registrationResult.UserId}", registrationResult);
        }
        catch (InvalidOperationException e)
        {
            return BadRequest(e.Message);
        }
        catch (AuthenticationException e)
        {
            return StatusCode(StatusCodes.Status403Forbidden, e.Message);
        }
    }

    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(statusCode: StatusCodes.Status200OK, type: typeof(UserProfileDetailsApiModel))]
    public async Task<IActionResult> LoginAsync(
        [FromBody] LoginUserDataModel loginData)
    {
        var issuer   = Environment.GetEnvironmentVariable("JwtIssuer") ?? _configuration["Jwt:Issuer"];
        var audience = Environment.GetEnvironmentVariable("JwtAudience") ?? _configuration["Jwt:Audience"];
        var secret   = Environment.GetEnvironmentVariable("JwtSecret") ?? _configuration["Jwt:Secret"];

        try
        {
            var result =
                await _userAuthenticationService.LoginAsync(loginData, secret, issuer, audience);

            return Ok(result);
        }
        catch (AuthenticationException e)
        {
            return StatusCode(StatusCodes.Status403Forbidden, e.Message);
        }
        catch (ArgumentNullException e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost("confirmEmail")]
    public async Task<IActionResult> ConfirmEmail([FromBody] ConfirmEmailRequest confirmEmailRequest)
    {
        await _userAuthenticationService.ConfirmEmail(confirmEmailRequest.Email,
                                                      confirmEmailRequest.Token);

        return Ok("Email confirmed!");
    }


    [HttpPost("forgotPassword")]
    public async Task<IActionResult> ForgotPasswordEmailRequest([FromBody] ModifyPasswordRequest request)
    {
        var frontEndUrl = (Environment.GetEnvironmentVariable("FrontEndUrl") ??
                           _configuration["FrontEnd:URL"]) +
                          _configuration["FrontEnd:ResetPasswordRoute"];

        await _passwordResetService.ForgotPasswordRequest(request,
                                                          frontEndUrl);

        return Ok("Email confirmed!");
    }

    [HttpPut("resetPassword")]
    public async Task<IActionResult> ResetPasswordWithEmailToken([FromBody] ResetPasswordRequest request)
    {
        await _passwordResetService.ResetPassword(request);

        return Ok("Password changed successfully.");
    }
}