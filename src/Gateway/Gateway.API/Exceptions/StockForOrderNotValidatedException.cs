namespace Gateway.API.Exceptions;

public class StockForOrderNotValidatedException : Exception
{
    public StockForOrderNotValidatedException(string? message) : base(message)
    {
    }
}