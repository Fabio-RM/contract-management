using Application.Clients.Exceptions;
using Core.Interfaces.Repositories;
using Core.ValueObjects;
using MediatR;

namespace Application.Clients.Commands;

public static class RenameClient
{
    public record Command(Guid ClientId, string NewName) : IRequest<Unit>;
    public class Handler : IRequestHandler<Command, Unit>
    {
        private readonly IClientWriteRepository _repository;

        public Handler(IClientWriteRepository repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
        {
            ClientName newName = new ClientName(request.NewName);
            var client = await _repository.GetByIdAsync(request.ClientId, cancellationToken);

            if (client is null) throw new ClientNotFoundException();

            client.Rename(newName);

            return Unit.Value;
        }
    }   
}