using Appointments.Domain.BusinessEntities.Patients.Models;
using MediatR;
using System;

namespace Appointments.Domain.BusinessEntities.Patients.Commands
{
    /// <summary>
    /// Register Patient Command
    /// </summary>
    public sealed class RegisterPatientCommand : IRequest<RegisteredPatientDto>
    {
        /// <summary>
        /// FirstName
        /// </summary>
        public string FirstName { get; }

        /// <summary>
        /// Last Name
        /// </summary>
        public string LastName { get; }

        /// <summary>
        /// Date Of Birth
        /// </summary>
        public DateTime DateOfBirth { get; }

        /// <summary>
        /// Email Address
        /// </summary>
        public string EmailAddress { get; }

        /// <summary>
        /// Telephone Number
        /// </summary>
        public string TelephoneNumber { get; }

        /// <summary>
        /// Address
        /// </summary>
        public string Address { get; }

        /// <summary>
        /// PostCode
        /// </summary>
        public string PostCode { get; }

        /// <summary>
        /// Register Patient Command
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="dateOfBirth"></param>
        /// <param name="emailAddress"></param>
        /// <param name="telephoneNumber"></param>
        /// <param name="address"></param>
        /// <param name="postCode"></param>
        public RegisterPatientCommand(string firstName, string lastName, DateTime dateOfBirth, string emailAddress, string telephoneNumber, string address, string postCode)
        {
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
            EmailAddress = emailAddress;
            TelephoneNumber = telephoneNumber;
            Address = address;
            PostCode = postCode;
        }
    }
}
