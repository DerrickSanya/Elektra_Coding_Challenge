using Appointments.Application.Patients.Models;
using Appointments.Application.Services.Interfaces;
using Appointments.Domain.BusinessEntities.Patients.Commands;
using Appointments.Domain.BusinessEntities.Patients.Models;
using MediatR;
using System.Threading.Tasks;

namespace Appointments.Application.Services
{
    /// <summary>
    /// Patient Service
    /// </summary>
    public sealed class PatientService : IPatientService
    {
        /// <summary>
        /// _mediator
        /// </summary>
        private readonly IMediator _mediator;

        /// <summary>
        /// Patient Service
        /// </summary>
        /// <param name="mediator"></param>
        public PatientService(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Register
        /// </summary>
        /// <param name="patient"></param>
        /// <returns></returns>
        public async Task<RegisteredPatientDto> Register(RegisterPatientDto patient)
        {
           return await _mediator.Send(new RegisterPatientCommand(patient.FirstName, patient.LastName, patient.DateOfBirth, patient.EmailAddress, patient.TelephoneNumber, patient.Address, patient.PostCode));
        }
    }
}
