namespace Appointments.Domain.BusinessEntities.Appointments.Rules.Interfaces
{
    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// IAppointment Equipment Is Available Validator
    /// </summary>
    public interface IAppointmentEquipmentIsAvailableValidator
    {
        /// <summary>
        /// Is Equipment Available
        /// </summary>
        /// <param name="appointmentDate"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        Task<bool> IsEquipmentAvailable(DateTime appointmentDate, TimeSpan startTime, TimeSpan endTime);
    }
}
