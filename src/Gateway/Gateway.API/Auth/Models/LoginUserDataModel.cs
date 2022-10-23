namespace Gateway.API.Auth.Models;

public class LoginUserDataModel
{
    public string UserNameOrEmail { get; set; }

    public string Password { get; set; }
}