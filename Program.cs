using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DutchTreat.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace DutchTreat
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            RunSeeding(host);
            CreateHostBuilder(args).Build().Run();
        }

        private static void RunSeeding(IHost host)
        {
            var scopeFactory = (IServiceScopeFactory)host.Services.GetService(typeof(IServiceScopeFactory));

            using (var scope = scopeFactory.CreateScope())
            {
                var seeder = (DutchSeeder)scope.ServiceProvider.GetService(typeof(DutchSeeder));
                seeder.SeedAsync().Wait();
            }            
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration(SetupConfiguration)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        private static void SetupConfiguration(HostBuilderContext ctx, IConfigurationBuilder builder)
        {
            // Remove the default configuration options
            builder.Sources.Clear();

            builder.AddJsonFile("config.json", false, true)
                    .AddEnvironmentVariables();
        }
    }
}
