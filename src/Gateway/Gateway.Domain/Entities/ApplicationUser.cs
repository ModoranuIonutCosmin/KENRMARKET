using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Gateway.Domain.DomainEvents;
using Gateway.Domain.Shared;
using Microsoft.AspNetCore.Identity;

namespace Gateway.Domain.Entities;

public class ApplicationUser : IdentityUser<Guid>, IEntity
{
    public string FirstName { get; set; }

    public string LastName { get; set; }

    [NotMapped] [JsonIgnore] public List<IDomainEvent> DomainEvents => _domainEvents;
    [NotMapped] [JsonIgnore] public List<IDomainEvent> _domainEvents = new();
    public ApplicationUser()
    {
        this.AddDomainEvent(new UserRegisteredDomainEvent(this));
    }

    public void AddDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }
}