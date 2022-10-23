namespace Order.Domain.Exceptions
{
    public class OrderDoesntExistException: Exception
    {
        public OrderDoesntExistException()
        {
        }

        public OrderDoesntExistException(string? message) : base(message)
        {
        }
    }
}

