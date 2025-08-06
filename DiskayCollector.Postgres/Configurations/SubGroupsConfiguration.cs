using DiskayCollector.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DiskayCollector.Postgres.Configurations;

public class SubGroupsConfiguration : IEntityTypeConfiguration<SubGroupItemEntity> {
    public void Configure(EntityTypeBuilder<SubGroupItemEntity> builder) {
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id).ValueGeneratedNever();
        builder.Property(p => p.Name).IsRequired();
        builder.Property(p => p.Description);
        builder.Property(p => p.SubGroup).IsRequired();
        builder
            .HasOne(p => p.Item)
            .WithMany(p => p.SubGroupsItems)
            .HasForeignKey(k => k.ItemId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}