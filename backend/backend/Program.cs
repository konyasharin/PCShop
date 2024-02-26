using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using backend.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

DotNetEnv.Env.Load();
var connectionString = Environment.GetEnvironmentVariable("ConnectionString");
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseNpgsql(connectionString);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Тестирование API
app.MapGet("/", () => "hello");
app.MapPut("/api/ComputerCase/createComputerCase", () => "computerCase");
app.MapGet("/api/Cooler/createCooler", () => "cooler");
app.MapGet("/api/MotherBoard/createMotherBoard", () => "motherboard");
app.MapGet("/api/PowerUnit/createPowerUnit", () => "powerunit");
app.MapGet("/api/Processor/createProcessor", () => "processor");
app.MapGet("/api/RAM/createRam", () => "ram");
app.MapGet("/api/SSD/createSsd", () => "ssd");
app.MapGet("/api/VideoCard/createVideoCard", () => "videoCard");

app.MapGet("/api/ComputerCase/1", () => "Данные computercase");
app.MapGet("/api/Cooler/1", () => "Данные cooler");
app.MapGet("/api/Motherboard/1", () => "Данные motherboard");
app.MapGet("/api/PowerUnit/1", () => "Данные powerunit");
app.MapGet("/api/Processor/1", () => "Данные processor");
app.MapGet("/api/RAM/1", () => "Данные ram");
app.MapGet("/api/SSD/1", () => "Данные ssd");
app.MapGet("/api/VideoCard/1", () => "Данные videocard");
app.Run();