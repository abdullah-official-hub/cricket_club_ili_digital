using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;

namespace Gateway
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    var envMode = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
                    webBuilder.UseStartup<Startup>();
                    webBuilder.ConfigureAppConfiguration(config => config.AddJsonFile($"ocelotRouting.{envMode}.json"));
                });
    }
}