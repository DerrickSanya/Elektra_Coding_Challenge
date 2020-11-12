using Appointments.Domain.Base;
using System;

namespace Appointments.Domain.BusinessEntities.Appointments.Events
{
    /// <summary>
    /// Appointment Rescheduled Event
    /// </summary>
    public sealed class AppointmentRescheduledEvent : BaseDomainEvent
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
        /// PreviousDate
        /// </summary>
        public DateTime PreviousDate { get; private set; }

        /// <summary>
        /// PreviousStartTime
        /// </summary>
        public TimeSpan PreviousStartTime { get; private set; }

        /// <summary>
        /// PreviousEndTime
        /// </summary>
        public TimeSpan PreviousEndTime { get; private set; }

        /// <summary>
        /// CurrentDate
        /// </summary>
        public DateTime CurrentDate { get; private set; }

        /// <summary>
        /// CurrentStartTime
        /// </summary>
        public TimeSpan CurrentStartTime { get; private set; }

        /// <summary>
        /// CurrentEndTime
        /// </summary>
        public TimeSpan CurrentEndTime { get; private set; }

        #endregion

        /// <summary>
        /// Appointment Cancelled Event
        /// </summary>
        /// <param name="appointmentId"></param>
        /// <param name="referenceCode"></param>
        /// <param name="name"></param>
        /// <param name="emailAddress"></param>
        /// <returns></returns>
        public static AppointmentRescheduledEvent Create(int appointmentId, string referenceCode, DateTime previousDate, TimeSpan previousStartTime, TimeSpan previousEndTime, DateTime currentDate, TimeSpan currentStartTime, TimeSpan currentEndTime)
        {
            return new AppointmentRescheduledEvent { 
                AppointmentId = appointmentId, 
                ReferenceCode = referenceCode,
                PreviousDate = previousDate,
                PreviousStartTime = previousStartTime,
                PreviousEndTime = previousEndTime,
                CurrentDate = currentDate,
                CurrentStartTime = currentStartTime,
                CurrentEndTime = currentEndTime
            };
        }
    }
}
