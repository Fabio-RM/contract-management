using Core.Common;
using Core.DomainErrors;
using Core.ValueObjects;
using Shared.Results;

namespace Core.AggregateRoots;

public class Client : AuditableEntity
{
    public Guid Id { get; private set; }
    public Cnpj ClientCnpj { get; private set; }
    public Name ClientName { get; private set; }
    public ClientStatus Status { get; private set; }
    public DateTime? DeletedAt { get; private set; }

    // To be used be EF Core
    private Client() { }
    private Client(Cnpj clientCnpj, Name clientName)
    {
        Id = Guid.NewGuid();
        ClientCnpj = clientCnpj;
        ClientName = clientName;
        Status = ClientStatus.Active;
        DeletedAt = null;
    }

    // Factory Method
    public static Result<Client> Create(string cnpj, string name)
    {
        var cnpjResult = Cnpj.Create(cnpj);
        var nameResult = Name.Create(name);
        
        var validation = Result.Validate(cnpjResult, nameResult);

        if (validation.IsFailure)
            return Result<Client>.Failure(validation.Errors);
        
        return Result<Client>.Success(new Client(cnpjResult.Value, nameResult.Value));
    }

    public Result Rename(Name newName)
    {
        if (Status.Equals(ClientStatus.Inactive))
            return Result.Failure(ClientErrors.IsInactive);

        ClientName = newName;
        
        return Result.Success();
    }

    public Result Deactivate(DateTime utcNow)
    {
        if (Status.Equals(ClientStatus.Inactive)) 
            return Result.Failure(ClientErrors.IsInactive);
        
        Status = ClientStatus.Inactive;
        DeletedAt = utcNow;
        
        return Result.Success();
    }

    public Result Activate()
    {
        if (Status.Equals(ClientStatus.Active))
            return Result.Failure(ClientErrors.IsActive);
        
        Status = ClientStatus.Active;
        DeletedAt = null;
        
        return Result.Success();
    }

    public bool IsActive()
    {
        return Status.Equals(ClientStatus.Active);
    }
}