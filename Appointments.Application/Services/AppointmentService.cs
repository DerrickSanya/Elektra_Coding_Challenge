using Appointments.Application.Services.Interfaces;
using Appointments.Domain.BusinessEntities.Appointments;
using Appointments.Domain.BusinessEntities.Appointments.Commands;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Appointments.Application.Services
{
    /// <summary>
    /// Appointment Service
    /// </summary>
    public class AppointmentService : IAppointmentService
    {
        /// <summary>
        /// _mediator
        /// </summary>
        private readonly IMediator _mediator;

        /// <summary>
        /// _appointmentReadRepository
        /// </summary>
        private IAppointmentReadRepository _appointmentReadRepository;

        /// <summary>
        /// AppointmentService
        /// </summary>
        public AppointmentService(IMediator mediator, IAppointmentReadRepository appointmentReadRepository)
        {
            _mediator = mediator;
            _appointmentReadRepository = appointmentReadRepository;
        }

        /// <summary>
        /// GetAllAppointments
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<AppointmentDetail>> GetAllAppointments()
        {
            return await _appointmentReadRepository.GetAllAppointments();
        }

        /// <summary>
        /// Book Appointment
        /// </summary>
        /// <param name="patientId"></param>
        /// <param name="bookAppointmentRequest"></param>
        /// <returns></returns>
        public async Task BookAppointment(int patientId, DateTime appointmentDate)
        {
            await _mediator.Send(new BookAppointmentCommand(patientId, appointmentDate));
        }

        /// <summary>
        /// Cancel Appointment
        /// </summary>
        /// <param name="patientId"></param>
        /// <param name="appointmentDate"></param>
        /// <returns></returns>
        public async Task CancelAppointment(int patientId, DateTime appointmentDate)
        {
            await _mediator.Send(new CancelAppointmentCommand(patientId, appointmentDate));
        }
       
        /// <summary>
        /// Reschedule Appointment
        /// </summary>
        /// <param name="patientId"></param>
        /// <param name="currentAppointmentDate"></param>
        /// <param name="newAppointmentDate"></param>
        /// <returns></returns>
        public async Task RescheduleAppointment(int patientId, DateTime currentAppointmentDate, DateTime newAppointmentDate)
        {
            await _mediator.Send(new RescheduleAppointmentCommand(patientId, currentAppointmentDate, newAppointmentDate));
        }
    }
}
