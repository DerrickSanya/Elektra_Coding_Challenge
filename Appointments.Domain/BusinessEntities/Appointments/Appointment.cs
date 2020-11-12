using System;
using Appointments.Domain.Base;
using Appointments.Domain.Base.Interfaces;
using Appointments.Domain.BusinessEntities.Appointments.Rules;
using Appointments.Domain.BusinessEntities.Appointments.Rules.Interfaces;
using Appointments.Domain.BusinessEntities.Patients;
using Appointments.Utilities.SqlGenerator.Attributes;

namespace Appointments.Domain.BusinessEntities.Appointments
{
    /// <summary>
    /// Appointment
    /// </summary>
    [Scheme("patient"), StoredAs("Appointments")]
    public class Appointment : BaseDomainEntity, IAggregateRoot
    {
        #region Properties
        /// <summary>
        /// Identifier field
        /// </summary>
        [KeyProperty(Identity = true)]
        public int Id { get; set; }

        /// <summary>
        /// Identifier field
        /// </summary>
        public int PatientId { get; set; }

        /// <summary>
        /// Equipment Id
        /// </summary>
        public int EquipmentId { get; set; }

        /// <summary>
        /// Reference Code
        /// </summary>
        public string ReferenceCode { get; set; }

        /// <summary>
        /// AppointmentDate
        /// </summary>
        public DateTime AppointmentDate { get; set; }

        /// <summary>
        /// Appointment Start time
        /// </summary>
        public TimeSpan StartTime { get; set; }

        /// <summary>
        /// appointment end time
        /// </summary>
        public TimeSpan EndTime { get; set; }

        /// <summary>
        /// Is Confirmation EmailSent
        /// </summary>
        public bool IsConfirmationEmailSent { get; set; }

        /// <summary>
        /// Is Cancelled
        /// </summary>
        public bool IsCancelled { get; set; }

        /// <summary>
        /// Created On
        /// </summary>
        public DateTime CreatedOn { get; set; }

        /// <summary>
        /// Last Modified
        /// </summary>
        public DateTime? LastModified { get; set; }

        /// <summary>
        /// Patient
        /// </summary>
        [NonStored]
        public Patient Patient { get; set; }

        #endregion

        #region ctors
        /// <summary>
        /// Appointment
        /// </summary>
        public Appointment()
        { }
        #endregion

        #region Methods

        /// <summary>
        /// BookAppointment
        /// </summary>
        /// <param name="patientId"></param>
        /// <param name="equipmentId"></param>
        /// <param name="referenceCode"></param>
        /// <param name="appointmentDate"></param>
        /// <param name="patientMustExistRuleValidator"></param>
        /// <param name="timeSlotRuleValidator"></param>
        /// <param name="appointmentDateIsLessThanTwoWeeksInAdvanceRuleValidator"></param>
        /// <returns></returns>
        public static Appointment BookAppointment(int patientId, int equipmentId, string referenceCode, DateTime appointmentDate, TimeSpan startTime, TimeSpan endTime, IAppointmentEquipmentIsAvailableValidator appointmentEquipmentIsAvailableValidator, IAppointmentPatientMustExistRuleValidator patientMustExistRuleValidator)
        {
            // validate the business rules   

            // Appointments can only be made for 2 weeks later at most.
            ValidateBusinessRule(new AppointmentIsLessThanTwoWeeksInAdvanceRule(appointmentDate));

            // Assumption that the patient has to registered for them to be able to book an appointment.
            ValidateBusinessRule(new AppointmentPatientMustExistRule(patientMustExistRuleValidator, patientId));

            // Appointments can be made between 08:00 and 16:00. An appointment takes 1 hour.
            ValidateBusinessRule(new AppointmentBookingTimeSlotRule(startTime, endTime));

            ValidateBusinessRule(new AppointmentEquipmentIsAvailableRule(appointmentEquipmentIsAvailableValidator, appointmentDate, startTime, endTime));

            return new Appointment(patientId, equipmentId, referenceCode, appointmentDate, startTime, endTime);
        }

        /// <summary>
        /// Update Appointment
        /// </summary>
        /// <param name="appointmentId"></param>
        /// <param name="currentAppointmentDate"></param>
        /// <param name="newAppointmentDate"></param>
        /// <param name="newAppointmentStartTime"></param>
        /// <param name="newAppointmentEndTime"></param>
        /// <param name="appointmentMustExistValidator"></param>
        /// <param name="appointmentEquipmentIsAvailableValidator"></param>
        /// <returns></returns>
        public static void UpdateAppointment(int appointmentId, DateTime currentAppointmentDate, DateTime newAppointmentDate, TimeSpan newAppointmentStartTime, TimeSpan newAppointmentEndTime, IAppointmentMustExistValidator appointmentMustExistValidator, IAppointmentEquipmentIsAvailableValidator appointmentEquipmentIsAvailableValidator)
        {
            // The appointment must exist.
            ValidateBusinessRule(new AppointmentMustExistRule(appointmentMustExistValidator, appointmentId));

            // Appointment can only be changed up to 2 days before the appointment date
            ValidateBusinessRule(new AppointmentChangeIsLessThan2DaysPriorToDateRule(currentAppointmentDate, newAppointmentDate));

            // Appointments can only be made for 2 weeks later at most.
            ValidateBusinessRule(new AppointmentIsLessThanTwoWeeksInAdvanceRule(newAppointmentDate));

            // Equipment Availability rule
            ValidateBusinessRule(new AppointmentEquipmentIsAvailableRule(appointmentEquipmentIsAvailableValidator, newAppointmentDate, newAppointmentStartTime, newAppointmentEndTime));
        }

        /// <summary>
        /// Cancel Appointment
        /// </summary>
        /// <param name="appointmentMustExistRuleValidator"></param>
        /// <param name="appointmentId"></param>
        /// <param name="referenceCode"></param>
        public static void CancelAppointment(int appointmentId, DateTime date, IAppointmentMustExistValidator appointmentMustExistValidator)
        {
            // validate the appointment exists and is not 3 days prior to schduled date.
            ValidateBusinessRule(new AppointmentMustExistRule(appointmentMustExistValidator, appointmentId));
            ValidateBusinessRule(new AppointmentCannotBeCancelled3DaysPriorToDateRule(date));
        }

        /// <summary>
        /// Mark Welcome Email Sent
        /// </summary>
        public void MarkConfirmationEmailSent()
        {
            IsConfirmationEmailSent = true;
        }

        #endregion

        #region Helpers
        /// <summary>
        /// 
        /// </summary>
        /// <param name="patientId"></param>
        /// <param name="equipmentId"></param>
        /// <param name="referenceCode"></param>
        /// <param name="appointmentDate"></param>
        private Appointment(int patientId, int equipmentId, string referenceCode, DateTime appointmentDate, TimeSpan startTime, TimeSpan endTime)
        {
            PatientId = patientId;
            EquipmentId = equipmentId;
            ReferenceCode = referenceCode;
            AppointmentDate = appointmentDate;
            StartTime = startTime;
            EndTime = endTime;
            IsConfirmationEmailSent = false;
            IsCancelled = false;
            CreatedOn = DateTime.Now;
            LastModified = null;
        }
        #endregion
    }
}
