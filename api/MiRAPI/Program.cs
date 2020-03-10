using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Web;

namespace MiRAPI
{
    public class Program
    {
        private static Logger logger;

        public static async Task Main(string[] args)
        {
            try
            {

                var pathToExe = Process.GetCurrentProcess().MainModule.FileName;

                var pathToContentRoot = Path.GetDirectoryName(pathToExe);

                Directory.SetCurrentDirectory(pathToContentRoot);

                logger = NLog.Web.NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();

                await CreateHostBuilder(args).Build().RunAsync();
            }
            catch (Exception ex)
            {
                System.IO.File.AppendAllText(@"log.txt", $"Error on start {ex.Message} - {ex.StackTrace}");
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseWindowsService()
            .ConfigureServices((hostContext, services) =>
            {
                //services.AddHostedService<BackgroundWorker>();
            })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    logger.Info($"Use startup");

                    webBuilder.UseStartup<Startup>();
                })
            .ConfigureAppConfiguration((context, builder) =>
            {

                logger.Info($"API started in configuration {context.HostingEnvironment.EnvironmentName}");

                builder.SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

                logger.Info($"Base directory: {Directory.GetCurrentDirectory()}");
            })

                .ConfigureLogging(logging =>
                {                    
                    logging.ClearProviders();
                    logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);

                    logger.Info($"setted logging");
                })
                .UseNLog();
    }
}
