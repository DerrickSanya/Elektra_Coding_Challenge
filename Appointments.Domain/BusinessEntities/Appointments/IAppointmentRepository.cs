namespace Appointments.Domain.BusinessEntities.Appointments
{
    using System.Threading.Tasks;

    /// <summary>
    /// IAppointmentRepository
    /// </summary>
    public interface IAppointmentRepository
    {
        /// <summary>
        /// Get Appointment By Id
        /// </summary>
        /// <param name="patientId"></param>
        /// <returns></returns>
        Task<Appointment> GetByIdAsync(int id);

        /// <summary>
        /// GetByReferenceCodeAsync
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Appointment> GetByReferenceCodeAsync(string referenceCode);

        /// <summary>
        /// Add Appointment
        /// </summary>
        /// <param name="appointment"></param>
        /// <returns></returns>
        Task AddAsync(Appointment appointment);

        /// <summary>
        /// UpdateAppointmentAsync
        /// </summary>
        /// <param name="patient"></param>
        /// <returns></returns>
        Task UpdateAppointmentAsync(Appointment appointment);
    }
}
