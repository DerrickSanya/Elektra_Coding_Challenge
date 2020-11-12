using Appointments.Utilities.SqlGenerator.Attributes;
using System;

namespace Appointments.Domain.BusinessEntities.Appointments
{
    [Scheme("patient"), StoredAs("vAppointmentDetails")]
    public class AppointmentDetail
    {
        public int Id { get; set; }
        public string ReferenceCode { get; set; }
        public string PatientName { get; set; }
        public DateTime AppointmentDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public DateTime CreatedOn { get; set; }
    }  
}
