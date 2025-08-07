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

    public async Task<DayScheduleEntity?> GetScheduleByDate(DateOnly date, string group_name) {
        return await _database.DaySchedules
            .Include(i => i.Items)
                .ThenInclude(i => i.SubGroupsItems)
            .FirstOrDefaultAsync(i => i.Date == date && i.MainGroup == group_name);
    }

    public async Task<DayScheduleEntity?> GetScheduleByFilter(DateOnly date, string group_name, List<string?> subGroups) {
        var result = await _database.DaySchedules
            .Include(s => s.Items)
            .ThenInclude(i => i.SubGroupsItems)
            .FirstOrDefaultAsync(s => s.Date == date && s.MainGroup == group_name);

        if (result == null) return null;

        if (subGroups.Count != 0){
            result.Items = result.Items
                .Where(item => item.SubGroupsItems.Count == 0 || item.SubGroupsItems.Any(sg => subGroups.Contains(sg.SubGroup)))
                .ToList();

            foreach (var item in result.Items.Where(i => i.SubGroupsItems != null)){
                item.SubGroupsItems = item.SubGroupsItems!
                    .Where(sg => subGroups.Contains(sg.SubGroup))
                    .ToList();
            }
   
        }
        return result;
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