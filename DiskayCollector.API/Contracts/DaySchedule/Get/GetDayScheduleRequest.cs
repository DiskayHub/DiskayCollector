namespace DiskayCollector.API.Contracts.DaySchedule.Get;

public record GetDayScheduleRequest(
    DateOnly date,
    string group_name
);