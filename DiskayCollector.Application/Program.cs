using System.Text;
using System.Text.Json;
using DiskayCollector.Application.Contracts;
using DiskayCollector.Application.Services;

var requestBody = ApiScheduleRequest.CreateDefault (
      date: new DateOnly(2025, 4, 18),
      group: "ИТ23-11"
);

var httpClient = new HttpClient();
var baseUrl = "https://portal.it-college.ru/schedule.php";

var dayDate = new DateOnly(2025, 4, 14);
var group = "ИТ24-13";

var scheduleService = new ScheduleService(httpClient,  baseUrl);
var scheduleDays = await scheduleService.GetWeekSchedule(dayDate, group);

foreach (var scheduleDay in scheduleDays){
      Console.WriteLine("---НАЧАЛО_ДНЯ---");
      Console.WriteLine("ДАТА: " + scheduleDay.Day.ToString("yyyy-MM-dd"));
      Console.WriteLine("ДЕНЬ: " + scheduleDay.Day.DayOfWeek);
      Console.WriteLine("ГРУППА: " + scheduleDay.MainGroup.Name);

      foreach (var item in scheduleDay.Items) {
            Console.WriteLine("ПРЕДМЕТ: " + item.Name + " --> " + item.StartTime.ToString("HH:mm"));
            if (item.SubGroupsItems != null){
                  foreach (var subGroup in item.SubGroupsItems){
                        Console.WriteLine("-->Группа: " + subGroup.SubGroup.Name);
                        Console.WriteLine("---->Кабинет: " + subGroup.RoomName);
                        if (!string.IsNullOrEmpty(subGroup.Description)){
                              Console.WriteLine("---->Подгруппа: " + subGroup.Description);
                        }
                  }
            }
      }     
      Console.WriteLine("---КОНЕЦ_ДНЯ---");
      Console.WriteLine();
}