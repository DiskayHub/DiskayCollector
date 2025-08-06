namespace DiskayCollector.Core.Models;

public class ItemEntity {
    
    public Guid Id { get; }
    public string Name { get; }
    public string Description { get; }
    public string RoomName { get; }
    public List<SubGroupItemEntity>? SubGroupsItems { get; set;  }
    public TimeOnly StartTime { get; }
    public TimeOnly EndTime { get; }
    
    public Guid DayScheduleId { get;  }
    public DayScheduleEntity DaySchedule { get; }

    public ItemEntity() {}
    
    private ItemEntity(Guid id, string name, string? description, string roomName, 
        List<SubGroupItemEntity> subGroupsItems, TimeOnly startTime, TimeOnly endTime, Guid dayScheduleId) 
    {
        Id = id;
        Name = name;
        Description = string.IsNullOrEmpty(description) ? "" : description;
        RoomName = roomName;
        SubGroupsItems = subGroupsItems;
        StartTime = startTime;
        EndTime = endTime;
        DayScheduleId = dayScheduleId;
    }

    public static ItemEntity Create(string name, string? description, string roomName, List<SubGroupItemEntity> subGroupsItems, 
        TimeOnly startTime, TimeOnly endTime, Guid dayScheduleId) {
        var itemEntity = new ItemEntity(
            id:  Guid.NewGuid(),
            name: name,
            description: description,
            roomName: roomName,
            subGroupsItems: subGroupsItems,
            startTime: startTime,
            endTime: endTime,
            dayScheduleId: dayScheduleId
        );
        return itemEntity;
    }
}