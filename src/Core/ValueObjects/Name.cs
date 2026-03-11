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
        if (string.IsNullOrWhiteSpace(value))
            return Result<Name>.Failure(NameErrors.NameEmpty);
        
        string normalizedName = value.Trim();

        if (normalizedName.Length > MaxLength)
            return Result<Name>.Failure(NameErrors.NameTooLong);
        
        return Result<Name>.Success(new Name(normalizedName));
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}