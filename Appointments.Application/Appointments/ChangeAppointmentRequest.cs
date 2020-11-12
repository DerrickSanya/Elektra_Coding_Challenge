namespace Appointments.Application.Appointments
{
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Change Appointment Request
    /// </summary>
    public class ChangeAppointmentRequest
    {
        /// <summary>
        /// CurrentAppointmentDate
        /// </summary>
        [Required]
        public DateTime CurrentAppointmentDate { get; set; }

        /// <summary>
        /// NewAppointmentDate
        /// </summary>
        [Required]
        public DateTime NewAppointmentDate { get; set; }
    }
}
