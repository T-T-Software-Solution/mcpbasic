# Weather MCP Server

This project is a Model Context Protocol (MCP) server that provides weather data and alerts for the United States using the [National Weather Service API](https://www.weather.gov/documentation/services-web-api). It is implemented in C# using .NET 8 and the ModelContextProtocol library.

## Features

- **Get weather alerts for a US state**: Retrieve active weather alerts for any US state.
- **Get weather forecast for a location**: Retrieve detailed weather forecasts for a given latitude and longitude.

## Project Structure

- `Program.cs`: Configures and runs the MCP server, sets up dependency injection, and registers the weather tools.
- `WeatherTools.cs`: Implements the MCP server tools for weather alerts and forecasts.
- `HttpCLientExt.cs`: Provides an extension method for reading JSON responses from HTTP requests.
- `weather.csproj`: Project file with dependencies and build configuration.

## Requirements

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- Internet access (to reach the weather.gov API)

## Getting Started

1. **Clone the repository** (if not already):
   ```powershell
   git clone <your-repo-url>
   cd mcpbasic/weather
   ```

2. **Restore dependencies:**
   ```powershell
   dotnet restore
   ```

3. **Build the project:**
   ```powershell
   dotnet build
   ```

4. **Run the server:**
   ```powershell
   dotnet run
   ```

The server will start and listen for MCP requests via stdio.

## Usage

The server exposes two main tools:

### 1. GetAlerts
- **Description:** Get weather alerts for a US state.
- **Parameters:**
  - `state` (string): The two-letter US state code (e.g., `CA`, `TX`).
- **Returns:** List of active alerts or a message if none are active.

### 2. GetForecast
- **Description:** Get weather forecast for a location.
- **Parameters:**
  - `latitude` (double): Latitude of the location.
  - `longitude` (double): Longitude of the location.
- **Returns:** Multi-period weather forecast for the specified location.

## Example MCP Tool Usage

- **GetAlerts**
  ```json
  {
    "tool": "GetAlerts",
    "parameters": { "state": "CA" }
  }
  ```

- **GetForecast**
  ```json
  {
    "tool": "GetForecast",
    "parameters": { "latitude": 34.05, "longitude": -118.25 }
  }
  ```

## Tutorial source

Please learn more at https://modelcontextprotocol.io/quickstart/server
