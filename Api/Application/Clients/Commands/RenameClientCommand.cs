using MediatR;

namespace Api.Application.Clients.Commands;

public record RenameClientCommand(Guid Id, string NewName) : IRequest<Unit>;