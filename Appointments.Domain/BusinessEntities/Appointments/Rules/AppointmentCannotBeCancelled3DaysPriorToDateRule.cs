using System;
using Appointments.Domain.Base.Interfaces;

namespace Appointments.Domain.BusinessEntities.Appointments.Rules
{
    /// <summary>
    /// AppointmentCannotBeCancelled3DaysPriorToDateRule
    /// </summary>
    public class AppointmentCannotBeCancelled3DaysPriorToDateRule : IBusinessRule
    {
        /// <summary>
        /// _appointmentDate
        /// </summary>
        private readonly DateTime _appointmentDate;

        /// <summary>
        /// Appointment Cannot Be Cancelled 3Days Prior To Date Rule
        /// </summary>
        /// <param name="appointmentDate"></param>
        public AppointmentCannotBeCancelled3DaysPriorToDateRule(DateTime appointmentDate)
        {
            _appointmentDate = appointmentDate;
        }

        /// <summary>
        /// 
        /// </summary>
        public string RuleName => "AppointmentCannotBeCancelled3DaysPriorToScheduledDate";

        /// <summary>
        /// ViolationMessage
        /// </summary>
        public string ViolationMessage => "An appointment cannot be cancelled 3 days prior to the scheduled date.";

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool IsViolated()
        {
            return !(_appointmentDate.Date <= DateTime.Now.AddDays(3).Date);
        }
    }
}
