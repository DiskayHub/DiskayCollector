using DiskayCollector.Core.Models;

namespace DiskayCollector.Core.Repositories;

public interface IItemRepository {
    Task<Guid> Create(ItemEntity item);
    Task<Guid> Update(Guid id, string name,  string? description, string roomName, List<SubGroupItemEntity> subGroupsItems, TimeOnly startTime, TimeOnly endTime, Guid dayScheduleId);
    Task<Guid> Delete(Guid id);
}