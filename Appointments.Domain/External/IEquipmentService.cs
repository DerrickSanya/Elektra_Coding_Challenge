namespace Appointments.Domain.External
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// IEquipment Service
    /// </summary>
    public interface IEquipmentService
    {
        /// <summary>
        /// GetEquipmentById
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Equipment> GetEquipmentById(int id);

        /// <summary>
        /// GetEquipment
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dateAvailable"></param>
        /// <param name="isAvailable"></param>
        /// <returns></returns>
        Task<Equipment> GetEquipment(int id, DateTime dateAvailable, bool isAvailable);

        /// <summary>
        /// GetEquipments
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Equipment>> GetEquipments();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="equipment"></param>
        /// <returns></returns>
        Task MarkAsUnAvailable(Equipment equipment);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="equipment"></param>
        /// <returns></returns>
        Task MarkAsAvailable(Equipment equipment);

        /// <summary>
        /// Get Available Date
        /// </summary>
        /// <returns></returns>
        Task<Equipment> GetEquipmentAvailableOnAppointmentDateAsync(DateTime appointmentDate, TimeSpan startTime, TimeSpan endTime);
    }
}
