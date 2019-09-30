using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Web;

namespace API.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var logger = NLogBuilder
                .ConfigureNLog("NLog.config")
                .GetCurrentClassLogger();

            try
            {
                logger.Info("Starting catalogue api server");

                CreateWebHostBuilder(args)
                    .Build()
                    .Run();
            }
            catch (Exception ex)
            {
                logger.Fatal(ex, "Host for catalogue api server terminated unexpectedly");
            }
            finally
            {
                NLog.LogManager.Shutdown();
            }
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .ConfigureLogging(logging =>
                {
                    logging.ClearProviders();
                    logging.SetMinimumLevel(LogLevel.Information);
                })
                .UseNLog();
    }
}
