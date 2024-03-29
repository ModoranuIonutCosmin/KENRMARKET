﻿using Gateway.API.Auth.ExtensionMethods;
using Gateway.Application.Interfaces;
using Gateway.Domain.Entities;
using Gateway.Domain.Exceptions;
using Gateway.Domain.Models.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;

namespace Gateway.API.Auth;

public class UserPasswordResetService : IUserPasswordResetService
{
    private readonly UserManager<ApplicationUser> _userManager;

    public UserPasswordResetService(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task ForgotPasswordRequest(ModifyPasswordRequest request,
        string frontEndUrlResetPasswordUrl)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);

        if (user is null)
        {
            throw new UserNotFoundException("Invalid user email!");
        }

        var token = await _userManager.GeneratePasswordResetTokenAsync(user);

        var queryParams = new Dictionary<string, string>
                          {
                              { "token", token },
                              { "email", request.Email }
                          };

        var resetLink = QueryHelpers.AddQueryString(frontEndUrlResetPasswordUrl, queryParams);

        //await _emailSender.SendResetPasswordEmail(user, resetLink);
    }

    public async Task ResetPassword(ResetPasswordRequest request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);

        if (user is null)
        {
            throw new UserNotFoundException("Invalid user email!");
        }

        var result = await _userManager.ResetPasswordAsync(user, request.Token, request.NewPassword);

        if (!result.Succeeded)
        {
            throw new InvalidPasswordResetLink(result.Errors.AggregateErrors());
        }
    }
}