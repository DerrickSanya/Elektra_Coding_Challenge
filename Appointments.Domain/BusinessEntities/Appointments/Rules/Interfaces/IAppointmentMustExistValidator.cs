namespace Appointments.Domain.BusinessEntities.Appointments.Rules.Interfaces
{
    using System.Threading.Tasks;

    /// <summary>
    /// IAppointmentMustExistValidator
    /// </summary>
    public interface IAppointmentMustExistValidator
    {
        /// <summary>
        /// Appointment Exists
        /// </summary>
        /// <param name="appointmentId"></param>
        /// <returns></returns>
        Task<bool> AppointmentExists(int appointmentId);
    }
}
