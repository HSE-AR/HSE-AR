using HseAr.Core.BsonJson;
using HseAr.Data.DataProjections;
using HseAr.Data.Entities;
using HseAr.Infrastructure;
using MongoDB.Bson;
using Newtonsoft.Json.Linq;

namespace HseAr.DataAccess.Mongodb.Mappers
{
    public class SceneMapper : IMapper<SceneEntity, Scene>, IMapper<Scene, SceneEntity>
    {
        
        Scene IMapper<SceneEntity,Scene>.Map(SceneEntity source)
            => new Scene() 
                {
                    Id = source.Id,
                    Materials = JArray.Parse(source.Materials.ToJson()),
                    Metadata = JObject.Parse(source.Metadata.ToJson()),
                    Geometries = JArray.Parse(source.Geometries.ToJson()),
                    Object = JObject.Parse(source.Object.ToJson())
                };

        SceneEntity IMapper<Scene,SceneEntity>.Map(Scene source)
            => new SceneEntity()
                {
                    Id = source.Id,
                    Materials = Serializer.GetBsonArrayFromString(source.Materials.ToString()),
                    Metadata = BsonDocument.Parse(source.Metadata.ToString()),
                    Geometries = Serializer.GetBsonArrayFromString(source.Geometries.ToString()),
                    Object = BsonDocument.Parse(source.Object.ToString())
                };
    }
}