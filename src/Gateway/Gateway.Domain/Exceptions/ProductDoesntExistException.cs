namespace Gateway.Domain.Exceptions;

public class ProductDoesntExistException : Exception
{
    public ProductDoesntExistException(string? message) : base(message)
    {
    }
}