using Appointments.Domain.Base.Interfaces;
using System;

namespace Appointments.Domain.BusinessEntities.Appointments.Rules
{
    /// <summary>
    /// Appointment Change Is Greater Than 2Days Prior To Date Rule
    /// </summary>
    public class AppointmentChangeIsLessThan2DaysPriorToDateRule : IBusinessRule
    { 
        /// <summary>
      /// _appointmentDate
      /// </summary>
        private readonly DateTime _appointmentDate;
        private readonly DateTime _newDate;

        /// <summary>
        /// Appointment Cannot Be Changed 2Days Prior To Date Rule
        /// </summary>
        /// <param name="appointmentDate"></param>
        public AppointmentChangeIsLessThan2DaysPriorToDateRule(DateTime appointmentDate, DateTime newDate)
        {
            _appointmentDate = appointmentDate;
            _newDate = newDate;
        }

        /// <summary>
        /// 
        /// </summary>
        public string RuleName => "AppointmentChangeIsLesshan2DaysPriorToScheduledDate";

        /// <summary>
        /// ViolationMessage
        /// </summary>
        public string ViolationMessage => "An appointment cannot be changed 2 days prior to the scheduled date.";

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool IsViolated()
        {
            return (_appointmentDate.Date <= DateTime.Now.AddDays(2).Date);
        }
    }
}

