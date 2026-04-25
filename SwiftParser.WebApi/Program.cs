using NLog.Web;
using SwiftParser.WebApi.Repositories;
using SwiftParser.WebApi.Repositories.Contracts;
using SwiftParser.WebApi.Services;
using SwiftParser.WebApi.Services.Contracts;

var builder = WebApplication.CreateBuilder(args);

// Configure NLog as the logging provider
builder.Logging.ClearProviders();
builder.Host.UseNLog();

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Dependency Injection registrations
builder.Services.AddScoped<ISwiftParserService, SwiftParserService>();
builder.Services.AddScoped<ISwiftRepository, SwiftRepository>();

var app = builder.Build();

// Initialize the SQLite database schema on startup
using (var scope = app.Services.CreateScope())
{
    var repository = scope.ServiceProvider.GetRequiredService<ISwiftRepository>();
    await repository.InitializeDatabaseAsync();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();