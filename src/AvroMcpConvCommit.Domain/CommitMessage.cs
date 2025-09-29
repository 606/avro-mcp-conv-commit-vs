namespace AvroMcpConvCommit.Domain;

public class CommitMessage
{
    public CommitType Type { get; set; }
    public string? Scope { get; set; }
    public string Description { get; set; } = string.Empty;
    public string? Body { get; set; }
    public string? Footer { get; set; }
    public bool IsBreakingChange { get; set; }

    public string ToConventionalString()
    {
        var type = Type.ToString().ToLower();
        var scope = string.IsNullOrEmpty(Scope) ? "" : $"({Scope})";
        var breaking = IsBreakingChange ? "!" : "";
        var footer = string.IsNullOrEmpty(Footer) ? "" : $"\n\n{Footer}";
        var body = string.IsNullOrEmpty(Body) ? "" : $"\n\n{Body}";

        return $"{type}{scope}{breaking}: {Description}{body}{footer}";
    }
}