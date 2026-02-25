using Application.Clients.Exceptions;
using Core.Interfaces.Repositories;
using MediatR;

namespace Application.Clients.Commands;

public static class ActivateClient
{
    public record Command(Guid ClientId) : IRequest<Unit>;
    
    public class Handler : IRequestHandler<Command, Unit>
    {
        private readonly IClientRepository _repository;
        
        public Handler(IClientRepository repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
        {
            var client = await _repository.GetByIdAsync(request.ClientId, cancellationToken);
        
            if (client is null) throw new ClientNotFoundException();
        
            client.Activate();
        
            return Unit.Value;
        }
    }
}