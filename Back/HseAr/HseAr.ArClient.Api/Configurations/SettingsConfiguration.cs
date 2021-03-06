﻿using HseAr.Core.Settings;
using HseAr.DataAccess.Mongodb;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HseAr.ArClient.Api.Configurations
{
    public static class SettingsConfiguration
    {
        public static IServiceCollection AddSettings(this IServiceCollection services, IConfiguration configuration)
            => services.Configure<Configuration>(configuration);
    }
}