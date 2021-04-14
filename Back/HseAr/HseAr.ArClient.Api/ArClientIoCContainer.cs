using HseAr.ArClient.Api.ViewModelConstructor;
using HseAr.Dependencies;
using Microsoft.Extensions.DependencyInjection;

namespace HseAr.ArClient.Api
{
    public static  class ArClientIoCContainer
    {
        public static IServiceCollection RegisterDependencies(this IServiceCollection services)
        { 
            services
                .RegisterMappers()
                .RegisterRepositories()
                .RegisterServices()
                .RegisterIdentity()
                .RegisterHttpClients();

            services.AddViewModelConstructors();
            
            return services;
        }
        
        private static IServiceCollection AddViewModelConstructors(this IServiceCollection services)
        {
            return services
                .AddTransient<IArPlacesModelConstructor, ArPlacesModelConstructor>();
        }
        
    }
}