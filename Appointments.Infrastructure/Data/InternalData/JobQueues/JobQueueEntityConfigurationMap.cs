using Appointments.Domain.Audit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Appointments.Infrastructure.Data.InternalData.JobQueues
{
    /// <summary>
    /// Job Queue Entity Configuration Map
    /// </summary>
    public sealed class JobQueueEntityConfigurationMap : IEntityTypeConfiguration<JobQueue>
    {
        /// <summary>
        /// Configure
        /// </summary>
        /// <param name="entity"></param>
        public void Configure(EntityTypeBuilder<JobQueue> entity)
        {
            entity.ToTable("JobQueues", "app");

            entity.HasKey(b => b.Id);
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CommandType).HasMaxLength(255).IsRequired();
            entity.Property(e => e.CommandData).IsRequired();
            entity.Property(e => e.ProcessedDate);
        }
    }
}