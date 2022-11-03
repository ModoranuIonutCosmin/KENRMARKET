namespace Gateway.API.Exceptions;

public class InvalidConfirmationLinkException : Exception
{
    public InvalidConfirmationLinkException(string? message) : base(message)
    {
    }
}