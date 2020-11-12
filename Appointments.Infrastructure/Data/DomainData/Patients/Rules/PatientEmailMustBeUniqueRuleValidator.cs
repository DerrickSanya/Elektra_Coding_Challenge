using Appointments.Domain.Base.Interfaces;
using Appointments.Domain.BusinessEntities.Patients;
using Appointments.Domain.BusinessEntities.Patients.Rules;
using Appointments.Utilities.DependencyInjection;
using System.Threading.Tasks;

namespace Appointments.Infrastructure.Data.DomainData.Patients.Rules
{
    /// <summary>
    /// Patient Email Must Be Unique Rule Validator
    /// </summary>
    public class PatientEmailMustBeUniqueRuleValidator : IPatientEmailMustBeUniqueRuleValidator
    {
        /// <summary>
        /// Is Unique
        /// </summary>
        /// <param name="emailAddress"></param>
        /// <returns></returns>
        public async Task<bool> IsUnique(string emailAddress)
        {
           return !await DependencyResolver.Current.GetInstance<IBaseRepository<Patient>>().ExistsAsync(new { EmailAddress = emailAddress });
        }
    }
}