using Gateway.Domain.Models.Auth;

namespace Gateway.Application.Interfaces;

public interface IUserPasswordResetService
{
    Task ForgotPasswordRequest(ModifyPasswordRequest request,
        string frontEndUrlResetPasswordUrl);

    Task ResetPassword(ResetPasswordRequest request);
}