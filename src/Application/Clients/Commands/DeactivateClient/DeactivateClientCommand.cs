using MediatR;

namespace Application.Clients.Commands.DeactivateClient;

public record DeactivateClientCommand(Guid Id) : IRequest<Unit>;