namespace Appointments.Utilities.Configuration
{
    using System.IO;
    using Microsoft.Extensions.Configuration;

    /// <summary>
    /// AppConfig
    /// </summary>
    public static class AppConfig
    {
        /// <summary>
        /// GetDefaultConnectionString
        /// </summary>
        /// <returns></returns>
        public static string GetDefaultConnectionString()
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            return config.GetConnectionString("AppointmentEnities");
        }

        /// <summary>
        /// GetConnectionString with name parameter
        /// </summary>
        /// <param name="connectionStringName"></param>
        /// <returns></returns>
        public static string GetConnectionString(string connectionStringName)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            return config.GetConnectionString(connectionStringName);
        }      
    }
}