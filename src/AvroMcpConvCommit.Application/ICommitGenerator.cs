using AvroMcpConvCommit.Domain;

namespace AvroMcpConvCommit.Application;

public interface ICommitGenerator
{
    CommitMessage Generate(string description, CommitType type, string? scope = null, string? body = null, string? footer = null, bool isBreaking = false);
    string GenerateConventionalString(CommitMessage message);
}