using DiskayCollector.Core.Models;
using DiskayCollector.Core.Repositories;
using DiskayCollector.Postgres;
using DiskayCollector.Postgres.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

builder.Services.AddDbContext<DayItemsDbContext>(
    options => {
        options.UseNpgsql(configuration.GetConnectionString("DayItemsDbContext"));  
    }
);

builder.Services.AddScoped<IDayScheduleRepository, DayScheduleRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.Run();