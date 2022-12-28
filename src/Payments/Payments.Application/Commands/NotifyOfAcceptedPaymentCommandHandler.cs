using IntegrationEvents.Contracts;
using IntegrationEvents.Models;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using Payments.Application.Interfaces;
using Payments.Domain.Entities;

namespace Payments.Application.Commands;

public class NotifyOfAcceptedPaymentCommandHandler : IRequestHandler<NotifyOfAcceptedPaymentCommand, Payment>
{
    private readonly ILogger<NotifyOfAcceptedPaymentCommandHandler> _logger;
    private readonly IPaymentsRepository                            _paymentsRepository;
    private readonly IPublishEndpoint                               _publishEndpoint;
    private readonly IUnitOfWork                                    _unitOfWork;

    public NotifyOfAcceptedPaymentCommandHandler(IPaymentsRepository paymentsRepository,
        IUnitOfWork unitOfWork,
        IPublishEndpoint publishEndpoint,
        ILogger<NotifyOfAcceptedPaymentCommandHandler> logger)
    {
        _paymentsRepository = paymentsRepository;
        _unitOfWork         = unitOfWork;
        _publishEndpoint    = publishEndpoint;
        _logger             = logger;
    }

    public async Task<Payment> Handle(NotifyOfAcceptedPaymentCommand request, CancellationToken cancellationToken)
    {
        var payment = new Payment
                      {
                          OrderId       = request.OrderId,
                          PayerId       = request.PayerId,
                          PaymentAmount = request.PaymentAmount,
                          PaymentDate   = request.PaymentDate
                      };

        //TODO: De asigurat atomicitate
        var existentPayments
            = await _paymentsRepository.GetAllWhereAsync(p => p.OrderId.Equals(request.OrderId));
        if (existentPayments.Any())
        {
            _logger.LogInformation("Payment for this orderId={@orderId} already exists", request.OrderId);

            return existentPayments.First();
        }

        //TODO: AutoMapper?

        await _paymentsRepository.AddPaymentAsync(payment);


        await _publishEndpoint.Publish(new OrderPaymentSuccessfulIntegrationEvent(payment.PaymentAmount,
                                        payment.OrderId, payment.PaymentDate, payment.PayerId, OrderStatus.Paid),
                                       cancellationToken);

        await _unitOfWork.CommitTransaction();

        _logger.LogInformation("Payment for this orderId={@orderId} stored", request.OrderId);
        _logger.LogInformation("Payment success event sent to order microservice for orderId={@orderId}",
                               request.OrderId);


        return payment;
    }
}