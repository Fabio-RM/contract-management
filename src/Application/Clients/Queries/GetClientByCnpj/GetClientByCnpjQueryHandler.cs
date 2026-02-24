using Application.Clients.DTOs;
using Application.Clients.Exceptions;
using Application.Clients.Interfaces.Repositories;
using MediatR;

namespace Application.Clients.Queries.GetClientByCnpj;

public class GetClientByCnpjQueryHandler : IRequestHandler<GetClientByCnpjQuery, ClientDto>
{
    private readonly IClientQueryRepository _repository;

    public GetClientByCnpjQueryHandler(IClientQueryRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<ClientDto> Handle(GetClientByCnpjQuery request, CancellationToken cancellationToken)
    {
        var client = await _repository.GetByCnpjAsync(request.ClientCnpj, cancellationToken);

        if (client == null) throw new ClientNotFoundException();
        
        return new ClientDto(
            client.Id,
            client.Cnpj,
            client.Name,
            client.Status);
    }
}