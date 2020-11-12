namespace Appointments.Domain.BusinessEntities.Patients.Commands
{
    using MediatR;

    /// <summary>
    /// MarkPatientAsWelcomedCommand
    /// </summary>
    public sealed class MarkPatientAsWelcomedCommand : IRequest
    {
        /// <summary>
        /// PatientId
        /// </summary>
        public int PatientId { get; }

        /// <summary>
        /// MarkPatientAsWelcomedCommand
        /// </summary>
        /// <param name="patientId"></param>
        public MarkPatientAsWelcomedCommand(int patientId)
        {
            PatientId = patientId;
        }
    }
}
