using Api.Core.AggregateRoots;
using Api.Core.ValueObjects;

namespace Api.Core.Interfaces.Repositories;

public interface IClientRepository
{
    public Task<Client?> GetByIdAsync(Guid id);
    public Task<Client?> GetByCnpjAsync(ClientCnpj cnpj);
    public Task AddAsync(Client client);
    public Task UpdateAsync(Client client);
    public Task<bool> ExistsByCnpjAsync(ClientCnpj cnpj);
}