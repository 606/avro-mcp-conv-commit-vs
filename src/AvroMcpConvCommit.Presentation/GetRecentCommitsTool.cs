using ModelContextProtocol.Server;
using System.ComponentModel;
using AvroMcpConvCommit.Application;

[McpServerToolType]
public static class GetRecentCommitsTool
{
    [McpServerTool, Description("Retrieves recent commit messages from the git repository.")]
    public static async Task<string> GetRecentCommits(IGitService gitService, [Description("Number of recent commits to retrieve")] int count = 10)
    {
        var commits = await gitService.GetRecentCommitsAsync(count);
        if (commits.Count == 0)
        {
            return "No commits found or not in a git repository.";
        }

        return string.Join("\n", commits.Select((c, i) => $"{i + 1}. {c}"));
    }
}