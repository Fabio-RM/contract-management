using Application.Clients.DTOs;
using Application.Clients.Exceptions;
using Application.Clients.Interfaces.Repositories;
using Application.Common.Interfaces;
using Core.DomainErrors;
using MediatR;
using Shared.Results;

namespace Application.Clients.Queries;

public static class GetClientById
{
    public record Query(Guid ClientId) : IQuery<Result<ClientDto?>>
    {
        public Guid ClientId { get; } = ClientId;
    }
    
    public class Handler : IRequestHandler<Query, Result<ClientDto?>>
    {
        public readonly IClientQueryRepository _repository;

        public Handler(IClientQueryRepository repository)
        {
            _repository = repository;
        }
    
        public async Task<Result<ClientDto?>> Handle(Query request, CancellationToken cancellationToken)
        {
            var client = await _repository.GetByIdAsync(request.ClientId, cancellationToken);

            if (client == null) 
                return Result<ClientDto?>.Failure(ClientErrors.NotFound);

            var dto = new ClientDto(
                client.Id,
                client.Cnpj,
                client.Name,
                client.Status
            );
            
            return Result<ClientDto?>.Success(dto);
        }
    }
}