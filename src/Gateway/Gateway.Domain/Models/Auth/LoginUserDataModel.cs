namespace Gateway.Domain.Models.Auth;

public class LoginUserDataModel
{
    public string UserNameOrEmail { get; set; }

    public string Password { get; set; }
}