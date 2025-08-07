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
        var scheduleDays = await _scheduleService.GetWeekSchedule(dateNow, "ИТ24-13");

        if (scheduleDays.Count != 0){
            foreach (var day in scheduleDays) {
                await _database.DaySchedules.AddAsync(day);
                await _database.SaveChangesAsync();   
            }
        }
    }
}