using MediatR;

namespace Api.Application.Clients.Commands;

public record CreateClientCommand(string Cnpj, string Name) : IRequest<Guid>;