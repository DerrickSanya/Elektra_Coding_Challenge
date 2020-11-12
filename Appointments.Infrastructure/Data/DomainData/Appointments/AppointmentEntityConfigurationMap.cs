using Appointments.Domain.BusinessEntities.Appointments;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Appointments.Infrastructure.Data.DomainData.Appointments
{
    /// <summary>
    /// Appointment Entity ConfigurationMap
    /// </summary>
    public sealed class AppointmentEntityConfigurationMap : IEntityTypeConfiguration<Appointment>
    {
        /// <summary>
        /// Configure
        /// </summary>
        /// <param name="entity"></param>
        public void Configure(EntityTypeBuilder<Appointment> entity)
        {
            entity.ToTable("Appointments", "patient");

            entity.HasKey(b => b.Id);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.PatientId).IsRequired();

            entity.Property(e => e.EquipmentId).IsRequired();
            entity.Property(e => e.ReferenceCode).IsRequired();
            entity.Property(e => e.AppointmentDate).IsRequired();
            entity.Property(e => e.StartTime).IsRequired();
            entity.Property(e => e.EndTime).IsRequired();
            entity.Property(e => e.IsConfirmationEmailSent).IsRequired();
            entity.Property(e => e.IsCancelled).IsRequired();
            entity.Property(e => e.LastModified);
            entity.Property(e => e.CreatedOn).IsRequired();

            // Relationships appontment must have patient
            entity.HasOne(p => p.Patient)
                  .WithMany(a => a.Appointments)
                  .HasForeignKey(b => b.PatientId);
        }
    }
}