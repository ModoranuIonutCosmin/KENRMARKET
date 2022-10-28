using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using Products.Domain.Entities;

namespace Products.Infrastructure.Mappings;

public static class ProductMappings
{
    public static void Map()
    {
        BsonClassMap.RegisterClassMap<Product>(cm =>
        {
            cm.AutoMap();

            cm.MapProperty(p => p.Price)
                .SetSerializer(new DecimalSerializer(BsonType.Decimal128));

            cm.MapProperty(p => p.Quantity)
                .SetSerializer(new DecimalSerializer(BsonType.Decimal128));

            cm.MapProperty(p => p.Discount)
                .SetSerializer(new DecimalSerializer(BsonType.Decimal128));

            cm.SetIgnoreExtraElements(true);
        });
    }
}