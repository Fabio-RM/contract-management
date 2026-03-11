using System.Text.RegularExpressions;
using Core.Common;
using Core.DomainErrors;
using Shared.Results;

namespace Core.ValueObjects;

public class Cnpj : ValueObject
{
    private const int MaxLength = 14;

    public string Value { get; }

    private Cnpj(string value)
    {
        Value = value;
    }
    
    public static Result<Cnpj> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return Result<Cnpj>.Failure(CnpjErrors.CnpjEmpty);
        
        string normalizedCnpj = Regex.Replace(value, @"[./-]", "").Trim();

        if (normalizedCnpj.Length != MaxLength)
            return Result<Cnpj>.Failure(CnpjErrors.CnpjInvalidLength);

        if (!normalizedCnpj.All(char.IsDigit))
            return Result<Cnpj>.Failure(CnpjErrors.CnpjInvalidFormat);
        
        return Result<Cnpj>.Success(new Cnpj(normalizedCnpj));
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}