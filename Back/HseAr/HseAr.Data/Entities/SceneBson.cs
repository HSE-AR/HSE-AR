using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace HseAr.Data.Entities
{
    public class SceneBson
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        
        [BsonElement("metadata")]
        public BsonDocument Metadata { get; set; }
        
        [BsonElement("geometries")]
        public BsonArray Geometries { get; set; }
        
        [BsonElement("materials")]
        public BsonArray Materials { get; set; }
        
        [BsonElement("object")]
        public BsonDocument Object { get; set; }
        
        [BsonElement("textures")]
        public BsonArray Textures { get; set; }
        
        [BsonElement("images")]
        public BsonArray Images { get; set; }
        
        [BsonElement("animations")]
        public BsonArray Animations { get; set; }
    }
}