using Application.Clients.DTOs;
using Application.Clients.Interfaces.Repositories;
using Application.Common.Pagination;
using MediatR;

namespace Application.Clients.Queries.GetAllClients;

public class GetAllClientsQueryHandler : IRequestHandler<GetAllClientsQuery, PagedResults<ClientDto>>
{
    private readonly IClientQueryRepository _repository;

    public GetAllClientsQueryHandler(IClientQueryRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<PagedResults<ClientDto>> Handle(GetAllClientsQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAllClientsAsync(cancellationToken);
    }
}