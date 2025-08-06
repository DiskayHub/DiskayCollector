using DiskayCollector.API.Contracts.DaySchedule.Get;
using DiskayCollector.Core.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DiskayCollector.API.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class DayScheduleController : ControllerBase {
        private readonly IDayScheduleRepository _dayScheduleRepository;

        [Route("GetAllDaysSchedule")]
        [HttpGet]

        public async Task<IActionResult> GetDaySchedule() {
            var days = await _dayScheduleRepository.GetAll();
            return Ok(days);
        }
    }
}
