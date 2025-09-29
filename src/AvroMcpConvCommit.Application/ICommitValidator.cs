using AvroMcpConvCommit.Domain;

namespace AvroMcpConvCommit.Application;

public interface ICommitValidator
{
    ValidationResult Validate(string commitMessage);
}