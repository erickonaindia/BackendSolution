using Microsoft.EntityFrameworkCore;
using Solution.Persistence.Contexts;
using Solution.Application;
using Solution.Persistence;
using Microsoft.OpenApi.Models;
using Solution.Persistence.Services;
using Microsoft.AspNetCore.Hosting;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Add services to the container.
//CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("OpenPolicy", (builder) =>
        builder
        .AllowCredentials()
        .SetIsOriginAllowed(origin => true)
        .AllowAnyMethod()
        .AllowAnyHeader()
    );
});

// Dependency Services
builder.Services.AddApplicationServices();
builder.Services.AddPersistenceServices(builder.Configuration);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Test API",
        Version = "v1"
    });
});

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(configuration.GetConnectionString("DefaultDbConn")));

var app = builder.Build();

// create a logger factory
var loggerFactory = LoggerFactory.Create(
    builder => builder
        .AddConsole()
        .AddDebug()
        .SetMinimumLevel(LogLevel.Debug)
);

// create a logger
var logger = loggerFactory.CreateLogger<Program>();

// logging
logger.LogTrace("Trace message");
logger.LogDebug("Debug message");
logger.LogInformation("Info message");
logger.LogWarning("Warning message");
logger.LogError("Error message");
logger.LogCritical("Critical message");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.UseCors("OpenPolicy");

var csvReaderService = app.Services.GetRequiredService<CsvReaderService>();
var csvFilePath = Path.Combine(AppContext.BaseDirectory, "Data", "TestExampleFile.csv");
csvReaderService.ReadCsvFile(csvFilePath);

app.Run();
