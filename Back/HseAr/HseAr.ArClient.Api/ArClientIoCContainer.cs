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
            
            return services;
        }
        
    }
}