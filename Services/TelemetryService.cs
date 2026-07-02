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
}
