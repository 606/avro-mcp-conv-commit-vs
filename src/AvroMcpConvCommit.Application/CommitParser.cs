using AvroMcpConvCommit.Domain;

namespace AvroMcpConvCommit.Application;

public class CommitParser : ICommitParser
{
    public CommitMessage? Parse(string commitMessage)
    {
        if (string.IsNullOrWhiteSpace(commitMessage))
            return null;

        var lines = commitMessage.Split('\n');
        var header = lines[0];

        var match = System.Text.RegularExpressions.Regex.Match(header, @"^(\w+)(\([^)]+\))?(!)?:\s*(.+)$");
        if (!match.Success)
            return null;

        var typeStr = match.Groups[1].Value;
        if (!Enum.TryParse<CommitType>(typeStr, true, out var type))
            return null;

        var scope = match.Groups[2].Success ? match.Groups[2].Value.Trim('(', ')') : null;
        var isBreaking = match.Groups[3].Success;
        var description = match.Groups[4].Value;

        var body = lines.Length > 1 && !string.IsNullOrWhiteSpace(lines[1]) ? string.Join("\n", lines.Skip(1)) : null;

        return new CommitMessage
        {
            Type = type,
            Scope = scope,
            Description = description,
            Body = body,
            IsBreakingChange = isBreaking
        };
    }
}