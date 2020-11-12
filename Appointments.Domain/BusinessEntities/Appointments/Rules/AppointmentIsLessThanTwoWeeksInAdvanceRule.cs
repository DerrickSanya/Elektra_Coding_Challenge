using System;
using Appointments.Domain.Base.Interfaces;

namespace Appointments.Domain.BusinessEntities.Appointments.Rules
{
    /// <summary>
    /// AppointmentIsLessThanTwoWeeksInAdvanceRule
    /// </summary>
    public class AppointmentIsLessThanTwoWeeksInAdvanceRule: IBusinessRule
    {
        /// <summary>
        /// _appointmentDate
        /// </summary>
        private readonly DateTime _appointmentDate;

        /// <summary>
        ///  Appointment Is Less Than Two Weeks In Advance Rule
        /// </summary>
        /// <param name="appointmentIsLessThanTwoWeeksInAdvanceRuleValidator"></param>
        /// <param name="appointmentDate"></param>
        public AppointmentIsLessThanTwoWeeksInAdvanceRule(DateTime appointmentDate)
        {
            _appointmentDate = appointmentDate;
        }

        /// <summary>
        /// RuleName
        /// </summary>
        public string RuleName => "AppointmentDateMustBeNotLaterThanTwoWeeksFromTodayRule";

        /// <summary>
        /// Is Violated
        /// </summary>
        /// <returns></returns>
        public bool IsViolated() 
        {
            // Not specified but cannot book appointments in the past.
            return ((_appointmentDate.Date >= DateTime.Now.Date) &&  (_appointmentDate.Date >  DateTime.Now.AddDays(14).Date));
        }

        /// <summary>
        /// Message
        /// </summary>
        public string ViolationMessage => $"Cannot book an Appointment later than two weeks from today ({DateTime.Now:dd/MM/yyyy}) or in the past.";
    }
}
