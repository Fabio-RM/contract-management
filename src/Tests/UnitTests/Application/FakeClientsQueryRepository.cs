using Application.Clients.DTOs;
using Application.Clients.Interfaces.Repositories;
using Application.Clients.Queries;
using Application.Common.Pagination;

namespace Tests.UnitTests.Application;

public class FakeClientsQueryRepository : IClientQueryRepository
{
    private readonly List<ClientDto> _clients = new();

    public FakeClientsQueryRepository(ClientDto[] clientsDtoToAdd)
    {
        _clients.AddRange(clientsDtoToAdd);
    }
    
    public Task<PagedResults<ClientDto>> GetAllClientsAsync(GetAllClients.Query query, CancellationToken cancellationToken)
    {
        // Apply filters if any
        IQueryable<ClientDto> clients = _clients.AsQueryable();
        
        if (!string.IsNullOrWhiteSpace(query.CnpjFilter))
            clients = clients.Where(c => c.Cnpj.Contains(query.CnpjFilter));
        if (!string.IsNullOrWhiteSpace(query.NameFilter))
            clients = clients.Where(c => c.Name.Contains(query.NameFilter));
        if (!string.IsNullOrWhiteSpace(query.StatusFilter))
            clients = clients.Where(c => c.Status.Equals(query.StatusFilter));
        
        var total = clients.Count();
        
        var items = clients
            .Skip((query.CurrentPage - 1) * query.PageSize)
            .Take(query.PageSize)
            .ToList();
        
        return Task.FromResult(
            new PagedResults<ClientDto>(
                items,
                total,
                query.CurrentPage,
                query.PageSize)
            );
    }

    public Task<ClientDto?> GetByIdAsync(Guid clientId, CancellationToken cancellationToken)
    {
        return Task.FromResult(_clients.FirstOrDefault(c => c.Id == clientId));
        
    }
}