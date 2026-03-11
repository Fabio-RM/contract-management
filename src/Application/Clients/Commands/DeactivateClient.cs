using Application.Common.Interfaces;
using Core.DomainErrors;
using Core.Interfaces.Repositories;
using MediatR;
using Shared.Results;

namespace Application.Clients.Commands;

public static class DeactivateClient
{
    public record Command(Guid ClientId) : ICommand<Result>;
    public class Handler : IRequestHandler<Command, Result>
    {
        private readonly IClientWriteRepository _repository;
    
        public Handler(IClientWriteRepository repository)
        {
            _repository = repository;
        }
    
        public async Task<Result> Handle(Command request, CancellationToken cancellationToken)
        {
            var client = await _repository.GetByIdAsync(request.ClientId, cancellationToken);
        
            if (client is null) 
                return Result.Failure(ClientErrors.NotFound);
        
            var result = client.Deactivate(DateTime.UtcNow);
            
            if (result.IsFailure)
                return Result.Failure(result.Errors);
        
            return Result.Success();
        }
    }
}