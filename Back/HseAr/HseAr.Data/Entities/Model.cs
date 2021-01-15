using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace HseAr.Data.Entities
{
    public class Model
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }


        public string Name { get; set; }

        public DateTime CreatedAtUtc { get; set; }

        public BsonDocument Scene { get; set; }
    }
}