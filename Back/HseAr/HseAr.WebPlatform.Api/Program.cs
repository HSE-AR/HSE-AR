using HseAr.Core.Configure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace HseAr.WebPlatform.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
            => Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostingContext, config) 
                    => ConfigurationManager.AddJsonSettingFiles(hostingContext, config))
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
        
    }
}