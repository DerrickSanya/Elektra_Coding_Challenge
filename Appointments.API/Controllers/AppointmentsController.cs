using System;
using System.Net;
using System.Threading.Tasks;
using Appointments.Application.Appointments;
using Appointments.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Appointments.API.Controllers
{
    /// <summary>
    /// Appointments Controller
    /// </summary>
    [Route("api/patients")]
    [ApiController]
    public class AppointmentsController : ControllerBase
    {
        /// <summary>
        /// _appointmentService
        /// </summary>
        private readonly IAppointmentService _appointmentService;

        /// <summary>
        /// Patients Controller
        /// </summary>
        /// <param name="patientsService"></param>
        public AppointmentsController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Route("appointments")]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var results = await _appointmentService.GetAllAppointments();
            return Ok(results);
        }

        /// <summary>
        /// Book Appointment
        /// </summary>
        /// <param name="patientId"></param>
        /// <param name="bookAppointmentRequest"></param>
        /// <returns></returns>
        [Route("{patientId}/appointments")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<IActionResult> BookAppointment([FromRoute] int patientId, [FromBody] DateTime appointmentDate)
        {
            await _appointmentService.BookAppointment(patientId, appointmentDate);
            return Accepted();
        }

        /// <summary>
        /// Cancel Appointment
        /// </summary>
        /// <param name="patientId"></param>
        /// <param name="appointmentDate"></param>
        /// <returns></returns>
        [Route("{patientId}/appointments")]
        [HttpDelete]
        public async Task<IActionResult> CancelAppointment([FromRoute] int patientId, [FromBody] DateTime appointmentDate)
        {
            await _appointmentService.CancelAppointment(patientId, appointmentDate);
            return Ok();
        }

        /// <summary>
        /// Reschedule Appointment
        /// </summary>
        /// <param name="patientId"></param>
        /// <param name="currentAppointmentDate"></param>
        /// <param name="newAppointmentDate"></param>
        /// <returns></returns>
        [Route("{patientId}/appointments/")]
        [HttpPut]
        public async Task<IActionResult> RescheduleAppointment([FromRoute] int patientId, [FromBody] ChangeAppointmentRequest changeAppointmentRequest)
        {
            await _appointmentService.RescheduleAppointment(patientId, changeAppointmentRequest.CurrentAppointmentDate, changeAppointmentRequest.NewAppointmentDate);
            return Ok();
        }
    }
}