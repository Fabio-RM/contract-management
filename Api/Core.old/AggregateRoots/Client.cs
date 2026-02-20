using Api.Core.Exceptions;
using Api.Core.ValueObjects;

namespace Api.Core.AggregateRoots;

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

    private Client(ClientCnpj cnpj, ClientName name)
    {
        ArgumentNullException.ThrowIfNull(cnpj);
        ArgumentNullException.ThrowIfNull(name);

        Id = Guid.NewGuid();
        Cnpj = cnpj;
        Name = name;
        Status = ClientStatus.Active;
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

    public void Deactivate()
    {
        if (Status == ClientStatus.Inactive) throw new ClientInactiveException();
        Status = ClientStatus.Inactive;
    }

    public void Activate()
    {
        if (Status == ClientStatus.Active) throw new ClientActiveException();
        Status = ClientStatus.Active;
    }
}