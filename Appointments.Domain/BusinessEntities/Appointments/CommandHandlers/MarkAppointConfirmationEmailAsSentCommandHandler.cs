using Appointments.Domain.BusinessEntities.Appointments.Commands;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Appointments.Domain.BusinessEntities.Appointments.CommandHandlers
{
    /// <summary>
    /// Mark Appoint Confirmation EmailAsSent CommandHandler
    /// </summary>
    public sealed class MarkAppointConfirmationEmailAsSentCommandHandler : IRequestHandler<MarkAppointConfirmationEmailAsSentCommand>
    {
        /// <summary>
        /// _appointmentRepository
        /// </summary>
        private readonly IAppointmentRepository _appointmentRepository;

        /// <summary>
        /// Mark Appoint Confirmation EmailAsSent CommandHandler
        /// </summary>
        /// <param name="_appointmentRepository"></param>
        public MarkAppointConfirmationEmailAsSentCommandHandler(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }

        /// <summary>
        /// Handle Event
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<Unit> Handle(MarkAppointConfirmationEmailAsSentCommand request, CancellationToken cancellationToken)
        {
            var appointment = await _appointmentRepository.GetByIdAsync(request.AppointmentId);
            appointment.IsConfirmationEmailSent = true;
            await _appointmentRepository.UpdateAppointmentAsync(appointment);
            return await Unit.Task;
        }
    }
}