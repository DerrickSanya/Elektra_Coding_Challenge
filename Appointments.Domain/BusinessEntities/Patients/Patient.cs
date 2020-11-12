using System;
using System.Collections.Generic;
using Appointments.Domain.Base;
using Appointments.Domain.Base.Interfaces;
using Appointments.Domain.BusinessEntities.Appointments;
using Appointments.Domain.BusinessEntities.Patients.Rules;
using Appointments.Utilities.SqlGenerator.Attributes;

namespace Appointments.Domain.BusinessEntities.Patients
{
    /// <summary>
    /// Patient
    /// </summary>
    [Scheme("patient"), StoredAs("Patients")]
    public class Patient : BaseDomainEntity, IAggregateRoot
    {
        /// <summary>
        /// Patient default .ctor
        /// </summary>
        public Patient()
        {
            Appointments = new List<Appointment>();
        }

        #region Properties
        /// <summary>
        /// Identifier field
        /// </summary>
        [KeyProperty(Identity = true)]
        public int Id { get; set; }

        /// <summary>
        /// FirstName
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Last Name
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Date Of Birth
        /// </summary>
        public DateTime DateOfBirth { get; set; }

        /// <summary>
        /// Email Address
        /// </summary>
        public string EmailAddress { get; set; }

        /// <summary>
        /// Telephone Number
        /// </summary>
        public string TelephoneNumber { get; set; }

        /// <summary>
        /// Address
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// PostCode
        /// </summary>
        public string PostCode { get; set; }

        /// <summary>
        /// IsWelcomeEmailSent
        /// </summary>
        public bool IsWelcomeEmailSent { get; set; }

        /// <summary>
        /// RegistrationDate
        /// </summary>
        public DateTime RegistrationDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [NonStored]
        public virtual ICollection<Appointment> Appointments { get; set; }
        #endregion

        #region Factory Methods
        /// <summary>
        /// Register Patient
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="dateOfBirth"></param>
        /// <param name="emailAddress"></param>
        /// <param name="telephoneNumber"></param>
        /// <param name="address"></param>
        /// <param name="postCode"></param>
        /// <param name="patientUniqueChecker"></param>
        /// <returns></returns>
        public static Patient RegisterPatient(IPatientEmailMustBeUniqueRuleValidator patientEmailMustBeUniqueRuleValidator, string firstName, string lastName, DateTime dateOfBirth, string emailAddress, string telephoneNumber, string address, string postCode)
        {
            // validate the business rule
            ValidateBusinessRule(new PatientEmailMustBeUniqueRule(patientEmailMustBeUniqueRuleValidator, emailAddress));

            return new Patient(firstName, lastName, dateOfBirth, emailAddress, telephoneNumber, address, postCode);
        }

        /// <summary>
        /// MarkWelcomeEmailSent
        /// </summary>
        public void MarkWelcomeEmailSent()
        {
            IsWelcomeEmailSent = true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="dateOfBirth"></param>
        /// <param name="emailAddress"></param>
        /// <param name="telephoneNumber"></param>
        /// <param name="address"></param>
        /// <param name="postCode"></param>
        private Patient(string firstName, string lastName, DateTime dateOfBirth, string emailAddress, string telephoneNumber, string address, string postCode)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.DateOfBirth = dateOfBirth;
            this.EmailAddress = emailAddress;
            this.TelephoneNumber = telephoneNumber;
            this.Address = address;
            this.PostCode = postCode;
            this.IsWelcomeEmailSent = false;
            this.RegistrationDate = DateTime.Now;
           // this.AddDomainEvent(new PatientRegisteredEvent(this.Id));
        }
        #endregion
    }
}
