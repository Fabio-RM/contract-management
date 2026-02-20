using System.Text.RegularExpressions;
using Core.Common;

namespace Core.ValueObjects;

public class ClientCnpj : ValueObject
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

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Cnpj;
    }
}