using Application.Clients.DTOs;
using Application.Clients.Queries.Models;
using Application.Common.Pagination;

namespace Application.Clients.Interfaces.Repositories;

public interface IClientQueryRepository
{
    Task<PagedResults<ClientDto>> GetAllClientsAsync(CancellationToken cancellationToken);
    Task<ClientReadModel?> GetByIdAsync(Guid clientId, CancellationToken cancellationToken);
    Task<ClientReadModel?> GetByCnpjAsync(string cnpj, CancellationToken cancellationToken);
    Task<PagedResults<ClientDto>> GetByNameAsync(string name, CancellationToken cancellationToken);
}