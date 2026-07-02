# TelemetryServer

TelemetryServer is a simple ASP.NET Core Web API built in C# that simulates serving racing telemetry data.

## Features

- Reads telemetry data from a JSON file
- Returns all telemetry records
- Returns the fastest lap
- Returns telemetry for a specific car
- Returns a session summary including:
  - Total telemetry records
  - Number of cars tracked
  - Fastest lap
  - Average lap time
  - Top speed

## Technologies

- C#
- ASP.NET Core
- JSON
- REST API

## API Endpoints

- `GET /telemetry`
- `GET /fastest-lap`
- `GET /cars/{carNumber}`
- `GET /session-summary`

## Running the Project

```bash
dotnet restore
dotnet run
