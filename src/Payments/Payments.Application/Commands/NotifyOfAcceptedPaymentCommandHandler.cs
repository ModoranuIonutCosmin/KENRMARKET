using IntegrationEvents.Contracts;
using IntegrationEvents.Models;
using MassTransit;
using MediatR;
using Payments.Application.Interfaces;
using Payments.Domain.Entities;

namespace Payments.Application.Commands;

public class NotifyOfAcceptedPaymentCommandHandler : IRequestHandler<NotifyOfAcceptedPaymentCommand, Payment>
{
    private readonly IPaymentsRepository _paymentsRepository;
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly IUnitOfWork _unitOfWork;

    public NotifyOfAcceptedPaymentCommandHandler(IPaymentsRepository paymentsRepository,
        IUnitOfWork unitOfWork,
        IPublishEndpoint publishEndpoint)
    {
        _paymentsRepository = paymentsRepository;
        _unitOfWork = unitOfWork;
        _publishEndpoint = publishEndpoint;
    }

    public async Task<Payment> Handle(NotifyOfAcceptedPaymentCommand request, CancellationToken cancellationToken)
    {
        var payment = new Payment
        {
            OrderId = request.OrderId,
            PayerId = request.PayerId,
            PaymentAmount = request.PaymentAmount,
            PaymentDate = request.PaymentDate
        };

        //TODO: De asigurat atomicitate

        //TODO: AutoMapper?

        await _paymentsRepository.AddPaymentAsync(payment);

        await _publishEndpoint.Publish(new OrderPaymentSuccessfulForEvent(payment.PaymentAmount,
            payment.OrderId, payment.PaymentDate, payment.PayerId, OrderStatus.Paid), cancellationToken);

        await _unitOfWork.CommitTransaction();


        return payment;
    }
}