namespace Gateway.API.Exceptions;

public class InvalidPasswordResetLink : Exception
{
    public InvalidPasswordResetLink(string? message) : base(message)
    {
    }
}