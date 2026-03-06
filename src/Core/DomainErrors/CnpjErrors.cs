using Shared.Errors;

namespace Core.DomainErrors;

public static class CnpjErrors
{
    public static readonly Error CnpjEmpty =
        Error.Validation(
            "Cnpj.Empty",
            "CNPJ não pode ser vazio");

    public static readonly Error CnpjInvalidLength =
        Error.Validation(
            "Cnpj.InvalidLength",
            "CNPJ deve ter 14 dígitos");

    public static readonly Error CnpjInvalidFormat =
        Error.Validation(
            "Cnpj.InvalidFormat",
            "CNPJ deve conter apenas números");    
}