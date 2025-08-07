using DiskayCollector.Core.Models;

namespace DiskayCollector.Core.Repositories;

public interface IDayScheduleRepository {
    public Task<Guid> Create(DayScheduleEntity dayScheduleEntity);
    public Task<Guid> Update(Guid id, List<ItemEntity> items);
    public Task<List<DayScheduleEntity>> GetAll();
    Task<DayScheduleEntity?> GetScheduleByDate(DateOnly date, string group_name);
    Task<DayScheduleEntity?> GetScheduleByFilter(DateOnly date, string group_name, List<string?> subGroups);
    public Task<Guid> Delete(Guid id);
}