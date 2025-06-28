# Weather & MCPHostApp Solution

![Solution Intro](intro.png)

This solution contains two main projects that demonstrate the use of the Model Context Protocol (MCP) for tool-augmented AI applications:

- **weather**: An MCP server that provides weather data and alerts using the National Weather Service API.
- **MCPHostApp**: A client application that connects to an MCP server (such as `weather`) and enables conversational AI interactions using Azure OpenAI.

---

## Projects Overview

### [weather](./weather/readme.md)
- **Type:** MCP Server
- **Description:**
  - Exposes weather tools (alerts and forecasts) via the MCP protocol.
  - Implements endpoints for getting weather alerts by US state and forecasts by latitude/longitude.
  - Uses the National Weather Service API as its data source.
- **How to Use:**
  - Build and run as a standalone MCP server, or connect to it from a compatible MCP client (like MCPHostApp).
  - See the [weather project README](./weather/readme.md) for setup and usage details.

### [MCPHostApp](./MCPHostApp/readme.md)
- **Type:** MCP Client
- **Description:**
  - Connects to an MCP server via stdio (child process) and lists available tools.
  - Uses Azure OpenAI for chat-based, tool-augmented conversations.
  - Accepts the MCP server project path as a command line argument.
- **How to Use:**
  - Configure Azure OpenAI credentials using user secrets.
  - Run the app, specifying the path to the MCP server project (e.g., `weather`).
  - See the [MCPHostApp project README](./MCPHostApp/readme.md) for setup and usage details.

---

## Getting Started

1. **Restore dependencies for the solution:**
   ```powershell
   dotnet restore
   ```
2. **Build the solution:**
   ```powershell
   dotnet build
   ```
3. **(Optional) Create an `mcp.json` configuration in `.vscode`:**
   - Create a folder named `.vscode` in your workspace if it doesn't exist.
   - Inside `.vscode`, create a file named `mcp.json`.
   - Example contents for `mcp.json`:
     ```jsonc
     {
         "servers": {
             "time": {
                 "command": "docker",
                 "args": [
                     "run",
                     "-i",
                     "--rm",
                     "mcp/time"
                 ]
             },
             "postgres": {
                 "command": "npx",
                 "args": [
                     "-y",
                     "@modelcontextprotocol/server-postgres",
                     "postgresql://yourdbusername:yourdbpassword@yourdbserver:65432/yourdbname"
                 ]
             },
             "weather": {
                 "command": "dotnet",
                 "args": ["run", "--project", "weather", "--no-build"]
             }
         }
     }
     ```
   - This file allows you to define and launch MCP servers easily from VS Code or compatible tools.
4. **Follow the instructions in each project README** to run the server and client.

---

## Solution Structure

- `/weather` — MCP server for weather data ([README](./weather/readme.md))
- `/MCPHostApp` — MCP client for conversational AI ([README](./MCPHostApp/readme.md))

---

## License

This solution is provided for educational and demonstration purposes.
