namespace Appointments.Infrastructure.InternalProcessing.Commands
{
    using Appointments.Domain.Audit;
    using Appointments.Infrastructure.Data.DomainData.Appointments;
    using MediatR;
    using System;

    /// <summary>
    /// Send Email Command
    /// </summary>
    public sealed class SendEmailCommand : IRequest
    {
        #region properties

        /// <summary>
        /// EmailAddress
        /// </summary>
        public string EmailAddress { get; set; }

        /// <summary>
        /// Subject
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// Message
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// NotificationQueue
        /// </summary>
        public NotificationQueue Notification { get; set; }

        /// <summary>
        /// AppointmentNotification
        /// </summary>
        public AppointmentNotification AppointmentNotification { get; set; }

        #endregion

        /// <summary>
        /// SendEmailCommand
        /// </summary>
        /// <param name="emailAddress"></param>
        /// <param name="subject"></param>
        /// <param name="message"></param>
        public SendEmailCommand(NotificationQueue notificationQueue, string emailAddress, string subject, string message, AppointmentNotification appointmentNotification = null)
        {
            Notification = notificationQueue;
            EmailAddress = emailAddress;
            Subject = subject;
            Message = message;
            AppointmentNotification = appointmentNotification;
        }
    }
}
