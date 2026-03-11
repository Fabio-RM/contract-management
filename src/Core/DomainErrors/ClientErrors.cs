using Shared.Results;

namespace Core.DomainErrors;

public static class ClientErrors
{
    public static readonly Error AlreadyActive =
        Error.Conflict(
            "Client.AlreadyActive", 
            "Cliente já está ativo");
    
    public static readonly Error AlreadyInactive =
        Error.Conflict(
            "Client.AlreadyInactive", 
            "Cliente já está inativo");
    
    public static readonly Error AlreadyExists =
        Error.Conflict(
            "Client.AlreadyExists",
            "Client já cadastrado");
    
    public static readonly Error NotFound =
        Error.NotFound(
            "Client.NotFound",
            "Cliente não encontrado");
}