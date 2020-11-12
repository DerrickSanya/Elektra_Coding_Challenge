using System.Threading.Tasks;
using Appointments.Utilities.DependencyInjection;
using Appointments.Domain.BusinessEntities.Appointments.Rules.Interfaces;
using Appointments.Domain.Base.Interfaces;
using Appointments.Domain.BusinessEntities.Patients;

namespace Appointments.Infrastructure.Data.DomainData.Appointments.Rules
{
    /// <summary>
    /// Appointment Patient Must Exist Rule Validator
    /// </summary>
    public class AppointmentPatientMustExistRuleValidator : IAppointmentPatientMustExistRuleValidator
    {
        /// <summary>
        /// Patient Exists
        /// </summary>
        /// <param name="patientId"></param>
        /// <returns></returns>
        public async Task<bool> PatientExists(int patientId)
        {
            return await DependencyResolver.Current.GetInstance<IBaseRepository<Patient>>().ExistsAsync(new { Id = patientId });
        }
    }
}
