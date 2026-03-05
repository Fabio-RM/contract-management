using Application.Clients.DTOs;
using Application.Clients.Interfaces.Repositories;
using Application.Common.Interfaces;
using Application.Common.Pagination;
using MediatR;

namespace Application.Clients.Queries;

public static class GetAllClients
{
    public record Query(
        string? CnpjFilter = null,
        string? NameFilter = null,
        string? StatusFilter = null,
        string? OrderBy = null,
        bool? Descending = false)
        : PagedQuery, IQuery<PagedResults<ClientDto>>;
    
    public class Handler : IRequestHandler<Query, PagedResults<ClientDto>>
    {
        private readonly IClientQueryRepository _repository;

        public Handler(IClientQueryRepository repository)
        {
            _repository = repository;
        }
    
        public async Task<PagedResults<ClientDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            return await _repository.GetAllClientsAsync(request, cancellationToken);
        }
    }
}