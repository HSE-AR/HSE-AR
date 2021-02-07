using System;
using HseAr.BusinessLayer.AccountService;
using HseAr.BusinessLayer.AccountService.Models;
using HseAr.BusinessLayer.AuthService;
using HseAr.BusinessLayer.BuildingService;
using HseAr.BusinessLayer.BuildingService.Models;
using HseAr.BusinessLayer.FloorService;
using HseAr.BusinessLayer.FloorService.Models;
using HseAr.BusinessLayer.Jwt;
using HseAr.BusinessLayer.Mappers;
using HseAr.BusinessLayer.SceneService;
using HseAr.Data;
using HseAr.Data.DataProjections;
using HseAr.Data.Entities;
using HseAr.Data.Interfaces;
using HseAr.DataAccess.EFCore;
using HseAr.DataAccess.EFCore.Mappers;
using HseAr.DataAccess.EFCore.Repositories;
using HseAr.DataAccess.Mongodb;
using HseAr.DataAccess.Mongodb.Mappers;
using HseAr.DataAccess.Mongodb.Repositories;
using HseAr.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace HseAr.Dependencies
{
    public static class IocContainerConfiguration
    {
        public static IServiceCollection RegisterIdentity(this IServiceCollection services)
        {
            services
                .AddIdentity<User, IdentityRole<Guid>>(o =>
                {
                    o.Password.RequireNonAlphanumeric = false;
                    o.Password.RequireUppercase = false;
                    o.Password.RequireLowercase = false;
                })
                .AddEntityFrameworkStores<EFCoreContext>();

            return services;
        }
        
        public static IServiceCollection RegisterRepositories(this IServiceCollection services)
            => services
                .AddTransient<IUnitOfWork, UnitOfWork>()
                .AddTransient<IFloorRepository, FloorRepository>()
                .AddTransient<IBuildingRepository, BuildingRepository>()
                .AddTransient<ISceneModificationRepository, SceneModificationRepository>()
                .AddTransient<ISceneElementRepository, SceneElementRepository>()
                .AddTransient<ISceneRepository, SceneRepository>();
        
        
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            return services
                .AddSingleton<IModelsDatabaseSettings>(sp
                    => sp.GetRequiredService<IOptions<ModelsDatabaseSettings>>().Value)
                
                .AddTransient<IJwtGenerator, JwtGenerator>()
                .AddTransient<IBuildingService, BuildingService>()
                .AddTransient<IFloorService, FloorService>()
                .AddTransient<ISceneService, SceneService>()
                .AddTransient<IAuthService, AuthService>()
                .AddTransient<IAccountService, AccountService>();
        }
        
        public static IServiceCollection RegisterMappers(this IServiceCollection services)
        {
            return services
                .AddTransient<IMapper, ContainerMapper>()
                .AddTransient<IMapper<SceneModificationEntity, SceneModification>, SceneModificationMapper>()
                .AddTransient<IMapper<SceneModification, SceneModificationEntity>, SceneModificationMapper>()
                .AddTransient<IMapper<Scene, SceneEntity>, SceneMapper>()
                .AddTransient<IMapper<SceneEntity, Scene>, SceneMapper>()
                .AddTransient<IMapper<Floor, FloorEntity>, FloorMapper>()
                .AddTransient<IMapper<FloorEntity, Floor>, FloorMapper>()
                .AddTransient<IMapper<Building, BuildingEntity>, BuildingMapper>()
                .AddTransient<IMapper<BuildingEntity, Building>, BuildingMapper>()

                .AddTransient<IMapper<Building, BuildingContext>, BuildingContextMapper>()
                .AddTransient<IMapper<BuildingContext, Building>, BuildingContextMapper>()
                .AddTransient<IMapper<Floor, FloorContext>, FloorContextMapper>()
                .AddTransient<IMapper<FloorContext, Floor>, FloorContextMapper>()
                .AddTransient<IMapper<User, AccountContext>, AccountContextMapper>();
        }
    }
}