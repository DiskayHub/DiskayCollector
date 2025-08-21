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

    public async Task<DayScheduleEntity?> GetScheduleByDate(DateOnly date, string groupName) {
        return await _database.DaySchedules
            .Include(i => i.Items)
                .ThenInclude(i => i.SubGroupsItems)
            .FirstOrDefaultAsync(i => i.Date == date && i.MainGroup == groupName);
    }

    public async Task<DayScheduleEntity?> GetScheduleByFilter(DateOnly date, string groupName, string? mainSubGroup, string? engGroup, string? profGroup) {
        var result = await _database.DaySchedules
            .Include(s => s.Items)
            .ThenInclude(i => i.SubGroupsItems)
            .FirstOrDefaultAsync(s => s.Date == date && s.MainGroup == groupName);

        if (result == null) return null;

        result.Items = result.Items
            .Where(item =>
            {
                // --- Если подгрупп не было изначально (общий предмет) → оставляем
                if (item.SubGroupsItems == null || item.SubGroupsItems.Count == 0)
                    return true;

                // --- Подгруппы есть → фильтруем
                var filtered = item.SubGroupsItems!
                    .Where(subGroup =>
                        // Английский
                        (subGroup.Name == "АнглЯз" &&
                         (engGroup == null || subGroup.SubGroup == engGroup)) ||

                        // Профильный предмет
                        (subGroup.Name == "ПрофПр" &&
                         (profGroup == null || subGroup.SubGroup == profGroup)) ||

                        // Основные подгруппы
                        ((subGroup.SubGroup == "Подгр1" || subGroup.SubGroup == "Подгр2") &&
                         (mainSubGroup == null || subGroup.SubGroup == mainSubGroup))
                    )
                    .ToList();

                // обновляем список подгрупп у предмета
                item.SubGroupsItems = filtered;

                // --- если ничего не осталось → предмет выкидываем
                return filtered.Any();
            })
            .ToList();

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