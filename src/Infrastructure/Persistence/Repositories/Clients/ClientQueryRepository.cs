using Application.Clients.DTOs;
using Application.Clients.Interfaces.Repositories;
using Application.Clients.Queries;
using Application.Common.Pagination;
using Core.Common;
using Core.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories.Clients;

public class ClientQueryRepository : IClientQueryRepository
{
    private readonly AppDbContext _context;
    
    public ClientQueryRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<PagedResults<ClientDto>> GetAllClientsAsync(GetAllClients.Query query, CancellationToken cancellationToken)
    {
        var clientsQuery = _context.Clients
            .AsNoTracking()
            .AsQueryable();
        
        if (!string.IsNullOrWhiteSpace(query.CnpjFilter))
            clientsQuery = clientsQuery.Where(c => c.Cnpj.Value.Contains(query.CnpjFilter));
        
        if (!string.IsNullOrWhiteSpace(query.NameFilter))
            clientsQuery = clientsQuery.Where(c => c.Name.Value.Contains(query.NameFilter));

        if (!string.IsNullOrWhiteSpace(query.StatusFilter))
        {
            var status = Enumeration.FromDisplayName<ClientStatus>(query.StatusFilter);
            clientsQuery = clientsQuery.Where(c => c.Status == status);   
        }

        if (!string.IsNullOrWhiteSpace(query.OrderBy))
        {
            var descending = query.Descending ?? false;
            
            clientsQuery = query.OrderBy.ToLower() switch
            {
                "name" => descending
                    ? clientsQuery.OrderByDescending(c => c.Name.Value)
                    : clientsQuery.OrderBy(c => c.Name.Value),
                "cnpj" => descending
                    ? clientsQuery.OrderByDescending(c => c.Cnpj.Value)
                    : clientsQuery.OrderBy(c => c.Cnpj.Value),
                "status" => descending
                    ? clientsQuery.OrderByDescending(c => c.Status.DisplayName)
                    : clientsQuery.OrderBy(c => c.Status.DisplayName),
                _ => clientsQuery
            };
        }
        else    // If no order by provided, falls to default
        {
            clientsQuery = clientsQuery.OrderBy(c => c.Name.Value).ThenBy(c => c.Cnpj.Value);
        }
        
        var totalCount = await clientsQuery.CountAsync(cancellationToken);

        var results = clientsQuery
            .Select(c => new ClientDto(
                c.Id,
                c.Cnpj.Value,
                c.Name.Value,
                c.Status.DisplayName
            ));
        
        var items = await results
            .Skip(query.Skip)
            .Take(query.PageSize)
            .ToListAsync(cancellationToken);
        
        return new PagedResults<ClientDto>(
            items,
            totalCount,
            query.CurrentPage,
            query.PageSize);
    }

    public async Task<ClientDto?> GetByIdAsync(Guid clientId, CancellationToken cancellationToken)
    {
        return await _context.Clients
            .AsNoTracking()
            .Select(c => new ClientDto(
                c.Id,
                c.Cnpj.Value,
                c.Name.Value,
                c.Status.DisplayName)
            )
            .FirstOrDefaultAsync(c => c.Id == clientId, cancellationToken);
    }
}