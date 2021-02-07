using HseAr.DataAccess.EFCore;
using HseAr.DataAccess.Mongodb;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace HseAr.Partner.Api.Configurations
{
    public static class ConnectionConfiguration
    {
        public static IServiceCollection AddDbConnections(this IServiceCollection services, IConfiguration configuration)
        { 
            var connection = configuration["DB_CONNECTION"];
            
            services.AddDbContext<EFCoreContext>(options => options.UseSqlite(connection,
                b => b.MigrationsAssembly("HseAr.Partner.Api")));
            
            services.AddSingleton<MongoContext>();

            return services;
        }
    }
}