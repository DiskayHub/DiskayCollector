namespace DiskayCollector.Core.Models;

public class DayScheduleEntity {
    public DateOnly Day { get; set; }
    public GroupEntity MainGroup { get; set; }
    public List<ItemEntity> Items { get; set; }

    public DayScheduleEntity(DateOnly day, GroupEntity mainGroup, List<ItemEntity> items) {
        Day = day;
        MainGroup = mainGroup;
        Items = items;
    }
}