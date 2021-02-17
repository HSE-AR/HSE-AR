using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HseAr.Data;
using HseAr.Data.Entities;
using HseAr.Data.Interfaces;
using HseAr.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace HseAr.DataAccess.Mongodb.Repositories
{
    public class SceneRepository : ISceneRepository
    {
        private readonly IMongoCollection<SceneBson> _scenes;
        private readonly IMapper _mapper;
        private readonly List<ISceneModificationHandler> _modificationHandlers; 

        public SceneRepository(MongoContext mongoContext, IMapper mapper, IServiceProvider serviceProvider)
        {
            _mapper = mapper;
            _scenes = mongoContext.Scenes;
            _modificationHandlers = serviceProvider.GetServices<ISceneModificationHandler>().ToList();
        }

        public async Task<bool> ApplyModification(SceneModification sceneModification)
        {
            UpdateResult result = null;
            
            foreach (var handler in _modificationHandlers)
            {
                if (handler.CatchTypeMatch(sceneModification.Type.ToString()))
                {
                    result = await handler.Modify(sceneModification);
                    break;
                }
            }

            return result != null && result.IsAcknowledged;
        }

        public async Task<List<Scene>> GetList()
            => (await _scenes.Find(s => true).ToListAsync())
                .Select(scene => _mapper.Map<SceneBson, Scene>(scene))
                .ToList();

        public async Task<Scene> GetById(string id) 
            => _mapper.Map<SceneBson, Scene>(await _scenes.Find(s => s.Id == id).FirstOrDefaultAsync());

        public async Task<Scene> Create(Scene scene)
        {
            var sceneEntity = _mapper.Map<Scene, SceneBson>(scene);
            await _scenes.InsertOneAsync(sceneEntity);
            
            return _mapper.Map<SceneBson, Scene>(sceneEntity);
        }

        public async Task<DeleteResult> Remove(string id) 
            => await _scenes.DeleteOneAsync(scene => scene.Id == id);
    }
}