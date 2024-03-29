﻿using MediatR;

namespace Order.Domain.Shared;

public interface IDomainEvent : INotification
{
    public Guid CustomerId { get; set; }
}