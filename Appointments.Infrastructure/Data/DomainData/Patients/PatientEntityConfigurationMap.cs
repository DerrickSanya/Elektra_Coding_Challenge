using Appointments.Domain.BusinessEntities.Patients;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Appointments.Infrastructure.Data.DomainData.Patients
{
    /// <summary>
    /// Patient Entity ConfigurationMap
    /// </summary>
    public sealed class PatientEntityConfigurationMap : IEntityTypeConfiguration<Patient>
    {
        /// <summary>
        /// Configure
        /// </summary>
        /// <param name="entity"></param>
        public void Configure(EntityTypeBuilder<Patient> entity)
        {
            entity.ToTable("Patients", "patient");

            entity.HasKey(b => b.Id);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.FirstName).HasMaxLength(150).IsRequired();
            entity.Property(e => e.LastName).HasMaxLength(150).IsRequired();
            entity.Property(e => e.DateOfBirth).IsRequired();
            entity.Property(e => e.EmailAddress).HasMaxLength(25).IsRequired();
            entity.Property(e => e.TelephoneNumber).HasMaxLength(25);
            entity.Property(e => e.Address).HasMaxLength(50);
            entity.Property(e => e.PostCode).HasMaxLength(8);
            entity.Property(e => e.IsWelcomeEmailSent).IsRequired();
            entity.Property(e => e.RegistrationDate).IsRequired();
        }
    }
}