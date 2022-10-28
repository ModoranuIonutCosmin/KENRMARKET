namespace Payments.Domain.DataModels;

public enum OrderStatus
{
    InitialCreation,
    Submitted,
    Paid,
    Shipped
}