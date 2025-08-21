namespace DiskayCollector.API.Contracts.DaySchedule.Get;

public record GetScheduleFilter(
    DateOnly date,
    string group_name,
    string? MainSubGroup, 
    string? ProfileSubGroup, 
    string? EnglishSubGroup
);