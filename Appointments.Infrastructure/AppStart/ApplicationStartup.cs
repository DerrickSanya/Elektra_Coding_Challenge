using System.Linq;
using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Hellang.Middleware.ProblemDetails;
using Appointments.Infrastructure.Database.DbContexts;
using Appointments.Domain.BusinessEntities.Patients;
using Appointments.Infrastructure.Modules;
using Appointments.Utilities.DependencyInjection;
using Appointments.Infrastructure.Helpers;
using Appointments.Domain.Base.Exceptions;
using Appointments.Infrastructure.Database;
using Serilog;
using System.Collections.Generic;
using Appointments.Infrastructure.Data.InternalData.NotificationQueues;
using Appointments.Infrastructure.Data.DomainData.Appointments;

namespace Appointments.Infrastructure.AppStart
{
    /// <summary>
    /// Application Startup
    /// </summary>
    public class ApplicationStartup
    {
        /// <summary>
        /// Initialize
        /// </summary>
        /// <param name="services"></param>
        /// <param name="connectionString"></param>
        /// <param name="logger"></param>
        /// <param name="executionContextAccessor"></param>
        /// <returns></returns>
        /// public static IServiceProvider Initialize(IServiceCollection services, string connectionString, ILogger logger, IExecutionContextAccessor executionContextAccessor)
        public static void Initialize(IServiceCollection services, string connectionString)
        {
            // TODO: get options from appsettings and retrieve the timing for the cached objects.
            CreateAutofacServiceProvider(services, connectionString);
        }

        /// <summary>
        /// Create Autofac Service Provider
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        private static void CreateAutofacServiceProvider(IServiceCollection services, string connectionString)
        {
            DependencyResolver.AddModules(services, new DataModule().Bindings.ToList());
            DependencyResolver.AddModules(services, new ServicesModule().Bindings.ToList());

            
            services.AddTransient(x => new AppointmentsDbContext(new DbContextOptionsBuilder<AppointmentsDbContext>().UseSqlServer(connectionString).Options));
            services.AddTransient<ISqlConnectionManager>(x => new SqlConnectionManager(connectionString));

            services.AddMediatR(new Assembly[] { typeof(Patient).GetTypeInfo().Assembly, typeof(AppointmentNotification).GetTypeInfo().Assembly });

            services.AddProblemDetails(x =>
            {
                x.Map<InvalidCommandException>(ex => new InvalidCommandProblemDetails(ex));
                x.Map<BusinessRuleViolationException>(ex => new BusinessRuleViolationExceptionProblemDetails(ex));
            });
        }
    }
}
