using Shared.Errors;

namespace Core.DomainErrors;

public static class NameErrors
{
    public static readonly Error NameEmpty =
        Error.Validation(
            "Name.Empty",
            "Nome não pode ser vazio");
}