using TelemetryServer.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddSingleton<TelemetryService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapGet("/", () =>
{
    return "TelemetryServer is running. Try /telemetry, /fastest-lap, or /cars/12";
});

app.MapGet("/telemetry", (TelemetryService service) =>
{
    return service.GetAllTelemetry();
});

app.MapGet("/fastest-lap", (TelemetryService service) =>
{
    var fastestLap = service.GetFastestLap();

    if (fastestLap == null)
    {
        return Results.NotFound("No telemetry data found.");
    }

    return Results.Ok(fastestLap);
});

app.MapGet("/cars/{carNumber}", (int carNumber, TelemetryService service) =>
{
    var carTelemetry = service.GetTelemetryByCarNumber(carNumber);

    if (carTelemetry.Count == 0)
    {
        return Results.NotFound($"No telemetry found for car {carNumber}.");
    }

    return Results.Ok(carTelemetry);
});

app.Run();
