using Appointments.UnitTest.Helpers;
using NUnit.Framework;
using NSubstitute;
using Appointments.Domain.BusinessEntities.Appointments.Rules.Interfaces;
using System;
using Appointments.Domain.BusinessEntities.Appointments;
using Appointments.Domain.BusinessEntities.Appointments.Rules;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Assert = NUnit.Framework.Assert;

namespace Appointments.UnitTest.Appointments
{
    /// <summary>
    /// 
    /// </summary>
    [TestClass]
    public class AppointmentBookingTests : BaseTest
    {
        /// <summary>
        /// AppointmentBookingIsSuccessful
        /// </summary>
        [TestMethod]
        public void AppointmentBookingIsSuccessful()
        {
            // arrange
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

            // act
            var appointment = Appointment.BookAppointment(patientId, equipmentId, referenceCode, appointmentDate, startTime, endTime, equipmentIsAvailableValidator, patientMustExistRuleValidator);

            //Assert
            Assert.IsTrue(appointment != null);
            Assert.IsInstanceOf(typeof(Appointment),  appointment);
        }

        /// <summary>
        /// AppointmentBookingIsNotSuccessful_BookingIsOver2WeeksInAdvance
        /// </summary>
        [TestMethod]
        public void AppointmentBookingIsNotSuccessful_BookingIsOver2WeeksInAdvanceRuleBroken()
        {
            // arrange
            var patientId = 1;
            var equipmentId = 5;
            var referenceCode = "TK23ZF";
            var appointmentDate = DateTime.Now.AddDays(16);
            var startTime = new TimeSpan(09, 00, 00);
            var endTime = new TimeSpan(10, 00, 00);

            var equipmentIsAvailableValidator = Substitute.For<IAppointmentEquipmentIsAvailableValidator>();
            var patientMustExistRuleValidator = Substitute.For<IAppointmentPatientMustExistRuleValidator>();
            equipmentIsAvailableValidator.IsEquipmentAvailable(appointmentDate, startTime, endTime).Returns(true);
            patientMustExistRuleValidator.PatientExists(patientId).Returns(true);

            //Assert
            AssertBrokenRule<AppointmentIsLessThanTwoWeeksInAdvanceRule>(() =>
            {
                // Act
                Appointment.BookAppointment(patientId, equipmentId, referenceCode, appointmentDate, startTime, endTime, equipmentIsAvailableValidator, patientMustExistRuleValidator);
            });
        }

        /// <summary>
        /// AppointmentBookingIsNotSuccessful_PatientDoesNotExist
        /// </summary>
        [TestMethod]
        public void AppointmentBookingIsNotSuccessful_PatientMustExistRuleBroken()
        {
            // arrange
            var patientId = 1;
            var equipmentId = 5;
            var referenceCode = "TK23ZF";
            var appointmentDate = DateTime.Now.AddDays(3);
            var startTime = new TimeSpan(09, 00, 00);
            var endTime = new TimeSpan(10, 00, 00);

            var equipmentIsAvailableValidator = Substitute.For<IAppointmentEquipmentIsAvailableValidator>();
            var patientMustExistRuleValidator = Substitute.For<IAppointmentPatientMustExistRuleValidator>();
            equipmentIsAvailableValidator.IsEquipmentAvailable(appointmentDate, startTime, endTime).Returns(true);
            patientMustExistRuleValidator.PatientExists(patientId).Returns(false);

            //Assert
            AssertBrokenRule<AppointmentPatientMustExistRule>(() =>
            {
                // Act
                Appointment.BookAppointment(patientId, equipmentId, referenceCode, appointmentDate, startTime, endTime, equipmentIsAvailableValidator, patientMustExistRuleValidator);
            });
        }

        /// <summary>
        /// AppointmentBookingIsNotSuccessful_AppointmentBookingTimeSlotRuleBroken
        /// </summary>
        [TestMethod]
        public void AppointmentBookingIsNotSuccessful_AppointmentBookingTimeSlotRuleBroken()
        {
            // arrange
            var patientId = 1;
            var equipmentId = 5;
            var referenceCode = "TK23ZF";
            var appointmentDate = DateTime.Now.AddDays(2);
            var startTime = new TimeSpan(18, 00, 00);
            var endTime = new TimeSpan(19, 00, 00);

            var equipmentIsAvailableValidator = Substitute.For<IAppointmentEquipmentIsAvailableValidator>();
            var patientMustExistRuleValidator = Substitute.For<IAppointmentPatientMustExistRuleValidator>();
            equipmentIsAvailableValidator.IsEquipmentAvailable(appointmentDate, startTime, endTime).Returns(true);
            patientMustExistRuleValidator.PatientExists(patientId).Returns(true);

            //Assert
            AssertBrokenRule<AppointmentBookingTimeSlotRule>(() =>
            {
                // Act
                Appointment.BookAppointment(patientId, equipmentId, referenceCode, appointmentDate, startTime, endTime, equipmentIsAvailableValidator, patientMustExistRuleValidator);
            });
        }

        /// <summary>
        /// AppointmentBookingIsNotSuccessful_AppointmentEquipmentIsAvailableRuleBroken
        /// </summary>
        [TestMethod]
        public void AppointmentBookingIsNotSuccessful_AppointmentEquipmentIsAvailableRuleBroken()
        {
            // arrange
            var patientId = 1;
            var equipmentId = 5;
            var referenceCode = "TK23ZF";
            var appointmentDate = DateTime.Now.AddDays(5);
            var startTime = new TimeSpan(09, 00, 00);
            var endTime = new TimeSpan(10, 00, 00);

            var equipmentIsAvailableValidator = Substitute.For<IAppointmentEquipmentIsAvailableValidator>();
            var patientMustExistRuleValidator = Substitute.For<IAppointmentPatientMustExistRuleValidator>();
            equipmentIsAvailableValidator.IsEquipmentAvailable(appointmentDate, startTime, endTime).Returns(false);
            patientMustExistRuleValidator.PatientExists(patientId).Returns(true);

            //Assert
            AssertBrokenRule<AppointmentEquipmentIsAvailableRule>(() =>
            {
                // Act
                Appointment.BookAppointment(patientId, equipmentId, referenceCode, appointmentDate, startTime, endTime, equipmentIsAvailableValidator, patientMustExistRuleValidator);
            });
        }
    }
}
