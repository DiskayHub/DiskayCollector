using DiskayCollector.CollegeAPI;
using DiskayCollector.CollegeAPI.Contracts;
using DiskayCollector.Postgres;

namespace DiskayCollector.Service.Record;

public class RecordService {
    private readonly DayItemsDbContext _database;
    private readonly ScheduleService _scheduleService;

    public RecordService(DayItemsDbContext database,  ScheduleService scheduleService) {
        _database = database;
        _scheduleService = scheduleService;
    }

    public async Task FastRecord() {
        var dateNow = new DateOnly(2025, 04, 14);
        var scheduleDay = await _scheduleService.GetDaySchedule(dateNow, "ИТ24-13");

        if (scheduleDay != null){
            await _database.DaySchedules.AddAsync(scheduleDay);
            await _database.SaveChangesAsync();
        }
    }
}