using Application.Clients.DTOs;
using Application.Clients.Exceptions;
using Application.Clients.Interfaces.Repositories;
using Application.Common.Interfaces;
using MediatR;

namespace Application.Clients.Queries;

public static class GetClientById
{
    public record Query(Guid ClientId) : IQuery<ClientDto>
    {
        public Guid ClientId { get; } = ClientId;
    }
    
    public class Handler : IRequestHandler<Query, ClientDto?>
    {
        public readonly IClientQueryRepository _repository;

        public Handler(IClientQueryRepository repository)
        {
            _repository = repository;
        }
    
        public async Task<ClientDto?> Handle(Query request, CancellationToken cancellationToken)
        {
            var client = await _repository.GetByIdAsync(request.ClientId, cancellationToken);

            if (client == null) throw new ClientNotFoundException();

            return new ClientDto(
                client.Id,
                client.Cnpj,
                client.Name,
                client.Status
            );
        }
    }
}