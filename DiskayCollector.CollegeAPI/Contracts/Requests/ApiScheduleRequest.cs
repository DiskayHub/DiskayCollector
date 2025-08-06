using System.Globalization;
using System.Text.RegularExpressions;

namespace DiskayCollector.Service.Contracts;
public class ApiScheduleRequest {
    public string d_start {get; }
    public string d_end { get; }
    public string group { get; }
    private ApiScheduleRequest(string d_start, string d_end, string group) {
        this.d_start = d_start;
        this.d_end = d_end;
        this.group = group;
    }
    
    private static bool CheckValidDate(string date) {
        bool result = DateTime.TryParseExact(
            date,
            "yyyy-MM-ddTHH:mm:ss.fffZ",
            CultureInfo.InvariantCulture, 
            DateTimeStyles.AssumeUniversal,
            out DateTime parsed_data
        );
        return result;
    }

    private static bool CheckValidGroup(string group) {
        return Regex.IsMatch(group, @"^ИТ\d{2}-\d{2}$");
    }

    public static ApiScheduleRequest Create(string d_start, string d_end, string group) {
        if (CheckValidDate(d_start) && CheckValidDate(d_end)) {
            if (CheckValidGroup(group)){
                return new ApiScheduleRequest(d_start, d_end, group);
            }
            throw new ArgumentException("\nAPI_SCHEDULE_REQUEST: Invalid group format");
        }

        throw new ArgumentException("\nAPI_SCHEDULE_REQUEST: Invalid datetime format");
    }

    public static ApiScheduleRequest CreatePeriod(DateOnly day_start, DateOnly day_end, string group) {
        var d_start = new DateTime(day_start.Year, day_start.Month, day_start.Day, 19, 0, 0, DateTimeKind.Utc)
            .ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
        var d_end = new DateTime(day_end.Year, day_end.Month, day_end.Day, 19, 0, 0, DateTimeKind.Utc)
            .ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
        return Create(d_start, d_end, group);
    }

    public static ApiScheduleRequest CreateDefault(DateOnly date, string group) {
        var date_start = date.AddDays(-1);
        var date_end = date;
        
        var d_start = new DateTime(date_start.Year, date_start.Month, date_start.Day, 19, 0, 0, DateTimeKind.Utc)
            .ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
        var d_end = new DateTime(date_end.Year, date_end.Month, date_end.Day, 19, 0, 0, DateTimeKind.Utc)
            .ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
        
        return Create(d_start, d_end, group);
    }
}
