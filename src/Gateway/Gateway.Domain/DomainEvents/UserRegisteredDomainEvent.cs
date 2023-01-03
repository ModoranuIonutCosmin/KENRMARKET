using Gateway.Domain.Entities;
using Gateway.Domain.Shared;

namespace Gateway.Domain.DomainEvents;

public class UserRegisteredDomainEvent : IDomainEvent
{
    public UserRegisteredDomainEvent(ApplicationUser applicationUser)
    {
        ApplicationUser = applicationUser;
    }
    
    public ApplicationUser ApplicationUser { get; init; }
}