using System.Text.Json;
using DiskayCollector.API.Contracts.Service;
using DiskayCollector.Postgres;
using Microsoft.AspNetCore.Mvc;

namespace DiskayCollector.API.Controllers {
    [Route("api/[controller]")]
    [ApiController]

    public class ServiceController : ControllerBase {
        private readonly string _serviceName;
        private readonly DayItemsDbContext _context;

        public ServiceController(DayItemsDbContext context) {
            _context = context;
            _serviceName = "DiskayCollector";
        }

        [Route("Ping")]
        [HttpGet]

        public async Task<IActionResult> CheckServiceStatus() {
            var dbRequest = await _context.IsActive();

            if (dbRequest){
                var response = new PingResponse(
                    serviceName: _serviceName,
                    serviceStatus: "OK",
                    dataBaseStatus: "OK"
                );
                var jsonResponse = JsonSerializer.Serialize(response);
            
                return Ok(jsonResponse);
            }
            else {
                var response = new PingResponse(
                    serviceName: _serviceName,
                    serviceStatus: "OK",
                    dataBaseStatus: "INACTIVE"
                );
                var jsonResponse = JsonSerializer.Serialize(response);
                
                return Ok(jsonResponse); 
            }
        }
    }
}