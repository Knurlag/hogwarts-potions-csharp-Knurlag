using System;
using System.Threading.Tasks;
using HogwartsPotions.data;
using HogwartsPotions.Data;
using log4net;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace HogwartsPotions
{
    public class Program
    {
        private static ILog logger = LogManager.GetLogger("logger");
        public static async Task Main(string[] args)
        {

            var host = CreateHostBuilder(args).Build();
            await CreateDbIfNotExists(host);
            host.Run();
        }


        private static async Task CreateDbIfNotExists(IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<HogwartsContext>();
                    await DbInitializer.Initialize(context);
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