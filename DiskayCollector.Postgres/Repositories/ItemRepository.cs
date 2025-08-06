using DiskayCollector.Core.Models;
using DiskayCollector.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DiskayCollector.Postgres.Repositories;

public class ItemRepository : IItemRepository {
    private readonly DayItemsDbContext _database;

    public ItemRepository(DayItemsDbContext database) {
        _database = database;
    }
    
    public async Task<Guid> Create(ItemEntity item) {
        await _database.Items.AddAsync(item);
        await _database.SaveChangesAsync();
        return item.Id;
    }

    public async Task<Guid> Update(Guid id, string name, string? description, string roomName, List<SubGroupItemEntity> subGroupsItems, TimeOnly startTime,
        TimeOnly endTime, Guid dayScheduleId) {
        await _database.Items
            .Where(item => item.Id == id)
            .ExecuteUpdateAsync(s => s
                .SetProperty(p => p.Name, name)
                .SetProperty(p => p.Description, description)
                .SetProperty(p => p.RoomName, roomName)
                .SetProperty(p => p.SubGroupsItems, subGroupsItems)
                .SetProperty(p => p.StartTime, startTime)
                .SetProperty(p => p.EndTime, endTime)
                .SetProperty(p => p.DayScheduleId, dayScheduleId)
            );
        await _database.SaveChangesAsync();
        return id;
    }
}