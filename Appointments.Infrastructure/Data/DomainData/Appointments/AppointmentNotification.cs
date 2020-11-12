namespace Appointments.Infrastructure.Data.DomainData.Appointments
{
    using System;
    /// <summary>
    /// AppointmentNotification
    /// </summary>
    public class AppointmentNotification
    {
        public int AppointmentId { get; set; }
        public string ReferenceCode { get; set; } 
        public string Name { get; set; }
        public string EmailAddress { get; set; }
        public DateTime  CreatedOn { get; set; }
    }
}
