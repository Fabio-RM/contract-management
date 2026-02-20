using Core.Common;

namespace Core.ValueObjects;

public class ClientName : ValueObject
{
    private const int MaxLength = 255;
    public string Name { get; }

    public ClientName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name cannot be empty", nameof(name));

        if (name.Length > MaxLength)
            throw new ArgumentException($"Name cannot be longer than {MaxLength} characters", nameof(name));

        Name = name;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Name;
    }
}