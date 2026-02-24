using Application.Clients.DTOs;
using Application.Clients.Queries.GetAllClients;
using Application.Clients.Queries.Models;
using Application.Common.Pagination;

namespace Application.Clients.Interfaces.Repositories;

public interface IClientQueryRepository
{
    Task<PagedResults<ClientDto>> GetAllClientsAsync(GetAllClientsQuery query, CancellationToken cancellationToken);
    Task<ClientReadModel?> GetByIdAsync(Guid clientId, CancellationToken cancellationToken);
}