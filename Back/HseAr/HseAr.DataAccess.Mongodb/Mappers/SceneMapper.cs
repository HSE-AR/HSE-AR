using HseAr.Core.BsonJson;
using HseAr.Data.DataProjections;
using HseAr.Data.Entities;
using HseAr.Infrastructure;
using MongoDB.Bson;
using Newtonsoft.Json.Linq;

namespace HseAr.DataAccess.Mongodb.Mappers
{
    public class SceneMapper : IMapper<SceneBson, Scene>, IMapper<Scene, SceneBson>
    {
        
        Scene IMapper<SceneBson,Scene>.Map(SceneBson source)
            => new Scene() 
                {
                    Id = source.Id,
                    Materials = JArray.Parse(source.Materials.ToJson()),
                    Metadata = JObject.Parse(source.Metadata.ToJson()),
                    Geometries = JArray.Parse(source.Geometries.ToJson()),
                    Object = JObject.Parse(source.Object.ToJson()),
                    Images = JArray.Parse(source.Images.ToJson()),
                    Textures = JArray.Parse(source.Textures.ToJson()),
                    Animations = JArray.Parse(source.Animations.ToJson()),
                };

        SceneBson IMapper<Scene,SceneBson>.Map(Scene source)
            => new SceneBson()
                {
                    Id = source.Id,
                    Materials = Serializer.GetBsonArrayFromString(source.Materials.ToString()),
                    Metadata = BsonDocument.Parse(source.Metadata.ToString()),
                    Geometries = Serializer.GetBsonArrayFromString(source.Geometries.ToString()),
                    Object = BsonDocument.Parse(source.Object.ToString()),
                    Textures = Serializer.GetBsonArrayFromString(source.Textures.ToString()),
                    Images = Serializer.GetBsonArrayFromString(source.Images.ToString()),
                    Animations = Serializer.GetBsonArrayFromString(source.Animations.ToString()),
                };
    }
}