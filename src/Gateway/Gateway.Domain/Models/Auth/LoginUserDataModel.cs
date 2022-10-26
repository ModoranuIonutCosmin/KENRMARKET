namespace Gateway.API.Auth.Models.Auth;

public class LoginUserDataModel
{
    public string UserNameOrEmail { get; set; }

    public string Password { get; set; }
}