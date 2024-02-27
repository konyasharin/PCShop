using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using backend.Data;
using backend.Controllers;
using backend.Repositories;
using System.Text.Json;

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

// ������������ API
app.MapGet("/", () => "hello");
app.MapPost("/ComputerCase/createcomputercase", (HttpContext data) =>
{
    
    return Results.Json("{'data': ${data}");
});
app.MapGet("/Cooler/createcooler", () => "cooler");
app.MapGet("/MotherBoard/createmotherboard", () => "motherboard");
app.MapGet("/PowerUnit/createpowerunit", () => "powerunit");
app.MapGet("/Processor/createprocessor", () => "processor");
app.MapGet("/RAM/createram", () => "ram");
app.MapGet("/api/SSD/createssd", () => "ssd");
app.MapGet("/api/VideoCard/createvideocard", () => "videoCard");

app.MapGet("/api/ComputerCase/1", () => "������ computercase");
app.MapGet("/api/Cooler/1", () => "������ cooler");
app.MapGet("/api/Motherboard/1", () => "������ motherboard");
app.MapGet("/api/PowerUnit/1", () => "������ powerunit");
app.MapGet("/api/Processor/1", () => "������ processor");
app.MapGet("/api/RAM/1", () => "������ ram");
app.MapGet("/api/SSD/1", () => "������ ssd");
app.MapGet("/api/VideoCard/1", () => "������ videocard");

app.Run();