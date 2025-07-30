using System.Globalization;
using System.Text.Json;
using DiskayCollector.Application.Contracts;
using DiskayCollector.Application.Data;
using DiskayCollector.Application.Interfaces;
using DiskayCollector.Core.Models;

namespace DiskayCollector.Application.Controllers;

public class ScheduleService : IScheduleService {
    private readonly HttpClient _httpClient;
    private readonly string _baseUrl;
    
    public ScheduleService(HttpClient httpClient,  string baseUrl) {
        _httpClient = httpClient;
        _baseUrl = baseUrl;
    }

    public async Task<DayScheduleEntity>? GetDaySchedule(DateOnly day, string group) {
        try{
            var body = ApiScheduleRequest.CreatePeriod(
                day_start: new DateOnly(2025, 4, 15),
                day_end: new DateOnly(2025, 4, 20),
                group: group
            );
            
            var bodyRequest = new ScheduleBodyRequest(body);

            var response = await _httpClient.PostAsync(_baseUrl, bodyRequest.GetBodyContent());

            if (response.IsSuccessStatusCode){
                var responseStringContent =  await response.Content.ReadAsStringAsync();
                var result =  JsonSerializer.Deserialize<List<ApiItem>>(responseStringContent);

                var items = ScheduleFormatter.FormatScheduleItems(
                    data: result, 
                    dayFilter: day
                );

                var DaySchedule = new DayScheduleEntity(
                    day: day,
                    mainGroup: new GroupEntity(group),
                    items: items
                );
                
                return DaySchedule;
            }
            return null;
        }
        catch (Exception ex){
            throw new Exception("SCHEDULE_SERVICE_ERROR:", ex);
        }
    }

    public Task<List<DayScheduleEntity>> GetWeekSchedule(DateOnly date, string group) {
        throw new NotImplementedException();
    }
}