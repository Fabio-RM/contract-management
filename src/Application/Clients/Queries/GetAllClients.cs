using Application.Clients.DTOs;
using Application.Clients.Interfaces.Repositories;
using Application.Common.Interfaces;
using Application.Common.Pagination;
using MediatR;
using Shared.Results;

namespace Application.Clients.Queries;

public static class GetAllClients
{
    public record Query(
        string? CnpjFilter = null,
        string? NameFilter = null,
        string? StatusFilter = null,
        string? OrderBy = null,
        bool? Descending = false)
        : PagedQuery, IQuery<Result<PagedResults<ClientDto>>>;
    
    public class Handler : IRequestHandler<Query, Result<PagedResults<ClientDto>>>
    {
        private readonly IClientQueryRepository _repository;

        public Handler(IClientQueryRepository repository)
        {
            _repository = repository;
        }
    
        public async Task<Result<PagedResults<ClientDto>>> Handle(Query request, CancellationToken cancellationToken)
        {
            var result = await _repository.GetAllClientsAsync(request, cancellationToken);
            
            return Result<PagedResults<ClientDto>>.Success(result);
        }
    }
}