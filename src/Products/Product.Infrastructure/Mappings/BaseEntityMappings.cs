using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using Products.Domain.Shared;

namespace Products.Infrastructure.Mappings;

public class BaseEntityMappings
{
    public static void Map()
    {
        BsonClassMap.RegisterClassMap<Entity>(cm =>
        {
            cm.UnmapMember(c => c.DomainEvents);

            cm.MapIdProperty(c => c.Id)
              .SetIdGenerator(GuidGenerator.Instance);
        });
    }
}