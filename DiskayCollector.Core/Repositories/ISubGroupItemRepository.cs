using DiskayCollector.Core.Models;

namespace DiskayCollector.Core.Repositories;

public interface ISubGroupItemRepository {
    public Task<Guid> Create(SubGroupItemEntity item);
    public Task<Guid> Update(Guid id, string name, string roomName, string description, string subGroup, Guid itemId);
}