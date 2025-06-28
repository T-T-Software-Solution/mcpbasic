using Azure.AI.OpenAI;
using Microsoft.Extensions.AI;
using Microsoft.Extensions.Configuration;
using ModelContextProtocol.Client;

// Build a configuration to read user secrets
IConfigurationRoot config = new ConfigurationBuilder()
    .AddUserSecrets<Program>() // Assumes this code is in your Program.cs file
    .Build();

// Retrieve secrets with null checks
string? endpointStr = config["AzureOpenAI:Endpoint"];
string? key = config["AzureOpenAI:Key"];
string? deployment = config["AzureOpenAI:Deployment"];

if (string.IsNullOrWhiteSpace(endpointStr) || string.IsNullOrWhiteSpace(key) || string.IsNullOrWhiteSpace(deployment))
{
    Console.WriteLine("Azure OpenAI configuration is missing. Please set Endpoint, Key, and Deployment in user secrets.");
    return;
}

var endpoint = new Uri(endpointStr);

// Create an IChatClient using Azure OpenAI with the settings from user secrets
IChatClient client =
    new ChatClientBuilder(
        new AzureOpenAIClient(endpoint, new Azure.AzureKeyCredential(key))
            .GetChatClient(deployment).AsIChatClient())
    .UseFunctionInvocation()
    .Build();

// Accept command line arguments for the MCP server project path
string mcpProjectPath = args.Length > 1 ? args[1] : string.Empty;
if (string.IsNullOrWhiteSpace(mcpProjectPath))
{
    Console.WriteLine("Usage: dotnet run -- <PathToWeatherProject>");
    return;
}

// Wrap the MCP client in an 'await using' block to ensure it's disposed correctly
await using (IMcpClient mcpClient = await McpClientFactory.CreateAsync(
    new StdioClientTransport(new()
    {
        Command = "dotnet",
        Arguments = ["run", "--project", mcpProjectPath, "--no-build"],
        Name = "Minimal MCP Server",
    })))
{
    // List all available tools from the MCP server.
    Console.WriteLine("Available tools:");
    IList<McpClientTool> tools = await mcpClient.ListToolsAsync();
    foreach (McpClientTool tool in tools)
    {
        Console.WriteLine($"{tool}");
    }
    Console.WriteLine();

    // Conversational loop that can utilize the tools via prompts.
    List<ChatMessage> messages = [];
    while (true)
    {
        Console.Write("Prompt: ");
        var userInput = Console.ReadLine();
        if (string.IsNullOrEmpty(userInput))
        {
            // When the user presses Enter on an empty line, break the loop
            break;
        }
        messages.Add(new(ChatRole.User, userInput));

        List<ChatResponseUpdate> updates = [];
        await foreach (ChatResponseUpdate update in client
            .GetStreamingResponseAsync(messages, new() { Tools = [.. tools] }))
        {
            Console.Write(update);
            updates.Add(update);
        }
        Console.WriteLine();

        messages.AddMessages(updates);
    }
} // <-- mcpClient is automatically disposed here, and the child process is terminated.

Console.WriteLine("\nApplication exited."); // Added for clarity