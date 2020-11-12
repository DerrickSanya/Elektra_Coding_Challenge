using Appointments.Domain.Base;

namespace Appointments.Domain.BusinessEntities.Appointments.Events
{
    public sealed class AppointmentCancelledEvent : BaseDomainEvent
    {
        #region Properties
        /// <summary>
        /// AppointmentId
        /// </summary>
        public int AppointmentId { get; private set; }

        /// <summary>
        /// ReferenceCode
        /// </summary>
        public string ReferenceCode { get; private set; }

        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// EmailAddress
        /// </summary>
        public string EmailAddress { get; private set; }
        #endregion

        /// <summary>
        /// Appointment Cancelled Event
        /// </summary>
        /// <param name="appointmentId"></param>
        /// <param name="referenceCode"></param>
        /// <param name="name"></param>
        /// <param name="emailAddress"></param>
        /// <returns></returns>
        public static AppointmentCancelledEvent Create(int appointmentId, string referenceCode, string name, string emailAddress)
        {
            return new AppointmentCancelledEvent { AppointmentId = appointmentId, ReferenceCode = referenceCode, Name = name, EmailAddress = emailAddress };
        }
    }
}
