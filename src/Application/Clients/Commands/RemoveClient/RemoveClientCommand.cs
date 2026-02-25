using MediatR;

namespace Application.Clients.Commands.RemoveClient;

public record RemoveClientCommand(Guid Id) : IRequest<Unit>;