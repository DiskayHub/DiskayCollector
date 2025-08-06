using System.Text.Json;
using DiskayCollector.CollegeAPI.Contracts;
using DiskayCollector.CollegeAPI.Data;
using DiskayCollector.CollegeAPI.Interfaces;
using DiskayCollector.Core.Models;
using DiskayCollector.CollegeAPI.Modules;

namespace DiskayCollector.CollegeAPI.Services;

public class ScheduleService : IScheduleService {
    private readonly HttpClient _httpClient;
    private readonly string _baseUrl;
    
    public ScheduleService(HttpClient httpClient,  string baseUrl) {
        _httpClient = httpClient;
        _baseUrl = baseUrl;
    }

    public async Task<DayScheduleEntity>? GetDaySchedule(DateOnly day, string group) {
        try{
            var body = ApiScheduleRequest.CreateDefault(day, group);
            
            var bodyRequest = new ScheduleBodyRequest(body);

            var response = await _httpClient.PostAsync(_baseUrl, bodyRequest.GetBodyContent());

            if (response.IsSuccessStatusCode){
                var responseStringContent =  await response.Content.ReadAsStringAsync();
                var result =  JsonSerializer.Deserialize<List<ApiItem>>(responseStringContent);
                
                var daySchedule = ScheduleFormatter.FormatScheduleDay(result, day, group);

                if (daySchedule != null) return daySchedule;
            }
            return null;
        }
        catch (Exception ex){
            throw new Exception("SCHEDULE_SERVICE_ERROR:", ex);
        }
    }

    public async Task<List<DayScheduleEntity>> GetWeekSchedule(DateOnly dateStart, string group) {
        // try{
        //     var weekPeriod = TimeHelper.GetWeekPeriod(dateStart);
        //     var body = ApiScheduleRequest.CreatePeriod(
        //         day_start: weekPeriod.Start,
        //         day_end: weekPeriod.End,
        //         group: group
        //     );
        //
        //     var bodyRequest = new ScheduleBodyRequest(body);
        //
        //     var response = await _httpClient.PostAsync(_baseUrl, bodyRequest.GetBodyContent());
        //
        //     if (response.IsSuccessStatusCode){
        //         var responseStringContent = await response.Content.ReadAsStringAsync();
        //         var result = JsonSerializer.Deserialize<List<ApiItem>>(responseStringContent);
        //
        //         var dayEntities = ScheduleFormatter.FormatSchedulePeriod(result, group);
        //         return dayEntities;
        //         
        //     }
        //     return null;
        // }
        // catch (Exception ex){
        //     throw new Exception("SCHEDULE_SERVICE_ERROR:", ex);
        // }
        throw new NotImplementedException();
    }
}