using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

using Serilog;

using System;
using System.IO;

namespace SmartData.Api
{
    public class Program
    {
        public static IConfiguration Configuration { get; } = new ConfigurationBuilder()
             .SetBasePath(Directory.GetCurrentDirectory())
             .AddJsonFile("appsettings.json").Build();

        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                   .ReadFrom.Configuration(Configuration)
                   .CreateLogger();
            try
            {
                Log.Information("Host starting...");

                CreateWebHostBuilder(args).Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IWebHost CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseWebRoot("")
                .UseStartup<Startup>()
                .UseSerilog()
                .Build();
    }
}
