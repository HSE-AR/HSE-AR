using HseAr.BusinessLayer.PointCloudService.Models;
using HseAr.Dependencies;
using HseAr.Infrastructure;
using HseAr.Scanner.Api.Mappers;
using HseAr.Scanner.Api.Models.PointCloud;
using HseAr.Scanner.Api.ViewModelConstructors;
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

            services
                .AddViewModelConstructors()
                .AddMappers();

            return services;
        }

        private static IServiceCollection AddViewModelConstructors(this IServiceCollection services)
        {
            return services
                .AddTransient<IPointCloudModelConstructor, PointCloudModelConstructor>();
        }

        private static IServiceCollection AddMappers(this IServiceCollection services)
        {
            return services
                .AddTransient<IMapper<PointCloudCreationForm, PointCloudContext>, PointCloudCreationFormMapper>();
        }

    }
}