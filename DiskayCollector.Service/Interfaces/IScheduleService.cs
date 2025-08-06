using DiskayCollector.Core.Models;

namespace DiskayCollector.ScheduleService.Interfaces;

public interface IScheduleService {
    public Task<DayScheduleEntity>? GetDaySchedule(DateOnly date, string group);
    public Task<List<DayScheduleEntity>> GetWeekSchedule(DateOnly dateStart, string group);
}