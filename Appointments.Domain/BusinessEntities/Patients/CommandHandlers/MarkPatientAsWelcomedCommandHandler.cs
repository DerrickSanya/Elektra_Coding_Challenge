using Appointments.Domain.BusinessEntities.Patients.Commands;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Appointments.Domain.BusinessEntities.Patients.CommandHandlers
{
    /// <summary>
    /// Mark Patient As Welcomed CommandHandler
    /// </summary>
    public sealed class MarkPatientAsWelcomedCommandHandler : IRequestHandler<MarkPatientAsWelcomedCommand>
    {
        /// <summary>
        /// _patientRepository
        /// </summary>
        private readonly IPatientRepository _patientRepository;

        /// <summary>
        /// Mark Patient As Welcomed CommandHandler
        /// </summary>
        public MarkPatientAsWelcomedCommandHandler(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }

        /// <summary>
        /// Handle Command
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<Unit> Handle(MarkPatientAsWelcomedCommand request, CancellationToken cancellationToken)
        {
            var patient = await _patientRepository.GetByIdAsync(request.PatientId);
            patient.IsWelcomeEmailSent = true;
            await _patientRepository.UpdatePatientAsync(patient);
            return await Unit.Task;
        }
    }
}
