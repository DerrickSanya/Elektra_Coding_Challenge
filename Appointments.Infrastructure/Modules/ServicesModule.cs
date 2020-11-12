using Microsoft.Extensions.DependencyInjection;
using Appointments.Utilities.DependencyInjection.Binding;
using Appointments.Application.Services.Interfaces;
using Appointments.Application.Services;
using Microsoft.Extensions.Hosting;
using Appointments.Infrastructure.Data.InternalData.NotificationQueues;

namespace Appointments.Infrastructure.Modules
{
    public class ServicesModule : ModuleBinder
    {
        /// <inheritdoc />
        /// <summary>
        /// Load
        /// </summary>
        public override void Load()
        {
            // Services Binding
            Bind(typeof(IPatientService), typeof(PatientService), ServiceLifetime.Scoped);
            Bind(typeof(IAppointmentService), typeof(AppointmentService), ServiceLifetime.Scoped);

            // Email service
            Bind(typeof(IMailService), typeof(MailService), ServiceLifetime.Transient);

            // sechduler for processing outbox messages
            Bind(typeof(IHostedService), typeof(NotificationsQueueScheduler), ServiceLifetime.Singleton);
        }
    }
}
