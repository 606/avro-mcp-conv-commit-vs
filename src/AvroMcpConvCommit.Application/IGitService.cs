using AvroMcpConvCommit.Domain;

namespace AvroMcpConvCommit.Application;

public interface IGitService
{
    Task<List<string>> GetRecentCommitsAsync(int count = 10);
    Task<string?> GetLastCommitMessageAsync();
    Task<List<string>> GetGitStatusAsync();
    Task<CommitType> AnalyzeChangesAsync();
}