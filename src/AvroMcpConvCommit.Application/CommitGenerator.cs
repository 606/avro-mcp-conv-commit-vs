using AvroMcpConvCommit.Domain;

namespace AvroMcpConvCommit.Application;

public class CommitGenerator : ICommitGenerator
{
    public CommitMessage Generate(string description, CommitType type, string? scope = null, string? body = null, string? footer = null, bool isBreaking = false)
    {
        return new CommitMessage
        {
            Type = type,
            Scope = scope,
            Description = description,
            Body = body,
            Footer = footer,
            IsBreakingChange = isBreaking
        };
    }

    public string GenerateConventionalString(CommitMessage message)
    {
        return message.ToConventionalString();
    }
}