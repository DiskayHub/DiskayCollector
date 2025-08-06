using DiskayCollector.CollegeAPI;
using DiskayCollector.CollegeAPI.Contracts;
using DiskayCollector.Core.Models;
using DiskayCollector.Postgres;

var dateNow = new DateOnly(2025, 04, 14);
var timeNow = new TimeOnly(9, 0);
var app = new App(dateNow, timeNow);
await app.Start();

var requestBody = ApiScheduleRequest.CreateDefault (
    date: new DateOnly(2025, 4, 18),
    group: "ИТ23-11"
);

var httpClient = new HttpClient();
var baseUrl = "https://portal.it-college.ru/schedule.php";

var dayDate = new DateOnly(2025, 4, 14);
var group = "ИТ24-13";

var scheduleService = new ScheduleService(httpClient,  baseUrl);
var scheduleDay = await scheduleService.GetDaySchedule(dayDate, group);

Console.WriteLine("---НАЧАЛО_ДНЯ---");
Console.WriteLine("ДАТА: " + scheduleDay.Date.ToString("yyyy-MM-dd"));
Console.WriteLine("ДЕНЬ: " + scheduleDay.Date.DayOfWeek);
Console.WriteLine("ГРУППА: " + scheduleDay.MainGroup);

foreach (var item in scheduleDay.Items) {
    Console.WriteLine("ПРЕДМЕТ: " + item.Name + " --> " + item.StartTime.ToString("HH:mm"));
    if (item.SubGroupsItems != null){
        foreach (var subGroup in item.SubGroupsItems){
            Console.WriteLine("-->Группа: " + subGroup.Name);
            Console.WriteLine("---->Кабинет: " + subGroup.RoomName);
            if (!string.IsNullOrEmpty(subGroup.Description)){
                Console.WriteLine("---->Подгруппа: " + subGroup.Description);
            }
        }
    }
}     
Console.WriteLine("---КОНЕЦ_ДНЯ---");
Console.WriteLine();


public class App {
    private readonly DateOnly _dateNow;
    private readonly TimeOnly _timeNow;
    private readonly List<string> _allRooms;
    private readonly List<string> _allGroups;
    private readonly ScheduleService _service;
    
    public App(DateOnly dateNow, TimeOnly timeNow) {
        var httpClient = new HttpClient();
        var baseUrl = "https://portal.it-college.ru/schedule.php";
        
        _dateNow = dateNow;
        _timeNow = timeNow;

        _allRooms = ["1-1", "1-2", "1-3", "1,4", "1-5",
            "2-1", "2-2", "2-3", "2-4", "2-5",
            "3-1", "3-2", "3-3", "3-4", "3-5",
            "403", "404", "405", "406"
        ];

        _allGroups = [
            "ИТ24-11", "ИТ24-12", "ИТ24-13", "ИТ24-14", "ИТ23-11", "ИТ23-12", "ИТ23-13", "ИТ23-14", "ИТ22-11",
            "ИТ22-12", "ИТ21-11"
        ];

        _service = new ScheduleService(httpClient, baseUrl);
    }

    public async Task Start() {
        
        var daySchedules = (await Task.WhenAll(_allGroups.Select(g => _service.GetDaySchedule(_dateNow, g))))
            .Where(item => item != null && item.MainGroup != null)
            .ToArray();
        
        var busyRooms = new List<string>();
        Console.WriteLine("ЗАНЯТЫЕ КАБИНЕТЫ: ");
        foreach (var schedule in daySchedules) {
            foreach (var item in  schedule.Items) {
                if (_timeNow >= item.StartTime && _timeNow <= item.EndTime){
                    if (item.SubGroupsItems == null){
                        Console.WriteLine(item.RoomName);
                        busyRooms.Add(item.RoomName);   
                    }
                    else{
                        foreach (var itemSub in item.SubGroupsItems) {
                            Console.WriteLine(itemSub.RoomName);
                            busyRooms.Add(itemSub.RoomName);
                        }
                    }
                }
            }
        }
        
        Console.WriteLine("СВОБОДНЫЕ КАБИНЕТЫ -> " + _timeNow.ToString("HH:mm"));
        foreach (var room in _allRooms) {
            if (!busyRooms.Contains(room)){
                Console.WriteLine("КАБИНЕТ: " + room);
            }
        }
    }
}

