namespace Gateway.Domain.Exceptions;

public class InvalidConfirmationLinkException : Exception
{
    public InvalidConfirmationLinkException(string? message) : base(message)
    {
    }
}