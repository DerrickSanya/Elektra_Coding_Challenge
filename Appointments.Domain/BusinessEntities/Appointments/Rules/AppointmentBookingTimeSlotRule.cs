using System;
using Appointments.Domain.Base.Interfaces;

namespace Appointments.Domain.BusinessEntities.Appointments.Rules
{
    /// <summary>
    /// Appointment Booking Time Slot Rule
    /// </summary>
    public class AppointmentBookingTimeSlotRule : IBusinessRule
    {
        /// <summary>
        /// _appointmentDate
        /// </summary>
        private readonly TimeSpan _startTime;

        private readonly TimeSpan _endTime;

        /// <summary>
        /// Appointment Booking Time Slot Rule
        /// </summary>
        /// <param name="appointmentBookingTimeSlotRule"></param>
        /// <param name="appointmentDate"></param>
        public AppointmentBookingTimeSlotRule(TimeSpan startTime, TimeSpan endTime)
        {
            _startTime = startTime;
            _endTime = endTime;
        }

        /// <summary>
        /// RuleName
        /// </summary>
        public string RuleName => "AppointmentDateMustBeBetween_08:00-16:00_Rule";

        /// <summary>
        /// Is Violated the start time should be on after 08:00 and less than or equal 16:00
        /// </summary>
        /// <returns></returns>
        public bool IsViolated()
        {
            return !(_startTime >= new TimeSpan(08, 00, 00) && _startTime <= new TimeSpan(16, 00, 00));
        }

        /// <summary>
        /// Message
        /// </summary>
        public string ViolationMessage => $"The Appointment Time must be between 08:00 - 16:00.";
    }
}
