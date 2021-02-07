using HseAr.Core.Settings;
using HseAr.DataAccess.Mongodb;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HseAr.Partner.Api.Configurations
{
    public static class SettingsConfiguration
    {
        public static IServiceCollection AddSettings(this IServiceCollection  services, IConfiguration configuration )
            => services
                .Configure<EnvironmentConfig>(configuration)
                .Configure<ModelsDatabaseSettings>(
                    configuration.GetSection(nameof(ModelsDatabaseSettings)));
    }
}