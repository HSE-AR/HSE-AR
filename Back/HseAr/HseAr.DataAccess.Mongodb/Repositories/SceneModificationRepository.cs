using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HseAr.Data.DataProjections;
using HseAr.Data.Entities;
using MongoDB.Driver;

namespace HseAr.DataAccess.Mongodb.Repositories
{
    public class SceneModificationRepository
    {
        private readonly IMongoCollection<SceneModificationEntity> _modifications;

        public SceneModificationRepository(MongoContext mongoContext)
        {
            _modifications = mongoContext.Modifications;
        }

        public async Task<ICollection<SceneModificationEntity>> GetAsync() 
            => await _modifications.Find(m => true).ToListAsync();

        public async Task<SceneModificationEntity> GetAsync(string id) 
            => await _modifications.Find(m => m.Id == id).FirstOrDefaultAsync();


        public async Task<SceneModificationEntity> CreateAsync(SceneModificationEntity sceneModificationEntity)
        {
            sceneModificationEntity.EditedAtUtc = DateTime.Now;
            await _modifications.InsertOneAsync(sceneModificationEntity);
            return sceneModificationEntity;
        }

        public async Task<ReplaceOneResult> UpdateAsync(string id, SceneModificationEntity modelIn) 
            => await _modifications.ReplaceOneAsync(m => m.Id == id, modelIn);

        public async Task<DeleteResult> RemoveAsync(Scene modelIn) 
            => await _modifications.DeleteOneAsync(model => model.Id == modelIn.Id);

        public async Task<DeleteResult> RemoveAsync(string id) 
            => await _modifications.DeleteOneAsync(model => model.Id == id);
    }
}