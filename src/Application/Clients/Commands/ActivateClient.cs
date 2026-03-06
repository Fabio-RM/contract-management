using Application.Common.Interfaces;
using Application.Common.Results;
using Core.Interfaces.Repositories;
using MediatR;

namespace Application.Clients.Commands;

public static class ActivateClient
{
    public record Command(Guid ClientId) : ICommand<Result<Unit>>;
    
    public class Handler : IRequestHandler<Command, Result<Unit>>
    {
        private readonly IClientWriteRepository _repository;
        
        public Handler(IClientWriteRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
        {
            var client = await _repository.GetByIdAsync(request.ClientId, cancellationToken);
        
            if (client is null) 
                return Result<Unit>.Failure("Client not found");
        
            client.Activate();
        
            return Result<Unit>.Success();
        }
    }
}