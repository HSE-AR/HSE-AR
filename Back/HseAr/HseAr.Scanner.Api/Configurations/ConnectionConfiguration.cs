using HseAr.DataAccess.EFCore;
using HseAr.DataAccess.Mongodb;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HseAr.Scanner.Api.Configurations
{
    public static class ConnectionConfiguration
    {
        public static IServiceCollection AddDbConnections(this IServiceCollection services, IConfiguration configuration)
        { 
            var connection = configuration.GetConnectionString("DataAccessPostgreSqlProvider");
            
            services.AddDbContext<EFCoreContext>(options => 
                options.UseNpgsql(connection,
                b => b.MigrationsAssembly("HseAr.Scanner.Api")),
            ServiceLifetime.Transient
                );
            
            services.AddSingleton<MongoContext>();

            return services;
        }
    }
}