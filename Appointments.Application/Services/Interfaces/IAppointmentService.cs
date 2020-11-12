namespace Appointments.Application.Services.Interfaces
{
    using global::Appointments.Domain.BusinessEntities.Appointments;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// IAppointmentService
    /// </summary>
    public interface IAppointmentService
    {
        /// <summary>
        /// GetAllAppointments
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<AppointmentDetail>> GetAllAppointments();

        /// <summary>
        /// Book Appointment
        /// </summary>
        /// <param name="patientId"></param>
        /// <param name="appointmentDate"></param>
        /// <returns></returns>
        Task BookAppointment(int patientId, DateTime appointmentDate);

        /// <summary>
        /// Cancel Appointment
        /// </summary>
        /// <param name="patientId"></param>
        /// <param name="appointmentDate"></param>
        /// <returns></returns>
        Task CancelAppointment(int patientId, DateTime appointmentDate);

        /// <summary>
        /// Reschedule Appointment
        /// </summary>
        /// <param name="patientId"></param>
        /// <param name="currentAppointmentDate"></param>
        /// <param name="newAppointmentDate"></param>
        /// <returns></returns>
        Task RescheduleAppointment(int patientId, DateTime currentAppointmentDate, DateTime newAppointmentDate);
    }
}
