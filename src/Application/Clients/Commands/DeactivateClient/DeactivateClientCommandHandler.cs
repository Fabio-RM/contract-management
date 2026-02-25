using Application.Clients.Exceptions;
using Application.Common.Interfaces;
using Core.Interfaces.Repositories;
using MediatR;

namespace Application.Clients.Commands.DeactivateClient;

public class DeactivateClientCommandHandler : IRequestHandler<DeactivateClientCommand, Unit>
{
    private readonly IClientRepository _repository;
    private readonly IDateTimeProvider _dateTimeProvider;
    
    public DateTime UtcNow => DateTime.Now;
    
    public DeactivateClientCommandHandler(IClientRepository repository, IDateTimeProvider dateTimeProvider)
    {
        _repository = repository;
        _dateTimeProvider = dateTimeProvider;
    }
    
    public async Task<Unit> Handle(DeactivateClientCommand request, CancellationToken cancellationToken)
    {
        var client = await _repository.GetByIdAsync(request.Id, cancellationToken);
        
        if (client is null) throw new ClientNotFoundException();
        
        client.Deactivate(_dateTimeProvider.UtcNow);
        await _repository.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
}