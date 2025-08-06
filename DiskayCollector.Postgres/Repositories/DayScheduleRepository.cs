using DiskayCollector.Core.Models;
using DiskayCollector.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DiskayCollector.Postgres.Repositories;

public class DayScheduleRepository : IDayScheduleRepository {
    private readonly DayItemsDbContext _database;

    public DayScheduleRepository(DayItemsDbContext database) {
        _database = database;
    }
    public async Task<Guid> Create(DayScheduleEntity dayScheduleEntity) {
        await _database.DaySchedules.AddAsync(dayScheduleEntity);
        await _database.SaveChangesAsync();
        return dayScheduleEntity.Id;
    }

    public async Task<List<DayScheduleEntity>> GetAll() {
        return await _database.DaySchedules
            .Include(i => i.Items)
            .ToListAsync();
    }

    public async Task<Guid> Update(Guid id, List<ItemEntity> items) {
        await _database.DaySchedules
            .Where(s => s.Id == id)
            .ExecuteUpdateAsync(s =>
                s.SetProperty(p => p.Items, items)
            );
        return id;
    }

    public async Task<Guid> Delete(Guid id) {
        await _database.DaySchedules
            .Where(s => s.Id == id)
            .ExecuteDeleteAsync();
        return id;
    }
}