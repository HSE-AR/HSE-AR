using System.IO;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;


namespace HseAr.Core.Configure
{
    public static class ConfigurationManager
    {
        public static void AddJsonSettingFiles(HostBuilderContext hostingContext, IConfigurationBuilder config)
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