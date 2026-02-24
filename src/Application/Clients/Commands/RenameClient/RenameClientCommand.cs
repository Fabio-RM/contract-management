using MediatR;

namespace Application.Clients.Commands.RenameClient;

public record RenameClientCommand(Guid Id, string NewName) : IRequest<Unit>;
