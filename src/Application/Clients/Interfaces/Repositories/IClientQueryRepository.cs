using Application.Clients.DTOs;
using Application.Clients.Queries;
using Application.Common.Pagination;

namespace Application.Clients.Interfaces.Repositories;

public interface IClientQueryRepository
{
    public Task<PagedResults<ClientDto>> GetAllClientsAsync(GetAllClients.Query query, CancellationToken cancellationToken);
    public Task<ClientDto?> GetByIdAsync(Guid clientId, CancellationToken cancellationToken);
}