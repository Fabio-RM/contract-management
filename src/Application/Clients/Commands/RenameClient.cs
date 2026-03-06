using Application.Common.Interfaces;
using Core.Interfaces.Repositories;
using Core.ValueObjects;
using MediatR;
using Shared.Results;

namespace Application.Clients.Commands;

public static class RenameClient
{
    public record Command(Guid ClientId, string NewName) : ICommand<Result<Unit>>;
    public class Handler : IRequestHandler<Command, Result<Unit>>
    {
        private readonly IClientWriteRepository _repository;

        public Handler(IClientWriteRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
        {
            ClientName newName = new ClientName(request.NewName);
            var client = await _repository.GetByIdAsync(request.ClientId, cancellationToken);

            if (client is null) 
                return Result<Unit>.Failure("Client not found");

            client.Rename(newName);

            return Result<Unit>.Success(Unit.Value);
        }
    }   
}