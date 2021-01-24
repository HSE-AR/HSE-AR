using System.Collections.Generic;
using HseAr.WebPlatform.Api.Models.Floor;

namespace HseAr.WebPlatform.Api.Models.Building
{
    public class BuildingContextModel
    {
        public BuildingModel Building { get; set; }
        
        public List<FloorModel> Floors { get; set; }
    }
}