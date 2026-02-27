using Application.Clients.Exceptions;
using Application.Common.Interfaces;
using Core.Interfaces.Repositories;
using MediatR;

namespace Application.Clients.Commands;

public static class DeactivateClient
{
    public record Command(Guid ClientId) : ICommand<Unit>;
    public class Handler : IRequestHandler<Command, Unit>
    {
        private readonly IClientWriteRepository _repository;
        private readonly IDateTimeProvider _dateTimeProvider;
    
        public DateTime UtcNow => DateTime.Now;
    
        public Handler(IClientWriteRepository repository, IDateTimeProvider dateTimeProvider)
        {
            _repository = repository;
            _dateTimeProvider = dateTimeProvider;
        }
    
        public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
        {
            var client = await _repository.GetByIdAsync(request.ClientId, cancellationToken);
        
            if (client is null) throw new ClientNotFoundException();
        
            client.Deactivate(_dateTimeProvider.UtcNow);
        
            return Unit.Value;
        }
    }
}