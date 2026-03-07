using Shared.Results;

namespace Core.DomainErrors;

public static class NameErrors
{
    public static readonly Error NameEmpty =
        Error.Validation(
            "Name.Empty",
            "Nome não pode ser vazio");
    
    public static readonly Error NameTooLong =
        Error.Validation(
            "Name.MaxLength",
            "Nome não pode ter mais que 255 caracteres");
}