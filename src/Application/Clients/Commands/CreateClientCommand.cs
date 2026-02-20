using MediatR;

namespace Application.Clients.Commands;

public record CreateClientCommand(string Cnpj, string Name) : IRequest<Guid>;