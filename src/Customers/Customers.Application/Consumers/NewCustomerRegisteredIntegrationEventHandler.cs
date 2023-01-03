using Customers.Application.Interfaces;
using Customers.Domain.Entities;
using IntegrationEvents.Base;
using IntegrationEvents.Contracts;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Customers.Application.Consumers;

public class NewCustomerRegisteredIntegrationEventHandler : IntegrationEventHandler<NewCustomerRegisteredIntegrationEvent>
{
    private readonly ICustomersService                                     _customersService;
    private readonly ILogger<NewCustomerRegisteredIntegrationEventHandler> _logger;

    public NewCustomerRegisteredIntegrationEventHandler(ICustomersService customersService,
        ILogger<NewCustomerRegisteredIntegrationEventHandler> logger)
    {
        _customersService = customersService;
        _logger      = logger;
    }
    public override async Task Handle(NewCustomerRegisteredIntegrationEvent @event)
    {
        
        _logger.LogInformation("[Customer service] Received new customer with email={@email} message", @event.Email);
        await _customersService.RegisterNewCustomer(@event.UserId,
                                                    @event.FirstName,
                                                    @event.LastName,
                                                    null,
                                                    @event.UserName,
                                                    @event.Email,
                                                    null,
                                                    null,
                                                    null);
        _logger.LogInformation("[Customer service] Processed new customer message  with email={@email}", @event.Email);
    }
}