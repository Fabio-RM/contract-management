using Core.Exceptions;
using Core.ValueObjects;

namespace Core.AggregateRoots;

public class Client
{
    public Guid Id { get; private set; }
    public ClientCnpj Cnpj { get; private set; }
    public ClientName Name { get; private set; }
    public ClientStatus Status { get; private set; }
    public DateTime? DeletedAt { get; private set; }

    // To be used be EF Core
    private Client() { }
    private Client(ClientCnpj cnpj, ClientName name)
    {
        ArgumentNullException.ThrowIfNull(cnpj);
        ArgumentNullException.ThrowIfNull(name);

        Id = Guid.NewGuid();
        Cnpj = cnpj;
        Name = name;
        Status = ClientStatus.Active;
        DeletedAt = null;
    }

    // Factory Method
    public static Client Create(ClientCnpj cnpj, ClientName name)
    {
        return new Client(cnpj, name);
    }

    public void Rename(ClientName name)
    {
        ArgumentNullException.ThrowIfNull(name);
        if (Status.Equals(ClientStatus.Inactive)) throw new ClientInactiveException();

        Name = name;
    }

    public void Deactivate(DateTime utcNow)
    {
        if (Status.Equals(ClientStatus.Inactive)) throw new ClientInactiveException();
        Status = ClientStatus.Inactive;
        DeletedAt = utcNow;
    }

    public void Activate()
    {
        if (Status.Equals(ClientStatus.Active)) throw new ClientActiveException();
        Status = ClientStatus.Active;
        DeletedAt = null;
    }

    public bool IsActive()
    {
        return Status.Equals(ClientStatus.Active);
    }
    
    public string GetStatus()
    {
        return Status.DisplayName;
    }
}