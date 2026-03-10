using Application.Clients.Exceptions;
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
        private readonly IDateTimeProvider _dateTimeProvider;
    
        public Handler(IClientWriteRepository repository, IDateTimeProvider dateTimeProvider)
        {
            _repository = repository;
            _dateTimeProvider = dateTimeProvider;
        }
    
        public async Task<Result> Handle(Command request, CancellationToken cancellationToken)
        {
            var client = await _repository.GetByIdAsync(request.ClientId, cancellationToken);
        
            if (client is null) 
                return Result.Failure(ClientErrors.NotFound);
        
            client.Deactivate(_dateTimeProvider.UtcNow);
        
            return Result.Success();
        }
    }
}