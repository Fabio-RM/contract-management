using Application.Common.Interfaces;
using Core.AggregateRoots;
using Core.DomainErrors;
using Core.Interfaces.Repositories;
using MediatR;
using Shared.Results;

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
            var clientResult = Client.Create(request.Cnpj, request.Name);
            
            if (clientResult.IsFailure)
                return Result<Guid>.Failure(clientResult.Errors);
            
            bool cnpjExists = await _repository.ExistsByCnpjAsync(clientResult.Value.ClientCnpj, cancellationToken);
            if (cnpjExists)
                return Result<Guid>.Failure(ClientErrors.AlreadyExists);
            
            await _repository.AddAsync(clientResult.Value, cancellationToken);

            return Result<Guid>.Success(clientResult.Value.Id);
        }
    }
}