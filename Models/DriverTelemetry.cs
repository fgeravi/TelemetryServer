namespace TelemetryServer.Models;

public class DriverTelemetry
{
    public int CarNumber { get; set; }
    public string DriverName { get; set; } = "";
    public int LapNumber { get; set; }
    public int Position { get; set; }
    public double LapTimeSeconds { get; set; }
    public double SpeedMph { get; set; }
}
