using Core.Common;

namespace Core.ValueObjects;

public class ClientName : ValueObject
{
    private const int MaxLength = 255;
    public string Value { get; }

    public ClientName(string value)
    {
        string normalizedName = value.Trim();
        
        if (string.IsNullOrWhiteSpace(normalizedName))
            throw new ArgumentException("Name cannot be empty", nameof(normalizedName));

        if (normalizedName.Length > MaxLength)
            throw new ArgumentException($"Name cannot be longer than {MaxLength} characters", nameof(normalizedName));

        Value = normalizedName;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}