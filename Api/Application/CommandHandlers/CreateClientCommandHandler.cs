using Api.Application.Commands;
using Api.Application.Exceptions;
using Api.Core.AggregateRoots;
using Api.Core.Interfaces.Repositories;
using Api.Core.ValueObjects;

namespace Api.Application.CommandHandlers;

public class CreateClientCommandHandler
{
    private readonly IClientRepository _repository;
    
    public CreateClientCommandHandler(IClientRepository repository)
    {
        _repository = repository;
    }

    public async Task<Guid> HandleAsync(CreateClientCommand command, CancellationToken cancellationToken)
    {
        ClientCnpj cnpj = new ClientCnpj(command.Cnpj);
        ClientName name = new ClientName(command.Name);
        
        bool cnpjExists = await _repository.ExistsByCnpjAsync(cnpj);
        if (cnpjExists)
            throw new ClientAlreadyExistsException($"Client {cnpj} already exists");
        
        var client = new Client(cnpj, name);
        await _repository.AddAsync(client);
        
        return client.Id;
    }
}