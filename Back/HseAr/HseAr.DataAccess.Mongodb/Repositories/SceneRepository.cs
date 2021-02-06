using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HseAr.Data.DataProjections;
using HseAr.Data.Entities;
using HseAr.Data.Interfaces;
using HseAr.Infrastructure;
using MongoDB.Driver;

namespace HseAr.DataAccess.Mongodb.Repositories
{
    public class SceneRepository : ISceneRepository
    {
        private readonly IMongoCollection<SceneEntity> _scenes;
        private readonly IMapper _mapper;

        public SceneRepository(MongoContext mongoContext, IMapper mapper)
        {
            _mapper = mapper;
            _scenes = mongoContext.Scenes;
        }

        public async Task<List<Scene>> GetList()
            => (await _scenes.Find(s => true).ToListAsync())
                .Select(scene => _mapper.Map<SceneEntity, Scene>(scene))
                .ToList();

        public async Task<Scene> GetById(string id) 
            => _mapper.Map<SceneEntity, Scene>(await _scenes.Find(s => s.Id == id).FirstOrDefaultAsync());

        public async Task<Scene> Create(Scene scene)
        {
            var sceneEntity = _mapper.Map<Scene, SceneEntity>(scene);
            await _scenes.InsertOneAsync(sceneEntity);
            
            return _mapper.Map<SceneEntity, Scene>(sceneEntity);
        }

        public async Task<DeleteResult> Remove(string id) 
            => await _scenes.DeleteOneAsync(scene => scene.Id == id);
    }
}