using DiskayCollector.Core.Models;

namespace DiskayCollector.Application.Interfaces;

public interface IScheduleService {
    public Task<DayScheduleEntity>? GetDaySchedule(DateOnly date, string group);
    public Task<List<DayScheduleEntity>> GetWeekSchedule(DateOnly date, string group);
}