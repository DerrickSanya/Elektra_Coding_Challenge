using Appointments.Application.Services.Interfaces;
using Appointments.Domain.Audit;
using Appointments.Domain.Base.Interfaces;
using Appointments.Domain.BusinessEntities.Appointments.Commands;
using Appointments.Infrastructure.InternalProcessing.Commands;
using MediatR;
using Serilog;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Appointments.Infrastructure.InternalProcessing.CommandHandlers
{
    /// <summary>
    /// Send Email Command Handler
    /// </summary>
    public sealed class SendEmailCommandHandler : IRequestHandler<SendEmailCommand>
    {
        /// <summary>
        /// _notificationsRepository
        /// </summary>
        private readonly IBaseRepository<NotificationQueue> _notificationsRepository;

        /// <summary>
        /// _emailService
        /// </summary>
        private readonly IMailService _emailService;

        /// <summary>
        /// ILogger
        /// </summary>
        private readonly ILogger _logger;

        /// <summary>
        /// _mediator
        /// </summary>
        private readonly IMediator _mediator;

        /// <summary>
        /// SendEmailCommandHandler
        /// </summary>
        /// <param name="emailService"></param>
        public SendEmailCommandHandler(IBaseRepository<NotificationQueue> notificationsRepository, IMediator mediator, IMailService emailService)
        {
            _notificationsRepository = notificationsRepository;
            _emailService = emailService;
            _logger = Log.Logger;
            _mediator = mediator;
        }

        /// <summary>
        /// Handle  Request
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<Unit> Handle(SendEmailCommand request, CancellationToken cancellationToken)
        {
            await _emailService.SendEmailMessage(request.EmailAddress, request.Subject, request.Message);

            if (request.Notification.MessageType == "Appointments.Domain.BusinessEntities.Appointments.Events.AppointmentBookedEvent")
            {     // mark IsConfirmationEmailSent to true.            
                await _mediator.Send(new MarkAppointConfirmationEmailAsSentCommand(request.AppointmentNotification.AppointmentId));
                _logger.Information($"Appointment Confirmation Email sent to {request.AppointmentNotification.EmailAddress} with Reference Code {request.AppointmentNotification.ReferenceCode}.");
            }

            // update as processed.
            request.Notification.ProcessedDate = DateTime.Now;
            await _notificationsRepository.UpdateAsync(request.Notification);
            return Unit.Value;
        }
    }
}
