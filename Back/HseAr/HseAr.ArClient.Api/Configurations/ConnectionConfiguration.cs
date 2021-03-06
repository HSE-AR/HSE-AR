﻿using HseAr.DataAccess.EFCore;
using HseAr.DataAccess.Mongodb;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace HseAr.ArClient.Api.Configurations
{
    public static class ConnectionConfiguration
    {
        public static IServiceCollection AddDbConnections(this IServiceCollection services, IConfiguration configuration)
        { 
            var connection = configuration.GetConnectionString("DataAccessPostgreSqlProvider");
            
            services.AddDbContext<EFCoreContext>(options => options.UseNpgsql(connection,
                b => b.MigrationsAssembly("HseAr.ArClient.Api")));
            
            services.AddSingleton<MongoContext>();

            return services;
        }
    }
}