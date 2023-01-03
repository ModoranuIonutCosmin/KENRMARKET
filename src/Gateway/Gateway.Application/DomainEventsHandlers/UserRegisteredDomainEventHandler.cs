using Gateway.Domain.DomainEvents;
using IntegrationEvents.Contracts;
using MassTransit;

namespace Gateway.Application.DomainEventsHandlers;

public class UserRegisteredDomainEventHandler : DomainEventHandler<UserRegisteredDomainEvent>
{
    private readonly IPublishEndpoint _publishEndpoint;

    public UserRegisteredDomainEventHandler(IPublishEndpoint publishEndpoint)
    {
        _publishEndpoint = publishEndpoint;
    }

    public override async Task Handle(UserRegisteredDomainEvent notification, CancellationToken cancellationToken)
    {
        //TODO: Outbox
        
        await _publishEndpoint.Publish(new NewCustomerRegisteredIntegrationEvent(firstName: notification.ApplicationUser.FirstName,
                                            lastName: notification.ApplicationUser.LastName, email: notification.ApplicationUser.Email,
                                            userName: notification.ApplicationUser.UserName, userId: notification.ApplicationUser.Id));
    }
}