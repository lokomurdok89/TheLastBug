
using API.Extension;
using Application.Utils;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
var logger = new LoggerConfiguration()
					.ReadFrom.Configuration(builder.Configuration)
					.Enrich.FromLogContext()
                    .Enrich.WithEnvironmentName()
                    .Enrich.WithMachineName()
					.CreateLogger();

builder.Logging.AddSerilog(logger);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddTransient<IUtils, Utils>();

var app = builder.Build();
// Configure the HTTP request pipeline.
//add policy cors
app.UseCors("CorsPolicy");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.Use((context, next) =>
            {
                context.Request.EnableBuffering();
                return next();
            });
app.UseMiddleware<RequestResponseLoggingMiddleware>();
app.ConfigureExceptionHandler();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;

try
{
    var context = services.GetRequiredService<DataContext>();
    await context.Database.MigrateAsync();
    await Seed.SeedData(context);
}
catch (Exception ex)
{
    
    var loggerDB = services.GetRequiredService<ILogger<Program>>();
    loggerDB.LogError(ex,"Error al cargar la base de datos");
}

app.Run();
