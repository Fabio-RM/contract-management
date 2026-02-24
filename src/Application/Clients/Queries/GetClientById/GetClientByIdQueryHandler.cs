using Application.Clients.DTOs;
using Application.Clients.Exceptions;
using Application.Clients.Interfaces.Repositories;
using MediatR;

namespace Application.Clients.Queries.GetClientById;

public class GetClientByIdQueryHandler : IRequestHandler<GetClientByIdQuery, ClientDto>
{
    public readonly IClientQueryRepository _repository;

    public GetClientByIdQueryHandler(IClientQueryRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<ClientDto> Handle(GetClientByIdQuery request, CancellationToken cancellationToken)
    {
        var client = await _repository.GetByIdAsync(request.clientId, cancellationToken);

        if (client == null) throw new ClientNotFoundException();

        return new ClientDto(
            client.Id,
            client.Cnpj,
            client.Name,
            client.Status
        );
    }
}