using Api.Application.Clients.Commands;
using Api.Application.Exceptions;
using Api.Core.AggregateRoots;
using Api.Core.Interfaces.Repositories;
using Api.Core.ValueObjects;
using MediatR;

namespace Api.Application.Clients.Handlers;

public class CreateClientCommandHandler  : IRequestHandler<CreateClientCommand, Guid>
{
    private readonly IClientRepository _repository;
    
    public CreateClientCommandHandler(IClientRepository repository)
    {
        _repository = repository;
    }

    public async Task<Guid> Handle(CreateClientCommand command, CancellationToken cancellationToken)
    {
        ClientCnpj cnpj = new ClientCnpj(command.Cnpj);
        ClientName name = new ClientName(command.Name);
        
        bool cnpjExists = await _repository.ExistsByCnpjAsync(cnpj, cancellationToken);
        
        if (cnpjExists) throw new ClientAlreadyExistsException();
        
        var client = Client.Create(cnpj, name);
        await _repository.AddAsync(client, cancellationToken);
        
        return client.Id;
    }
}