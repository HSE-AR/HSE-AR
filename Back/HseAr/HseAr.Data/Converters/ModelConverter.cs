using System.Collections.Generic;
using System.Linq;
using HseAr.Data.DTO;
using HseAr.Data.Entities;
using MongoDB.Bson;
using Newtonsoft.Json.Linq;

namespace HseAr.Data.Converters
{
    public static class ModelConverter
    {
        public static ModelDto Convert(Model model) =>
            new ModelDto
            {
                Id = model.Id,
                Name = model.Name,
                Scene = JObject.Parse(model.Scene.ToJson()),
                CreatedAtUtc = model.CreatedAtUtc
            };

        public static Model Convert(ModelDto modelDto) =>
            new Model
            {
                Id = modelDto.Id,
                Name = modelDto.Name,
                Scene = BsonDocument.Parse(modelDto.Scene.ToString()),
                CreatedAtUtc = modelDto.CreatedAtUtc
            };

        public static ICollection<Model> Convert(ICollection<ModelDto> modelsDto) =>
            modelsDto.Select(x => Convert(x)).ToList();

        public static ICollection<ModelDto> Convert(ICollection<Model> models) =>
            models.Select(x => Convert(x)).ToList();
    }
}
