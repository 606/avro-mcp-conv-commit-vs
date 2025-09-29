using AvroMcpConvCommit.Application;
using AvroMcpConvCommit.Domain;

namespace AvroMcpConvCommit.Infrastructure;

public class GitService : IGitService
{
    public async Task<List<string>> GetRecentCommitsAsync(int count = 10)
    {
        var commits = new List<string>();
        try
        {
            var process = new System.Diagnostics.Process
            {
                StartInfo = new System.Diagnostics.ProcessStartInfo
                {
                    FileName = "git",
                    Arguments = $"log --oneline -{count}",
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                }
            };
            process.Start();
            var output = await process.StandardOutput.ReadToEndAsync();
            await process.WaitForExitAsync();

            commits = output.Split('\n').Where(line => !string.IsNullOrWhiteSpace(line)).ToList();
        }
        catch
        {
            // If git not available, return empty
        }
        return commits;
    }

    public async Task<string?> GetLastCommitMessageAsync()
    {
        try
        {
            var process = new System.Diagnostics.Process
            {
                StartInfo = new System.Diagnostics.ProcessStartInfo
                {
                    FileName = "git",
                    Arguments = "log -1 --pretty=%B",
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                }
            };
            process.Start();
            var output = await process.StandardOutput.ReadToEndAsync();
            await process.WaitForExitAsync();

            return output.Trim();
        }
        catch
        {
            return null;
        }
    }

    public async Task<List<string>> GetGitStatusAsync()
    {
        var status = new List<string>();
        try
        {
            var process = new System.Diagnostics.Process
            {
                StartInfo = new System.Diagnostics.ProcessStartInfo
                {
                    FileName = "git",
                    Arguments = "status --porcelain",
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                }
            };
            process.Start();
            var output = await process.StandardOutput.ReadToEndAsync();
            await process.WaitForExitAsync();

            status = output.Split('\n').Where(line => !string.IsNullOrWhiteSpace(line)).ToList();
        }
        catch
        {
            // If git not available, return empty
        }
        return status;
    }

    public async Task<CommitType> AnalyzeChangesAsync()
    {
        var status = await GetGitStatusAsync();
        
        if (status.Count == 0)
        {
            return CommitType.Chore; // No changes
        }

        // Analyze file extensions and paths to determine commit type
        var hasTests = status.Any(line => line.Contains(".test.") || line.Contains(".spec.") || 
                                        line.Contains("test/") || line.Contains("tests/"));
        var hasDocs = status.Any(line => line.Contains(".md") || line.Contains(".txt") || 
                                     line.Contains(".rst") || line.Contains("docs/") ||
                                     line.Contains("README") || line.Contains("CHANGELOG"));
        var hasConfig = status.Any(line => line.Contains(".json") || line.Contains(".yaml") || 
                                      line.Contains(".yml") || line.Contains(".config") ||
                                      line.Contains(".ini") || line.Contains(".toml") ||
                                      line.Contains(".xml") || line.Contains("Dockerfile") ||
                                      line.Contains(".dockerfile"));
        var hasCode = status.Any(line => line.Contains(".cs") || line.Contains(".js") || 
                                   line.Contains(".ts") || line.Contains(".py") || 
                                   line.Contains(".java") || line.Contains(".cpp") ||
                                   line.Contains(".c") || line.Contains(".h") ||
                                   line.Contains(".php") || line.Contains(".rb") ||
                                   line.Contains(".go") || line.Contains(".rs"));
        var hasBuild = status.Any(line => line.Contains("package.json") || line.Contains("requirements.txt") ||
                                   line.Contains("setup.py") || line.Contains("pom.xml") ||
                                   line.Contains("build.gradle") || line.Contains("Makefile") ||
                                   line.Contains(".csproj") || line.Contains(".sln") ||
                                   line.Contains("Cargo.toml") || line.Contains("go.mod"));

        // Determine commit type based on changes
        if (hasTests && !hasCode && !hasDocs && !hasConfig)
        {
            return CommitType.Test;
        }
        if (hasDocs && !hasCode && !hasTests && !hasConfig)
        {
            return CommitType.Docs;
        }
        if (hasConfig && !hasCode && !hasTests && !hasDocs)
        {
            return CommitType.Chore;
        }
        if (hasBuild && !hasCode && !hasTests && !hasDocs && !hasConfig)
        {
            return CommitType.Build;
        }
        if (hasCode)
        {
            // For code changes, we need to check if it's a new feature or fix
            // This is a simple heuristic - in a real implementation, you might
            // want to analyze the actual code changes
            var addedFiles = status.Count(line => line.StartsWith("A ") || line.StartsWith("?? "));
            var modifiedFiles = status.Count(line => line.StartsWith("M "));
            var deletedFiles = status.Count(line => line.StartsWith("D "));
            
            if (addedFiles > modifiedFiles && addedFiles > deletedFiles)
            {
                return CommitType.Feat; // Mostly additions = new features
            }
            else
            {
                return CommitType.Fix; // Mostly modifications = fixes
            }
        }

        // Default fallback
        return CommitType.Chore;
    }
}