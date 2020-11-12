using Appointments.Utilities;
using MediatR;
using System;
namespace Appointments.Domain.BusinessEntities.Appointments.Commands
{
    /// <summary>
    /// BookAppointment Command
    /// </summary>
    public sealed class BookAppointmentCommand : IRequest
    {
        #region Properties
        /// <summary>
        /// PatientId
        /// </summary>
        public int PatientId { get; set; }

        // public int equipmentId, 
        /// public string referenceCode 

        /// <summary>
        /// Appointment Date
        /// </summary>
        public DateTime AppointmentDate { get; set; }

        /// <summary>
        /// StartTime
        /// </summary>
        public TimeSpan StartTime { get; set; }

        /// <summary>
        /// StartTime
        /// </summary>
        public TimeSpan EndTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ReferenceCode { get; set; }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="patientId"></param>
        /// <param name="appointmentDate"></param>
        /// <param name="startTime"></param>
        public BookAppointmentCommand(int patientId, DateTime appointmentDate)
        {
            PatientId = patientId;
            AppointmentDate = appointmentDate.Date;
            StartTime = appointmentDate.TimeOfDay;
            ReferenceCode = Extensions.GetUniqueReferenceCode(); // gnerate unique code for user to easily memorise.
            EndTime = TimeSpan.FromHours(appointmentDate.TimeOfDay.Hours + 1); // 1 hour time slots assumption
        }
    }
}
