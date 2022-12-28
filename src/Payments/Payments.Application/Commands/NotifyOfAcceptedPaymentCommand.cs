using MediatR;
using Payments.Domain.Entities;

namespace Payments.Application.Commands;

public class NotifyOfAcceptedPaymentCommand : IRequest<Payment>
{
    public Guid           OrderId       { get; set; }
    public Guid           PayerId       { get; set; }
    public DateTimeOffset PaymentDate   { get; set; }
    public decimal        PaymentAmount { get; set; }
}