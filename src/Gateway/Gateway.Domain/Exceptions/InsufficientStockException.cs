namespace Gateway.Domain.Exceptions
{
    public class InsufficientStockException : Exception
    {
        public InsufficientStockException(string? message) : base(message)
        {

        }
    }
}

