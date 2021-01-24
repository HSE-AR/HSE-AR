using System.Threading.Tasks;
using HseAr.BusinessLayer.Scene.Constructors;
using HseAr.Data.DataProjections;
using HseAr.Data.Interfaces;

namespace HseAr.BusinessLayer.Scene
{
    public class SceneService : ISceneService
    {
        private readonly ISceneRepository _sceneRepo;

        public SceneService(ISceneRepository sceneRepo)
        {
            _sceneRepo = sceneRepo;
        }

        public async Task<Data.DataProjections.Scene> AddEmptyScene()
        {
            var emptyScene = EmptySceneConstructor.CreateEmptyScene();

            var sceneResult = await _sceneRepo.Create(emptyScene);
            return sceneResult;
        }
    }
}

