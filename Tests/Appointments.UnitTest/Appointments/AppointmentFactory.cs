using Appointments.Domain.BusinessEntities.Appointments;
using Appointments.Domain.BusinessEntities.Appointments.Rules.Interfaces;
using NSubstitute;
using System;

namespace Appointments.UnitTest.Appointments
{
    /// <summary>
    /// Appointment Factory
    /// </summary>
    public class AppointmentFactory
    {
        /// <summary>
        /// Appointment
        /// </summary>
        /// <returns></returns>
        public static Appointment Create()
        {            
            var patientId = 1;
            var equipmentId = 5;
            var referenceCode = "TK23ZF";
            var appointmentDate = DateTime.Now.AddDays(1);
            var startTime = new TimeSpan(09, 00, 00);
            var endTime = new TimeSpan(10, 00, 00);

            var equipmentIsAvailableValidator = Substitute.For<IAppointmentEquipmentIsAvailableValidator>();
            var patientMustExistRuleValidator = Substitute.For<IAppointmentPatientMustExistRuleValidator>();
            equipmentIsAvailableValidator.IsEquipmentAvailable(appointmentDate, startTime, endTime).Returns(true);
            patientMustExistRuleValidator.PatientExists(patientId).Returns(true);

            return Appointment.BookAppointment(patientId, equipmentId, referenceCode, appointmentDate, startTime, endTime, equipmentIsAvailableValidator, patientMustExistRuleValidator);
        }
    }
}
