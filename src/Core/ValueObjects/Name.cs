using Core.Common;
using Core.DomainErrors;
using Shared.Results;

namespace Core.ValueObjects;

public class Name : ValueObject
{
    private const int MaxLength = 255;
    public string Value { get; }

    private Name(string value)
    {
        Value = value;
    }

    public static Result<Name> Create(string value)
    {
        var errors = new List<Error>();
        
        string normalizedName = value.Trim();

        if (string.IsNullOrWhiteSpace(normalizedName))
            return Result<Name>.Failure(NameErrors.NameEmpty);

        if (normalizedName.Length > MaxLength)
            return Result<Name>.Failure(NameErrors.NameTooLong);
        
        return Result<Name>.Success(new Name(normalizedName));
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}