using Appointments.Domain.Base.Interfaces;
using Appointments.Domain.BusinessEntities.Appointments.Rules.Interfaces;
using System;

namespace Appointments.Domain.BusinessEntities.Appointments.Rules
{
    /// <summary>
    /// Appointment Equipment Is Available Rule
    /// </summary>
    public class AppointmentEquipmentIsAvailableRule: IBusinessRule
    {
        /// <summary>
        /// Appointment Equipment Is Available Validator
        /// </summary>
        private readonly IAppointmentEquipmentIsAvailableValidator _appointmentEquipmentIsAvailableValidator;

        /// <summary>
        /// _appointmentDate
        /// </summary>
        private readonly DateTime _appointmentDate;

        /// <summary>
        /// _startTime
        /// </summary>
        private readonly TimeSpan _startTime;

        /// <summary>
        /// _endTime
        /// </summary>
        private readonly TimeSpan _endTime;



        /// <summary>
        /// Appointment Equipment Is Available Rule
        /// </summary>
        /// <param name="appointmentEquipmentIsAvailableValidator"></param>
        /// <param name="appointmentDate"></param>
        public AppointmentEquipmentIsAvailableRule(IAppointmentEquipmentIsAvailableValidator appointmentEquipmentIsAvailableValidator, DateTime appointmentDate, TimeSpan startTime, TimeSpan endTime)
        {
            _appointmentEquipmentIsAvailableValidator = appointmentEquipmentIsAvailableValidator;
            _appointmentDate = appointmentDate;
            _startTime = startTime;
            _endTime = endTime;
        }

        /// <summary>
        /// RuleName
        /// </summary>
        public string RuleName => "AppointmentEquipmentMustBeAvailableRule";

        /// <summary>
        /// ViolationMessage
        /// </summary>
        public string ViolationMessage => $"There is no Available Equipment for the specified date {_appointmentDate}.";

        /// <summary>
        /// Is Violated
        /// </summary>
        /// <returns></returns>
        public bool IsViolated() => !_appointmentEquipmentIsAvailableValidator.IsEquipmentAvailable(_appointmentDate, _startTime, _endTime).Result;
    }
}
