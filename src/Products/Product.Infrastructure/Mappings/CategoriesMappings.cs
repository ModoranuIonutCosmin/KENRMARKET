using MongoDB.Bson.Serialization;
using Products.Domain.Entities;

namespace Products.Infrastructure.Mappings;

public static class CategoriesMappings
{
    public static void Map()
    {
        BsonClassMap.RegisterClassMap<Category>(cm =>
        {
            cm.AutoMap();


            cm.SetIgnoreExtraElements(true);
        });
    }
}