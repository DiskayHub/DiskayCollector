namespace DiskayCollector.ScheduleService.Contracts.Other;

public record TimePeriod(
    DateOnly Start,
    DateOnly End
);