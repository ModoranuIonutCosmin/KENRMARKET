namespace IntegrationEvents.Models;

public enum OrderStatus
{
    InitialCreation          = 1,
    PendingValidation        = 2,
    StocksValidationAccepted = 3,
    StocksValidationRejected = 4,
    Paid                     = 5,
    Shipped                  = 6
}