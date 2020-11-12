using Appointments.Domain.Base.Interfaces;
using Appointments.Domain.BusinessEntities.Appointments.Rules.Interfaces;

namespace Appointments.Domain.BusinessEntities.Appointments.Rules
{
    /// <summary>
    /// Appointment Patient Must Exist Rule
    /// </summary>
    public class AppointmentPatientMustExistRule : IBusinessRule
    {
        /// <summary>
        /// Appointment Patient Must Exist Rule
        /// </summary>
        private readonly IAppointmentPatientMustExistRuleValidator _appointmentPatientMustExistRuleValidator;

        /// <summary>
        /// _appointmentDate
        /// </summary>
        private readonly int _patientId;

        /// <summary>
        ///  Appointment Patient Must Exist Rule
        /// </summary>
        /// <param name="appointmentBookingTimeSlotRule"></param>
        /// <param name="appointmentDate"></param>
        public AppointmentPatientMustExistRule(IAppointmentPatientMustExistRuleValidator appointmentPatientMustExistRuleValidator, int patientId)
        {
            _appointmentPatientMustExistRuleValidator = appointmentPatientMustExistRuleValidator;
            _patientId = patientId;
        }

        /// <summary>
        /// RuleName
        /// </summary>
        public string RuleName => "PatientMustExistRule";

        /// <summary>
        /// Violation Message
        /// </summary>
        public string ViolationMessage => $"The Patient does not exist with Id: {_patientId}.";

        /// <summary>
        /// Is Violated
        /// </summary>
        /// <returns></returns>
        public bool IsViolated() => !_appointmentPatientMustExistRuleValidator.PatientExists(_patientId).Result;
    }
}