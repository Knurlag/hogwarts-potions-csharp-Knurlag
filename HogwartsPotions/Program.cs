using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using HogwartsPotions.data;
using HogwartsPotions.Data;
using log4net;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using static System.Net.Mime.MediaTypeNames;

namespace HogwartsPotions
{
    public class Program
    {
        private static ILog logger = LogManager.GetLogger("logger");
        public static void Main(string[] args)
        {

            var host = CreateHostBuilder(args).Build();
            CreateDbIfNotExists(host);
            logger.Error("testtst");
            host.Run();
        }


        private static void CreateDbIfNotExists(IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<HogwartsContext>();
                    DbInitializer.Initialize(context);
                }
                catch (Exception ex)
                {
                    logger.Error( "An error occurred creating the DB.", ex);
                }
            }
        }
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}