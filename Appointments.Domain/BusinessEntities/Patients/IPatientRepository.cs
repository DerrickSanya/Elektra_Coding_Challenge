namespace Appointments.Domain.BusinessEntities.Patients
{
    using System.Threading.Tasks;

    /// <summary>
    /// IPatientRepository
    /// </summary>
    public interface IPatientRepository
    {
        /// <summary>
        /// Get Patient By Id
        /// </summary>
        /// <param name="patientId"></param>
        /// <returns></returns>
        Task<Patient> GetByIdAsync(int id);

        /// <summary>
        /// Add Patient
        /// </summary>
        /// <param name="patient"></param>
        /// <returns></returns>
        Task AddAsync(Patient patient);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="patient"></param>
        /// <returns></returns>
        Task UpdatePatientAsync(Patient patient);
    }
}