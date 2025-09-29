using ModelContextProtocol.Server;
using System.ComponentModel;
using AvroMcpConvCommit.Application;
using System.Text.Json;

[McpServerToolType]
public static class ParseCommitTool
{
    [McpServerTool, Description("Parses a conventional commit message into its components.")]
    public static string ParseCommit(ICommitParser parser, [Description("The commit message to parse")] string commitMessage)
    {
        var message = parser.Parse(commitMessage);
        if (message == null)
        {
            return "Failed to parse commit message.";
        }

        var result = new
        {
            Type = message.Type.ToString(),
            Scope = message.Scope,
            Description = message.Description,
            Body = message.Body,
            Footer = message.Footer,
            IsBreakingChange = message.IsBreakingChange
        };

        return JsonSerializer.Serialize(result, new JsonSerializerOptions { WriteIndented = true });
    }
}