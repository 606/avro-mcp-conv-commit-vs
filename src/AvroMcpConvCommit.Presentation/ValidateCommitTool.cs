using ModelContextProtocol.Server;
using System.ComponentModel;
using AvroMcpConvCommit.Application;

[McpServerToolType]
public static class ValidateCommitTool
{
    [McpServerTool, Description("Validates a commit message against conventional commit standards.")]
    public static string ValidateCommit(ICommitValidator validator, [Description("The commit message to validate")] string commitMessage)
    {
        var result = validator.Validate(commitMessage);
        if (result.IsValid)
        {
            return "Commit message is valid.";
        }
        else
        {
            return $"Commit message is invalid: {string.Join(", ", result.Errors)}";
        }
    }
}