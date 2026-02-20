using Application.Clients.Commands;
using Application.Clients.Exceptions;
using Core.Interfaces.Repositories;
using Core.ValueObjects;
using MediatR;

namespace Application.Clients.Handlers;

public class RenameClientCommandHandler : IRequestHandler<RenameClientCommand, Unit>
{
    private readonly IClientRepository _repository;

    public RenameClientCommandHandler(IClientRepository repository)
    {
        _repository = repository;
    }

    public async Task<Unit> Handle(RenameClientCommand command, CancellationToken cancellationToken)
    {
        ClientName newName = new ClientName(command.NewName);
        var client = await _repository.GetByIdAsync(command.Id, cancellationToken);

        if (client is null) throw new ClientNotFoundException();

        client.Rename(newName);
        await _repository.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}