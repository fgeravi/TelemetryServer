using System.Text.Json;
using TelemetryServer.Models;

namespace TelemetryServer.Services;

public class TelemetryService
{
    private readonly string _filePath = "Data/sampleTelemetry.json";

    public List<DriverTelemetry> GetAllTelemetry()
    {
        string json = File.ReadAllText(_filePath);

        var telemetry = JsonSerializer.Deserialize<List<DriverTelemetry>>(
            json,
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }
        );

        return telemetry ?? new List<DriverTelemetry>();
    }

    public DriverTelemetry? GetFastestLap()
    {
        return GetAllTelemetry()
            .OrderBy(t => t.LapTimeSeconds)
            .FirstOrDefault();
    }

    public List<DriverTelemetry> GetTelemetryByCarNumber(int carNumber)
    {
        return GetAllTelemetry()
            .Where(t => t.CarNumber == carNumber)
            .ToList();
    }

    public object GetSessionSummary()
    {
        var telemetry = GetAllTelemetry();

        if (telemetry.Count == 0)
        {
            return new
            {
                Message = "No telemetry data found."
            };
        }

        return new
        {
            TotalRecords = telemetry.Count,
            CarsTracked = telemetry.Select(t => t.CarNumber).Distinct().Count(),
            FastestLap = telemetry.OrderBy(t => t.LapTimeSeconds).First(),
            AverageLapTimeSeconds = telemetry.Average(t => t.LapTimeSeconds),
            TopSpeedMph = telemetry.Max(t => t.SpeedMph)
        };
    }
}
