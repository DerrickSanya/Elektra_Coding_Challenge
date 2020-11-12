using MediatR;
using Newtonsoft.Json;
using System.Threading;
using System.Threading.Tasks;
using Appointments.Domain.Audit;
using Appointments.Domain.Base.Interfaces;
using Appointments.Domain.BusinessEntities.Appointments.Events;
namespace Appointments.Domain.BusinessEntities.Appointments.EventHandlers
{
    /// <summary>
    /// Appointment Rescheduled Event Handler
    /// </summary>
    public sealed class AppointmentRescheduledEventHandler : INotificationHandler<AppointmentRescheduledEvent>
    {
        /// <summary>
        /// _patientsRepository
        /// </summary>
        private readonly IBaseRepository<NotificationQueue> _notificationQueueRepository;

        /// <summary>
        /// AppointmentRescheduledEventHandler
        /// </summary>
        /// <param name="notificationQueueRepository"></param>
        public AppointmentRescheduledEventHandler(IBaseRepository<NotificationQueue> notificationQueueRepository)
        {
            _notificationQueueRepository = notificationQueueRepository;
        }

        /// <summary>
        ///  Add to the notifications queue to process later with back ground process.
        /// </summary>
        /// <param name="notification"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task Handle(AppointmentRescheduledEvent notification, CancellationToken cancellationToken)
        {
            var notificationToAdd = new NotificationQueue(notification.CreatedOn, notification.GetType().FullName, JsonConvert.SerializeObject(notification));
            await _notificationQueueRepository.InsertAsync(notificationToAdd);
        }
    }
}
