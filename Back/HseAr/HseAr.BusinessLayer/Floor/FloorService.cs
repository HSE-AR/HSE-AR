using System;
using System.Threading.Tasks;
using HseAr.BusinessLayer.Scene;
using HseAr.Data.DataProjections;
using HseAr.Data.Entities;
using HseAr.Data.Interfaces;

namespace HseAr.BusinessLayer.Floor
{
    public class FloorService : IFloorService
    {
        private readonly ISceneService _sceneService;
        private readonly IFloorRepository _floorRepo;
        private readonly ISceneRepository _sceneRepo;

        public FloorService(
            IFloorRepository floorRepo,
            ISceneRepository sceneRepo,
            ISceneService sceneService)
        { 
            _floorRepo = floorRepo;
            _sceneRepo = sceneRepo;
            _sceneService = sceneService;
        }

        public async Task<Data.DataProjections.Floor> CreateFloor(Data.DataProjections.Floor floorDto)
        {
            var sceneResult = await _sceneService.AddEmptyScene();
            floorDto.SceneId = sceneResult.Id;
            
            return await _floorRepo.Add(floorDto);
        }

        public async Task<Data.DataProjections.Scene> GetSceneByFloorId(Guid id)
        {
            var floor = await _floorRepo.GetById(id);
            return await _sceneRepo.GetById(floor.SceneId);
        }
    }
}