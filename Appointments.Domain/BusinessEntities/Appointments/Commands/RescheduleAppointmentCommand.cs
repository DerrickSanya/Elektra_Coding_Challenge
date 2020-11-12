namespace Appointments.Domain.BusinessEntities.Appointments.Commands
{
    using System;
    using MediatR;

    /// <summary>
    /// RescheduleAppointmentCommand
    /// </summary>
    public sealed class RescheduleAppointmentCommand : IRequest
    {
        #region Properties
        /// <summary>
        /// PatientId
        /// </summary>
        public int PatientId { get; set; }

        /// <summary>
        /// Appointment Date
        /// </summary>
        public DateTime CurrentAppointmentDate { get; set; }

        /// <summary>
        /// Current Appointment Time
        /// </summary>
        public TimeSpan CurrentAppointmentStartTime { get; set; }

        /// <summary>
        /// Current Appointment EndTime
        /// </summary>
        public TimeSpan CurrentAppointmentEndTime { get; set; }

        /// <summary>
        /// NewAppointmentDate
        /// </summary>
        public DateTime NewAppointmentDate { get; set; }

        /// <summary>
        /// NewAppointmentTime
        /// </summary>
        public TimeSpan NewAppointmentStartTime { get; set; }

        /// <summary>
        /// NewAppointmentEndTime
        /// </summary>
        public TimeSpan NewAppointmentEndTime { get; set; }
        #endregion

        /// <summary>
        /// Reschedule Appointment Command
        /// </summary>
        /// <param name="patientId"></param>
        /// <param name="currentAppointmentDate"></param>
        /// <param name="newAppointmentDate"></param>
        public RescheduleAppointmentCommand(int patientId, DateTime currentAppointmentDate, DateTime newAppointmentDate)
        {
            PatientId = patientId;
            CurrentAppointmentDate = currentAppointmentDate.Date;
            CurrentAppointmentStartTime = currentAppointmentDate.TimeOfDay;
            CurrentAppointmentEndTime = TimeSpan.FromHours(currentAppointmentDate.TimeOfDay.Hours + 1);
            NewAppointmentDate = newAppointmentDate.Date;
            NewAppointmentStartTime = newAppointmentDate.TimeOfDay;
            NewAppointmentEndTime = TimeSpan.FromHours(newAppointmentDate.TimeOfDay.Hours + 1);
        }
    }
}