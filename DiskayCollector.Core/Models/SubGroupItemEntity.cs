using System.Text.RegularExpressions;

namespace DiskayCollector.Core.Models;

public class SubGroupItemEntity {
    
    public Guid Id { get; }
    public string Name { get; }
    public string RoomName { get; }
    public string Description { get; }
    public string SubGroup { get; }
    
    public Guid ItemId { get; }
    public ItemEntity Item { get; }

    public SubGroupItemEntity() {}

    private SubGroupItemEntity(Guid id, string name, string roomName, string description, string subGroup, Guid itemId) {
        Id = id;
        Name = name;
        RoomName = roomName;
        Description = description;
        SubGroup = subGroup;
        ItemId = itemId;
    }

    public static SubGroupItemEntity Create(string name, string roomName, string description, string subGroup, Guid itemId) {
        var subGroupItemEntity = new SubGroupItemEntity(
            id: Guid.NewGuid(), 
            name: name,
            roomName: roomName,
            description: description,
            subGroup: subGroup,
            itemId: itemId
        );
        return subGroupItemEntity;
    }
}