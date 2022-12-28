using Gateway.Domain.Models.Auth;

namespace Gateway.Application.Interfaces;

public interface IUserAuthenticationService
{
    Task<RegisterUserDataModelResponse> RegisterAsync(
        RegisterUserDataModelRequest registerData,
        string frontEndUrl);

    Task<UserProfileDetailsApiModel> LoginAsync(LoginUserDataModel loginData,
        string jwtSecret, string issuer, string audience);

    Task ConfirmEmail(string email, string confirmationToken);
}