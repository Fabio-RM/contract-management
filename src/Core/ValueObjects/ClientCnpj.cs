using System.Text.RegularExpressions;
using Core.Common;

namespace Core.ValueObjects;

public class ClientCnpj : ValueObject
{
    private const int MaxLength = 14;

    public string Value { get; }

    public ClientCnpj(string value)
    {
        string normalizedCnpj = Regex.Replace(value, @"[./-]", "").Trim();

        if (string.IsNullOrWhiteSpace(normalizedCnpj))
            throw new ArgumentException("CNPJ value cannot be null or empty.", nameof(normalizedCnpj));

        if (normalizedCnpj.Length != MaxLength)
            throw new ArgumentException($"Invalid CNPJ length: {normalizedCnpj.Length}");

        if (!normalizedCnpj.All(c => char.IsDigit(c)))
            throw new ArgumentException($"CNPJ must contains only numbers");

        Value = normalizedCnpj;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}