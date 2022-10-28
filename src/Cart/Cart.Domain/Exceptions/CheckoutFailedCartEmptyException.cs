namespace Cart.Domain.Exceptions;

public class CheckoutFailedCartEmptyException : Exception
{
    public CheckoutFailedCartEmptyException(string? message) : base(message)
    {
    }
}