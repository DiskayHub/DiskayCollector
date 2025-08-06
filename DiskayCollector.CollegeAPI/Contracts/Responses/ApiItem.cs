namespace DiskayCollector.Service.Contracts;

public record ApiItem(
    string ClID,
    string Day,
    string group,
    string topic,
    string start,
    string end,
    string room,
    string color,
    string title,
    List<ApiSubGroup>? SubGroup
    );