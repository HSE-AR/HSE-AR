using System;
using HseAr.BusinessLayer.AccountService;
using HseAr.BusinessLayer.AccountService.Models;
using HseAr.BusinessLayer.ArClientService;
using HseAr.BusinessLayer.AuthService;
using HseAr.BusinessLayer.BuildingService;
using HseAr.BusinessLayer.BuildingService.Models;
using HseAr.BusinessLayer.CompanyService;
using HseAr.BusinessLayer.CompanyService.Models;
using HseAr.BusinessLayer.FloorService;
using HseAr.BusinessLayer.FloorService.Models;
using HseAr.BusinessLayer.Jwt;
using HseAr.BusinessLayer.Mappers;
using HseAr.BusinessLayer.PointCloudService;
using HseAr.BusinessLayer.PointCloudService.Models;
using HseAr.BusinessLayer.SceneService;
using HseAr.Core.Settings;
using HseAr.Data;
using HseAr.Data.DataProjections;
using HseAr.Data.Entities;
using HseAr.Data.Interfaces;
using HseAr.DataAccess.EFCore;
using HseAr.DataAccess.EFCore.Repositories;
using HseAr.DataAccess.Mongodb.Mappers;
using HseAr.DataAccess.Mongodb.Repositories;
using HseAr.DataAccess.Mongodb.SceneModificationHandlers;
using HseAr.Infrastructure;
using HseAr.Integration.SceneExport;
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
                .AddEntityFrameworkStores<EFCoreContext>()
                .AddUserManager<HseArUserManager>();

            return services;
        }

        public static IServiceCollection RegisterHandlers(this IServiceCollection services)
           => services
                .AddTransient<ISceneModificationHandler, InsertObjectIntoSceneHandler>()
                .AddTransient<ISceneModificationHandler, DeleteObjectFromSceneHandler>()
                .AddTransient<ISceneModificationHandler, AddLightToSceneHandler>()
                .AddTransient<ISceneModificationHandler, DeleteLightFromSceneHandler>()
                .AddTransient<ISceneModificationHandler, UpdateTransformHandler>();

        public static IServiceCollection RegisterRepositories(this IServiceCollection services)
            => services
                .AddTransient<IUnitOfWork, UnitOfWork>()
                .AddTransient<IFloorRepository, FloorRepository>()
                .AddTransient<IBuildingRepository, BuildingRepository>()
                .AddTransient<ICompanyRepository, CompanyRepository>()
                .AddTransient<IArClientRepository,ArClientRepository>()
                .AddTransient<IPositionRepository,PositionRepository>()
                .AddTransient<ISceneRepository, SceneRepository>()
                .AddTransient<IPointCloudRepository, PointCloudRepository>();
        
        
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            return services
                .AddSingleton(sp => sp.GetRequiredService<IOptions<Configuration>>().Value)

                .AddTransient<IJwtGenerator, JwtGenerator>()
                .AddTransient<IBuildingService, BuildingService>()
                .AddTransient<IFloorService, FloorService>()
                .AddTransient<ISceneService, SceneService>()
                .AddTransient<IAuthService, AuthService>()
                .AddTransient<IAccountService, AccountService>()
                .AddTransient<ICompanyService,CompanyService>()
                .AddTransient<IArClientService, ArClientService>()
                .AddTransient<IPointCloudService, PointCloudService>();
        }
        
        public static IServiceCollection RegisterMappers(this IServiceCollection services)
        {
            return services
                .AddTransient<IMapper, ContainerMapper>()
                .AddTransient<IMapper<SceneModificationBson, SceneModification>, SceneModificationMapper>()
                .AddTransient<IMapper<SceneModification, SceneModificationBson>, SceneModificationMapper>()
                .AddTransient<IMapper<Scene, SceneBson>, SceneMapper>()
                .AddTransient<IMapper<SceneBson, Scene>, SceneMapper>()

                .AddTransient<IMapper<Building, BuildingContext>, BuildingContextMapper>()
                .AddTransient<IMapper<BuildingContext, Building>, BuildingContextMapper>()
                .AddTransient<IMapper<Floor, FloorContext>, FloorContextMapper>()
                .AddTransient<IMapper<FloorContext, Floor>, FloorContextMapper>()
                .AddTransient<IMapper<Company, CompanyContext>, CompanyContextMapper>()
                .AddTransient<IMapper<CompanyContext, Company>, CompanyContextMapper>()
                .AddTransient<IMapper<User, AccountContext>, AccountContextMapper>()
                .AddTransient<IMapper<PointCloud, PointCloudContext>, PointCloudContextMapper>()
                .AddTransient<IMapper<PointCloudContext, PointCloud>, PointCloudContextMapper>(); ;
        }
        
        public static IServiceCollection RegisterHttpClients(this IServiceCollection services)
        {
            return services
                .AddHttpClient()
                .AddTransient<ISceneExportApiClient, SceneExportApiClient>();
        }
    }
}