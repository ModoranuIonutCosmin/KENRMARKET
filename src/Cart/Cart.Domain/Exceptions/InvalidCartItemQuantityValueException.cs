namespace Cart.Domain.Exceptions;

public class InvalidCartItemQuantityValueException : Exception
{
    public InvalidCartItemQuantityValueException(string? message) : base(message)
    {
    }
}