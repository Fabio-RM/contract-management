using Core.AggregateRoots;
using Core.Interfaces.Repositories;
using Core.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories.Clients;

public class ClientWriteRepository : IClientWriteRepository
{
    private readonly AppDbContext _context;
    
    public ClientWriteRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Client?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.Clients.FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
    }

    public async Task AddAsync(Client client, CancellationToken cancellationToken)
    {
         await _context.Clients.AddAsync(client, cancellationToken);
    }

    public async Task<bool> ExistsByCnpjAsync(Cnpj cnpj, CancellationToken cancellationToken)
    {
        return await _context.Clients.AnyAsync(c => c.ClientCnpj.Value == cnpj.Value, cancellationToken);
    }
}