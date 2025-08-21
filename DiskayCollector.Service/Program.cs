using System.Text;
using DiskayCollector.CollegeAPI;
using DiskayCollector.Postgres;
using DiskayCollector.Service.Record;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace DiskayCollector.Service;

class Program
{
    static async Task Main(string[] args) {
        var serviceProvider = ConfigureServices();
        
        using (var scope = serviceProvider.CreateScope()) {
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
            var dbContext = scope.ServiceProvider.GetRequiredService<DayItemsDbContext>();
            var scheduleService = new ScheduleService(new HttpClient(), "https://portal.it-college.ru/schedule.php");
            
            var recordService = new RecordService(dbContext, scheduleService);
            await recordService.FastRecord();
        }
    }

    static IServiceProvider ConfigureServices()
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath("/home/laxerem/Documents/my_projects/DiskayHub/DiskayCollector/DiskayCollector.Service")
            .AddJsonFile("appsettings.json")
            .Build();

        var services = new ServiceCollection();
        
        // Логирование
        services.AddLogging(configure => 
            configure.AddConsole().SetMinimumLevel(LogLevel.Debug));
        
        // DbContext
        services.AddDbContext<DayItemsDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DayItemsDbContext"))
                .UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole())));
        
        return services.BuildServiceProvider();
    }
}