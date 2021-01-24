using HseAr.Data.DataProjections;
using HseAr.Data.Entities;
using HseAr.Infrastructure;
using MongoDB.Bson;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace HseAr.DataAccess.Mongodb.Mappers
{
    public class SceneModificationMapper : IMapper<SceneModificationEntity, SceneModification>, IMapper<SceneModification, SceneModificationEntity>
    {
        SceneModification IMapper<SceneModificationEntity,SceneModification>.Map(SceneModificationEntity source)
            => new SceneModification() 
            {
                Id = source.Id,
                EditedAtUtc = source.EditedAtUtc,
                Object = GetJsonOrNull(source.Object.ToJson()),
                Geometry = GetJsonOrNull(source.Geometry.ToJson()),
                ObjectChild  = GetJsonOrNull(source.ObjectChild .ToJson()),
                Material = GetJsonOrNull(source.Material .ToJson()),
                ModelId = source.ModelId,
                SceneComponentType = source.SceneComponentType,
                Type = source.Type,
                PropertyModificationType = source.PropertyModificationType
            };
        
        SceneModificationEntity IMapper<SceneModification,SceneModificationEntity>.Map(SceneModification source)
            => new SceneModificationEntity() 
            {
                Id = source.Id,
                EditedAtUtc = source.EditedAtUtc,
                Object = GetBsonOrNull(source.Object.ToJson()),
                Geometry = GetBsonOrNull(source.Geometry.ToJson()),
                ObjectChild  = GetBsonOrNull(source.ObjectChild .ToJson()),
                Material = GetBsonOrNull(source.Material .ToJson()),
                ModelId = source.ModelId,
                SceneComponentType = source.SceneComponentType,
                Type = source.Type,
                PropertyModificationType = source.PropertyModificationType
            };
        
        private JObject GetJsonOrNull(string source)
        {
            if (source != null)
            {
                return JObject.Parse(source.ToJson());
            }
                
            return null;
        }

        private BsonDocument GetBsonOrNull(string source)
        {
            if (source != null)
            {
                return BsonDocument.Parse(source.ToJson());
            }
                
            return null;
        }
        
        
    }
}
