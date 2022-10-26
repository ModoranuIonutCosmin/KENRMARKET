using Gateway.API.Auth.Models.Auth;
using Gateway.API.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Gateway.API.Controllers;

[ApiController]
[ApiVersion("1.0")]
public class AccountController : BaseController
{
    private readonly IConfiguration _configuration;
    private readonly IUserPasswordResetService _passwordResetService;
    private readonly IUserAuthenticationService _userAuthenticationService;

    public AccountController(
        IConfiguration configuration,
        IUserAuthenticationService userAuthenticationService,
        IUserPasswordResetService passwordResetService
    )
    {
        _configuration = configuration;
        _userAuthenticationService = userAuthenticationService;
        _passwordResetService = passwordResetService;
    }

    [HttpPost("register")]
    public async Task<RegisterUserDataModelResponse> RegisterAsync(
        [FromBody] RegisterUserDataModelRequest registerData)
    {
        var frontEndUrl = (Environment.GetEnvironmentVariable("FrontEndUrl") ??
                           _configuration["FrontEnd:URL"]) +
                          _configuration["FrontEnd:ConfirmationRoute"];

        return await _userAuthenticationService.RegisterAsync(registerData, frontEndUrl);
    }

    [HttpPost("login")]
    public async Task<UserProfileDetailsApiModel> LoginAsync(
        [FromBody] LoginUserDataModel loginData)
    {
        var issuer = Environment.GetEnvironmentVariable("JwtIssuer") ?? _configuration["Jwt:Issuer"];
        var audience = Environment.GetEnvironmentVariable("JwtAudience") ?? _configuration["Jwt:Audience"];
        var secret = Environment.GetEnvironmentVariable("JwtSecret") ?? _configuration["Jwt:Secret"];

        return
            await _userAuthenticationService.LoginAsync(loginData, secret, issuer, audience);
    }

    [HttpPost("ConfirmEmail")]
    public async Task<IActionResult> ConfirmEmail([FromBody] ConfirmEmailRequest confirmEmailRequest)
    {
        await _userAuthenticationService.ConfirmEmail(confirmEmailRequest.Email,
            confirmEmailRequest.Token);

        return Ok("Email confirmed!");
    }


    [HttpPost("ForgotPassword")]
    public async Task<IActionResult> ForgotPasswordRequest([FromBody] ModifyPasswordRequest request)
    {
        var frontEndUrl = (Environment.GetEnvironmentVariable("FrontEndUrl") ??
                           _configuration["FrontEnd:URL"]) +
                          _configuration["FrontEnd:ResetPasswordRoute"];

        await _passwordResetService.ForgotPasswordRequest(request,
            frontEndUrl);

        return Ok("Email confirmed!");
    }

    [HttpPut("ResetPassword")]
    public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequest request)
    {
        await _passwordResetService.ResetPassword(request);

        return Ok("Password changed successfully.");
    }
}