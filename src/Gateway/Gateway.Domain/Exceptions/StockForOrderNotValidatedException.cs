namespace Gateway.Domain.Exceptions;

public class StockForOrderNotValidatedException : Exception
{
    public StockForOrderNotValidatedException(string? message) : base(message)
    {
    }
}