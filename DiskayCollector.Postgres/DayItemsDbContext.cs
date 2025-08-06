using DiskayCollector.Core.Models;
using DiskayCollector.Postgres.Configurations;
using Microsoft.EntityFrameworkCore;

namespace DiskayCollector.Postgres;

public class DayItemsDbContext : DbContext {
    public DayItemsDbContext(DbContextOptions<DayItemsDbContext> options) : base(options) {
        
    }
    
    public DbSet<DayScheduleEntity> DaySchedules { get; set; }
    public DbSet<ItemEntity> Items { get; set; }
    public DbSet<SubGroupItemEntity>  SubGroupItems { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        modelBuilder.ApplyConfiguration(new DaySchedulesConfiguration());
        modelBuilder.ApplyConfiguration(new ItemsConfiguration());
        modelBuilder.ApplyConfiguration(new SubGroupsConfiguration());
            
        base.OnModelCreating(modelBuilder);
    }
    
    public async Task<bool> IsActive() {
        try {
            return await this.Database.CanConnectAsync();
        }
        catch {
            return false;
        }
    }
}