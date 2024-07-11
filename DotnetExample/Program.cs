using DotnetExample.Service;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.File($"Logs/LogsDeExemplo.log")
    .WriteTo.Console()
    .CreateLogger();

Log.Logger.Information("The server is starting");

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Services.AddControllers();

    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    builder.Services.AddSingleton<CarService>();

    var app = builder.Build();
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
catch (Exception e)
{
    Log.Logger.Error("The server stoped due to an internal error", e);
}
finally
{
    Log.CloseAndFlush();
}



