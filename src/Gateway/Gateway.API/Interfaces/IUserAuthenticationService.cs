﻿using Gateway.API.Auth.Models.Auth;

namespace Gateway.API.Interfaces;

public interface IUserAuthenticationService
{
    Task<RegisterUserDataModelResponse> RegisterAsync(
        RegisterUserDataModelRequest registerData,
        string frontEndUrl);

    Task<UserProfileDetailsApiModel> LoginAsync(LoginUserDataModel loginData,
        string jwtSecret, string issuer, string audience);

    Task ConfirmEmail(string email, string confirmationToken);
}