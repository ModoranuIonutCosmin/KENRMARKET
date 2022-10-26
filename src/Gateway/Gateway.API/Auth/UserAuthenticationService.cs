﻿using System.Security.Authentication;
using Gateway.API.Auth.ExtensionMethods;
using Gateway.API.Auth.Models.Auth;
using Gateway.API.Exceptions;
using Gateway.API.Interfaces;
using Gateway.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Gateway.API.Auth;

public class UserAuthenticationService : IUserAuthenticationService
{
    private readonly UserManager<ApplicationUser> _userManager;

    public UserAuthenticationService(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<RegisterUserDataModelResponse> RegisterAsync(
        RegisterUserDataModelRequest registerData,
        string frontEndUrl)
    {
        if (registerData == null || string.IsNullOrWhiteSpace(registerData.UserName))
            throw new InvalidOperationException(
                "Bad format for request! Please provide every detail required for registration!");

        var user = new ApplicationUser
        {
            UserName = registerData.UserName,
            FirstName = registerData.FirstName,
            LastName = registerData.LastName,
            Email = registerData.Email
        };

        var result = await _userManager.CreateAsync(user, registerData.Password);

        if (!result.Succeeded) 
            throw new AuthenticationException(result.Errors.AggregateErrors());

        var userIdentity = await _userManager.FindByNameAsync(user.UserName);
    
        ////Generare token de verificarfe email...
        //var emailConfirmationToken = await _userManager
        //    .GenerateEmailConfirmationTokenAsync(userIdentity);

        //var param = new Dictionary<string, string>
        //{
        //    {"email", user.Email},
        //    {"token", emailConfirmationToken}
        //};

        //var confirmUrl = new Uri(QueryHelpers.AddQueryString(frontEndUrl, param));

        ////Trimite email catre user pentru confirmare
        //var emailSendingResult = await _emailSender
        //    .SendConfirmationEmail(userIdentity, confirmUrl.ToString());

        ////Daca nu s-a trimis email-ul cu success
        //if (!emailSendingResult.Successful) Debug.WriteLine($"Email couldn't be sent for user {userIdentity.UserName}");

        return new RegisterUserDataModelResponse
        {
            UserName = userIdentity.UserName,
            FirstName = userIdentity.FirstName,
            LastName = userIdentity.LastName,
            Email = userIdentity.Email,
            Token = "Bearer"
        };
    }

    public async Task<UserProfileDetailsApiModel> LoginAsync(LoginUserDataModel loginData,
        string jwtSecret, string issuer, string audience)
    {
        if (loginData == null) throw new ArgumentNullException(nameof(loginData));

        //Este email?
        var isEmail = loginData.UserNameOrEmail.Contains("@");

        //Cauta userul dupa username sau email
        var user = isEmail
            ? await _userManager.FindByEmailAsync(loginData.UserNameOrEmail)
            : await _userManager.FindByNameAsync(loginData.UserNameOrEmail);


        if (user == null)
        {
            throw new AuthenticationException("Invalid credentials!");
        }

        //Verifica daca parola este corecta fara a incrementa numarul de incercari. (peek)
        var isValidPassword = await _userManager.CheckPasswordAsync(user, loginData.Password);

        if (!isValidPassword)
        {
            throw new AuthenticationException("Invalid credentials!");
        }

        return new UserProfileDetailsApiModel
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            UserName = user.UserName,
            Token = user.GenerateJwtToken(jwtSecret, issuer, audience),
        };
    }

    public async Task ConfirmEmail(string email, string confirmationToken)
    {
        var user = await _userManager.FindByEmailAsync(email);

        if (user == null) throw new InvalidConfirmationLinkException("Invalid confirmation link");

        var status = await _userManager.ConfirmEmailAsync(user, confirmationToken);

        if (!status.Succeeded) throw new InvalidConfirmationLinkException(status.Errors.AggregateErrors());
    }
}