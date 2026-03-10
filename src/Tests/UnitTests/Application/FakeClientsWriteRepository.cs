using Core.AggregateRoots;
using Core.Interfaces.Repositories;
using Core.ValueObjects;

namespace Tests.UnitTests.Application;

public class FakeClientsWriteRepository : IClientWriteRepository
{
    private readonly List<Client> _clients = new();
    
    public Task<Client?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        foreach (var client in _clients)
        {
            if (client.Id == id)
                return Task.FromResult<Client?>(client);
        }
        
        return Task.FromResult<Client?>(null);
    }

    public Task AddAsync(Client client, CancellationToken cancellationToken)
    {
        _clients.Add(client);
        return Task.CompletedTask;
    }

    public Task<bool> ExistsByCnpjAsync(Cnpj cnpj, CancellationToken cancellationToken)
    {
        if (_clients.Any(client => client.ClientCnpj.Equals(cnpj)))
            return Task.FromResult(true);
        
        return Task.FromResult(false);
    }
}