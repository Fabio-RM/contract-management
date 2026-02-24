using Application.Clients.Queries.Models;

namespace Application.Clients.Interfaces.Repositories;

public interface IClientQueryRepository
{
    Task<ClientReadModel?> GetByIdAsync(Guid clientId, CancellationToken cancellationToken);
}