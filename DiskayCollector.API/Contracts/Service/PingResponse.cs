namespace DiskayCollector.API.Contracts.Service;

public record PingResponse (
    string serviceName,
    string serviceStatus,
    string dataBaseStatus
);