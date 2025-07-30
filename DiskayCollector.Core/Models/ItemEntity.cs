namespace DiskayCollector.Core.Models;

public class ItemEntity {
    public ItemEntity(string name, string? description, string roomName, List<SubGroupItemEntity> subGroupsItems, TimeOnly startTime, TimeOnly endTime) {
        Name = name;
        Description = string.IsNullOrEmpty(description) ? "" : description;
        RoomName = roomName;
        SubGroupsItems = subGroupsItems;
        StartTime = startTime;
        EndTime = endTime;
    }
    
    public string Name { get; set; }
    public string Description { get; set; }
    public string RoomName { get; set; }
    public List<SubGroupItemEntity>? SubGroupsItems { get; set; }
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
}