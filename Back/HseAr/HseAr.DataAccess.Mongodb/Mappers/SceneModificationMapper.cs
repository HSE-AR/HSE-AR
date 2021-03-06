﻿using HseAr.Data.DataProjections;
using HseAr.Data.Entities;
using HseAr.Infrastructure;
using MongoDB.Bson;
using Newtonsoft.Json.Linq;

namespace HseAr.DataAccess.Mongodb.Mappers
{
    public class SceneModificationMapper : IMapper<SceneModificationBson, SceneModification>, IMapper<SceneModification, SceneModificationBson>
    {
        SceneModification IMapper<SceneModificationBson,SceneModification>.Map(SceneModificationBson source)
            => new SceneModification() 
            {
                Id = source.Id,
                EditedAtUtc = source.EditedAtUtc,
                DataJson = GetJsonOrNull(source.DataBson.ToJson()),
                SceneId = source.SceneId,
                Type = source.Type,
            };
        
        SceneModificationBson IMapper<SceneModification,SceneModificationBson>.Map(SceneModification source)
            => new SceneModificationBson() 
            {
                Id = source.Id,
                EditedAtUtc = source.EditedAtUtc,
                DataBson = GetBsonOrNull(source.DataJson.ToString()),
                SceneId = source.SceneId,
                Type = source.Type,
            };
        
        private JObject GetJsonOrNull(string source)
        {
            if (source != null)
            {
                return JObject.Parse(source);
            }
                
            return null;
        }

        private BsonDocument GetBsonOrNull(string source)
        {
            if (source != null)
            {
                return BsonDocument.Parse(source);
            }
                
            return null;
        }
        
        
    }
}
