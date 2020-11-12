using Appointments.Domain.BusinessEntities.Patients.Commands;
using Appointments.Domain.BusinessEntities.Patients.Events;
using Appointments.Domain.BusinessEntities.Patients.Models;
using Appointments.Domain.BusinessEntities.Patients.Rules;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Appointments.Domain.BusinessEntities.Patients.CommandHandlers
{
    /// <summary>
    /// RegisterPatientCommandHandler
    /// </summary>
    public sealed class RegisterPatientCommandHandler : IRequestHandler<RegisterPatientCommand, RegisteredPatientDto>
    {
        private readonly IPatientRepository _patientRepository;
        private readonly IPatientEmailMustBeUniqueRuleValidator _patientEmailMustBeUniqueRuleValidator;
        private readonly IMediator _mediator;

        /// <summary>
        /// RegisterPatientCommandHandler .ctor
        /// </summary>
        /// <param name="patientRepository"></param>
        /// <param name="patientEmailMustBeUniqueRuleValidator"></param>
        /// <param name="mediator"></param>
        public RegisterPatientCommandHandler(IPatientRepository patientRepository, IPatientEmailMustBeUniqueRuleValidator patientEmailMustBeUniqueRuleValidator, IMediator mediator)
        {
           _patientRepository = patientRepository;
           _patientEmailMustBeUniqueRuleValidator = patientEmailMustBeUniqueRuleValidator;
           _mediator = mediator;
        }

        /// <summary>
        /// Handle Command
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<RegisteredPatientDto> Handle(RegisterPatientCommand request, CancellationToken cancellationToken)
        {
            var patientToRegister = Patient.RegisterPatient(_patientEmailMustBeUniqueRuleValidator, request.FirstName, request.LastName, request.DateOfBirth, request.EmailAddress, request.TelephoneNumber, request.Address, request.PostCode);
            await _patientRepository.AddAsync(patientToRegister);

            // raise other events that use the generated Id
            await _mediator.Publish(RegisteredPatientEvent.Create(patientToRegister.Id, $"{request.FirstName} {request.LastName}", request.EmailAddress), cancellationToken);
            
            //MarkPatientAsWelcomedCommand
            await _mediator.Send(new MarkPatientAsWelcomedCommand(patientToRegister.Id));
            return new RegisteredPatientDto { PatientId = patientToRegister .Id };
        }
    }
}
