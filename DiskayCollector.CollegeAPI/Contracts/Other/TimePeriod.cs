namespace DiskayCollector.Service.Contracts.Other;

public record TimePeriod(
    DateOnly Start,
    DateOnly End
);