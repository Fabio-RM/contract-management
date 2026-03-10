using Core.AggregateRoots;
using Core.ValueObjects;

namespace Core.Interfaces.Repositories;

public interface IClientWriteRepository
{
    public Task<Client?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    public Task AddAsync(Client client, CancellationToken cancellationToken);
    public Task<bool> ExistsByCnpjAsync(Cnpj cnpj, CancellationToken cancellationToken);
}