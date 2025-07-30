using System.Diagnostics.Contracts;
using System.Text;
using System.Text.Json;
using DiskayCollector.Application.Contracts;
using DiskayCollector.Core.Models;

namespace DiskayCollector.Application.Data;

public class ScheduleBodyRequest {
    private ApiScheduleRequest _body;
    
    public ScheduleBodyRequest(ApiScheduleRequest body) {
        _body = body;
    }
    
    public StringContent GetBodyContent() {
        try{
            var string_json = JsonSerializer.Serialize(_body);
            var result = new StringContent(string_json, Encoding.UTF8, "application/json");

            return result;
        }

        catch (Exception ex){
            throw new Exception("Error get body content", ex);
        }
    }
}