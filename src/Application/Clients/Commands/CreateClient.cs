using Application.Clients.Exceptions;
using Core.AggregateRoots;
using Core.Interfaces.Repositories;
using Core.ValueObjects;
using MediatR;

namespace Application.Clients.Commands;

public static class CreateClient
{
    public record Command(string Cnpj, string Name) : IRequest<Guid>;
    
    public class Handler : IRequestHandler<Command, Guid>
    {
        private readonly IClientRepository _repository;

        public Handler(IClientRepository repository)
        {
            _repository = repository;
        }

        public async Task<Guid> Handle(Command request, CancellationToken cancellationToken)
        {
            ClientCnpj cnpj = new ClientCnpj(request.Cnpj);
            ClientName name = new ClientName(request.Name);

            bool cnpjExists = await _repository.ExistsByCnpjAsync(cnpj, cancellationToken);

            if (cnpjExists) throw new ClientAlreadyExistsException();

            var client = Client.Create(cnpj, name);
            await _repository.AddAsync(client, cancellationToken);
            await _repository.SaveChangesAsync(cancellationToken);

            return client.Id;
        }
    }
}