using Appointments.Domain.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Appointments.Domain.BusinessEntities.Appointments.Events
{
    /// <summary>
    /// Appointment Booked Event
    /// </summary>
    public sealed class AppointmentBookedEvent : BaseDomainEvent
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

        #region Factory

        /// <summary>
        /// AppointmentBookedEvent
        /// </summary>
        /// <param name="appointmentId"></param>
        /// <param name="referenceCode"></param>
        /// <param name="name"></param>
        /// <param name="emailAddress"></param>
        /// <returns></returns>
        public static AppointmentBookedEvent Create(int appointmentId, string referenceCode, string name, string emailAddress)
        {
            return new AppointmentBookedEvent { AppointmentId = appointmentId, ReferenceCode = referenceCode,  Name = name, EmailAddress = emailAddress };
        }
        #endregion
    }
}
