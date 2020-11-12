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
    /// UpdateAppointmentTests
    /// </summary>
    [TestClass]
    public class UpdateAppointmentTests : BaseTest
    {
        /// <summary>
        /// AppointmenUpdateIsSuccessful
        /// </summary>
        [TestMethod]
        public void AppointmenUpdateIsSuccessful()
        {
            // Arrange
            var appointmentId = 1;
            var currentAppointmentDate = DateTime.Now.AddDays(5);
            var newAppointmentDate = DateTime.Now.AddDays(2); 
            var newAppointmentStartTime = new TimeSpan(10, 00, 00);
            var newAppointmentEndTime = new TimeSpan(11, 00, 00);

            var appointmentMustExistValidator = Substitute.For<IAppointmentMustExistValidator>();
            var equipmentIsAvailableValidator = Substitute.For<IAppointmentEquipmentIsAvailableValidator>();
            appointmentMustExistValidator.AppointmentExists(appointmentId).Returns(true);
            equipmentIsAvailableValidator.IsEquipmentAvailable(newAppointmentDate, newAppointmentStartTime, newAppointmentEndTime).Returns(true);

            // act
            Appointment.UpdateAppointment(appointmentId, currentAppointmentDate, newAppointmentDate, newAppointmentStartTime, newAppointmentEndTime, appointmentMustExistValidator, equipmentIsAvailableValidator);

            // Assert
            // Assert
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void AppointmenUpdateIsNotSuccessful_AppointmentMustExistRuleIsBroken()
        {
            // Arrange
            var appointmentId = 1;
            var currentAppointmentDate = DateTime.Now.AddDays(5);
            var newAppointmentDate = DateTime.Now.AddDays(3);
            var newAppointmentStartTime = new TimeSpan(10, 00, 00);
            var newAppointmentEndTime = new TimeSpan(11, 00, 00);

            var appointmentMustExistValidator = Substitute.For<IAppointmentMustExistValidator>();
            var equipmentIsAvailableValidator = Substitute.For<IAppointmentEquipmentIsAvailableValidator>();
            appointmentMustExistValidator.AppointmentExists(appointmentId).Returns(false);
            equipmentIsAvailableValidator.IsEquipmentAvailable(newAppointmentDate, newAppointmentStartTime, newAppointmentEndTime).Returns(true);

            //Assert
            AssertBrokenRule<AppointmentMustExistRule>(() =>
            {
                // Act
                Appointment.UpdateAppointment(appointmentId, currentAppointmentDate, newAppointmentDate, newAppointmentStartTime, newAppointmentEndTime, appointmentMustExistValidator, equipmentIsAvailableValidator);
            });
        }

        /// <summary>
        /// AppointmenUpdateIsNotSuccessful_AppointmentChangeIsGreaterThan2DaysPriorToDateRuleIsBroken
        /// </summary>
        [TestMethod]
        public void AppointmenUpdateIsNotSuccessful_AppointmentChangeIsGreaterThan2DaysPriorToDateRuleIsBroken()
        {
            // Arrange
            var appointmentId = 1;
            var currentAppointmentDate = DateTime.Now;
            var newAppointmentDate = DateTime.Now.AddDays(1);
            var newAppointmentStartTime = new TimeSpan(10, 00, 00);
            var newAppointmentEndTime = new TimeSpan(11, 00, 00);

            var appointmentMustExistValidator = Substitute.For<IAppointmentMustExistValidator>();
            var equipmentIsAvailableValidator = Substitute.For<IAppointmentEquipmentIsAvailableValidator>();
            appointmentMustExistValidator.AppointmentExists(appointmentId).Returns(true);
            equipmentIsAvailableValidator.IsEquipmentAvailable(newAppointmentDate, newAppointmentStartTime, newAppointmentEndTime).Returns(true);

            //Assert
            AssertBrokenRule<AppointmentChangeIsLessThan2DaysPriorToDateRule>(() =>
            {
                // Act
                Appointment.UpdateAppointment(appointmentId, currentAppointmentDate, newAppointmentDate, newAppointmentStartTime, newAppointmentEndTime, appointmentMustExistValidator, equipmentIsAvailableValidator);
            });
        }
    }
}