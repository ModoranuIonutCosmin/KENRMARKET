namespace Gateway.Domain.Models.Orders;

public enum OrderStatus
{
    InitialCreation,
    PendingValidation,
    StocksValidationAccepted,
    StocksValidationRejected,
    Paid,
    Shipped,
}