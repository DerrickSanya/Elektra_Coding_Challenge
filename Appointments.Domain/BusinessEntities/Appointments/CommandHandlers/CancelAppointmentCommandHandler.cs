using Appointments.Domain.Base.Interfaces;
using Appointments.Domain.BusinessEntities.Appointments.Commands;
using Appointments.Domain.BusinessEntities.Appointments.Events;
using Appointments.Domain.BusinessEntities.Appointments.Rules.Interfaces;
using Appointments.Domain.BusinessEntities.Patients;
using Appointments.Domain.External;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Appointments.Domain.BusinessEntities.Appointments.CommandHandlers
{
    /// <summary>
    /// Cancel Appointment Command Handler
    /// </summary>
    public sealed class CancelAppointmentCommandHandler : IRequestHandler<CancelAppointmentCommand>
    {
        private readonly IAppointmentRepository _appointmentWriteRepository;
        private readonly IBaseRepository<Appointment> _appointmentRepository;
        private readonly IBaseRepository<Patient> _patientsRepository;
        private readonly IAppointmentMustExistValidator _appointmentMustExistValidator;
        private readonly IEquipmentService _equipmentService;
        private readonly IMediator _mediator;

        /// <summary>
        /// Cancel Appointment Command Handler
        /// </summary>
        /// <param name="appointmentRepository"></param>
        /// <param name="appointmentMustExistValidator"></param>
        /// <param name="mediator"></param>
        public CancelAppointmentCommandHandler(IAppointmentRepository appointmentWriteRepository, IBaseRepository<Appointment> appointmentRepository, IBaseRepository<Patient> patientsRepository, IAppointmentMustExistValidator appointmentMustExistValidator, IEquipmentService equipmentService, IMediator mediator)
        {
            _appointmentWriteRepository = appointmentWriteRepository;
            _appointmentRepository = appointmentRepository;
            _patientsRepository = patientsRepository;
            _appointmentMustExistValidator = appointmentMustExistValidator;
            _equipmentService = equipmentService;
            _mediator = mediator;
        }

        /// <summary>
        /// Handle Request
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<Unit> Handle(CancelAppointmentCommand request, CancellationToken cancellationToken)
        {
            var appointment = await _appointmentRepository.GetByScalarValueAsync(new { PatientId = request.PatientId, AppointmentDate = request.AppointmentDate, StartTime = request.AppointmentStartTime, EndTime= request.AppointmentEndTime });            
            Appointment.CancelAppointment(appointment.Id, appointment.AppointmentDate, _appointmentMustExistValidator);
            var patient = await _patientsRepository.GetByScalarValueAsync(new { Id = request.PatientId });

            // update entity
            appointment.IsCancelled = true;
            await _appointmentWriteRepository.UpdateAppointmentAsync(appointment);

            // add to outbox for later processing
            await _mediator.Publish(AppointmentCancelledEvent.Create(appointment.Id, appointment.ReferenceCode, $"{patient.FirstName} {patient.LastName}", patient.EmailAddress), cancellationToken);
            //var equipment = _equipmentService.GetEquipment(appointment.EquipmentId, new DateTime(),  )
            //await _equipmentService.MarkAsUnAvailable(equipmentAvailable); // mark item as unavailable item from the cached list
            return Unit.Value;
        }
    }
}
