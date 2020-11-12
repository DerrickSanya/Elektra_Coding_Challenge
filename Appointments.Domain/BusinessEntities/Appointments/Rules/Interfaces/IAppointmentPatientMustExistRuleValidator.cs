namespace Appointments.Domain.BusinessEntities.Appointments.Rules.Interfaces
{
    using System.Threading.Tasks;

    /// <summary>
    /// IAppointmentPatient MustExist RuleValidator
    /// </summary>
    public interface IAppointmentPatientMustExistRuleValidator
    {
        /// <summary>
        /// PatientExists
        /// </summary>
        /// <param name="patientId"></param>
        /// <returns></returns>
        Task<bool> PatientExists(int patientId);
    }
}
