using Application.Clients.DTOs;
using Application.Clients.Interfaces.Repositories;
using Application.Common.Interfaces;
using Application.Common.Pagination;
using MediatR;

namespace Application.Clients.Queries;

public static class GetAllClients
{
    public record Query() : PagedQuery, IQuery<PagedResults<ClientDto>>
    {
        public string? CnpjFilter { get; init; } = string.Empty;
        public string? NameFilter { get; init; } = string.Empty;
        public string? StatusFilter { get; init; } = string.Empty;
        public string? OrderBy { get; init; } = string.Empty;
        public bool? Descending { get; init; } = false;
    }
    
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