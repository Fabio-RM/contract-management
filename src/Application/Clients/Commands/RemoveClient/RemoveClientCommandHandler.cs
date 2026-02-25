using Application.Clients.Exceptions;
using Core.Interfaces.Repositories;
using MediatR;

namespace Application.Clients.Commands.RemoveClient;

public class RemoveClientCommandHandler : IRequestHandler<RemoveClientCommand, Unit>
{
    private readonly IClientRepository _repository;
    
    public RemoveClientCommandHandler(IClientRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<Unit> Handle(RemoveClientCommand request, CancellationToken cancellationToken)
    {
        var client = await _repository.GetByIdAsync(request.Id, cancellationToken);
        
        if (client is null) throw new ClientNotFoundException();
        
        await _repository.RemoveAsync(client, cancellationToken);
        await _repository.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
}