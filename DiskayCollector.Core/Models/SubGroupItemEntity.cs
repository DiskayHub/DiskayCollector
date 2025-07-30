using System.Text.RegularExpressions;

namespace DiskayCollector.Core.Models;

public class SubGroupItemEntity {
    public string Name { get; set; }
    public string RoomName { get; set; }
    public string Description { get; set; }
    public GroupEntity SubGroup { get; set; }

    public SubGroupItemEntity(string name, string roomName, string description, GroupEntity subGroup) {
        Name = name;
        RoomName = roomName;
        Description = description;
        SubGroup = subGroup;
    }
}