using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson.Serialization.Serializers;
using Products.Domain.Entities;

namespace Products.Infrastructure.Mappings
{
    public static class ProductMappings
    {
        public static void Map()
        {
            BsonClassMap.RegisterClassMap<Product>(cm =>
            {
                BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String));

                cm.AutoMap();
                cm.MapIdProperty(c => c.Id)
                    .SetIdGenerator(new GuidGenerator())
                    .SetSerializer(new GuidSerializer(BsonType.String));

                cm.SetIgnoreExtraElements(true);
            });
        }
    }
}

