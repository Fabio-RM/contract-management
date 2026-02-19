namespace Api.Application.Clients;

public record ClientDto(Guid Id, string Name, string Cnpj, bool IsActive);