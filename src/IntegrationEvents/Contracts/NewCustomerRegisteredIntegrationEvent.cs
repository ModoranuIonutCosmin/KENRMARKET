using IntegrationEvents.Base;

namespace IntegrationEvents.Contracts;

public class NewCustomerRegisteredIntegrationEvent : IIntegrationEvent
{
    public string FirstName { get; init; }

    public string LastName { get; init; }
    
    public string Email { get; init; }
    
    public string UserName { get; init; }
    
    public Guid UserId { get; init; }
    
    public Guid   Id       { get; init; }

    public NewCustomerRegisteredIntegrationEvent(string firstName, string lastName, string email, string userName, Guid userId)
    {
        FirstName   = firstName;
        LastName    = lastName;
        Email       = email;
        UserName    = userName;
        UserId = userId;
    }
}