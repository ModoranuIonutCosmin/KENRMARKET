using System.ComponentModel.DataAnnotations;

namespace Gateway.API.Auth.Models.Auth;

public class ResetPasswordRequest
{
    [EmailAddress][Required] public string Email { get; set; }

    public string Token { get; set; }

    public string NewPassword { get; set; }
}