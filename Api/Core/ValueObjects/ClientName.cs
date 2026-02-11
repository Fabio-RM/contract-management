namespace Api.Core.ValueObjects;

public record ClientName
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
}