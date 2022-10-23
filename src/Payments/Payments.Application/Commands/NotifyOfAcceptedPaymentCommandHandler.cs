using IntegrationEvents.Contracts;
using MassTransit;
using MediatR;
using Payments.Application.Interfaces;
using Payments.Domain.Entities;

namespace Payments.Application.Commands
{
    public class NotifyOfAcceptedPaymentCommandHandler: IRequestHandler<NotifyOfAcceptedPaymentCommand, Payment>
    {
        private readonly IPaymentsRepository _paymentsRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPublishEndpoint _publishEndpoint;

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
            Payment payment = new Payment()
            {
                OrderId = request.OrderId,
                PayerId = request.PayerId,
                PaymentAmount = request.PaymentAmount,
                PaymentDate = request.PaymentDate
            };

            //TODO: De asigurat atomicitate

            //TODO: AutoMapper?

            await _paymentsRepository.AddPaymentAsync(payment);

            await _publishEndpoint.Publish(new PaymentSuccessfulForOrderEvent(paymentAmount: payment.PaymentAmount,
                orderId: payment.OrderId, dateFinalized: payment.PaymentDate, payerId: payment.PayerId), cancellationToken);

            await _unitOfWork.CommitTransaction();
            

            return payment;
        }
    }
}

