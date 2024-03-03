using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using backend.Controllers;
using System.Text.Json;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.FileProviders;
using DotNetEnv;



var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();

builder.Services.AddCors(options =>
{

    options.AddDefaultPolicy(
        policy =>
        {
            policy.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});



var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

DotNetEnv.Env.Load();
var rootPath = Environment.GetEnvironmentVariable("RootPath");
app.UseStaticFiles(new StaticFileOptions { 
    FileProvider = new PhysicalFileProvider(Path.Combine(rootPath, "backup")),
    RequestPath = "/backup"
});
app.UseCors();
app.UseHttpsRedirection();
app.UseAuthentication();
app.MapControllers();

app.Run();