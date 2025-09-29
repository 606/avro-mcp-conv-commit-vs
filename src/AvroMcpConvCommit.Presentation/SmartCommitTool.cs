using ModelContextProtocol.Server;
using System.ComponentModel;
using AvroMcpConvCommit.Application;

[McpServerToolType]
public static class SmartCommitTool
{
    [McpServerTool, Description("Analyzes current git changes and generates an appropriate conventional commit message.")]
    public static async Task<string> GenerateSmartCommit(ISmartCommitGenerator smartGenerator, [Description("Optional custom description. If not provided, will be generated based on changes.")] string? customDescription = null)
    {
        try
        {
            var commitMessage = await smartGenerator.GenerateSmartCommitAsync(customDescription);
            return commitMessage;
        }
        catch (Exception ex)
        {
            return $"Error generating smart commit: {ex.Message}";
        }
    }
}