namespace Appointments.API.Helpers
{
    using Serilog;
    using Serilog.Events;
    using System.IO;

    /// <summary>
    /// LoggerConfig
    /// </summary>
    internal static class LoggerConfig
    {
        /// <summary>
        /// ILogger
        /// </summary>
        /// <returns></returns>
        public static ILogger Configure()
        {
            #if DEBUG

            var logTemplate = "Time: {Timestamp: dd-MM-yyyy HH:mm:ss}{NewLine} Category: {Level} {NewLine} Message: {Message:lj}{NewLine}{Exception}{NewLine}";
            var logFilePath = Path.GetFullPath("~/Logs/log-.txt").Replace("~\\", "");
            return Log.Logger = new LoggerConfiguration()
                                                       .MinimumLevel.Debug()
                                                       .WriteTo.File(path: logFilePath, outputTemplate: logTemplate, rollingInterval: RollingInterval.Day, retainedFileCountLimit: 31, rollOnFileSizeLimit: true)
                                                       .Enrich.FromLogContext()
                                                       .WriteTo.Console(restrictedToMinimumLevel: LogEventLevel.Information)
                                                       .CreateLogger();

            #else

                 var logTemplate = "Time: {Timestamp: dd-MM-yyyy HH:mm:ss}{NewLine} Category: {Level} {NewLine} Message: {Message:lj}{NewLine}{Exception}{NewLine}";
                 var logFilePath = Path.GetFullPath("~/Logs/log-{Date}.txt").Replace("~\\", "");
               return  Log.Logger = new LoggerConfiguration()
                 .MinimumLevel.Information()
                 .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                 .Enrich.FromLogContext()
                 .WriteTo.File(path: logFilePath, outputTemplate: logTemplate, shared: true, rollingInterval: RollingInterval.Day, retainedFileCountLimit: 31, rollOnFileSizeLimit: true)
                 .CreateLogger();

            #endif
        }
    }
}
