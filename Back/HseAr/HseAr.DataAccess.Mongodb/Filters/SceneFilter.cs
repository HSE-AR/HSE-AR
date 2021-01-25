using Microsoft.VisualBasic.CompilerServices;
using MongoDB.Bson;
using MongoDB.Driver;

namespace HseAr.DataAccess.Mongodb.Filters
{
    public class SceneFilter
    {
        public static FilterDefinition<BsonDocument> GetFilterById(string modelId)
        {
            return Builders<BsonDocument>.Filter.Eq("_id", ObjectId.Parse(modelId));
        }
    }
}