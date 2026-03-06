using Application.Common.Interfaces;
using Application.Common.Results;
using Core.AggregateRoots;
using Core.Interfaces.Repositories;
using Core.ValueObjects;
using MediatR;

namespace Application.Clients.Commands;

public static class CreateClient
{
    public record Command(string Cnpj, string Name) : ICommand<Result<Guid>>;
    
    public class Handler : IRequestHandler<Command, Result<Guid>>
    {
        private readonly IClientWriteRepository _repository;

        public Handler(IClientWriteRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<Guid>> Handle(Command request, CancellationToken cancellationToken)
        {
            ClientCnpj cnpj = new ClientCnpj(request.Cnpj);
            ClientName name = new ClientName(request.Name);

            bool cnpjExists = await _repository.ExistsByCnpjAsync(cnpj, cancellationToken);

            if (cnpjExists) 
                return Result<Guid>.Failure("Client already exists");

            var client = Client.Create(cnpj, name);
            await _repository.AddAsync(client, cancellationToken);

            return Result<Guid>.Success(client.Id);
        }
    }
}