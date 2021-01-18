using HseAr.DataAccess.Mongodb;
using Microsoft.Extensions.DependencyInjection;

namespace HseAr.WebPlatform.Api.Configurations
{
    public static class ConnectionConfiguration
    {
        public static IServiceCollection AddDbConnections(this IServiceCollection services)
        {
            services.AddSingleton<MongoContext>();

            return services;
        }
    }
}