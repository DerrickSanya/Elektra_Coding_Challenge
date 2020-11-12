using Appointments.Domain.BusinessEntities.Appointments;
using Appointments.Infrastructure.Data.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Appointments.Infrastructure.Data.DomainData.Appointments
{
    /// <summary>
    /// Appointment ReadRepository
    /// </summary>
    internal class AppointmentReadRepository : BaseRepository<AppointmentDetail>, IAppointmentReadRepository
    {
        /// <summary>
        /// GetAllAppointments
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<AppointmentDetail>> GetAllAppointments()
        {
            return await GetAllAsync();
        }
    }
}
