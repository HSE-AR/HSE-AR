using System.Linq;
using HseAr.BusinessLayer.Auth;
using HseAr.BusinessLayer.Building;
using HseAr.BusinessLayer.Floor;
using HseAr.BusinessLayer.Jwt;
using HseAr.BusinessLayer.Modification;
using HseAr.BusinessLayer.Scene;
using HseAr.Data.DataProjections;
using HseAr.Data.Entities;
using HseAr.Data.Mappers;
using HseAr.Data.Interfaces;
using HseAr.DataAccess.EFCore.Mappers;
using HseAr.DataAccess.EFCore.Repositories;
using HseAr.DataAccess.Mongodb;
using HseAr.DataAccess.Mongodb.Mappers;
using HseAr.DataAccess.Mongodb.Repositories;
using HseAr.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace HseAr.Dependencies.Container
{
    public static class IocContainerConfiguration
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
            => services
                .AddSingleton<SceneModificationRepository>()
                .AddTransient<IFloorRepository, FloorRepository>()
                .AddTransient<IBuildingRepository, BuildingRepository>()
                .AddTransient<ISceneRepository, SceneRepository>();
                
        
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            return services
                .AddSingleton<IModelsDatabaseSettings>(sp
                    => sp.GetRequiredService<IOptions<ModelsDatabaseSettings>>().Value)
                
                .AddTransient<IModificationService, ModificationService>()
                .AddTransient<IJwtGenerator, JwtGenerator>()
                .AddTransient<IBuildingService, BuildingService>()
                .AddTransient<IFloorService, FloorService>()
                .AddTransient<ISceneService, SceneService>()
                .AddTransient<IAuthService, AuthService>();
        }
        
        public static IServiceCollection AddMappers(this IServiceCollection services)
        {
            return services
                .AddTransient<IMapper, ContainerMapper>()
                .AddTransient<IMapper<TestSource, TestResult>, TestMapper>()
                .AddTransient<IMapper<SceneModificationEntity, SceneModification>, SceneModificationMapper>()
                .AddTransient<IMapper<SceneModification, SceneModificationEntity>, SceneModificationMapper>()
                .AddTransient<IMapper<Scene, SceneEntity>, SceneMapper>()
                .AddTransient<IMapper<SceneEntity, Scene>, SceneMapper>()
                .AddTransient<IMapper<Floor, FloorEntity>, FloorMapper>()
                .AddTransient<IMapper<FloorEntity, Floor>, FloorMapper>()
                .AddTransient<IMapper<Building, BuildingEntity>, BuildingMapper>()
                .AddTransient<IMapper<BuildingEntity, Building>, BuildingMapper>();
        }
    }
}