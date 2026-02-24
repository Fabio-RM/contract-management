using Application.Clients.Exceptions;
using Core.Interfaces.Repositories;
using Core.ValueObjects;
using MediatR;

namespace Application.Clients.Commands.RenameClient;

public class RenameClientCommandHandler : IRequestHandler<RenameClientCommand, Unit>
{
    private readonly IClientRepository _repository;

    public RenameClientCommandHandler(IClientRepository repository)
    {
        _repository = repository;
    }

    public async Task<Unit> Handle(RenameClientCommand request, CancellationToken cancellationToken)
    {
        ClientName newName = new ClientName(request.NewName);
        var client = await _repository.GetByIdAsync(request.Id, cancellationToken);

        if (client is null) throw new ClientNotFoundException();

        client.Rename(newName);
        await _repository.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}