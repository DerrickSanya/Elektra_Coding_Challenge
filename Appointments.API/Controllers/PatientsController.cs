using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Appointments.Application.Patients.Models;
using Appointments.Application.Services.Interfaces;
using Appointments.Domain.BusinessEntities.Patients.Models;
using Microsoft.AspNetCore.Mvc;

namespace Appointments.API.Controllers
{
    /// <summary>
    /// PatientsController
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        /// <summary>
        /// _patientsService
        /// </summary>
        private readonly IPatientService _patientsService;

        /// <summary>
        /// Patients Controller
        /// </summary>
        /// <param name="patientsService"></param>
        public PatientsController(IPatientService patientsService)
        {
            _patientsService = patientsService;
        }

        /// <summary>
        /// Register Patient
        /// </summary>
        /// <param name="patient"></param>
        /// <returns></returns>
        [HttpPost("register")]
        [ProducesResponseType(typeof(RegisteredPatientDto), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> Post(RegisterPatientDto patient)
        {
            var registeredPatient = await _patientsService.Register(patient);
            return Created(string.Empty, registeredPatient);
        }       
    }
}
