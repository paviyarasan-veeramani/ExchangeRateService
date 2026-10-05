using ExchangeRateService.Core.Interfaces;
using ExchangeRateService.Core.Services;
using ExchangeRateService.Infrastructure.Data;
using ExchangeRateService.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ExchangeRateDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddScoped<IExchangeRateRepository, ExchangeRateRepository>();
builder.Services.AddScoped<IConversionService, ConversionService>();

// Angular origins come from config, so each environment can differ
var origins = builder.Configuration.GetSection("Cors:AllowedOrigins").Get<string[]>() ?? Array.Empty<string>();
builder.Services.AddCors(o => o.AddDefaultPolicy(p =>
    p.WithOrigins(origins).AllowAnyHeader().AllowAnyMethod()));

builder.Services.AddHealthChecks();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseCors();
app.MapControllers();
app.MapHealthChecks("/health");

app.Run();

