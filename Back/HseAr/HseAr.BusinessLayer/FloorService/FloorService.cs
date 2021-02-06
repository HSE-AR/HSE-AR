using System;
using System.Threading.Tasks;
using HseAr.BusinessLayer.FloorService.Models;
using HseAr.BusinessLayer.SceneService;
using HseAr.Data;
using HseAr.Data.DataProjections;
using HseAr.Data.Entities;
using HseAr.Data.Interfaces;
using HseAr.Infrastructure;

namespace HseAr.BusinessLayer.FloorService
{
    public class FloorService : IFloorService
    {
        private readonly ISceneService _sceneService;
        private readonly IUnitOfWork _data;
        private readonly IMapper _mapper;

        public FloorService(
            IUnitOfWork data,
            ISceneService sceneService ,
            IMapper mapper)
        {
            _data = data;
            _sceneService = sceneService;
            _mapper = mapper;
        }

        public async Task<FloorContext> CreateFloor(FloorContext floorContext)
        {
            var sceneResult = await _sceneService.AddEmptyScene();
            floorContext.SceneId = sceneResult.Id;
            var floor = _mapper.Map<FloorContext, Floor>(floorContext);
            
            var floorResult = await _data.Floors.Add(floor);
            return _mapper.Map<Floor, FloorContext>(floorResult);
        }

        public async Task<FloorContext> GetFloorById(Guid id)
        {
            var floor = await _data.Floors.GetById(id);
            return _mapper.Map<Floor, FloorContext>(floor);
        }
        
    }
}