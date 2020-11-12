using Appointments.Domain.Base.Interfaces;
namespace Appointments.Domain.BusinessEntities.Patients.Rules
{
    /// <summary>
    /// Patient Email Must Be Unique Rule
    /// </summary>
    public class PatientEmailMustBeUniqueRule: IBusinessRule
    {
        /// <summary>
        /// _patientUniqueChecker
        /// </summary>
        private readonly IPatientEmailMustBeUniqueRuleValidator _patientEmailMustBeUniqueRuleValidator;

        /// <summary>
        /// _patientUniqueChecker
        /// </summary>
        private readonly string _email;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="patientUniqueChecker"></param>
        /// <param name="email"></param>
        public PatientEmailMustBeUniqueRule(IPatientEmailMustBeUniqueRuleValidator patientEmailMustBeUniqueRuleValidator, string email)
        {
            _patientEmailMustBeUniqueRuleValidator = patientEmailMustBeUniqueRuleValidator;
            _email = email;
        }

        /// <summary>
        /// RuleName
        /// </summary>
        public string RuleName => "RegisterPatientEmailAddresMustBeUnique";

        /// <summary>
        /// Is Violated
        /// </summary>
        /// <returns></returns>
        public bool IsViolated()
        {
            return !_patientEmailMustBeUniqueRuleValidator.IsUnique(_email).Result;
        }

        /// <summary>
        /// Violation Message
        /// </summary>
        public string ViolationMessage => "Patient with this email address already exists.";

    }
}
