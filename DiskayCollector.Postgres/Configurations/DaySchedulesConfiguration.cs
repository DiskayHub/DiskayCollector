using DiskayCollector.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DiskayCollector.Postgres.Configurations;

public class DaySchedulesConfiguration : IEntityTypeConfiguration<DayScheduleEntity> {
    
    public void Configure(EntityTypeBuilder<DayScheduleEntity> builder) {
        builder.HasKey(k => k.Id);
        builder.Property(p => p.Id).ValueGeneratedNever();
        builder.Property(p => p.MainGroup).IsRequired();
        builder
            .HasMany(p => p.Items)
            .WithOne()
            .HasForeignKey(k => k.DayScheduleId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}