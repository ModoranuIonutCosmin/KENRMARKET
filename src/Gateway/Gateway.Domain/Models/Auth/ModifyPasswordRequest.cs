using System.ComponentModel.DataAnnotations;

namespace Gateway.Domain.Models.Auth;

public class ModifyPasswordRequest
{
    [EmailAddress] [Required] public string Email { get; set; }
}