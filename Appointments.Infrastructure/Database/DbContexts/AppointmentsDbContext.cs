using Appointments.Domain.BusinessEntities.Appointments;
using Appointments.Domain.BusinessEntities.Patients;
using Appointments.Infrastructure.Data.DomainData.Appointments;
using Appointments.Infrastructure.Data.DomainData.Patients;
using Appointments.Infrastructure.Data.InternalData.JobQueues;
using Appointments.Infrastructure.Data.InternalData.NotificationQueues;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Appointments.Infrastructure.Database.DbContexts
{
    /// <summary>
    /// Appointments DbContext
    /// </summary>
    public class AppointmentsDbContext : DbContext
    {
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
       // public DbSet<NotificationQueue> NotificationQueues { get; set; }
       // public DbSet<JobQueue> JobQueues { get; set; }

        /// <summary>
        /// Appointments DbContext
        /// </summary>
        /// <param name="options"></param>
        public AppointmentsDbContext(DbContextOptions options) : base(options)
        {
        }

        /// <summary>
        /// OnModelCreating
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            RemoveConventions(modelBuilder);
           // modelBuilder.ApplyConfiguration(new JobQueueEntityConfigurationMap());
           // modelBuilder.ApplyConfiguration(new NotificationQueueEntityConfigurationMap());
            modelBuilder.ApplyConfiguration(new PatientEntityConfigurationMap());
            modelBuilder.ApplyConfiguration(new AppointmentEntityConfigurationMap());
            base.OnModelCreating(modelBuilder);
        }
        private void RemoveConventions(ModelBuilder modelBuilder)
        {
            #region Remove convention cascade delete
            var cascadeFKs = modelBuilder.Model.GetEntityTypes().SelectMany(t => t.GetForeignKeys()).Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);
            foreach (var fk in cascadeFKs)
                fk.DeleteBehavior = DeleteBehavior.Restrict;
            #endregion
        }
    }
}
