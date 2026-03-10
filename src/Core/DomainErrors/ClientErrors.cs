using Shared.Results;

namespace Core.DomainErrors;

public static class ClientErrors
{
    public static readonly Error IsActive =
        Error.Conflict(
            "Client.IsActive", 
            "Cliente está ativo");
    
    public static readonly Error IsInactive =
        Error.Conflict(
            "Client.IsInactive", 
            "Cliente está inativo");
    
    public static readonly Error AlreadyExists =
        Error.Conflict(
            "Client.AlreadyExists",
            "Client já cadastrado");
    
    public static readonly Error NotFound =
        Error.NotFound(
            "Client.NotFound",
            "Cliente não encontrado");
}