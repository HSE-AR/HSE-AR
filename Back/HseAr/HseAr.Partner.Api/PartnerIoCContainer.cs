using HseAr.Dependencies;
using Microsoft.Extensions.DependencyInjection;

namespace HseAr.Partner.Api
{
    public static  class PartnerIoCContainer
    {
        public static IServiceCollection RegisterDependencies(this IServiceCollection services)
        { 
            services
                .RegisterMappers()
                .RegisterRepositories()
                .RegisterServices()
                .RegisterIdentity();
            
            return services;
        }
        
    }
}