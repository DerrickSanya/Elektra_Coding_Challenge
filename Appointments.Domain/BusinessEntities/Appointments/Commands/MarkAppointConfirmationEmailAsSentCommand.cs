namespace Appointments.Domain.BusinessEntities.Appointments.Commands
{
    using MediatR;

    /// <summary>
    /// Mark Appoint Confirmation Email As SentCommand
    /// </summary>
    public sealed class MarkAppointConfirmationEmailAsSentCommand : IRequest
    {
        /// <summary>
        /// AppointmentId
        /// </summary>
        public int AppointmentId { get; }

        /// <summary>
        /// MarkPatientAsWelcomedCommand
        /// </summary>
        /// <param name="patientId"></param>
        public MarkAppointConfirmationEmailAsSentCommand(int appointmentId)
        {
            AppointmentId = appointmentId;
        }
    }
}