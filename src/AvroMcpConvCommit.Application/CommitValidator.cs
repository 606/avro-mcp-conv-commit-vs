using AvroMcpConvCommit.Domain;

namespace AvroMcpConvCommit.Application;

public class CommitValidator : ICommitValidator
{
    public ValidationResult Validate(string commitMessage)
    {
        var result = new ValidationResult { IsValid = true };

        if (string.IsNullOrWhiteSpace(commitMessage))
        {
            result.IsValid = false;
            result.Errors.Add("Commit message cannot be empty.");
            return result;
        }

        // Basic conventional commit validation
        var lines = commitMessage.Split('\n');
        var header = lines[0];

        // Check for type(scope): description
        var match = System.Text.RegularExpressions.Regex.Match(header, @"^(\w+)(\([^)]+\))?!?:\s*(.+)$");
        if (!match.Success)
        {
            result.IsValid = false;
            result.Errors.Add("Header must be in format: type(scope)!: description");
            return result;
        }

        var typeStr = match.Groups[1].Value.ToLower();
        var validTypes = Enum.GetNames(typeof(CommitType)).Select(t => t.ToLower()).ToList();
        if (!validTypes.Contains(typeStr))
        {
            result.IsValid = false;
            result.Errors.Add($"Invalid type: {typeStr}. Valid types: {string.Join(", ", validTypes)}");
        }

        var description = match.Groups[3].Value;
        if (description.Length > 72)
        {
            result.Errors.Add("Description should be 72 characters or less.");
        }

        return result;
    }
}