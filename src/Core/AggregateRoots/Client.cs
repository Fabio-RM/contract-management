using Core.Exceptions;
using Core.Interfaces;
using Core.ValueObjects;

namespace Core.AggregateRoots;

public class Client
{
    private enum ClientStatus
    {
        Active,
        Inactive
    }

    public Guid Id { get; private set; }
    public ClientCnpj Cnpj { get; private set; }
    public ClientName Name { get; private set; }
    private ClientStatus Status { get; set; }
    public DateTime? DeletedAt { get; private set; }

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
        if (Status == ClientStatus.Inactive) throw new ClientInactiveException();

        Name = name;
    }

    public void Deactivate(DateTime utcNow)
    {
        if (Status == ClientStatus.Inactive) throw new ClientInactiveException();
        Status = ClientStatus.Inactive;
        DeletedAt = utcNow;
    }

    public void Activate()
    {
        if (Status == ClientStatus.Active) throw new ClientActiveException();
        Status = ClientStatus.Active;
        DeletedAt = null;
    }

    public bool IsActive()
    {
        return Status == ClientStatus.Active;
    }
    
    public string GetStatus()
    {
        return Status.ToString();
    }
}