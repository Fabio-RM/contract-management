using Application.Clients.DTOs;
using Application.Common.Pagination;
using MediatR;

namespace Application.Clients.Queries.GetAllClients;

public record GetAllClientsQuery() : PagedQuery, IRequest<PagedResults<ClientDto>>
{
    public string? CnpjFilter { get; init; } = string.Empty;
    public string? NameFilter { get; init; } = string.Empty;
    public string? StatusFilter { get; init; } = string.Empty;
}