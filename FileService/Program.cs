using FileService;
using FileService.Services;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

var config = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json", optional: false)
        .Build();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder.Services.AddDbContext<DbContext, ApplicationContext>(options => options.UseNpgsql(config.GetConnectionString()) "));
builder.Services.AddDbContext<DbContext, ApplicationContext>(options =>
{
    options.UseNpgsql(config.GetConnectionString("PgSqlConnectionString"));
});
builder.Services.AddScoped<IFileProcessingService, FileProcessingService>();
builder.WebHost.UseHttpSys(options =>
{
    options.MaxRequestBodySize = 100_000_000;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
