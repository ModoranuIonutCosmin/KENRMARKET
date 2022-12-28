using System.ComponentModel.DataAnnotations;

namespace Gateway.Domain.Models.Auth;

public class RegisterUserDataModelRequest
{
    [Required] [MaxLength(100)] public string FirstName { get; set; }

    [Required] [MaxLength(100)] public string LastName { get; set; }

    [Required] [MaxLength(100)] public string UserName { get; set; }

    [Required] [MaxLength(1024)] public string Email { get; set; }

    [Required] [MaxLength(100)] public string Password { get; set; }
}