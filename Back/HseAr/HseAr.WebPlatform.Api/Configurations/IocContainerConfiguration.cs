using HseAr.BusinessLayer.Modification;
using HseAr.DataAccess.Mongodb;
using HseAr.DataAccess.Mongodb.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace HseAr.WebPlatform.Api.Configurations
{
    public static class IocContainerConfiguration
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
            => services
                .AddSingleton<ModificationRepository>()
                .AddSingleton<ModelsRepository>();


        public static IServiceCollection AddServices(this IServiceCollection services)
            => services
                .AddSingleton<IModelsDatabaseSettings>(sp
                    => sp.GetRequiredService<IOptions<ModelsDatabaseSettings>>().Value)
                .AddTransient<IModificationService, ModificationService>();
        
    }
}