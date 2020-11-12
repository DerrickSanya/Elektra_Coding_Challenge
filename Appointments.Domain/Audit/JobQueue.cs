using Appointments.Utilities.SqlGenerator.Attributes;
using System;

namespace Appointments.Domain.Audit
{
    /// <summary>
    /// JobQueue
    /// </summary>
    [Scheme("app"), StoredAs("JobQueues")]
    public class JobQueue
    {
        #region Properties
        /// <summary>
        /// Id
        /// </summary>
        [KeyProperty(Identity = false)]
        public Guid Id { get; set; }

        /// <summary>
        /// Command Type
        /// </summary>
        public string CommandType { get; set; }

        /// <summary>
        /// Command Data
        /// </summary>
        public string CommandData { get; set; }

        /// <summary>
        /// Processed Date
        /// </summary>
        public DateTime? ProcessedDate { get; set; }
        #endregion

        #region Contructor

        /// <summary>
        /// Job Queue
        /// </summary>
        public JobQueue()
        {
        }
        #endregion

        #region Factory
        /// <summary>
        /// 
        /// </summary>
        /// <param name="messageType"></param>
        /// <param name="messageData"></param>
        /// <returns></returns>
        public static JobQueue AddJob(string messageType, string messageData)
        {
            return new JobQueue { Id = Guid.NewGuid(), CommandType = messageType, CommandData = messageData, ProcessedDate = null };
        }
        #endregion
    }
}
