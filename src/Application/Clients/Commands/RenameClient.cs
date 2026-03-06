using Application.Common.Interfaces;
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
            Name newName = new Name(request.NewName);
            var client = await _repository.GetByIdAsync(request.ClientId, cancellationToken);

            if (client is null) 
                return Result.Failure("Client not found");

            client.Rename(newName);

            return Result.Success();
        }
    }   
}