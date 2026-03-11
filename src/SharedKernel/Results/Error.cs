namespace Shared.Results;

public sealed record Error(string Code, string Message)
{
    public static readonly Error None = new(string.Empty, string.Empty);

    public static Error Validation(string code, string message)
        => new($"Validation.{code}", message);

    public static Error NotFound(string code, string message)
        => new($"NotFound.{code}", message);

    public static Error Conflict(string code, string message)
        => new($"Conflict.{code}", message);
}