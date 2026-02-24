using Application.Clients.DTOs;
using Application.Common.Pagination;
using MediatR;

namespace Application.Clients.Queries.GetClientByName;

public record GetClientByNameQuery(string name) : PagedQuery, IRequest<PagedResults<ClientDto>>;