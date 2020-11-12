namespace Appointments.Domain.BusinessEntities.Appointments.Commands
{
    using System;
    using MediatR;

    /// <summary>
    /// Cancel Appointment Command
    /// </summary>
    public sealed class CancelAppointmentCommand : IRequest
    {
        #region Properties
        /// <summary>
        /// PatientId
        /// </summary>
        public int PatientId { get; set; }

        /// <summary>
        /// Appointment Date
        /// </summary>
        public DateTime AppointmentDate { get; set; }

        /// <summary>
        /// ApointmentTime
        /// </summary>
        public TimeSpan AppointmentStartTime { get; set; }

        /// <summary>
        /// AppointmentEndTime
        /// </summary>
        public TimeSpan AppointmentEndTime { get; set; }
        #endregion

        /// <summary>
        /// Cancel Appointment Command
        /// </summary>
        /// <param name="patientId"></param>
        /// <param name="appointmentDate"></param>
        /// <param name="appointmentTime"></param>
        public CancelAppointmentCommand(int patientId, DateTime appointmentDate)
        {
            PatientId = patientId;
            AppointmentDate = appointmentDate.Date;
            AppointmentStartTime = appointmentDate.TimeOfDay;
            AppointmentEndTime = TimeSpan.FromHours(appointmentDate.TimeOfDay.Hours + 1);
        }
    }
}