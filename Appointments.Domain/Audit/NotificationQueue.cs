using Appointments.Utilities.SqlGenerator.Attributes;
using System;

namespace Appointments.Domain.Audit
{
    [Scheme("app"), StoredAs("NotificationQueues")]
    public class NotificationQueue
    {
        #region Properties
        /// <summary>
        /// Identifier
        /// </summary>
        [KeyProperty(Identity = false)]
        public Guid Id { get; set; }

        /// <summary>
        /// OccurredOn
        /// </summary>
        public DateTime OccurredOn { get; set; }

        /// <summary>
        /// Message Type
        /// </summary>
        public string MessageType { get; set; }

        /// <summary>
        /// Message Data
        /// </summary>
        public string MessageData { get; set; }

        /// <summary>
        /// Processed Date
        /// </summary>
        public DateTime? ProcessedDate { get; set; }
        #endregion

        #region Contructor
        /// <summary>
        /// NotificationQueue
        /// </summary>
        public NotificationQueue()
        { }

        /// <summary>
        /// NotificationQueue
        /// </summary>
        /// <param name="occurredOn"></param>
        /// <param name="messageType"></param>
        /// <param name="messageData"></param>
        public NotificationQueue(DateTime occurredOn, string messageType, string messageData)
        {
            this.Id = Guid.NewGuid();
            this.OccurredOn = occurredOn;
            this.MessageType = messageType;
            this.MessageData = messageData;
        }
        #endregion

        #region Factory

        #endregion
    }
}
