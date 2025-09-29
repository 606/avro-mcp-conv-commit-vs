using AvroMcpConvCommit.Domain;

namespace AvroMcpConvCommit.Application;

public interface ICommitParser
{
    CommitMessage? Parse(string commitMessage);
}