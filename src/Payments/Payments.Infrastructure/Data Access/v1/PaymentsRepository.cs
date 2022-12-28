using Microsoft.Extensions.Logging;
using Payments.Application.Interfaces;
using Payments.Domain.Entities;
using Payments.Infrastructure.Data_Access.Base;

namespace Payments.Infrastructure.Data_Access.v1;

public class PaymentsRepository : Repository<Payment, Guid>, IPaymentsRepository
{
    private readonly ILogger<PaymentsRepository> _logger;
    private readonly PaymentsDBContext           _paymentsDbContext;
    private readonly IUnitOfWork                 _unitOfWork;

    public PaymentsRepository(PaymentsDBContext paymentsDbContext, ILogger<PaymentsRepository> logger,
        IUnitOfWork unitOfWork)
        : base(paymentsDbContext, logger, unitOfWork)
    {
        _paymentsDbContext = paymentsDbContext;
        _logger            = logger;
        _unitOfWork        = unitOfWork;
    }


    public async Task<Payment> AddPaymentAsync(Payment payment)
    {
        await _paymentsDbContext.AddAsync(payment);

        return payment;
    }
}