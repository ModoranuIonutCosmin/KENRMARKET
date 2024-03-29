﻿using Payments.Application.Interfaces.Base;
using Payments.Domain.Entities;

namespace Payments.Application.Interfaces;

public interface IPaymentsRepository : IRepository<Payment, Guid>
{
    public Task<Payment> AddPaymentAsync(Payment payment);
}