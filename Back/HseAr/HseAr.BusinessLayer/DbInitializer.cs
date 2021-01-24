using System;
using System.Threading.Tasks;
using HseAr.Data.Entities;
using HseAr.DataAccess.EFCore.DbInitializers;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace HseAr.BusinessLayer
{
    public static class DbInitializer
    {
        public static async Task Initialize(IServiceProvider services)
        {
            await EFCoreInitializer.Initialize(services);
        }
    }
}