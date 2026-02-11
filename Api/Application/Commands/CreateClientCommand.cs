using Api.Core.ValueObjects;

namespace Api.Application.Commands;

public record CreateClientCommand(string Cnpj, string Name);