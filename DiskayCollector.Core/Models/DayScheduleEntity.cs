namespace DiskayCollector.Core.Models;

public class DayScheduleEntity {
    public Guid Id { get; }
    public DateOnly Date { get; }
    public string MainGroup { get; }
    public List<ItemEntity> Items { get; }

    public DayScheduleEntity() {}

    private DayScheduleEntity(Guid id, DateOnly date, string mainGroup, List<ItemEntity> items) {
        Id = id;
        Date = date;
        MainGroup = mainGroup;
        Items = items;
    }

    public static DayScheduleEntity Create(DateOnly date, string mainGroup, List<ItemEntity> items) {
        var daySchedule = new DayScheduleEntity(
            id: Guid.NewGuid(),
            date: date,
            mainGroup: mainGroup,
            items: items
        );
        return daySchedule;
    }
}