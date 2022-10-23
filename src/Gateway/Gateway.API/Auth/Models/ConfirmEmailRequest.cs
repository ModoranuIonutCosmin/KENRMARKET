namespace Gateway.API.Auth.Models;

public class ConfirmEmailRequest
{
    public string Email { get; set; }
    public string Token { get; set; }
}