using Application.Clients.DTOs;
using MediatR;

namespace Application.Clients.Queries.GetClientById;

public record GetClientByIdQuery(Guid clientId) : IRequest<ClientDto>
{
    public Guid ClientId { get; } = clientId;
}