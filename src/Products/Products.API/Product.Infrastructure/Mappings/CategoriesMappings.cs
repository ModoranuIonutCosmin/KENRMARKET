using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson.Serialization.Serializers;
using Products.Domain.Entities;

namespace Products.Infrastructure.Mappings
{
    public static class CategoriesMappings
    {
        public static void Map()
        {
            BsonClassMap.RegisterClassMap<Category>(cm =>
            {
                cm.AutoMap();
                cm.MapIdMember(c => c.Id)
                    .SetIdGenerator(new StringObjectIdGenerator())
                    .SetSerializer(new StringSerializer(BsonType.ObjectId));

                cm.SetIgnoreExtraElements(true);
            });
        }
    }
}

