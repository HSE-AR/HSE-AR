using System;
using HseAr.Data.Enums;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace HseAr.Data.Entities
{
    public class SceneModificationEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public DateTime EditedAtUtc { get; set; }

        public SceneModificationType Type { get; set; }

        public SceneElementType SceneElementType { get; set; }

        public SceneModificationType PropertyModificationType { get; set; }

        public BsonDocument Geometry { get; set; }

        public BsonDocument Material { get; set; }

        public BsonDocument ObjectChild { get; set; }
        
        public BsonDocument Object { get; set; }

        public string ModelId { get; set; }
    }
}