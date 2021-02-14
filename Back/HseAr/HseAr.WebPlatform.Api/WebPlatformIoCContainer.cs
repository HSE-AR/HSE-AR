using HseAr.BusinessLayer.BuildingService.Models;
using HseAr.BusinessLayer.FloorService.Models;
using HseAr.Dependencies;
using HseAr.Infrastructure;
using HseAr.WebPlatform.Api.Mappers;
using HseAr.WebPlatform.Api.Models.Building;
using HseAr.WebPlatform.Api.Models.Floor;
using HseAr.WebPlatform.Api.ViewModelConstructors;
using Microsoft.Extensions.DependencyInjection;

namespace HseAr.WebPlatform.Api
{
    public static class WebPlatformIoCContainer
    {
        public static IServiceCollection RegisterDependencies(this IServiceCollection services)
        {
            services
                .RegisterMappers()
                .RegisterRepositories()
                .RegisterServices()
                .RegisterIdentity()
                .RegisterHttpClients();

            services
                .AddViewModelConstructors()
                .AddMappers();

            return services;
        }

        private static IServiceCollection AddViewModelConstructors(this IServiceCollection services)
        {
            return services
                .AddTransient<IBuildingModelConstructor, BuildingModelConstructor>()
                .AddTransient<IAccountModelConstructor, AccountModelConstructor>();
        }
        
        private static IServiceCollection AddMappers(this IServiceCollection services)
        {
            return services
                .AddTransient<IMapper<BuildingCreationForm, BuildingContext>, BuildingCreationFormMapper>()
                .AddTransient<IMapper<FloorCreationForm, FloorContext>, FloorCreationFormMapper>();
        }
        
    }
}