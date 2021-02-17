using System;
using HseAr.Data.Enums;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace HseAr.Data.Entities
{
    public class SceneModificationBson
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public DateTime EditedAtUtc { get; set; }

        public SceneModificationType Type { get; set; }
        
        public BsonDocument Object { get; set; }

        public string ModelId { get; set; }
    }
}