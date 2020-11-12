using System.Threading;
using System.Threading.Tasks;
using Appointments.Domain.Base.Interfaces;
using Appointments.Domain.BusinessEntities.Appointments.Commands;
using Appointments.Domain.BusinessEntities.Appointments.Events;
using Appointments.Domain.BusinessEntities.Appointments.Rules.Interfaces;
using Appointments.Domain.BusinessEntities.Patients;
using Appointments.Domain.External;
using MediatR;

namespace Appointments.Domain.BusinessEntities.Appointments.CommandHandlers
{
    /// <summary>
    /// Book Appointment CommandHandler
    /// </summary>
    public sealed class BookAppointmentCommandHandler : IRequestHandler<BookAppointmentCommand>
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IBaseRepository<Patient> _repository;
        private readonly IEquipmentService _equipmentService;
        private readonly IAppointmentEquipmentIsAvailableValidator _appointmentEquipmentIsAvailableValidator;       
        private readonly IAppointmentPatientMustExistRuleValidator _appointmentPatientMustExistRuleValidator;
        private readonly IMediator _mediator;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="appointmentRepository"></param>
        /// <param name="appointmentEquipmentIsAvailableValidator"></param>
        /// <param name="appointmentPatientMustExistRuleValidator"></param>
        /// <param name="mediator"></param>
        public BookAppointmentCommandHandler(IAppointmentRepository appointmentRepository, IBaseRepository<Patient> repository, IEquipmentService equipmentService, IAppointmentEquipmentIsAvailableValidator appointmentEquipmentIsAvailableValidator, IAppointmentPatientMustExistRuleValidator appointmentPatientMustExistRuleValidator, IMediator mediator)
        {
            _appointmentRepository = appointmentRepository;
            _repository = repository;
            _equipmentService = equipmentService;
            _appointmentEquipmentIsAvailableValidator = appointmentEquipmentIsAvailableValidator;
            _appointmentPatientMustExistRuleValidator = appointmentPatientMustExistRuleValidator;
            _mediator = mediator;
        }

        /// <summary>
        /// Handle
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<Unit> Handle(BookAppointmentCommand request, CancellationToken cancellationToken)
        {
            var equipmentAvailable = await _equipmentService.GetEquipmentAvailableOnAppointmentDateAsync(request.AppointmentDate, request.StartTime, request.EndTime);
            var equipmentId = equipmentAvailable != null ? equipmentAvailable.EquipmentId : 0;
            var appointment = Appointment.BookAppointment(request.PatientId, equipmentId, request.ReferenceCode, request.AppointmentDate, request.StartTime, request.EndTime, _appointmentEquipmentIsAvailableValidator, _appointmentPatientMustExistRuleValidator);
            await _appointmentRepository.AddAsync(appointment);

            // raise events
            var patient = await _repository.GetByScalarValueAsync(new { Id = request.PatientId});

            // add to outbox for processing
            await _mediator.Publish(AppointmentBookedEvent.Create(appointment.Id, request.ReferenceCode, $"{patient.FirstName} {patient.LastName}", patient.EmailAddress), cancellationToken);
           
            await _equipmentService.MarkAsUnAvailable(equipmentAvailable); // mark item as unavailable item from the cached list
            return Unit.Value; 
        }
    }
}
