using AvroMcpConvCommit.Domain;

namespace AvroMcpConvCommit.Application;

public interface ISmartCommitGenerator
{
    Task<string> GenerateSmartCommitAsync(string? customDescription = null);
}