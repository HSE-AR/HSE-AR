using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HseAr.Data.DTO;
using HseAr.Data.Entities;
using MongoDB.Driver;

namespace HseAr.DataAccess.Mongodb.Repositories
{
    public class ModificationRepository
    {
        private readonly IMongoCollection<SceneModification> _modifications;

        public ModificationRepository(MongoContext mongoContext)
        {
            _modifications = mongoContext.Modifications;
        }

        public async Task<ICollection<SceneModification>> GetAsync() 
            => await _modifications.Find(m => true).ToListAsync();

        public async Task<SceneModification> GetAsync(string id) 
            => await _modifications.Find(m => m.Id == id).FirstOrDefaultAsync();


        public async Task<SceneModification> CreateAsync(SceneModification sceneModification)
        {
            sceneModification.EditedAtUtc = DateTime.Now;
            await _modifications.InsertOneAsync(sceneModification);
            return sceneModification;
        }

        public async Task<ReplaceOneResult> UpdateAsync(string id, SceneModification modelIn) 
            => await _modifications.ReplaceOneAsync(m => m.Id == id, modelIn);

        public async Task<DeleteResult> RemoveAsync(ModelDto modelIn) 
            => await _modifications.DeleteOneAsync(model => model.Id == modelIn.Id);

        public async Task<DeleteResult> RemoveAsync(string id) 
            => await _modifications.DeleteOneAsync(model => model.Id == id);
    }
}