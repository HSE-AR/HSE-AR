using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HseAr.Data.Converters;
using HseAr.Data.DTO;
using HseAr.Data.Entities;

using MongoDB.Bson;
using MongoDB.Driver;

namespace HseAr.DataAccess.Mongodb.Repositories
{
    public class ModelsRepository
    {
        private readonly IMongoCollection<Model> _models;

        public ModelsRepository(MongoContext mongoContext)
        {
            _models = mongoContext.Models;
        }

        public async Task<ICollection<ModelDto>> GetAsync() =>
            ModelConverter.Convert(await _models.Find(m => true).ToListAsync());

        public async Task<ModelDto> GetAsync(string id) =>
            ModelConverter.Convert(await _models.Find(m => m.Id == id).FirstOrDefaultAsync());

        public async Task<ICollection<ModelDto>> GetAsync(ICollection<string> ids) =>
            ModelConverter.Convert(await _models.Find(m => ids.Contains(m.Id)).ToListAsync());

        public async Task<ModelDto> CreateAsync(ModelDto modelDto)
        {
            Model model = new Model
            {
                Name = modelDto.Name,
                Scene = BsonDocument.Parse(modelDto.Scene.ToString()),
                CreatedAtUtc = DateTime.Now
            };
            await _models.InsertOneAsync(model);
            return ModelConverter.Convert(model);
        }

        public async Task<ReplaceOneResult> UpdateAsync(string id, Model modelIn) =>
            await  _models.ReplaceOneAsync(m => m.Id == id, modelIn);

        public async Task<DeleteResult> RemoveAsync(ModelDto modelIn) =>
            await _models.DeleteOneAsync(model => model.Id == modelIn.Id);

        public async Task<DeleteResult> RemoveAsync(string id) =>
            await _models.DeleteOneAsync(model => model.Id == id);
    }
}