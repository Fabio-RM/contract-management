using MediatR;

namespace Application.Clients.Commands;

public record RenameClientCommand(Guid Id, string NewName) : IRequest<Unit>;
