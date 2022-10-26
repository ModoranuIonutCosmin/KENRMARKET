using System.ComponentModel.DataAnnotations;

namespace Gateway.API.Auth.Models.Auth;

public class ModifyPasswordRequest
{
    [EmailAddress][Required] public string Email { get; set; }
}