using Api.Core.ValueObjects;

namespace Api.Core.AggregateRoots;

public class Client
{
    public Guid Id { get; private  set; }
    public ClientCnpj Cnpj { get; private set; }
    public ClientName Name { get; private set; }
    public bool IsActive { get; private set; }

    public Client(ClientCnpj cnpj, ClientName name)
    {
        if (cnpj == null) throw new ArgumentNullException(nameof(cnpj));
        if (name == null) throw new ArgumentNullException(nameof(name));
        
        Id = Guid.NewGuid();
        Cnpj = cnpj;
        Name = name;
        IsActive = true;
    }

    public void ChangeClientName(ClientName name)
    {
        if (name == null) throw new ArgumentNullException(nameof(name));
        if (!IsActive) throw new InvalidOperationException("Client is not active");
        
        Name = name;
    }
    
    public void Deactivate()
    {
        if (!IsActive) throw new InvalidOperationException("Client is not active"); 
        IsActive = false;
    }

    public void Activate()
    {
        if (IsActive) throw new InvalidOperationException("Client is already active");
        IsActive = true;
    }
}