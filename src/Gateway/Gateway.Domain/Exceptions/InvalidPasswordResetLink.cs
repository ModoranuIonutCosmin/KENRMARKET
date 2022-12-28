namespace Gateway.Domain.Exceptions;

public class InvalidPasswordResetLink : Exception
{
    public InvalidPasswordResetLink(string? message) : base(message)
    {
    }
}