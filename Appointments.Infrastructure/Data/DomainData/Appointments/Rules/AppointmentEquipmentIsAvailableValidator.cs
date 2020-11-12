using System;
using System.Threading.Tasks;
using Appointments.Domain.BusinessEntities.Appointments.Rules.Interfaces;
using Appointments.Domain.External;
using Appointments.Utilities.DependencyInjection;

namespace Appointments.Infrastructure.Data.DomainData.Appointments.Rules
{
    /// <summary>
    /// Appointment Equipment Is Available Validator
    /// </summary>
    public class AppointmentEquipmentIsAvailableValidator : IAppointmentEquipmentIsAvailableValidator
    {
        /// <summary>
        /// IsEquipmentAvailable
        /// </summary>
        /// <param name="appointmentDate"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public async Task<bool> IsEquipmentAvailable(DateTime appointmentDate, TimeSpan startTime, TimeSpan endTime)
        {
            // Mock call to the external provider that manages the equipment to check if the equipment is available for that particular date
            var equipmentService = DependencyResolver.Current.GetInstance<IEquipmentService>();
            var availableEquipment = await equipmentService.GetEquipmentAvailableOnAppointmentDateAsync(appointmentDate, startTime, endTime);
            return availableEquipment != null;
        }
    }
}