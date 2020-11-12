using System.Threading.Tasks;

namespace Appointments.Domain.BusinessEntities.Patients.Rules
{
    /// <summary>
    /// IPatient EmailMust Be Unique Rule Validator
    /// </summary>
    public interface IPatientEmailMustBeUniqueRuleValidator
    {
        /// <summary>
        /// IsUnique
        /// </summary>
        /// <param name="emailAddress"></param>
        /// <returns></returns>
        Task<bool> IsUnique(string emailAddress);
    }
}
