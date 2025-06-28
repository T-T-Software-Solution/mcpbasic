# MCPHostApp

This project is a sample client application for interacting with an MCP (Model Context Protocol) server using Azure OpenAI and the ModelContextProtocol.Client library. It demonstrates how to connect to an MCP server, list available tools, and interact with the server in a conversational loop using OpenAI's chat capabilities.

## Features

- Connects to an MCP server via stdio transport (child process)
- Uses Azure OpenAI for chat-based interaction
- Lists available tools from the MCP server
- Supports conversational prompts and tool invocation
- Securely loads Azure OpenAI credentials from user secrets
- Accepts the MCP server project path as a command line argument

## Project Structure

- `Program.cs`: Main entry point. Handles configuration, OpenAI client setup, MCP client connection, and the conversational loop.
- `MCPHostApp.csproj`: Project file with dependencies and build configuration.

## Requirements

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- Azure OpenAI resource (endpoint, key, deployment)
- An MCP server project (such as the `weather` project in this solution)

## Getting Started

1. **Configure Azure OpenAI credentials**
   
   Store your Azure OpenAI endpoint, key, and deployment in user secrets:
   ```powershell
   dotnet user-secrets set "AzureOpenAI:Endpoint" "https://<your-endpoint>.openai.azure.com/"
   dotnet user-secrets set "AzureOpenAI:Key" "<your-key>"
   dotnet user-secrets set "AzureOpenAI:Deployment" "<your-deployment-name>"
   ```

2. **Restore dependencies:**
   ```powershell
   dotnet restore
   ```

3. **Build the project:**
   ```powershell
   dotnet build
   ```

4. **Run the MCP server** (if not already running):
   ```powershell
   dotnet run --project ..\weather --no-build
   ```

5. **Run the host app, specifying the MCP server project path:**
   ```powershell
   dotnet run -- ..\weather
   ```
   > The path after `--` should point to your MCP server project (e.g., the weather project).

## Usage

- On startup, the app lists all available tools from the MCP server.
- Enter a prompt at the `Prompt:` prompt. The app will use Azure OpenAI to generate a response, possibly invoking MCP tools.
- Press Enter on an empty line to exit the loop and terminate the application.

## Example Prompt

```
Prompt: What is the weather forecast for Los Angeles, CA?
```

## Notes

- The MCP server project path is now provided as a command line argument, not hardcoded in the source.
- The application uses user secrets for secure credential management. Do not hard-code secrets in source files.

## License

This project is provided for educational and demonstration purposes.
