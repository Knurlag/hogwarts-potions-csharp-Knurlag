using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using HogwartsPotions.data;
using HogwartsPotions.Models;
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
        public static void Main(string[] args)
        {
            AppDomain.CurrentDomain.UnhandledException += (s, e) =>
            {
                var exception = (Exception)e.ExceptionObject;
                ILog logger = LogManager.GetLogger("logger");
                logger.Error("An error occurred", exception);

            };
            var host = CreateHostBuilder(args).Build();
            CreateDbIfNotExists(host);

            
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
                    ILog logger = LogManager.GetLogger("logger");
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