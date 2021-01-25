using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HseAr.Data.DataProjections;
using HseAr.Data.Entities;
using HseAr.Data.Interfaces;
using HseAr.Infrastructure;
using MongoDB.Driver;

namespace HseAr.DataAccess.Mongodb.Repositories
{
    public class SceneModificationRepository : ISceneModificationRepository
    {
        private readonly IMongoCollection<SceneModificationEntity> _modifications;
        private readonly IMapper<SceneModification, SceneModificationEntity> _sceneModMapper;
        private readonly IMapper<SceneModificationEntity, SceneModification> _sceneModEntityMapper;

        public SceneModificationRepository(
            MongoContext mongoContext,
            IMapper<SceneModification, SceneModificationEntity> sceneModMapper,
            IMapper<SceneModificationEntity, SceneModification> sceneModEntityMapper)
        {
            _sceneModMapper = sceneModMapper;
            _modifications = mongoContext.Modifications;
            _sceneModEntityMapper = sceneModEntityMapper;
        }

        public async Task<SceneModification> CreateAsync(SceneModification sceneModification)
        {
            sceneModification.EditedAtUtc = DateTime.Now;
            var sceneModificationEntity = _sceneModMapper.Map(sceneModification);
            
            await _modifications.InsertOneAsync(sceneModificationEntity);
            return _sceneModEntityMapper.Map(sceneModificationEntity);
        }
        
        public async Task<DeleteResult> RemoveAsync(string id) 
            => await _modifications.DeleteOneAsync(model => model.Id == id);
    }
}