using DiskayCollector.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DiskayCollector.Postgres.Configurations;

public class ItemsConfiguration: IEntityTypeConfiguration<ItemEntity> {
    public void Configure(EntityTypeBuilder<ItemEntity> builder) {
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id).ValueGeneratedNever();
        builder.Property(p => p.Name).IsRequired();
        builder.Property(p => p.Description);
        builder.Property(p => p.RoomName);
        builder.Property(p => p.StartTime).IsRequired();
        builder.Property(p => p.EndTime).IsRequired();
        builder
            .HasOne(p => p.DaySchedule)
            .WithMany(d => d.Items)
            .HasForeignKey(k => k.DayScheduleId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}