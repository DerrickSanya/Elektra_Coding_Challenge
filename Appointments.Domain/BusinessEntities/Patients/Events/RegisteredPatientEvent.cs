using Appointments.Domain.Base;

namespace Appointments.Domain.BusinessEntities.Patients.Events
{
    /// <summary>
    /// Registered Patient Event
    /// </summary>
    public sealed class RegisteredPatientEvent : BaseDomainEvent
    {
        #region Properties
        /// <summary>
        /// PatientId
        /// </summary>
        public int PatientId { get; private set; }

        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// EmailAddress
        /// </summary>
        public string EmailAddress { get; private set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Registered Patient Event
        /// </summary>
        public RegisteredPatientEvent()
        { }
        #endregion

        #region Factory

        /// <summary>
        /// RegisteredPatientEvent
        /// </summary>
        /// <param name="patientId"></param>
        /// <param name="name"></param>
        /// <param name="emailAddress"></param>
        /// <returns></returns>
        public static RegisteredPatientEvent Create(int patientId, string name, string emailAddress)
        {
           return new RegisteredPatientEvent { PatientId = patientId, Name = name, EmailAddress = emailAddress };
        }
        #endregion
    }
}
