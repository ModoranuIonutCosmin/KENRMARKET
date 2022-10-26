using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Gateway.Domain.Shared
{
    public class Entity : IEntity
    {
        public Guid Id { get; set; }

        [NotMapped]
        [JsonIgnore]
        public List<IDomainEvent> DomainEvents { get; } = new();

        public void AddDomainEvent(IDomainEvent domainEvent)
        {
            this.DomainEvents.Add(domainEvent);
        }
    }
}

