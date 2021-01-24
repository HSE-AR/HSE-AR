using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace HseAr.Core.BsonJson
{
    public class Serializer
    {
        public static BsonArray GetBsonArrayFromString(string text)
        {
            using (var jsonReader = new JsonReader(text))
            {
                var serializer = new BsonArraySerializer();
                return serializer.Deserialize(BsonDeserializationContext.CreateRoot(jsonReader));
            }
        }
    }
}