namespace Order.Domain.DataModels;

public enum OrderStatus
{
    InitialCreation,
    PendingValidation,
    StocksValidationAccepted,
    StocksValidationRejected,
    Paid,
    Shipped,
}