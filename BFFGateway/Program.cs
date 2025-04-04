using BFFGateway.Handlers;
using MediatR;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();  // ✅ Logs to console
builder.Logging.SetMinimumLevel(LogLevel.Debug);  // ✅ Ensure all logs are shown

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

// Add MediatR
builder.Services.AddMediatR(typeof(ApiRequestHandler).Assembly);

// Register AutoMapper
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddHttpClient();

// Register HTTP Clients for different services
builder.Services.AddHttpClient("rediscachingwebapi", c =>
{
    c.BaseAddress = new Uri("http://rediscachingwebapi");
});

// Add services and controllers
builder.Services.AddControllers();

var app = builder.Build();
app.UseCors("AllowAll");
app.UseRouting();
app.UseAuthorization();
app.MapControllers();
app.Run();