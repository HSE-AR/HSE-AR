using System;
using System.Threading.Tasks;
using HseAr.BusinessLayer.Scene;
using HseAr.Data;
using HseAr.Data.DataProjections;
using HseAr.Data.Entities;
using HseAr.Data.Interfaces;

namespace HseAr.BusinessLayer.Floor
{
    public class FloorService : IFloorService
    {
        private readonly ISceneService _sceneService;
        private readonly IUnitOfWork _data;

        public FloorService(
            IUnitOfWork data,
            ISceneService sceneService)
        {
            _data = data;
            _sceneService = sceneService;
        }

        public async Task<Data.DataProjections.Floor> CreateFloor(Data.DataProjections.Floor floorDto)
        {
            var sceneResult = await _sceneService.AddEmptyScene();
            floorDto.SceneId = sceneResult.Id;
            
            return await _data.Floors.Add(floorDto);
        }
        
    }
}