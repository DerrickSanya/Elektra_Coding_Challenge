using Appointments.Domain.Audit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Appointments.Infrastructure.Data.InternalData.NotificationQueues
{
    /// <summary>
    /// Notification Queue Entity Configuration Map
    /// </summary>
    public sealed class NotificationQueueEntityConfigurationMap : IEntityTypeConfiguration<NotificationQueue>
    {
        /// <summary>
        /// Configure
        /// </summary>
        /// <param name="entity"></param>
        public void Configure(EntityTypeBuilder<NotificationQueue> entity)
        {
            entity.ToTable("NotificationQueues", "app");

            entity.HasKey(b => b.Id);
            entity.Property(b => b.Id).ValueGeneratedNever();
            entity.Property(e => e.OccurredOn).IsRequired();
            entity.Property(e => e.MessageType).HasMaxLength(250).IsRequired();
            entity.Property(e => e.MessageData).IsRequired();
            entity.Property(e => e.ProcessedDate);
        }
    }
}