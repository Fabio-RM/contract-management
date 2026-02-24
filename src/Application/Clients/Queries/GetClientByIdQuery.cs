using MediatR;

namespace Application.Clients.Queries;

public record GetClientByIdQuery(Guid clientId) : IRequest<ClientDto?>
{
    public Guid ClientId { get; } = clientId;
}