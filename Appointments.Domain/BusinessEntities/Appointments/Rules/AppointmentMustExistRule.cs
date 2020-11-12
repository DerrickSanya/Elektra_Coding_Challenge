using Appointments.Domain.Base.Interfaces;
using Appointments.Domain.BusinessEntities.Appointments.Rules.Interfaces;

namespace Appointments.Domain.BusinessEntities.Appointments.Rules
{
    /// <summary>
    /// AppointmentMustExistRule
    /// </summary>
    public class AppointmentMustExistRule : IBusinessRule
    {
        /// <summary>
        /// IAppointmentMustExistValidator
        /// </summary>
        private readonly IAppointmentMustExistValidator _appointmentMustExistValidator;

        /// <summary>
        /// _appointmentId
        /// </summary>
        private readonly int _appointmentId;

        /// <summary>
        /// Appointment Cannot Be Cancelled 3Days Prior To Date Rule
        /// </summary>
        /// <param name="appointmentDate"></param>
        public AppointmentMustExistRule(IAppointmentMustExistValidator appointmentMustExistValidator, int appointmentId)
        {
            _appointmentMustExistValidator = appointmentMustExistValidator;
            _appointmentId = appointmentId;
        }

        /// <summary>
        /// RuleName
        /// </summary>
        public string RuleName => "AppointmentMustExistRule";

        /// <summary>
        /// ViolationMessage
        /// </summary>
        public string ViolationMessage => "An appointment needs to exist before it can be cancelled.";

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool IsViolated() => !_appointmentMustExistValidator.AppointmentExists(_appointmentId).Result;
    }
}
