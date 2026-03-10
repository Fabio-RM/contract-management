using Application.Common.Interfaces;
using Core.DomainErrors;
using Core.Interfaces.Repositories;
using Core.ValueObjects;
using MediatR;
using Shared.Results;

namespace Application.Clients.Commands;

public static class RenameClient
{
    public record Command(Guid ClientId, string NewName) : ICommand<Result>;
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

            if (client == null) 
                return Result.Failure(ClientErrors.NotFound);

            var newName = Name.Create(request.NewName);
            
            client.Rename(newName.Value);

            return Result.Success();
        }
    }   
}