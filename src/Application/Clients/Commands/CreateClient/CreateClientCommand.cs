using MediatR;

namespace Application.Clients.Commands.CreateClient;

public record CreateClientCommand(string Cnpj, string Name) : IRequest<Guid>;