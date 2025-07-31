using DiskayCollector.Application.Contracts.Other;

namespace DiskayCollector.Application.Modules;

public class TimeHelper {
    public static TimePeriod GetWeekPeriod(DateOnly date) {
        var dayOfWeek = (int)date.DayOfWeek;

        if (dayOfWeek <= 5) {
            var dateStart = date.AddDays(-dayOfWeek);
            var dateEnd = date.AddDays(6);
            
            return new TimePeriod(dateStart, dateEnd);
        }
        
        throw new ArgumentOutOfRangeException(nameof(date));
    }
}