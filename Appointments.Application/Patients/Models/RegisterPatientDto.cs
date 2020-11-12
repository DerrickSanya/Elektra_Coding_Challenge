namespace Appointments.Application.Patients.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Register Patient Dto
    /// </summary>
    public class RegisterPatientDto
    {
        /// <summary>
        /// FirstName
        /// </summary>
        [Required]
        public string FirstName { get; set; }

        /// <summary>
        /// Last Name
        /// </summary>
        [Required]
        public string LastName { get; set; }

        /// <summary>
        /// Date Of Birth
        /// </summary>
        [Required]
        public DateTime DateOfBirth { get; set; }

        /// <summary>
        /// Email Address
        /// </summary>
        [Required]
        public string EmailAddress { get; set; }

        /// <summary>
        /// Telephone Number
        /// </summary>
        [Required]
        public string TelephoneNumber { get; set; }

        /// <summary>
        /// Address
        /// </summary>
        [Required]
        public string Address { get; set; }

        /// <summary>
        /// PostCode
        /// </summary>
        [Required]
        public string PostCode { get; set; }
    }
}
