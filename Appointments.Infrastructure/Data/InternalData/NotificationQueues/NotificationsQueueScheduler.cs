using Appointments.Application.Configuration;
using Appointments.Application.Services.Interfaces;
using Appointments.Domain.Audit;
using Appointments.Domain.Base.Interfaces;
using Appointments.Domain.BusinessEntities.Appointments.Events;
using Appointments.Infrastructure.Data.DomainData.Appointments;
using Appointments.Infrastructure.InternalProcessing.Commands;
using MediatR;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Appointments.Infrastructure.Data.InternalData.NotificationQueues
{
    /// <summary>
    /// NotificationsQueueScheduler
    /// </summary>
    public class NotificationsQueueScheduler : HostedServiceBase
    {
        const string AppointmentBooked = "Appointments.Domain.BusinessEntities.Appointments.Events.AppointmentCancelledEvent";
        const string AppointmentCancelled = "Appointments.Domain.BusinessEntities.Appointments.Events.AppointmentCancelledEvent";
        const string AppointmentRescheduled = "Appointments.Domain.BusinessEntities.Appointments.Events.AppointmentRescheduledEvent";

        /// <summary>
        /// _options
        /// </summary>
        private readonly IOptions<NotificationTimerConfig> _options;

        /// <summary>
        /// _notificationsRepository
        /// </summary>
        private readonly IBaseRepository<NotificationQueue> _notificationsRepository;

        /// <summary>
        /// _logger
        /// </summary>
        private readonly ILogger _logger;

        /// <summary>
        /// _mediator
        /// </summary>
        private readonly IMediator _mediator;    

        /// <summary>
        /// Notifications Queue Scheduler
        /// </summary>
        /// <param name="options"></param>
        public NotificationsQueueScheduler(IOptions<NotificationTimerConfig> options, IBaseRepository<NotificationQueue> notificationsRepository, IMailService emailService, IMediator mediator)
        {
            _options = options;
            _notificationsRepository = notificationsRepository;
            _logger = Log.Logger;
            _mediator = mediator;
        }

        /// <summary>
        /// ExecuteAsync
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                var notifications = await _notificationsRepository.GetManyAsync(new { ProcessedDate = null as object });
                if (notifications != null)
                {
                      _logger.Information($"The Notification Queque Has Items ready to process...");

                    foreach (var notification in notifications)
                    {
                       

                        // only processing these at the minute and should be to used to raise events
                        if (notification.MessageType == AppointmentBooked)
                        {
                            var appointmentNotification = JsonConvert.DeserializeObject<AppointmentNotification>(notification.MessageData);
                            var subject = $"Appointment Confirmation for {appointmentNotification.Name}";
                            var message = $"Your Appointment is confirmed Reference Code: {appointmentNotification.ReferenceCode}";
                            await _mediator.Send(new SendEmailCommand(notification, appointmentNotification.EmailAddress, subject, message, appointmentNotification));                           
                        }

                        if (notification.MessageType == AppointmentCancelled)
                        {
                            var appointmentNotification = JsonConvert.DeserializeObject<AppointmentNotification>(notification.MessageData);
                            var subject = $"Appointment Cancelled: {appointmentNotification.ReferenceCode}";
                            var message = $"The Appointment Was cancelled with Reference Code: {appointmentNotification.ReferenceCode}";
                            await _mediator.Send(new SendEmailCommand(notification, "equipment@elektra.com", subject, message, appointmentNotification));
                        }

                        if (notification.MessageType == AppointmentRescheduled)
                        {
                            var appointmentNotification = JsonConvert.DeserializeObject<AppointmentRescheduledEvent>(notification.MessageData);
                            var subject = $"Appointment Rescheduled: {appointmentNotification.ReferenceCode}";
                            var message = $"The Appointment with Reference Code: {appointmentNotification.ReferenceCode} Was Rescheduled from {appointmentNotification.PreviousDate: dd/MM/yyyy} {appointmentNotification.PreviousStartTime: hh:mm} to {appointmentNotification.CurrentStartTime: dd/MM/yyyy} {appointmentNotification.CurrentStartTime: hh:mm} ";
                            await _mediator.Send(new SendEmailCommand(notification, "equipment@elektra.com", subject, message));
                        }

                        await Task.Delay(TimeSpan.FromMilliseconds(1000), cancellationToken);
                    }
                }

                await Task.Delay(TimeSpan.FromMilliseconds(_options.Value.Frequency), cancellationToken);
            }
        }
    }
}
