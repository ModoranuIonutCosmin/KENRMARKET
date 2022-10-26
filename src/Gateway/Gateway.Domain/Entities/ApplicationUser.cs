using Gateway.Domain.Shared;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Gateway.Domain.Entities
{
    public class ApplicationUser : IdentityUser<Guid>, IEntity
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        [NotMapped]
        [JsonIgnore]
        public List<IDomainEvent> DomainEvents => new();

        public void AddDomainEvent(IDomainEvent domainEvent)
        {
            DomainEvents.Add(domainEvent);
        }
    }
}

