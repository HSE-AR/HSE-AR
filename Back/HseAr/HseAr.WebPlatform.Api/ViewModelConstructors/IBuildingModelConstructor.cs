using System.Collections.Generic;
using HseAr.BusinessLayer.BuildingService.Models;
using HseAr.WebPlatform.Api.Models.Building;

namespace HseAr.WebPlatform.Api.ViewModelConstructors
{
    public interface IBuildingModelConstructor
    { 
        BuildingCurrentViewModel ConstructCurrentModel(BuildingContext buildingContext);

        BuildingsViewModel ConstructModels(List<BuildingContext> buildingContext);
    }
}