using Application.Clients.DTOs;
using Application.Clients.Exceptions;
using Application.Clients.Interfaces.Repositories;
using Application.Common.Pagination;
using MediatR;

namespace Application.Clients.Queries.GetClientByName;

public class GetClientByNameQueryHandler : IRequestHandler<GetClientByNameQuery, PagedResults<ClientDto>>
{
    private readonly IClientQueryRepository _repository;

    public GetClientByNameQueryHandler(IClientQueryRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<PagedResults<ClientDto>> Handle(GetClientByNameQuery request, CancellationToken cancellationToken)
    {
        var client = await _repository.GetByNameAsync(request.name, cancellationToken);

        if (client == null) throw new ClientNotFoundException();

        return client;
    }
}