using System.Text.RegularExpressions;

namespace Core.ValueObjects;

public record ClientCnpj
{
    private const int MaxLength = 14;

    public string Cnpj { get; }

    public ClientCnpj(string cnpj)
    {
        string normalizedCnpj = Regex.Replace(cnpj, @"[./-]", "").Trim();

        if (string.IsNullOrWhiteSpace(normalizedCnpj))
            throw new ArgumentException("CNPJ value cannot be null or empty.", nameof(normalizedCnpj));

        if (normalizedCnpj.Length != MaxLength)
            throw new ArgumentException($"Invalid CNPJ length: {normalizedCnpj.Length}");

        if (!normalizedCnpj.All(c => char.IsDigit(c)))
            throw new ArgumentException($"CNPJ must contains only numbers");

        Cnpj = normalizedCnpj;
    }
}