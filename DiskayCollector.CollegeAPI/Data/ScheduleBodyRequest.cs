using System.Diagnostics.Contracts;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using DiskayCollector.CollegeAPI.Contracts;
using DiskayCollector.Core.Models;

namespace DiskayCollector.CollegeAPI.Data;

public class ScheduleBodyRequest {
    private ApiScheduleRequest _body;
    private JsonSerializerOptions _jsonOptions;
    
    public ScheduleBodyRequest(ApiScheduleRequest body) {
        _body = body;
        _jsonOptions = new JsonSerializerOptions {
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            PropertyNamingPolicy = null,
        };
    }
    
    public StringContent GetBodyContent() {
        try{
            var string_json = JsonSerializer.Serialize(_body,  _jsonOptions);
            var result = new StringContent(string_json, Encoding.UTF8, "application/json");

            return result;
        }

        catch (Exception ex){
            throw new Exception("Error get body content", ex);
        }
    }
}