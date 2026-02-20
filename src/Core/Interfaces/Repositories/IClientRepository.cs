using Core.AggregateRoots;
using Core.ValueObjects;

namespace Core.Interfaces.Repositories;

public interface IClientRepository
{
    public Task<Client?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    public Task<Client?> GetByCnpjAsync(ClientCnpj cnpj, CancellationToken cancellationToken);
    public Task AddAsync(Client client, CancellationToken cancellationToken);
    public Task DeleteAsync(Client client, CancellationToken cancellationToken);
    public Task SaveChangesAsync(CancellationToken cancellationToken);
    public Task<bool> ExistsByCnpjAsync(ClientCnpj cnpj, CancellationToken cancellationToken);
}