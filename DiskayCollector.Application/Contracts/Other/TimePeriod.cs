namespace DiskayCollector.Application.Contracts.Other;

public record TimePeriod(
    DateOnly Start,
    DateOnly End
);