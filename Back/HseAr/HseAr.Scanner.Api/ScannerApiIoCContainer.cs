﻿using HseAr.BusinessLayer.BuildingService.Models;
using HseAr.BusinessLayer.FloorService.Models;
using HseAr.Dependencies;
using HseAr.Infrastructure;

using Microsoft.Extensions.DependencyInjection;

namespace HseAr.Scanner.Api
{
    public static class ScannerApiIoCContainer
    {
        public static IServiceCollection RegisterDependencies(this IServiceCollection services)
        {
            services
                .RegisterMappers()
                .RegisterHandlers()
                .RegisterRepositories()
                .RegisterServices()
                .RegisterIdentity()
                .RegisterHttpClients();

            //services
            //    .AddViewModelConstructors()
            //    .AddMappers();

            return services;
        }

        //private static IServiceCollection AddViewModelConstructors(this IServiceCollection services)
        //{
        //    return services
        //        .AddTransient<IBuildingModelConstructor, BuildingModelConstructor>()
        //        .AddTransient<IEditorModelConstructor, EditorModelConstructor>()
        //        .AddTransient<IAccountModelConstructor, AccountModelConstructor>();
        //}
        
        //private static IServiceCollection AddMappers(this IServiceCollection services)
        //{
        //    return services
        //        .AddTransient<IMapper<BuildingCreationForm, BuildingContext>, BuildingCreationFormMapper>()
        //        .AddTransient<IMapper<FloorCreationForm, FloorContext>, FloorCreationFormMapper>();
        //}
        
    }
}