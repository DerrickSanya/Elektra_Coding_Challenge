namespace Appointments.Domain.BusinessEntities.Appointments
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// 
    /// </summary>
    public interface IAppointmentReadRepository
    {
        /// <summary>
        /// GetAllAppointments
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<AppointmentDetail>> GetAllAppointments();
    }
}
