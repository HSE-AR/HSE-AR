using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace HseAr.MiemApp.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
            => Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostingContext, config) => AddJsonSettingFiles(hostingContext, config))
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });

        private static void AddJsonSettingFiles(HostBuilderContext hostingContext, IConfigurationBuilder config)
        {
            var contentRootPath = hostingContext.HostingEnvironment.ContentRootPath;
            var parentDirectory = Path.GetDirectoryName(contentRootPath);
            var provider = new PhysicalFileProvider(parentDirectory);
                    
            config.AddJsonFile(
                provider,
                path: "global.json",
                optional: true,
                reloadOnChange: true);
            
            config.AddJsonFile(
                provider,
                path: "secret.json",
                optional: true,
                reloadOnChange: true);
        }
    }
}