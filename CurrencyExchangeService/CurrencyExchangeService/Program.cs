using CurrencyExchangeService;
using CurrencyExchangeService.Features.Coordinates.Intrefaces;
using CurrencyExchangeService.Features.Coordinates.Services;
using CurrencyExchangeService.Features.ExchangeRate.Interfaces;
using CurrencyExchangeService.Features.ExchangeRate.Services;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var logger = new LoggerConfiguration()
    .WriteTo.File("log.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();
var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .Build();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var appSettings = new AppSettings();
configuration.GetSection("AppSettings").Bind(appSettings);
builder.Services.AddSingleton(appSettings);
builder.Services.AddTransient<ICoordinateService, CoordinateService>();
builder.Services.AddScoped<IExchangeRateService, ExchangeRateService>();

builder.Services.AddLogging(loggingBuilder =>
{
    loggingBuilder.ClearProviders();
    loggingBuilder.AddSerilog();
});
builder.Host.UseSerilog(logger);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseSerilogRequestLogging();

app.MapControllers();

app.Run();
