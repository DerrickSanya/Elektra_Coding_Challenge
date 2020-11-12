using Appointments.Domain.Base.Interfaces;
using Appointments.Domain.BusinessEntities.Appointments.Commands;
using Appointments.Domain.BusinessEntities.Appointments.Events;
using Appointments.Domain.BusinessEntities.Appointments.Rules.Interfaces;
using Appointments.Domain.BusinessEntities.Patients;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Appointments.Domain.BusinessEntities.Appointments.CommandHandlers
{
    /// <summary>
    /// Reschedule Appointment Command Handler
    /// </summary>
    public sealed class RescheduleAppointmentCommandHandler : IRequestHandler<RescheduleAppointmentCommand>
    {
        private readonly IAppointmentRepository _appointmentWriteRepository;
        private readonly IBaseRepository<Appointment> _appointmentRepository;
        private readonly IBaseRepository<Patient> _patientsRepository;
        private readonly IAppointmentMustExistValidator _appointmentMustExistValidator;
        private readonly IAppointmentEquipmentIsAvailableValidator _appointmentEquipmentIsAvailableValidator;
        private readonly IMediator _mediator;

        /// <summary>
        /// Reschedule Appointment Command Handler
        /// </summary>
        /// <param name="appointmentRepository"></param>
        /// <param name="patientsRepository"></param>
        /// <param name="appointmentMustExistValidator"></param>
        /// <param name="appointmentEquipmentIsAvailableValidator"></param>
        /// <param name="mediator"></param>
        public RescheduleAppointmentCommandHandler(IAppointmentRepository appointmentWriteRepository, IBaseRepository<Appointment> appointmentRepository, IBaseRepository<Patient> patientsRepository, IAppointmentMustExistValidator appointmentMustExistValidator, IAppointmentEquipmentIsAvailableValidator appointmentEquipmentIsAvailableValidator, IMediator mediator)
        {
            _appointmentWriteRepository = appointmentWriteRepository;
            _appointmentRepository = appointmentRepository;
            _patientsRepository = patientsRepository;
            _appointmentMustExistValidator = appointmentMustExistValidator;
            _appointmentEquipmentIsAvailableValidator = appointmentEquipmentIsAvailableValidator;
            _mediator = mediator;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<Unit> Handle(RescheduleAppointmentCommand request, CancellationToken cancellationToken)
        {
            //  get appointment
            var appointment = await _appointmentRepository.GetByScalarValueAsync(new { PatientId = request.PatientId, AppointmentDate = request.CurrentAppointmentDate, StartTime = request.CurrentAppointmentStartTime, EndTime = request.CurrentAppointmentEndTime });
              
            // validate request
            Appointment.UpdateAppointment(appointment.Id, request.CurrentAppointmentDate, request.NewAppointmentDate, request.NewAppointmentStartTime, request.NewAppointmentEndTime, _appointmentMustExistValidator, _appointmentEquipmentIsAvailableValidator);
           
            // if no exception thrown then we can update the appointment entity in the db
            appointment.AppointmentDate = request.NewAppointmentDate;
            appointment.StartTime = request.NewAppointmentStartTime;
            appointment.EndTime = request.NewAppointmentEndTime;
            appointment.LastModified = DateTime.Now;
            await _appointmentWriteRepository.UpdateAppointmentAsync(appointment);

            // add to outbox for later processing
            await _mediator.Publish(AppointmentRescheduledEvent.Create(appointment.Id, appointment.ReferenceCode, request.CurrentAppointmentDate, request.CurrentAppointmentStartTime, request.CurrentAppointmentEndTime, request.NewAppointmentDate, request.NewAppointmentStartTime, request.NewAppointmentEndTime), cancellationToken);
            return Unit.Value;
        }
    }
}
