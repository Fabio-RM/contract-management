using Api.Domain.AggregateRoots;
using Api.Domain.ValueObjects;

namespace Api.Domain.Interfaces.Repositories;

public interface IClientRepository
{
    public Task<Client> GetByIdAsync(Guid id);
    public Task<Client> GetByCnpjAsync(ClientCnpj cnpj);
    public Task AddAsync(Client client);
    public Task UpdateAsync(Client client);
}