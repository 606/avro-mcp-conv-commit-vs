using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ModelContextProtocol.Server;
using AvroMcpConvCommit.Application;
using AvroMcpConvCommit.Infrastructure;

var builder = Host.CreateApplicationBuilder(args);
builder.Logging.AddConsole(consoleLogOptions =>
{
    // Configure all logs to go to stderr
    consoleLogOptions.LogToStandardErrorThreshold = LogLevel.Trace;
});

builder.Services.AddTransient<ICommitValidator, CommitValidator>();
builder.Services.AddTransient<ICommitGenerator, CommitGenerator>();
builder.Services.AddTransient<ICommitParser, CommitParser>();
builder.Services.AddTransient<IGitService, GitService>();
builder.Services.AddTransient<ISmartCommitGenerator, SmartCommitGenerator>();

builder.Services
    .AddMcpServer()
    .WithStdioServerTransport()
    .WithToolsFromAssembly();

await builder.Build().RunAsync();
