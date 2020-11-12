using System.Threading.Tasks;
using Appointments.Utilities.DependencyInjection;
using Appointments.Domain.BusinessEntities.Appointments.Rules.Interfaces;
using Appointments.Domain.Base.Interfaces;
using Appointments.Domain.BusinessEntities.Appointments;

namespace Appointments.Infrastructure.Data.DomainData.Appointments.Rules
{
    /// <summary>
    /// Appointment Must Exist Validator
    /// </summary>
    public class AppointmentMustExistValidator : IAppointmentMustExistValidator
    {
        /// <summary>
        /// Appointment Exists
        /// </summary>
        /// <param name="appointmentId"></param>
        /// <returns></returns>
        public async Task<bool> AppointmentExists(int appointmentId)
        {
            return await DependencyResolver.Current.GetInstance<IBaseRepository<Appointment>>().ExistsAsync(new { Id = appointmentId });
        }
    }
}