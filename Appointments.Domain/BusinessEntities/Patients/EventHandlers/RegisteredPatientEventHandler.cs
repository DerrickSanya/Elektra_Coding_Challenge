using Appointments.Domain.Audit;
using Appointments.Domain.Base.Interfaces;
using Appointments.Domain.BusinessEntities.Patients.Events;
using MediatR;
using Newtonsoft.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Appointments.Domain.BusinessEntities.Patients.EventHandlers
{
    /// <summary>
    /// Registered Patient Event Handler
    /// </summary>
    public sealed class RegisteredPatientEventHandler : INotificationHandler<RegisteredPatientEvent>
    {
        /// <summary>
        /// _patientsRepository
        /// </summary>
        private readonly IBaseRepository<NotificationQueue> _notificationQueueRepository;

        /// <summary>
        /// Registered Patient EventHandler
        /// </summary>
        /// <param name="notificationQueueRepository"></param>
        public RegisteredPatientEventHandler(IBaseRepository<NotificationQueue> notificationQueueRepository)
        {
            _notificationQueueRepository = notificationQueueRepository;
        }

        /// <summary>
        ///  Add to the notifications queue to process later with back ground process.
        /// </summary>
        /// <param name="notification"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task Handle(RegisteredPatientEvent notification, CancellationToken cancellationToken)
        {
            var notificationToAdd = new NotificationQueue(notification.CreatedOn, notification.GetType().FullName, JsonConvert.SerializeObject(notification));
            // await _notificationQueueRepository.InsertAsync(notificationToAdd);
            // dummy for now
            await Task.CompletedTask;
        }
    }
}
