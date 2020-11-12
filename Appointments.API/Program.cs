using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Serilog;
using Appointments.API.Helpers;

namespace Appointments.API
{
    public class Program
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            var logger = LoggerConfig.Configure();
            CreateWebHostBuilder(args).Build().Run();
            try
            {
                logger.Information("Appointments Web Api Starting up....!!!");
                CreateWebHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                logger.Fatal(ex, $"Appointments Web Api start-up failed to Start because: {ex.Message}");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        /// <summary>
        /// Creates the web host builder.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <returns></returns>
        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            return WebHost.CreateDefaultBuilder(args).UseSerilog().UseStartup<Startup>();
        }
    }
}
