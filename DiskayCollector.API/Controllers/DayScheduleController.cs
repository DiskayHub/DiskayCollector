using System.Text.Json;
using DiskayCollector.API.Contracts.DaySchedule.Get;
using DiskayCollector.Core.Models;
using DiskayCollector.Core.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DiskayCollector.API.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class DayScheduleController : ControllerBase {
        private readonly IDayScheduleRepository _dayScheduleRepository;

        public DayScheduleController(IDayScheduleRepository dayScheduleRepository) {
            _dayScheduleRepository = dayScheduleRepository;
        }
        private GetDayScheduleResponse SerializeDayData(DateOnly date, string group_name, List<ItemEntity> items) {
            var dayResponse = new GetDayScheduleResponse(
                date: date,
                mainGroup: group_name,
                items: items.Select(i => new ItemResponse(
                    name: i.Name,
                    description: i.Description,
                    room_name: i.RoomName,
                    startTime: i.StartTime,
                    endTime: i.EndTime,
                    subGroups: i.SubGroupsItems?.Select(s => new SubGroupResponse(
                        name: s.Name,
                        description: s.Description,
                        roomName: s.RoomName,
                        subGroup: s.SubGroup
                    )).ToList()
                )).ToList()
            );
            return dayResponse;
        }

        [Route("GetDayByDate")]
        [HttpGet]
        public async Task<IActionResult> GetDaySchedule([FromQuery] DateOnly date, string group_name) {
            try{
                var daySchedule = await _dayScheduleRepository.GetScheduleByDate(date, group_name);
                if (daySchedule != null) {
                    var dayResponse = SerializeDayData(date, group_name, daySchedule.Items);
                    return Ok(dayResponse);
                }
                return NotFound();
            }

            catch (Exception ex){
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [Route("GetScheduleByFilter")]
        [HttpGet]

        public async Task<IActionResult> GetScheduleByFilter([FromQuery] GetScheduleFilter query) {
            try{
                var daySchedule = await _dayScheduleRepository.GetScheduleByFilter(
                    date: query.date,
                    groupName: query.group_name,
                    engGroup: query.EnglishSubGroup,
                    MainSubGroup: query.MainSubGroup,
                    profGroup: query.ProfileSubGroup
                );

                if (daySchedule != null){
                    var dayResponse = SerializeDayData(query.date, query.group_name, daySchedule.Items);
                    return Ok(dayResponse);  
                }
                return NotFound();
            }
            
            catch (Exception ex){
                throw new Exception(ex.Message);
            }
        }
    }
}
