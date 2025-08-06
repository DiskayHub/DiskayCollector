using DiskayCollector.Core.Models;

namespace DiskayCollector.Core.Repositories;

public interface IDayScheduleRepository {
    public Task<Guid> Create(DayScheduleEntity dayScheduleEntity);
    public Task<Guid> Update(Guid id, List<ItemEntity> items);
    public Task<Guid> Delete(Guid id);
}