namespace Customers.Domain.Exceptions;

public class CustomerAlreadyExistsException : Exception
{
    public CustomerAlreadyExistsException(string? message) : base(message)
    {
    }
}