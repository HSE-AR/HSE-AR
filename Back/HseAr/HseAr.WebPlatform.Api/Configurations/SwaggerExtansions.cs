using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace HseAr.WebPlatform.Api.Configurations
{
    public static class SwaggerExtensions
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Wapi", Version = "v1" });
            });

            return services;
        }
    }
}